using Microsoft.Z3;
using Newtonsoft.Json.Linq;
using PKHeX.Core;
using PKHeX_Raid_Plugin.Connections;
using PKHeX_Raid_Plugin.Properties;
using SysBot.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PKHeX_Raid_Plugin.MapRegion;

namespace PKHeX_Raid_Plugin
{
    public partial class RaidList : Form, INotifyPropertyChanged
    {
        private RaidManager _raids;
        private readonly TextBox[] IVs;
        private List<RaidPKM> _pkms = [];
        private List<RaidParameters> _baseRaids = [];
        private List<RaidParameters> _ctRaids = [];
        private List<RaidParameters> _aotRaids = [];
        private List<MapRegion.Region> _currentRegions = [];
        private Label HoverLabel;
        private MessageAnnouncer _announcer;
        private string Ip = "";
        private int Port = 6000;
        private readonly SAV8SWSH _SAV = null!;
        private SwitchProtocol _selectedProtocol = SwitchProtocol.WiFi;
        public DeviceExecutor Executor = null!;
        public bool IsConnected() => Connected;
        public event PropertyChangedEventHandler? PropertyChanged;
        private bool _connected = false;
        private bool _isConnecting = false;

        [DefaultValue(false)]
        public bool Connected
        {
            get => _connected; 
            set
            {
                if (_connected != value)
                {
                    _connected = value;
                    OnPropertyChanged();
                }
            }
        }

        [DefaultValue(false)]
        public bool IsConnecting
        {
            get => _isConnecting;
            set
            {
                if (_isConnecting != value)
                {
                    _isConnecting = value;
                    OnPropertyChanged();
                    UpdateControlStates();
                }
            }
        }

        private int _progressValue = 0;

        [DefaultValue(0)]
        public int ProgressValue
        {
            get => _progressValue;
            set
            {
                if (_progressValue != value)
                {
                    _progressValue = value;
                    OnPropertyChanged();
                }
            }
        }

        public RaidList(SAV8SWSH sav)
        {
            InitializeComponent();
            IVs = [TB_HP_IV1, TB_ATK_IV1, TB_DEF_IV1, TB_SPA_IV1, TB_SPD_IV1, TB_SPE_IV1];
            _SAV = sav;
            CenterToParent();
            UpdateRaids(sav);
        }

        private int _originalHeight;
        private bool _isResizing = false;
        private bool _isLoaded = false;

        private void RaidList_Load(object sender, EventArgs e)
        {
            _isLoaded = true;
            _originalHeight = this.Height;
            _announcer = new MessageAnnouncer();

            CB_Den.DrawMode = DrawMode.OwnerDrawFixed;
            CB_Den.DrawItem -= CB_Den_DrawItem;
            CB_Den.DrawItem += CB_Den_DrawItem;
            this.PropertyChanged += OnPropertyChange;
            DenMap.MouseMove += DenMap_MouseMove;
            tb_ip.Text = Plugin_Settings.Default.address;
            tb_port.Text = Plugin_Settings.Default.port.ToString();
            protocolSwitch.State = Plugin_Settings.Default.protocol
                ? SwitchControl.SwitchState.Right
                : SwitchControl.SwitchState.Left;

            InitializeBindings();
        }

