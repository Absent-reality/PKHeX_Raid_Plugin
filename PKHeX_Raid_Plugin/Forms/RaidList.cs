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
        private string Ip = "";
        private int Port = 6000;
        private int _maxProgress = 10;
        private int _currentProgress = 0;
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
                }
            }
        }

        private readonly SAV8SWSH _SAV = null!;
        private SwitchProtocol _selectedProtocol = SwitchProtocol.WiFi;
        public DeviceExecutor Executor = null!;
        public bool IsConnected() => Connected;
        public event PropertyChangedEventHandler? PropertyChanged;

        public RaidList(SAV8SWSH sav)
        {
            InitializeComponent();
            IVs = [TB_HP_IV1, TB_ATK_IV1, TB_DEF_IV1, TB_SPA_IV1, TB_SPD_IV1, TB_SPE_IV1];
            _SAV = sav;
            CenterToParent();
            UpdateRaids(sav);
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
                    break;

                case >= 100:
                    baseMap = Resources.map_ioa;
                    raids = _aotRaids;
                    break;

                case < 100:
                    raids = _baseRaids;
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

            foreach (var raidParameters in raids)
            {
                if (raidParameters.IsWishingPiece)
                    DrawOverlay(graphics, Resources.wishingpiece, raidParameters, 40, 40, 21, -21);

                if (_raids.GenerateFromIndex(raidParameters).ShinyType != 0)
                    DrawOverlay(graphics, Resources.shiny, raidParameters, 40, 40, -21, -21);
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

        private int _originalHeight;
        private bool _isResizing = false;
        private bool _isLoaded = false;

        private void RaidList_Load(object sender, EventArgs e)
        {
            _isLoaded = true;
            _originalHeight = this.Height;

            CB_Den.DrawMode = DrawMode.OwnerDrawFixed;
            CB_Den.DrawItem -= CB_Den_DrawItem;
            CB_Den.DrawItem += CB_Den_DrawItem;

            tb_ip.Text = Plugin_Settings.Default.address;
            tb_port.Text = Plugin_Settings.Default.port.ToString();
            protocolSwitch.State = Plugin_Settings.Default.protocol
                ? SwitchControl.SwitchState.Right
                : SwitchControl.SwitchState.Left;
            InitializeBindings();
        }

        private void InitializeBindings()
        {
            btn_refresh.DataBindings.Add("Visible", this, "Connected");
            var binding = new Binding("Enabled", this, "IsConnecting", true, DataSourceUpdateMode.OnPropertyChanged);
            binding.Format += (s, e) => e.Value = !(bool?)e.Value;
            binding.Parse += (s, e) => e.Value = !(bool?)e.Value;
            Cnct_btn.DataBindings.Add(binding);
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

        private void Tb_ip_ValidIPChanged(object sender, ValidIPChangedEventArgs e)
        {
            Ip = e.ValidatedText ?? "";
        }

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
            var token = new CancellationToken();
            IsConnecting = true;

                try
                { 
                    var config = _selectedProtocol switch
                    {
                        SwitchProtocol.USB => new SwitchConnectionConfig { Port = Port, Protocol = SwitchProtocol.USB },
                        SwitchProtocol.WiFi => new SwitchConnectionConfig { IP = Ip, Port = Port, Protocol = SwitchProtocol.WiFi },
                        _ => throw new ArgumentOutOfRangeException("NoProtocol"),
                    };
                    var state = new DeviceState
                    {
                        Connection = config,
                        InitialRoutine = RoutineType.ReadWrite,
                    };
                    Executor = new DeviceExecutor(state);
                    await Executor.RunAsync(token).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Disconnect();
                    MessageBox.Show($"{ex.Message}");
                    IsConnecting = false;
                return;
                }

            Plugin_Settings.Default.address = Ip;
            Plugin_Settings.Default.port = Port;
            Plugin_Settings.Default.protocol = _selectedProtocol switch
            {
                SwitchProtocol.USB => true,
                _ => false,
            };
            Plugin_Settings.Default.Save();
            try
            {
                await Executor.Connect(token).ConfigureAwait(false);

                Connected = Executor.Connection.Connected;
                var info = Connected ? "Connected." : "Failed.";
                QueueAnnouncement(info, 2000);
                IsConnecting = false;

                var version = await Executor.ReadGame(token).ConfigureAwait(false);
                if (_SAV == null) return;
                _SAV.Version = version;

                await RefreshRaids(token);

                QueueAnnouncement("", 2000);
                Log($"{"ExecutorConnected"} {version} - {_SAV.OT} ({_SAV.TID16})");
            }
            catch (Exception ex)
            {
                Disconnect();
                MessageBox.Show($"Connection Failed \n {ex.Message}");
                Connected = false;
                return;
            }
        }

        public void Disconnect()
        {
            try { Executor.Disconnect(); }
            catch { }
            Connected = false;
            _currentProgress = 0;
            if (!progressBar.Visible)
                return;
            if (progressBar.InvokeRequired)
                progressBar.Invoke(() => progressBar.Visible = false);
            else
                progressBar.Visible = false;
            QueueAnnouncement("", 0);
        }

        private async Task RefreshRaids(CancellationToken token)
        {
            if (!Connected || !Executor.Connection.Connected)
                return;
            if (_SAV == null) return;

            QueueAnnouncement("Reading dens..", 2000);
            _currentProgress = 0;
            if (progressBar.InvokeRequired)
                progressBar.Invoke(() => progressBar.Visible = true);
            else progressBar.Visible = true;
            
            UpdateProgress(_currentProgress++, _maxProgress);

            var status = await Executor.GetBytes(BlockDefinitions.MyStatus, token);
            var myStatusBlock = _SAV.Accessor.GetBlock(BlockDefinitions.MyStatus.Key);
            myStatusBlock.ChangeData(status);
            UpdateProgress(_currentProgress++, _maxProgress);

            var raid = await Executor.GetBytes(BlockDefinitions.Raid, token);
            var raidBlock = _SAV.Accessor.GetBlock(BlockDefinitions.Raid.Key);
            raidBlock.ChangeData(raid);
            UpdateProgress(_currentProgress++, _maxProgress);

            var armorRaid = await Executor.GetBytes(BlockDefinitions.RaidArmor, token);
            var armorRaidBlock = _SAV.Accessor.GetBlock(BlockDefinitions.RaidArmor.Key);
            armorRaidBlock.ChangeData(armorRaid);
            UpdateProgress(_currentProgress++, _maxProgress);

            var crownRaid = await Executor.GetBytes(BlockDefinitions.RaidCrown, token);
            var crownRaidBlock = _SAV.Accessor.GetBlock(BlockDefinitions.RaidCrown.Key);
            crownRaidBlock.ChangeData(crownRaid);
            UpdateProgress(_currentProgress++, _maxProgress);

            while (_currentProgress < _maxProgress)
            {
                UpdateProgress(_currentProgress, _maxProgress);
                _currentProgress++;
            }

            UpdateRaids(_SAV);
            if (progressBar.InvokeRequired)
                await Task.Delay(2000, token);
            progressBar.Invoke(() => progressBar.Visible = false);
            _currentProgress = 0;
            QueueAnnouncement("Complete.", 2000);
        }

        private void UpdateProgress(int currProgress, int maxProgress)
        {
            var value = (100 * currProgress) / maxProgress;
            if (progressBar.InvokeRequired)
                progressBar.Invoke(() => progressBar.Value = value);
            else if (value > 100)
                progressBar.Value = 100;
            else
                progressBar.Value = value;
        }

       private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Connected):
                    Cnct_btn.Text = Connected ? "Disconnect" : "Connect";
                    break;

                case nameof(IsConnecting):

                    while (IsConnecting)
                    {
                        QueueAnnouncement("Connecting.", 1000);
                        QueueAnnouncement("Connecting..", 1000);
                        QueueAnnouncement("Connecting...", 1000);
                    }
                    break;
            }

        } 

        private void Log(string message)
        {
            if (Connected)
                Executor.Log(message);
        }

        private Queue<(string message, int delayMs)> _messageQueue = new();
        private System.Windows.Forms.Timer _messageTimer = new();
        private DateTime _lastMessageTime;

        private void QueueAnnouncement(string message, int delayMs)
        {
            if (InvokeRequired)
            {
                Invoke(() => QueueAnnouncement(message, delayMs));
                return;
            }

            _messageQueue.Enqueue((message, delayMs));

            _messageTimer ??= new System.Windows.Forms.Timer();
                _messageTimer.Interval = 100;
                _messageTimer.Tick += ProcessQueue;

            if (!_messageTimer.Enabled)
            {
                _lastMessageTime = DateTime.Now.AddMilliseconds(-1000); // Ensure first message triggers
                _messageTimer.Start();
            }
        }

        private void ProcessQueue(object? sender, EventArgs e)
        {
            if (_messageQueue.Count == 0)
            {
                _messageTimer.Stop();
                return;
            }

            if ((DateTime.Now - _lastMessageTime).TotalMilliseconds >= _messageQueue.Peek().delayMs)
            {
                var (msg, _) = _messageQueue.Dequeue();
                Announce(msg);
                _lastMessageTime = DateTime.Now;
            }
        }

        private void Announce(string message)
        {
            if (lbl_memo.InvokeRequired)
                lbl_memo.Invoke(() => lbl_memo.Text = message);
            else lbl_memo.Text = message;
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
