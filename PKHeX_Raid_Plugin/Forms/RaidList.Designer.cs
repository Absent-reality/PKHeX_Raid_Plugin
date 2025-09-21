namespace PKHeX_Raid_Plugin
{
    partial class RaidList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            GB_Left = new System.Windows.Forms.GroupBox();
            lbl_coordinates = new System.Windows.Forms.Label();
            btn_refresh = new System.Windows.Forms.Button();
            progressBar = new System.Windows.Forms.ProgressBar();
            groupBox1 = new System.Windows.Forms.GroupBox();
            lbl_memo = new System.Windows.Forms.Label();
            tb_ip = new IPTextBox();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            protocolSwitch = new SwitchControl();
            label4 = new System.Windows.Forms.Label();
            Cnct_btn = new System.Windows.Forms.Button();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            tb_port = new System.Windows.Forms.TextBox();
            CHK_Watts = new System.Windows.Forms.CheckBox();
            CHK_Wishing = new System.Windows.Forms.CheckBox();
            L_Shiny = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            L_Spe = new System.Windows.Forms.Label();
            L_Stars = new System.Windows.Forms.Label();
            L_SpD = new System.Windows.Forms.Label();
            L_Location = new System.Windows.Forms.Label();
            L_SpA = new System.Windows.Forms.Label();
            L_Ability = new System.Windows.Forms.Label();
            TB_SPE_IV1 = new System.Windows.Forms.TextBox();
            L_Def = new System.Windows.Forms.Label();
            TB_SPD_IV1 = new System.Windows.Forms.TextBox();
            L_Nature = new System.Windows.Forms.Label();
            TB_SPA_IV1 = new System.Windows.Forms.TextBox();
            L_Atk = new System.Windows.Forms.Label();
            L_ShinyInFrames = new System.Windows.Forms.Label();
            L_HP = new System.Windows.Forms.Label();
            CHK_Event = new System.Windows.Forms.CheckBox();
            raid_calc_btn = new System.Windows.Forms.Button();
            TB_DEF_IV1 = new System.Windows.Forms.TextBox();
            PB_PK1 = new System.Windows.Forms.PictureBox();
            CHK_Rare = new System.Windows.Forms.CheckBox();
            TB_ATK_IV1 = new System.Windows.Forms.TextBox();
            CHK_Active = new System.Windows.Forms.CheckBox();
            TB_HP_IV1 = new System.Windows.Forms.TextBox();
            L_DenSeed = new System.Windows.Forms.Label();
            CB_Den = new System.Windows.Forms.ComboBox();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            DenMap = new System.Windows.Forms.PictureBox();
            GB_Left.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PB_PK1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DenMap).BeginInit();
            SuspendLayout();
            // 
            // GB_Left
            // 
            GB_Left.Controls.Add(lbl_coordinates);
            GB_Left.Controls.Add(btn_refresh);
            GB_Left.Controls.Add(progressBar);
            GB_Left.Controls.Add(groupBox1);
            GB_Left.Controls.Add(CHK_Watts);
            GB_Left.Controls.Add(CHK_Wishing);
            GB_Left.Controls.Add(L_Shiny);
            GB_Left.Controls.Add(label1);
            GB_Left.Controls.Add(L_Spe);
            GB_Left.Controls.Add(L_Stars);
            GB_Left.Controls.Add(L_SpD);
            GB_Left.Controls.Add(L_Location);
            GB_Left.Controls.Add(L_SpA);
            GB_Left.Controls.Add(L_Ability);
            GB_Left.Controls.Add(TB_SPE_IV1);
            GB_Left.Controls.Add(L_Def);
            GB_Left.Controls.Add(TB_SPD_IV1);
            GB_Left.Controls.Add(L_Nature);
            GB_Left.Controls.Add(TB_SPA_IV1);
            GB_Left.Controls.Add(L_Atk);
            GB_Left.Controls.Add(L_ShinyInFrames);
            GB_Left.Controls.Add(L_HP);
            GB_Left.Controls.Add(CHK_Event);
            GB_Left.Controls.Add(raid_calc_btn);
            GB_Left.Controls.Add(TB_DEF_IV1);
            GB_Left.Controls.Add(PB_PK1);
            GB_Left.Controls.Add(CHK_Rare);
            GB_Left.Controls.Add(TB_ATK_IV1);
            GB_Left.Controls.Add(CHK_Active);
            GB_Left.Controls.Add(TB_HP_IV1);
            GB_Left.Controls.Add(L_DenSeed);
            GB_Left.Controls.Add(CB_Den);
            GB_Left.Dock = System.Windows.Forms.DockStyle.Fill;
            GB_Left.Location = new System.Drawing.Point(2, 2);
            GB_Left.Margin = new System.Windows.Forms.Padding(2);
            GB_Left.Name = "GB_Left";
            GB_Left.Padding = new System.Windows.Forms.Padding(2);
            GB_Left.Size = new System.Drawing.Size(270, 572);
            GB_Left.TabIndex = 0;
            GB_Left.TabStop = false;
            GB_Left.Text = "Den List";
            // 
            // lbl_coordinates
            // 
            lbl_coordinates.AutoSize = true;
            lbl_coordinates.Location = new System.Drawing.Point(152, 102);
            lbl_coordinates.Name = "lbl_coordinates";
            lbl_coordinates.Size = new System.Drawing.Size(38, 15);
            lbl_coordinates.TabIndex = 40;
            lbl_coordinates.Text = "label7";
            // 
            // btn_refresh
            // 
            btn_refresh.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btn_refresh.Location = new System.Drawing.Point(9, 466);
            btn_refresh.Name = "btn_refresh";
            btn_refresh.Size = new System.Drawing.Size(83, 23);
            btn_refresh.TabIndex = 39;
            btn_refresh.Text = "Refresh List";
            btn_refresh.UseVisualStyleBackColor = true;
            btn_refresh.Click += Refresh_Clicked;
            // 
            // progressBar
            // 
            progressBar.Location = new System.Drawing.Point(6, 562);
            progressBar.Name = "progressBar";
            progressBar.Size = new System.Drawing.Size(256, 10);
            progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            progressBar.TabIndex = 38;
            progressBar.Visible = false;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lbl_memo);
            groupBox1.Controls.Add(tb_ip);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(protocolSwitch);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(Cnct_btn);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(tb_port);
            groupBox1.Location = new System.Drawing.Point(106, 340);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(150, 172);
            groupBox1.TabIndex = 33;
            groupBox1.TabStop = false;
            // 
            // lbl_memo
            // 
            lbl_memo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lbl_memo.AutoSize = true;
            lbl_memo.Location = new System.Drawing.Point(45, 152);
            lbl_memo.Name = "lbl_memo";
            lbl_memo.Size = new System.Drawing.Size(0, 15);
            lbl_memo.TabIndex = 38;
            lbl_memo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tb_ip
            // 
            tb_ip.BackColor = System.Drawing.SystemColors.Window;
            tb_ip.Location = new System.Drawing.Point(39, 37);
            tb_ip.MaxLength = 15;
            tb_ip.Name = "tb_ip";
            tb_ip.PlaceholderText = "192.168.1.10";
            tb_ip.Size = new System.Drawing.Size(100, 23);
            tb_ip.TabIndex = 37;
            tb_ip.ValidIPChanged += Tb_ip_ValidIPChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(94, 101);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(27, 15);
            label6.TabIndex = 36;
            label6.Text = "Usb";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(6, 101);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(30, 15);
            label5.TabIndex = 35;
            label5.Text = "WiFi";
            // 
            // protocolSwitch
            // 
            protocolSwitch.AnimationEnabled = true;
            protocolSwitch.Location = new System.Drawing.Point(47, 96);
            protocolSwitch.MinimumSize = new System.Drawing.Size(32, 18);
            protocolSwitch.Name = "protocolSwitch";
            protocolSwitch.Size = new System.Drawing.Size(32, 22);
            protocolSwitch.TabIndex = 34;
            protocolSwitch.Text = "switchControl1";
            protocolSwitch.Toggled += Switch_Toggled;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline);
            label4.Location = new System.Drawing.Point(47, 19);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(81, 15);
            label4.TabIndex = 32;
            label4.Text = "For CFW only:";
            // 
            // Cnct_btn
            // 
            Cnct_btn.Location = new System.Drawing.Point(38, 124);
            Cnct_btn.Name = "Cnct_btn";
            Cnct_btn.Size = new System.Drawing.Size(80, 25);
            Cnct_btn.TabIndex = 27;
            Cnct_btn.Text = "Connect";
            Cnct_btn.UseVisualStyleBackColor = true;
            Cnct_btn.Click += Connect_Clicked;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(4, 68);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(32, 15);
            label3.TabIndex = 31;
            label3.Text = "Port:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            label2.Location = new System.Drawing.Point(16, 40);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(20, 15);
            label2.TabIndex = 30;
            label2.Text = "IP:";
            // 
            // tb_port
            // 
            tb_port.Enabled = false;
            tb_port.Location = new System.Drawing.Point(38, 65);
            tb_port.MaxLength = 5;
            tb_port.Name = "tb_port";
            tb_port.Size = new System.Drawing.Size(46, 23);
            tb_port.TabIndex = 29;
            tb_port.Text = "6000";
            tb_port.TextChanged += Tb_port_TextChanged;
            tb_port.KeyPress += Tb_port_KeyPress;
            // 
            // CHK_Watts
            // 
            CHK_Watts.AutoCheck = false;
            CHK_Watts.AutoSize = true;
            CHK_Watts.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            CHK_Watts.Location = new System.Drawing.Point(14, 243);
            CHK_Watts.Margin = new System.Windows.Forms.Padding(2);
            CHK_Watts.Name = "CHK_Watts";
            CHK_Watts.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            CHK_Watts.Size = new System.Drawing.Size(112, 19);
            CHK_Watts.TabIndex = 26;
            CHK_Watts.Text = "Watts Harvested";
            CHK_Watts.UseVisualStyleBackColor = true;
            // 
            // CHK_Wishing
            // 
            CHK_Wishing.AutoCheck = false;
            CHK_Wishing.AutoSize = true;
            CHK_Wishing.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            CHK_Wishing.Location = new System.Drawing.Point(14, 220);
            CHK_Wishing.Margin = new System.Windows.Forms.Padding(2);
            CHK_Wishing.Name = "CHK_Wishing";
            CHK_Wishing.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            CHK_Wishing.Size = new System.Drawing.Size(100, 19);
            CHK_Wishing.TabIndex = 25;
            CHK_Wishing.Text = "Wishing Piece";
            CHK_Wishing.UseVisualStyleBackColor = true;
            // 
            // L_Shiny
            // 
            L_Shiny.AutoSize = true;
            L_Shiny.Location = new System.Drawing.Point(106, 322);
            L_Shiny.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            L_Shiny.Name = "L_Shiny";
            L_Shiny.Size = new System.Drawing.Size(39, 15);
            L_Shiny.TabIndex = 24;
            L_Shiny.Text = "Shiny!";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(10, 36);
            label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(31, 15);
            label1.TabIndex = 23;
            label1.Text = "Den:";
            // 
            // L_Spe
            // 
            L_Spe.Location = new System.Drawing.Point(6, 390);
            L_Spe.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            L_Spe.Name = "L_Spe";
            L_Spe.Size = new System.Drawing.Size(58, 23);
            L_Spe.TabIndex = 22;
            L_Spe.Text = "Spe:";
            L_Spe.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // L_Stars
            // 
            L_Stars.Location = new System.Drawing.Point(145, 223);
            L_Stars.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            L_Stars.Name = "L_Stars";
            L_Stars.Size = new System.Drawing.Size(82, 24);
            L_Stars.TabIndex = 19;
            L_Stars.Text = "*****";
            L_Stars.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // L_SpD
            // 
            L_SpD.Location = new System.Drawing.Point(6, 366);
            L_SpD.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            L_SpD.Name = "L_SpD";
            L_SpD.Size = new System.Drawing.Size(58, 23);
            L_SpD.TabIndex = 21;
            L_SpD.Text = "SpD:";
            L_SpD.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // L_Location
            // 
            L_Location.AutoSize = true;
            L_Location.Location = new System.Drawing.Point(45, 62);
            L_Location.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            L_Location.Name = "L_Location";
            L_Location.Size = new System.Drawing.Size(59, 15);
            L_Location.TabIndex = 18;
            L_Location.Text = "$Location";
            // 
            // L_SpA
            // 
            L_SpA.Location = new System.Drawing.Point(6, 342);
            L_SpA.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            L_SpA.Name = "L_SpA";
            L_SpA.Size = new System.Drawing.Size(58, 23);
            L_SpA.TabIndex = 20;
            L_SpA.Text = "SpA:";
            L_SpA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // L_Ability
            // 
            L_Ability.AutoSize = true;
            L_Ability.Location = new System.Drawing.Point(106, 297);
            L_Ability.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            L_Ability.Name = "L_Ability";
            L_Ability.Size = new System.Drawing.Size(44, 15);
            L_Ability.TabIndex = 13;
            L_Ability.Text = "Ability:";
            // 
            // TB_SPE_IV1
            // 
            TB_SPE_IV1.Location = new System.Drawing.Point(65, 390);
            TB_SPE_IV1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TB_SPE_IV1.Name = "TB_SPE_IV1";
            TB_SPE_IV1.ReadOnly = true;
            TB_SPE_IV1.Size = new System.Drawing.Size(23, 23);
            TB_SPE_IV1.TabIndex = 15;
            // 
            // L_Def
            // 
            L_Def.Location = new System.Drawing.Point(6, 317);
            L_Def.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            L_Def.Name = "L_Def";
            L_Def.Size = new System.Drawing.Size(58, 23);
            L_Def.TabIndex = 19;
            L_Def.Text = "Def:";
            L_Def.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TB_SPD_IV1
            // 
            TB_SPD_IV1.Location = new System.Drawing.Point(65, 366);
            TB_SPD_IV1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TB_SPD_IV1.Name = "TB_SPD_IV1";
            TB_SPD_IV1.ReadOnly = true;
            TB_SPD_IV1.Size = new System.Drawing.Size(23, 23);
            TB_SPD_IV1.TabIndex = 13;
            // 
            // L_Nature
            // 
            L_Nature.AutoSize = true;
            L_Nature.Location = new System.Drawing.Point(106, 272);
            L_Nature.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            L_Nature.Name = "L_Nature";
            L_Nature.Size = new System.Drawing.Size(46, 15);
            L_Nature.TabIndex = 12;
            L_Nature.Text = "Nature:";
            // 
            // TB_SPA_IV1
            // 
            TB_SPA_IV1.Location = new System.Drawing.Point(65, 342);
            TB_SPA_IV1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TB_SPA_IV1.Name = "TB_SPA_IV1";
            TB_SPA_IV1.ReadOnly = true;
            TB_SPA_IV1.Size = new System.Drawing.Size(23, 23);
            TB_SPA_IV1.TabIndex = 11;
            // 
            // L_Atk
            // 
            L_Atk.Location = new System.Drawing.Point(6, 293);
            L_Atk.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            L_Atk.Name = "L_Atk";
            L_Atk.Size = new System.Drawing.Size(58, 23);
            L_Atk.TabIndex = 18;
            L_Atk.Text = "Atk:";
            L_Atk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // L_ShinyInFrames
            // 
            L_ShinyInFrames.AutoSize = true;
            L_ShinyInFrames.Location = new System.Drawing.Point(9, 119);
            L_ShinyInFrames.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            L_ShinyInFrames.Name = "L_ShinyInFrames";
            L_ShinyInFrames.Size = new System.Drawing.Size(91, 15);
            L_ShinyInFrames.TabIndex = 10;
            L_ShinyInFrames.Text = "Shiny in frames:";
            // 
            // L_HP
            // 
            L_HP.Location = new System.Drawing.Point(6, 269);
            L_HP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            L_HP.Name = "L_HP";
            L_HP.Size = new System.Drawing.Size(58, 23);
            L_HP.TabIndex = 17;
            L_HP.Text = "HP:";
            L_HP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CHK_Event
            // 
            CHK_Event.AutoCheck = false;
            CHK_Event.AutoSize = true;
            CHK_Event.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            CHK_Event.Location = new System.Drawing.Point(14, 197);
            CHK_Event.Margin = new System.Windows.Forms.Padding(2);
            CHK_Event.Name = "CHK_Event";
            CHK_Event.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            CHK_Event.Size = new System.Drawing.Size(82, 19);
            CHK_Event.TabIndex = 9;
            CHK_Event.Text = "Event Pool";
            CHK_Event.UseVisualStyleBackColor = true;
            // 
            // raid_calc_btn
            // 
            raid_calc_btn.Location = new System.Drawing.Point(30, 526);
            raid_calc_btn.Margin = new System.Windows.Forms.Padding(2);
            raid_calc_btn.Name = "raid_calc_btn";
            raid_calc_btn.Size = new System.Drawing.Size(215, 31);
            raid_calc_btn.TabIndex = 8;
            raid_calc_btn.Text = "Raid Calculator";
            raid_calc_btn.UseVisualStyleBackColor = true;
            raid_calc_btn.Click += ShowDenIVs;
            // 
            // TB_DEF_IV1
            // 
            TB_DEF_IV1.Location = new System.Drawing.Point(65, 317);
            TB_DEF_IV1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TB_DEF_IV1.Name = "TB_DEF_IV1";
            TB_DEF_IV1.ReadOnly = true;
            TB_DEF_IV1.Size = new System.Drawing.Size(23, 23);
            TB_DEF_IV1.TabIndex = 9;
            // 
            // PB_PK1
            // 
            PB_PK1.BackColor = System.Drawing.Color.Transparent;
            PB_PK1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            PB_PK1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            PB_PK1.Location = new System.Drawing.Point(145, 155);
            PB_PK1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            PB_PK1.Name = "PB_PK1";
            PB_PK1.Size = new System.Drawing.Size(81, 67);
            PB_PK1.TabIndex = 6;
            PB_PK1.TabStop = false;
            // 
            // CHK_Rare
            // 
            CHK_Rare.AutoCheck = false;
            CHK_Rare.AutoSize = true;
            CHK_Rare.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            CHK_Rare.Location = new System.Drawing.Point(14, 173);
            CHK_Rare.Margin = new System.Windows.Forms.Padding(2);
            CHK_Rare.Name = "CHK_Rare";
            CHK_Rare.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            CHK_Rare.Size = new System.Drawing.Size(76, 19);
            CHK_Rare.TabIndex = 5;
            CHK_Rare.Text = "Rare Pool";
            CHK_Rare.UseVisualStyleBackColor = true;
            // 
            // TB_ATK_IV1
            // 
            TB_ATK_IV1.Location = new System.Drawing.Point(65, 293);
            TB_ATK_IV1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TB_ATK_IV1.Name = "TB_ATK_IV1";
            TB_ATK_IV1.ReadOnly = true;
            TB_ATK_IV1.Size = new System.Drawing.Size(23, 23);
            TB_ATK_IV1.TabIndex = 6;
            // 
            // CHK_Active
            // 
            CHK_Active.AutoCheck = false;
            CHK_Active.AutoSize = true;
            CHK_Active.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            CHK_Active.Location = new System.Drawing.Point(14, 150);
            CHK_Active.Margin = new System.Windows.Forms.Padding(2);
            CHK_Active.Name = "CHK_Active";
            CHK_Active.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            CHK_Active.Size = new System.Drawing.Size(83, 19);
            CHK_Active.TabIndex = 4;
            CHK_Active.Text = "Den Active";
            CHK_Active.UseVisualStyleBackColor = true;
            // 
            // TB_HP_IV1
            // 
            TB_HP_IV1.Location = new System.Drawing.Point(65, 269);
            TB_HP_IV1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            TB_HP_IV1.Name = "TB_HP_IV1";
            TB_HP_IV1.ReadOnly = true;
            TB_HP_IV1.Size = new System.Drawing.Size(23, 23);
            TB_HP_IV1.TabIndex = 1;
            // 
            // L_DenSeed
            // 
            L_DenSeed.AutoSize = true;
            L_DenSeed.Location = new System.Drawing.Point(45, 82);
            L_DenSeed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            L_DenSeed.Name = "L_DenSeed";
            L_DenSeed.Size = new System.Drawing.Size(38, 15);
            L_DenSeed.TabIndex = 1;
            L_DenSeed.Text = "$Seed";
            // 
            // CB_Den
            // 
            CB_Den.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            CB_Den.FormattingEnabled = true;
            CB_Den.Items.AddRange(new object[] { "1 [b]: Rolling Fields 1", "2 [b]: Rolling Fields 2", "3 [b]: Rolling Fields 3", "4 [b]: Rolling Fields 4", "5 [b]: Rolling Fields 5", "6 [b]: Rolling Fields 6", "7 [b]: Rolling Fields 7", "8 [b]: Rolling Fields 8", "9 [b]: Rolling Fields 9", "10 [b]: Dappled Grove 1", "11 [b]: Dappled Grove 2", "12 [b]: Dappled Grove 3", "13 [b]: Dappled Grove 4", "14 [b]: Dappled Grove 5", "15 [b]: Watchtower Ruins 1", "16 [b]: Watchtower Ruins 2", "17 [b]: Watchtower Ruins 3", "18 [b]: East Lake Axewell 1", "19 [b]: East Lake Axewell 2", "20 [b]: East Lake Axewell 3", "21 [b]: East Lake Axewell 4", "22 [b]: West Lake Axewell 1", "23 [b]: West Lake Axewell 2", "24 [b]: West Lake Axewell 3", "25 [b]: West Lake Axewell 4", "26 [b]: West Lake Axewell 5", "27 [b]: West Lake Axewell 6", "28 [b]: Axew’s Eye 1", "29 [b]: South Lake Miloch 1", "30 [b]: South Lake Miloch 2", "31 [b]: South Lake Miloch 3", "32 [b]: South Lake Miloch 4", "33 [b]: South Lake Miloch 5", "34 [b]: Giant’s Seat 1", "35 [b]: Giant’s Seat 2", "36 [b]: Giant’s Seat 3", "37 [b]: Giant’s Seat 4", "38 [b]: Giant’s Seat 5", "39 [b]: North Lake Miloch 1", "40 [b]: North Lake Miloch 2", "41 [b]: North Lake Miloch 3", "42 [b]: North Lake Miloch 4", "43 [b]: North Lake Miloch 5", "44 [b]: East Lake Axewell 5", "45 [b]: North Lake Miloch 6", "46 [b]: Motostoke Riverbank 1", "47 [b]: Motostoke Riverbank 2", "48 [b]: Motostoke Riverbank 3", "49 [b]: Motostoke Riverbank 4", "50 [b]: Bridge Field 1", "51 [b]: Bridge Field 2", "52 [b]: Bridge Field 3", "53 [b]: Bridge Field 4", "54 [b]: Bridge Field 5", "55 [b]: Bridge Field 6", "56 [b]: Bridge Field 7", "57 [b]: Bridge Field 8", "58 [b]: Bridge Field 9", "59 [b]: Stony Wilderness 1", "60 [b]: Stony Wilderness 2", "61 [b]: Stony Wilderness 3", "62 [b]: Stony Wilderness 4", "63 [b]: Stony Wilderness 5", "64 [b]: Stony Wilderness 6", "65 [b]: Stony Wilderness 7", "66 [b]: Stony Wilderness 8", "67 [b]: Stony Wilderness 9", "68 [b]: Stony Wilderness 10", "69 [b]: Stony Wilderness 11", "70 [b]: Stony Wilderness 12", "71 [b]: Dusty Bowl 1", "72 [b]: Dusty Bowl 2", "73 [b]: Dusty Bowl 3", "74 [b]: Dusty Bowl 4", "75 [b]: Dusty Bowl 5", "76 [b]: Dusty Bowl 6", "77 [b]: Dusty Bowl 7", "78 [b]: Dusty Bowl 8", "79 [b]: Giant’s Mirror 1", "80 [b]: Dusty Bowl 9", "81 [b]: Giant’s Mirror 2", "82 [b]: Giant’s Mirror 3", "83 [b]: Giant’s Mirror 4", "84 [b]: Giant’s Mirror 5", "85 [b]: Hammerlocke Hills 1", "86 [b]: Hammerlocke Hills 2", "87 [b]: Hammerlocke Hills 3", "88 [b]: Hammerlocke Hills 4", "89 [b]: Hammerlocke Hills 5", "90 [b]: Hammerlocke Hills 6", "91 [b]: Hammerlocke Hills 7", "92 [b]: Giant’s Cap 1", "93 [b]: Giant’s Cap 2", "94 [b]: Giant’s Cap 3", "95 [b]: Giant’s Cap 4", "96 [b]: Giant’s Cap 5", "97 [b]: Lake of Outrage 1", "98 [b]: Lake of Outrage 2", "99 [b]: Lake of Outrage 3", "100 [b]: Lake of Outrage 4", "1 [IoA]: Fields of Honor 1", "2 [IoA]: Fields of Honor 2", "3 [IoA]: Fields of Honor 3", "4 [IoA]: Fields of Honor 4", "5 [IoA]: Fields of Honor 5", "6 [IoA]: Fields of Honor 6", "7 [IoA]: Fields of Honor 7", "8 [IoA]: Fields of Honor 8", "9 [IoA]: Fields of Honor 9", "10 [IoA]: Fields of Honor 10", "11 [IoA]: Soothing Wetlands 1", "12 [IoA]: Soothing Wetlands 2", "13 [IoA]: Soothing Wetlands 3", "14 [IoA]: Soothing Wetlands 4", "15 [IoA]: Soothing Wetlands 5", "16 [IoA]: Soothing Wetlands 6", "17 [IoA]: Soothing Wetlands 7", "18 [IoA]: Soothing Wetlands 8", "19 [IoA]: Soothing Wetlands 9", "20 [IoA]: Forest of Focus 1", "21 [IoA]: Forest of Focus 2", "22 [IoA]: Forest of Focus 3", "23 [IoA]: Forest of Focus 4", "24 [IoA]: Forest of Focus 5", "25 [IoA]: Forest of Focus 6", "26 [IoA]: Challenge Beach 1", "27 [IoA]: Challenge Beach 2", "28 [IoA]: Challenge Beach 3", "29 [IoA]: Challenge Beach 4", "30 [IoA]: Challenge Beach 5", "31 [IoA]: Challenge Beach 6", "32 [IoA]: Challenge Beach 7", "33 [IoA]: Challenge Beach 8", "34 [IoA]: Brawlers’ Cave 1", "35 [IoA]: Challenge Road 1", "36 [IoA]: Challenge Road 2", "37 [IoA]: Challenge Road 3", "38 [IoA]: Challenge Road 4", "39 [IoA]: Courageous Cavern 1", "40 [IoA]: Courageous Cavern 2", "41 [IoA]: Courageous Cavern 3", "42 [IoA]: Courageous Cavern 4", "43 [IoA]: Courageous Cavern 5", "44 [IoA]: Courageous Cavern 6", "45 [IoA]: Loop Lagoon 1", "46 [IoA]: Loop Lagoon 2", "47 [IoA]: Loop Lagoon 3", "48 [IoA]: Loop Lagoon 4", "49 [IoA]: Training Lowlands 1", "50 [IoA]: Training Lowlands 2", "51 [IoA]: Training Lowlands 3", "52 [IoA]: Training Lowlands 4", "53 [IoA]: Training Lowlands 5", "54 [IoA]: Training Lowlands 6", "55 [IoA]: Training Lowlands 7", "56 [IoA]: Potbottom Desert 1", "57 [IoA]: Potbottom Desert 2", "58 [IoA]: Potbottom Desert 3", "59 [IoA]: Workout Sea 1", "60 [IoA]: Workout Sea 2", "61 [IoA]: Workout Sea 3", "62 [IoA]: Workout Sea 4", "63 [IoA]: Workout Sea 5", "64 [IoA]: Workout Sea 6", "65 [IoA]: Workout Sea 7", "66 [IoA]: Stepping-Stone Sea 1", "67 [IoA]: Stepping-Stone Sea 2", "68 [IoA]: Stepping-Stone Sea 3", "69 [IoA]: Stepping-Stone Sea 4", "70 [IoA]: Stepping-Stone Sea 5", "71 [IoA]: Stepping-Stone Sea 6", "72 [IoA]: Stepping-Stone Sea 7", "73 [IoA]: Stepping-Stone Sea 8", "74 [IoA]: Stepping-Stone Sea 9", "75 [IoA]: Insular Sea 1", "76 [IoA]: Insular Sea 2", "77 [IoA]: Insular Sea 3", "78 [IoA]: Insular Sea 4", "79 [IoA]: Insular Sea 5", "80 [IoA]: Honeycalm Sea 1", "81 [IoA]: Honeycalm Sea 2", "82 [IoA]: Honeycalm Sea 3", "83 [IoA]: Honeycalm Sea 4", "84 [IoA]: Honeycalm Sea 5", "85 [IoA]: Honeycalm Island 1", "86 [IoA]: Honeycalm Island 2", "87 [IoA]: Honeycalm Island 3", "88 [IoA]: Honeycalm Island 4", "89 [IoA]: Honeycalm Island 5", "90 [IoA]: Honeycalm Island 6", "1 [CT]: Slippery Slope 1", "2 [CT]: Slippery Slope 2", "3 [CT]: Slippery Slope 3", "4 [CT]: Slippery Slope 4", "5 [CT]: Slippery Slope 5", "6 [CT]: Slippery Slope 6", "7 [CT]: Frostpoint Field 1", "8 [CT]: Frostpoint Field 2", "9 [CT]: Frostpoint Field 3", "10 [CT]: Frostpoint Field 4", "11 [CT]: Frostpoint Field 5", "12 [CT]: Giant’s Bed 1", "13 [CT]: Giant’s Bed 2", "14 [CT]: Giant’s Bed 3", "15 [CT]: Giant’s Bed 4", "16 [CT]: Giant’s Bed 5", "17 [CT]: Giant’s Bed 6", "18 [CT]: Giant’s Bed 7", "19 [CT]: Giant’s Bed 8", "20 [CT]: Giant’s Bed 9", "21 [CT]: Giant’s Bed 10", "22 [CT]: Giant’s Bed 11", "23 [CT]: Giant’s Bed 12", "24 [CT]: Giant’s Bed 13", "25 [CT]: Giant’s Bed 14", "26 [CT]: Giant’s Bed 15", "27 [CT]: Giant’s Bed 16", "28 [CT]: Giant’s Bed 17", "29 [CT]: Giant’s Bed 18", "30 [CT]: Giant’s Bed 19", "31 [CT]: Giant’s Bed 20", "32 [CT]: Giant’s Bed 21", "33 [CT]: Old Cemetery 1", "34 [CT]: Old Cemetery 2", "35 [CT]: Snowslide Slope 1", "36 [CT]: Snowslide Slope 2", "37 [CT]: Snowslide Slope 3", "38 [CT]: Snowslide Slope 4", "39 [CT]: Snowslide Slope 5", "40 [CT]: Snowslide Slope 6", "41 [CT]: Snowslide Slope 7", "42 [CT]: Snowslide Slope 8", "43 [CT]: Snowslide Slope 9", "44 [CT]: Path to the Peak 1", "45 [CT]: Path to the Peak 2", "46 [CT]: Path to the Peak 3", "47 [CT]: Crown Shrine 1", "48 [CT]: Giant’s Foot 1", "49 [CT]: Giant’s Foot 2", "50 [CT]: Giant’s Foot 3", "51 [CT]: Giant’s Foot 4", "52 [CT]: Giant’s Foot 5", "53 [CT]: Frigid Sea 1", "54 [CT]: Frigid Sea 2", "55 [CT]: Frigid Sea 3", "56 [CT]: Frigid Sea 4", "57 [CT]: Frigid Sea 5", "58 [CT]: Frigid Sea 6", "59 [CT]: Frigid Sea 7", "60 [CT]: Frigid Sea 8", "61 [CT]: Frigid Sea 9", "62 [CT]: Frigid Sea 10", "63 [CT]: Frigid Sea 11", "64 [CT]: Frigid Sea 12", "65 [CT]: Frigid Sea 13", "66 [CT]: Frigid Sea 14", "67 [CT]: Three-Point Pass 1", "68 [CT]: Three-Point Pass 2", "69 [CT]: Ballimere Lake 1", "70 [CT]: Ballimere Lake 2", "71 [CT]: Ballimere Lake 3", "72 [CT]: Ballimere Lake 4", "73 [CT]: Ballimere Lake 5", "74 [CT]: Ballimere Lake 6", "75 [CT]: Ballimere Lake 7", "76 [CT]: Ballimere Lake 8", "77 [CT]: Ballimere Lake 9", "78 [CT]: Ballimere Lake 10", "79 [CT]: Ballimere Lake 11", "80 [CT]: Ballimere Lake 12", "81 [CT]: Ballimere Lake 13", "82 [CT]: Ballimere Lake 14", "83 [CT]: Ballimere Lake 15", "84 [CT]: Ballimere Lake 16", "85 [CT]: Ballimere Lake 17", "86 [CT]: Dyna Tree Hill 1" });
            CB_Den.Location = new System.Drawing.Point(45, 32);
            CB_Den.Margin = new System.Windows.Forms.Padding(2);
            CB_Den.Name = "CB_Den";
            CB_Den.Size = new System.Drawing.Size(200, 23);
            CB_Den.TabIndex = 0;
            CB_Den.SelectedIndexChanged += ChangeDenIndex;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new System.Drawing.Point(2, 2);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(GB_Left);
            splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(2);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(DenMap);
            splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(2);
            splitContainer1.Size = new System.Drawing.Size(529, 576);
            splitContainer1.SplitterDistance = 274;
            splitContainer1.TabIndex = 2;
            // 
            // DenMap
            // 
            DenMap.BackgroundImage = Properties.Resources.map;
            DenMap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            DenMap.Dock = System.Windows.Forms.DockStyle.Fill;
            DenMap.Location = new System.Drawing.Point(2, 2);
            DenMap.Margin = new System.Windows.Forms.Padding(2);
            DenMap.Name = "DenMap";
            DenMap.Size = new System.Drawing.Size(247, 572);
            DenMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            DenMap.TabIndex = 0;
            DenMap.TabStop = false;
            DenMap.BackgroundImageChanged += DenMap_BackgroundImageChanged;
            DenMap.Paint += DenMap_Paint;
            // 
            // RaidList
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(533, 580);
            Controls.Add(splitContainer1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Margin = new System.Windows.Forms.Padding(2);
            MaximizeBox = false;
            MinimumSize = new System.Drawing.Size(0, 543);
            Name = "RaidList";
            Padding = new System.Windows.Forms.Padding(2);
            ShowIcon = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Raid List";
            Load += RaidList_Load;
            Resize += RaidList_Resize;
            GB_Left.ResumeLayout(false);
            GB_Left.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PB_PK1).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DenMap).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GB_Left;
        private System.Windows.Forms.ComboBox CB_Den;
        private System.Windows.Forms.Label L_DenSeed;
        private System.Windows.Forms.PictureBox PB_PK1;
        private System.Windows.Forms.CheckBox CHK_Rare;
        private System.Windows.Forms.CheckBox CHK_Active;
        private System.Windows.Forms.Label L_Spe;
        private System.Windows.Forms.Label L_SpD;
        private System.Windows.Forms.Label L_SpA;
        private System.Windows.Forms.Label L_Def;
        private System.Windows.Forms.Label L_Atk;
        private System.Windows.Forms.Label L_HP;
        private System.Windows.Forms.TextBox TB_SPE_IV1;
        private System.Windows.Forms.TextBox TB_DEF_IV1;
        private System.Windows.Forms.TextBox TB_SPD_IV1;
        private System.Windows.Forms.TextBox TB_ATK_IV1;
        private System.Windows.Forms.TextBox TB_SPA_IV1;
        private System.Windows.Forms.TextBox TB_HP_IV1;
        private System.Windows.Forms.Button raid_calc_btn;
        private System.Windows.Forms.CheckBox CHK_Event;
        private System.Windows.Forms.Label L_Ability;
        private System.Windows.Forms.Label L_Nature;
        private System.Windows.Forms.Label L_ShinyInFrames;
        private System.Windows.Forms.Label L_Location;
        private System.Windows.Forms.Label L_Stars;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label L_Shiny;
        private System.Windows.Forms.CheckBox CHK_Wishing;
        private System.Windows.Forms.CheckBox CHK_Watts;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox DenMap;
        private System.Windows.Forms.Button Cnct_btn;
        private System.Windows.Forms.TextBox tb_port;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private SwitchControl protocolSwitch;
        private IPTextBox tb_ip;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.Label lbl_memo;
        private System.Windows.Forms.Label lbl_coordinates;
    }
}