        private void InitializeBindings()
        {
            lbl_memo.DataBindings.Add("Text", _announcer, "Message");
            btn_refresh.DataBindings.Add("Visible", this, "Connected");
            progressBar.DataBindings.Add("Value", this, "ProgressValue", true, DataSourceUpdateMode.OnPropertyChanged);            
            lbl_coordinates.DataBindings.Add("Text", this, "MouseCoordinatesText", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        public void UpdateRaids(SAV8SWSH sav)
        {
            _raids = new RaidManager(sav.Blocks, sav.Version, sav.Badges, (uint)sav.TID16, (uint)sav.SID16);
            CB_Den.SelectedIndex = 0;
            GetAllDens();
            LoadDen(_raids[0]);
        }

        private void ChangeDenIndex(object sender, EventArgs e) => LoadDen(_raids[CB_Den.SelectedIndex]);

        private void ShowDenIVs(object sender, EventArgs e)
        {
            using var divs = new DenIVs(CB_Den.SelectedIndex, _raids);
            divs.ShowDialog();
        }

        private void LoadDen(RaidParameters raidParameters)
        {
            CHK_Active.Checked = raidParameters.IsActive;
            CHK_Rare.Checked = raidParameters.IsRare;
            CHK_Event.Checked = raidParameters.IsEvent;
            CHK_Wishing.Checked = raidParameters.IsWishingPiece;
            CHK_Watts.Checked = raidParameters.WattsHarvested;
            L_DenSeed.Text = $"{raidParameters.Seed:X16}";
            L_Stars.Text = RaidUtil.GetStarString(raidParameters);

            var pkm = _raids.GenerateFromIndex(raidParameters);
            var s = GameInfo.Strings;
            L_Ability.Text = $"Ability: {s.Ability[pkm.Ability]}";
            L_Nature.Text = $"Nature: {s.natures[pkm.Nature]}";
            L_ShinyInFrames.Text = $"Next Shiny Frame: {RandUtil.GetNextShinyFrame(raidParameters.Seed)}";
            L_Shiny.Visible = pkm.ShinyType != 0;
            L_Shiny.Text = pkm.ShinyType == 1 ? "Shiny: Star" : pkm.ShinyType == 2 ? (pkm.ForcedShinyType == 2 ? "Shiny: Forced Square" : "Shiny: Square!!!") : "Shiny locked";

            for (int i = 0; i < 6; i++)
            {
                IVs[i].Text = $"{pkm.IVs[i]:00}";
            }

            PB_PK1.BackgroundImage = RaidUtil.GetRaidResultSprite(pkm, CHK_Active.Checked);
            L_Location.Text = raidParameters.Location;

            if (raidParameters.X > 0 && raidParameters.Y > 0)
                UpdateBackground(raidParameters);
        }

        private void GetAllDens()
        {
            List<RaidParameters> currentRaids = [];
            _pkms.Clear();

            for (int i = 0; i < CB_Den.Items.Count; i++)
            {
                var raid = _raids[i];
                _pkms.Add(_raids.GenerateFromIndex(raid));
                currentRaids.Add(raid);
            }

            _baseRaids = [.. currentRaids.Where(r => r.Index < 100)];
            _ctRaids = [.. currentRaids.Where(r => r.Index >= 190)];
            _aotRaids = [.. currentRaids.Where(r => r.Index >= 100 && r.Index < 190)];
        }

        private void CB_Den_DrawItem(object? sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            if (sender is not ComboBox combo) return;
            var item = combo.Items[e.Index];
            if (item == null) return;
            var itemText = item.ToString();
            var itemFont = e.Font ?? SystemFonts.DefaultFont;

            Color foreColor = Color.Black;
            var raid = _raids[e.Index];
            var pkm = _pkms[e.Index];

            if (raid.IsWishingPiece || pkm.ShinyType != 0)
                itemFont = new(itemFont, FontStyle.Bold);

            if (raid.IsWishingPiece)
                foreColor = Color.Purple;
            else if (pkm.ShinyType != 0)
                foreColor = Color.Yellow;

            e.DrawBackground();
            using Brush textBrush = new SolidBrush(foreColor);

            e.Graphics.DrawString(itemText, itemFont, textBrush, e.Bounds);
            e.DrawFocusRectangle();
        }

        private void UpdateBackground(RaidParameters selectedRaid)
        {
            List<RaidParameters> raids = [];
            Bitmap baseMap = Resources.map;

            switch (selectedRaid.Index)
            {
                case >= 190:
                    baseMap = Resources.map_ct;
                    raids = _ctRaids;
                    _currentRegions = MapRegions.GetMapRegions(selectedRaid.Index);
                    break;

                case >= 100:
                    baseMap = Resources.map_ioa;                  
                    raids = _aotRaids;
                    _currentRegions = MapRegions.GetMapRegions(selectedRaid.Index);
                    break;

                case < 100:
                    raids = _baseRaids;
                    _currentRegions = MapRegions.GetMapRegions(selectedRaid.Index);
                    break;
            }

            Bitmap mapWithMarks = AllMapMarks(raids, new Bitmap(baseMap));
            Pen redPen = new(Color.Red, 10);
            using var graphics = Graphics.FromImage(mapWithMarks);

            if (selectedRaid.Index >= 190)
            {
                redPen = new(Color.Red, 20);
                graphics.DrawArc(redPen, selectedRaid.X - 10, selectedRaid.Y - 10, 25, 25, 0, 360);
            }
            else
                graphics.DrawArc(redPen, selectedRaid.X - 5, selectedRaid.Y - 5, 15, 15, 0, 360);

            DisplayImage(mapWithMarks);
        }

        private Bitmap AllMapMarks(List<RaidParameters> raids, Bitmap map)
        {
            using var graphics = Graphics.FromImage(map);

            foreach (var raid in raids)
            {
                bool isLarge = raid.Index >= 190;

                if (raid.IsWishingPiece)
                    DrawOverlay(graphics, Resources.wishingpiece, raid,
                        isLarge ? 60 : 40, isLarge ? 60 : 40,
                        isLarge ? 31 : 21, isLarge ? -31 : -21);

                if (_raids.GenerateFromIndex(raid).ShinyType != 0)
                    DrawOverlay(graphics, Resources.shiny, raid,
                        isLarge ? 60 : 40, isLarge ? 60 : 40,
                        isLarge ? -31 : -21, isLarge ? -31 : -21);
            }

            return map;
        }

        private static void DrawOverlay(Graphics graphics, Bitmap img, RaidParameters raidParameters, int width, int height, int offsetX, int offsetY)
        {
            if (img == null)
                return;

            int overlayCenterX = raidParameters.X + offsetX;
            int overlayCenterY = raidParameters.Y + offsetY;

            graphics.DrawImage(
                img,
                overlayCenterX - width / 2,
                overlayCenterY - height / 2,
                width,
                height);
        }

        public void DisplayImage(Image img)
        {
            if (img == null) return;
            DenMap.BackgroundImage = img;
        }

        private void DenMap_BackgroundImageChanged(object sender, EventArgs e)
        {
            if (DenMap.BackgroundImage == null) return;
            UpdateFormSize(DenMap.BackgroundImage);
        }

        private void UpdateFormSize(Image img)
        {
            Size adjustedSize = Size.Empty;
            int currentWidth = DenMap.Width;
            int currentHeight = DenMap.Height;
            var aspectRatio = (float)img.Width / (float)img.Height;
            int newWidth = (int)((float)currentHeight * aspectRatio);
            if (newWidth > currentWidth)
                adjustedSize = new Size(this.Width + (newWidth - currentWidth), this.Height);
            else if (newWidth < currentWidth)
                adjustedSize = new Size(this.Width - (currentWidth - newWidth), this.Height);
            if (adjustedSize != Size.Empty)
                this.Size = adjustedSize;
        }

        private void DenMap_MouseMove(object? sender, MouseEventArgs e)
        {
            if (DenMap.BackgroundImage == null)
                return;

            var img = DenMap.BackgroundImage;
            var pbSize = DenMap.ClientSize;

            float scaleX = (float)img.Width / pbSize.Width;
            float scaleY = (float)img.Height / pbSize.Height;

            int imgX = (int)(e.X * scaleX);
            int imgY = (int)(e.Y * scaleY);

            MousePoint = new Point(imgX, imgY);
            HoverPoint = new Point(e.X, e.Y);

            DenMap.Invalidate();
        }

        private Point _mousePoint = Point.Empty;

        [DefaultValue(typeof(Point), "0,0")]
        public Point MousePoint
        {
            get => _mousePoint;
            set
            {
                if (_mousePoint != value)
                {
                    _mousePoint = value;
                    OnPropertyChanged(nameof(MousePoint));
                    OnPropertyChanged(nameof(MouseCoordinatesText));
                }
            }
        }

        private Point _hoverPoint = Point.Empty;

        [DefaultValue(typeof(Point), "0,0")]
        public Point HoverPoint
        {
            get => _hoverPoint;
            set
            {
                if (_hoverPoint != value)
                {
                    _hoverPoint = value;
                    OnPropertyChanged();
                }
            }
        }

        public string MouseCoordinatesText => $"{DenMap.ClientSize.Width}, {HoverPoint.X}"; //$"X: {_mousePoint.X}, Y: {_mousePoint.Y}";

        private float _distance = 0;
        [DefaultValue(0)]
        public float Distance
        {
            get => _distance;
            set
            {
                if (_distance != value)
                {
                    _distance = value;
                    OnPropertyChanged();
                }
            }
        }

        private void DenMap_Paint(object? sender, PaintEventArgs e)
        {         
            if (_currentRegions == null || _currentRegions.Count == 0)
                return;

            var hoveredRegion = _currentRegions
                .Where(r => r.Contains(MousePoint))
                .OrderBy(r => r.Type)
                .FirstOrDefault();

            if (hoveredRegion == null || DenMap.BackgroundImage == null) return;

            string name = hoveredRegion.Name;
            var img = DenMap.BackgroundImage;

            if (img == null) return;
            var pbSize = DenMap.ClientSize;
            float scaleX = (float)pbSize.Width / img.Width;
            float scaleY = (float)pbSize.Height / img.Height;
            PointF[] scaledBoundary = [.. hoveredRegion.Boundary.Select(p => new PointF(p.X * scaleX, p.Y * scaleY))];
            using Pen outlinePen = new(Color.Gray, 2);
            e.Graphics.DrawPolygon(outlinePen, scaledBoundary);

            using Font font = new("Segoe UI", 10, FontStyle.Bold);
            SizeF textSize = e.Graphics.MeasureString(name, font);

            float locationX = HoverPoint.X + 10;
            float locationY = HoverPoint.Y + 10;

            if (HoverPoint.X >= DenMap.ClientSize.Width - textSize.Width)            
                   locationX = HoverPoint.X - textSize.Width;
            if (HoverPoint.Y >= DenMap.ClientSize.Height - textSize.Height)
                locationY = HoverPoint.Y - textSize.Height;

            e.Graphics.DrawString(name, font, Brushes.Black, new PointF(locationX, locationY));
        }

        private void UpdateControlStates()
        {
            bool enabled = !IsConnecting;
            SetEnabledRecursive(this, enabled);
        }

        private void SetEnabledRecursive(Control parent, bool enabled)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is Button or ComboBox or TextBox or CheckBox or IPTextBox or SwitchControl)
                    c.Enabled = enabled;

                // Recurse into child containers
                if (c.HasChildren)
                    SetEnabledRecursive(c, enabled);
            }       
        }

