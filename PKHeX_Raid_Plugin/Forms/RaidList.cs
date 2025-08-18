using FlatBuffers;
using PKHeX.Core;
using PKHeX_Raid_Plugin.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PKHeX_Raid_Plugin
{
    public partial class RaidList : Form
    {
        private readonly RaidManager _raids;
        private readonly TextBox[] IVs;
        private List<RaidPKM> _pkms = [];
        private List<RaidParameters> _baseRaids = [];
        private List<RaidParameters> _ctRaids = [];
        private List<RaidParameters> _aotRaids = [];

        public RaidList(SaveBlockAccessor8SWSH blocks, GameVersion game, int badges, int tid, int sid)
        {
            InitializeComponent();
            CB_Den.DrawMode = DrawMode.OwnerDrawFixed;
            CB_Den.DrawItem -= CB_Den_DrawItem;
            CB_Den.DrawItem += CB_Den_DrawItem;
            IVs = [TB_HP_IV1, TB_ATK_IV1, TB_DEF_IV1, TB_SPA_IV1, TB_SPD_IV1, TB_SPE_IV1];
            _raids = new RaidManager(blocks, game, badges, (uint)tid, (uint)sid);
            CB_Den.SelectedIndex = 0;
            CenterToParent();
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

        private void Button2_Click(object sender, EventArgs e) => this.Close();
    }
}