        private void RaidList_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal && !_isResizing && _isLoaded)
            {
                _isResizing = true;

                this.BeginInvoke((MethodInvoker)(() =>
                {
                    this.SuspendLayout();

                    this.Height = _originalHeight;

                    if (DenMap.BackgroundImage != null)
                        UpdateFormSize(DenMap.BackgroundImage);

                    this.ResumeLayout();
                    _isResizing = false;
                }));
            }
        }

        private async void Connect_Clicked(object sender, EventArgs e)
        {
            if (!Connected || !Executor.Connection.Connected)
                await AttemptConnection();
            else
                Disconnect();
        }

        private void Tb_port_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tb_port.Text.Trim(), out int result))
                Port = result;
            else
                return;
        }

        private void Tb_port_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (backspace, delete, etc.)
            if (char.IsControl(e.KeyChar))
            {
                base.OnKeyPress(e);
                return;
            }

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            base.OnKeyPress(e);
        }

        private void Tb_ip_ValidIPChanged(object sender, ValidIPChangedEventArgs e) => Ip = e.ValidatedText ?? "";

        private void Switch_Toggled(object sender, SwitchControl.ToggledEventArgs e)
        {
            (var protocol, Port) = e.State switch
            {
                SwitchControl.SwitchState.Left => (SwitchProtocol.WiFi, 5000),
                SwitchControl.SwitchState.Right => (SwitchProtocol.USB, 8000),
                _ => throw new InvalidOperationException($"Unexpected state: {e.State}")
            };
            _selectedProtocol = protocol;
            tb_port.Enabled = !e.IsLeft;
        }

        private DateTime _buttonTime = DateTime.MinValue;
        private async void Refresh_Clicked(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            if ((now - _buttonTime).TotalMilliseconds < 1000)
                return;
            _buttonTime = now;
            var token = new CancellationToken();
            await RefreshRaids(token);
        }

        private async Task AttemptConnection()
        {
            using var cts = new CancellationTokenSource();
            var token = cts.Token;

            IsConnecting = true;
            Connected = false;

            try
            {
                var config = BuildConnectionConfig();
                var state = new DeviceState
                {
                    Connection = config,
                    InitialRoutine = RoutineType.ReadWrite,
                };

                Executor = new DeviceExecutor(state);
                await Task.Run(() => Executor.RunAsync(token));

                SaveConnectionSettings();

                await Executor.Connect(token);
                Connected = Executor.Connection.Connected;
                IsConnecting = false;

                if (!Connected)
                    throw new InvalidOperationException("Connection attempt unsuccessful.");

                await FinalizeConnectionAsync(token);
            }
            catch (Exception ex)
            {
                Disconnect();
                MessageBox.Show($"Connection Failed\n{ex.Message}");
                IsConnecting = false;
                _announcer.Enqueue("", 0);
            }
        }

        private SwitchConnectionConfig BuildConnectionConfig() => _selectedProtocol switch
        {
            SwitchProtocol.USB => new SwitchConnectionConfig { Port = Port, Protocol = SwitchProtocol.USB },
            SwitchProtocol.WiFi => new SwitchConnectionConfig { IP = Ip, Port = Port, Protocol = SwitchProtocol.WiFi },
            _ => throw new ArgumentOutOfRangeException(nameof(_selectedProtocol), "Unsupported protocol"),
        };

        private void SaveConnectionSettings()
        {
            Plugin_Settings.Default.address = Ip;
            Plugin_Settings.Default.port = Port;
            Plugin_Settings.Default.protocol = _selectedProtocol == SwitchProtocol.USB;
            Plugin_Settings.Default.Save();
        }

        private async Task FinalizeConnectionAsync(CancellationToken token)
        {
            var version = await Executor.ReadGame(token);
            if (_SAV == null) return;

            _SAV.Version = version;
            await RefreshRaids(token);

            _announcer.Enqueue("", 0);
            Log($"ExecutorConnected {version} - {_SAV.OT} ({_SAV.TID16})");
        }

        public void Disconnect()
        {
            try { Executor.Disconnect(); } catch { }

            Connected = false;
            ProgressValue = 0;
            InvokeIfRequired(progressBar, () => progressBar.Visible = false);
            _announcer.Enqueue("", 0);
        }

        private async Task RefreshRaids(CancellationToken token)
        {
            if (!Connected || !Executor.Connection.Connected || _SAV == null)
                return;

            _announcer.Enqueue("Reading dens..", 2000);
            InvokeIfRequired(progressBar, () => progressBar.Visible = true);

            Progress<int> progressReport = new(value => ProgressValue = value);
            int totalSteps = 4;

            await UpdateBlockAsync(BlockDefinitions.MyStatus, progressReport, 1, totalSteps, token);
            await UpdateBlockAsync(BlockDefinitions.Raid, progressReport, 2, totalSteps, token);
            await UpdateBlockAsync(BlockDefinitions.RaidArmor, progressReport, 3, totalSteps, token);
            await UpdateBlockAsync(BlockDefinitions.RaidCrown, progressReport, 4, totalSteps, token);

            UpdateRaids(_SAV);

            await Task.Delay(2000, token);
            InvokeIfRequired(progressBar, () => progressBar.Visible = false);
            _announcer.EnqueueRange(["Complete.", ""], 2000);
            ProgressValue = 0;
        }

        private async Task UpdateBlockAsync(BlockDefinition def, IProgress<int> progress, int step, int totalSteps, CancellationToken token)
        {
            var data = await Executor.GetBytes(def, token);
            var block = _SAV.Accessor.GetBlock(def.Key);
            block.ChangeData(data);

            int percent = (100 * step) / totalSteps;
            progress.Report(Math.Min(percent, 100));
        }

        private void OnPropertyChange(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Connected):
                    Cnct_btn.Text = Connected ? "Disconnect" : "Connect";
                    break;

                case nameof(IsConnecting):
                    if (IsConnecting)
                        _announcer.EnqueueLoop(["Connecting.", "Connecting..", "Connecting..."], 1000, () => IsConnecting);
                    else
                        _announcer.Cancel();
                    break;
            }
        }

        private void Log(string message)
        {
            if (Connected)
                Executor.Log(message);
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void InvokeIfRequired(Control control, Action action)
        {
            if (control.InvokeRequired)
                control.Invoke(action);
            else
                action();
        }
    }
}
