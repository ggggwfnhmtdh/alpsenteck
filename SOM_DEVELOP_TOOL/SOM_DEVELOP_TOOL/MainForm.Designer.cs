using System.Drawing;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace SOM_DEVELOP_TOOL
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.PeripheralPage = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.xSpiFreqbox = new MaterialSkin.Controls.MaterialComboBox();
            this.SwitchProtocolCB = new MaterialSkin.Controls.MaterialCheckbox();
            this.AddrRadio = new MaterialSkin.Controls.MaterialCheckbox();
            this.DtrRadio = new MaterialSkin.Controls.MaterialCheckbox();
            this.xSpiCombox = new MaterialSkin.Controls.MaterialComboBox();
            this.DqsRadio = new MaterialSkin.Controls.MaterialCheckbox();
            this.RegValueBox = new MaterialSkin.Controls.MaterialTextBox();
            this.RegLengthBox = new MaterialSkin.Controls.MaterialTextBox();
            this.RegAddrBox = new MaterialSkin.Controls.MaterialTextBox();
            this.RegCombox = new MaterialSkin.Controls.MaterialComboBox();
            this.materialButton6 = new MaterialSkin.Controls.MaterialButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RegPathBox = new MaterialSkin.Controls.MaterialTextBox();
            this.LoadDefaultCB = new MaterialSkin.Controls.MaterialCheckbox();
            this.materialButton4 = new MaterialSkin.Controls.MaterialButton();
            this.ResetCB = new MaterialSkin.Controls.MaterialCheckbox();
            this.LoadTableBtn = new MaterialSkin.Controls.MaterialButton();
            this.CrosstalkCB = new MaterialSkin.Controls.MaterialCheckbox();
            this.LoadRegFileBtn = new MaterialSkin.Controls.MaterialButton();
            this.AttrbuteCB = new MaterialSkin.Controls.MaterialCheckbox();
            this.materialButton3 = new MaterialSkin.Controls.MaterialButton();
            this.RW_CB = new MaterialSkin.Controls.MaterialCheckbox();
            this.DefaultCB = new MaterialSkin.Controls.MaterialCheckbox();
            this.MemoryPage = new System.Windows.Forms.TabPage();
            this.materialExpansionPanel5 = new MaterialSkin.Controls.MaterialExpansionPanel();
            this.materialTextBox5 = new MaterialSkin.Controls.MaterialTextBox();
            this.materialButton1 = new MaterialSkin.Controls.MaterialButton();
            this.MCU_Name = new MaterialSkin.Controls.MaterialComboBox();
            this.materialButton11 = new MaterialSkin.Controls.MaterialButton();
            this.ImageFileBox = new MaterialSkin.Controls.MaterialTextBox();
            this.materialButton10 = new MaterialSkin.Controls.MaterialButton();
            this.MaterialButton2 = new MaterialSkin.Controls.MaterialButton();
            this.ProBar = new MaterialSkin.Controls.MaterialProgressBar();
            this.ConfigPage = new System.Windows.Forms.TabPage();
            this.materialExpansionPanel3 = new MaterialSkin.Controls.MaterialExpansionPanel();
            this.JlinkSpeedCBox = new System.Windows.Forms.ComboBox();
            this.IdRangeBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.M7_CPU1_CB = new MaterialSkin.Controls.MaterialSwitch();
            this.IdRangeBtn = new MaterialSkin.Controls.MaterialButton();
            this.M7_CPU0_CB = new MaterialSkin.Controls.MaterialSwitch();
            this.checkBox1 = new MaterialSkin.Controls.MaterialSwitch();
            this.IdSortCB = new MaterialSkin.Controls.MaterialSwitch();
            this.materialCheckBox3 = new MaterialSkin.Controls.MaterialSwitch();
            this.materialCheckBox1 = new MaterialSkin.Controls.MaterialSwitch();
            this.checkBox2 = new MaterialSkin.Controls.MaterialSwitch();
            this.TempPage = new MaterialSkin.Controls.MaterialTabControl();
            this.HomePage = new System.Windows.Forms.TabPage();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.MsgBox = new MaterialSkin.Controls.MaterialMultiLineTextBox();
            this.MainLogMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ScriptPage = new System.Windows.Forms.TabPage();
            this.MsgBox1 = new MaterialSkin.Controls.MaterialMultiLineTextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.OpenBtn = new MaterialSkin.Controls.MaterialButton();
            this.DelBtn = new MaterialSkin.Controls.MaterialButton();
            this.NewBtn = new MaterialSkin.Controls.MaterialButton();
            this.ScriptCBox = new MaterialSkin.Controls.MaterialComboBox();
            this.EditBtn = new MaterialSkin.Controls.MaterialButton();
            this.RunBtn = new MaterialSkin.Controls.MaterialButton();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.materialLabel7 = new MaterialSkin.Controls.MaterialLabel();
            this.materialTextBox4 = new MaterialSkin.Controls.MaterialTextBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            this.materialTextBox1 = new MaterialSkin.Controls.MaterialTextBox();
            this.materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            this.materialTextBox2 = new MaterialSkin.Controls.MaterialTextBox();
            this.materialLabel6 = new MaterialSkin.Controls.MaterialLabel();
            this.materialTextBox3 = new MaterialSkin.Controls.MaterialTextBox();
            this.materialButton5 = new MaterialSkin.Controls.MaterialButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DebugCB = new System.Windows.Forms.CheckBox();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.TestNoBox = new MaterialSkin.Controls.MaterialTextBox();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.TestLenBox = new MaterialSkin.Controls.MaterialTextBox();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.TestAddrBox = new MaterialSkin.Controls.MaterialTextBox();
            this.MRAM_TestBtn = new MaterialSkin.Controls.MaterialButton();
            this.menuIconList = new System.Windows.Forms.ImageList(this.components);
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.fILEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSciptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.publicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eDITToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.upDataParamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aLLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fUNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.agilentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTestCaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dumpFunctonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tESTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rTTEnableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.generateIDTABLEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cPUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.haltToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.regToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stackAnalysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pINToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sETToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rESETToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ihexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m7FlashToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.apolloBootLoaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.switchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.switchProtocolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vIEWToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ArmToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mAPToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.directoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pathInfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tOOLToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.hELPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.VerLab = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.materialLabel8 = new MaterialSkin.Controls.MaterialLabel();
            this.TestTimeBox = new MaterialSkin.Controls.MaterialTextBox();
            this.materialLabel9 = new MaterialSkin.Controls.MaterialLabel();
            this.BitNumBox = new MaterialSkin.Controls.MaterialTextBox();
            this.StartBtn = new MaterialSkin.Controls.MaterialButton();
            this.RepeatBox = new MaterialSkin.Controls.MaterialTextBox();
            this.RegularCB = new MaterialSkin.Controls.MaterialCheckbox();
            this.materialLabel10 = new MaterialSkin.Controls.MaterialLabel();
            this.EnhanceCB = new MaterialSkin.Controls.MaterialCheckbox();
            this.CrcCB = new MaterialSkin.Controls.MaterialCheckbox();
            this.PeripheralPage.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.MemoryPage.SuspendLayout();
            this.materialExpansionPanel5.SuspendLayout();
            this.ConfigPage.SuspendLayout();
            this.materialExpansionPanel3.SuspendLayout();
            this.TempPage.SuspendLayout();
            this.HomePage.SuspendLayout();
            this.MainLogMenu.SuspendLayout();
            this.ScriptPage.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // PeripheralPage
            // 
            this.PeripheralPage.BackColor = System.Drawing.Color.White;
            this.PeripheralPage.Controls.Add(this.groupBox3);
            this.PeripheralPage.Controls.Add(this.groupBox2);
            this.PeripheralPage.ImageKey = "round_bookmark_white_24dp.png";
            this.PeripheralPage.Location = new System.Drawing.Point(4, 31);
            this.PeripheralPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PeripheralPage.Name = "PeripheralPage";
            this.PeripheralPage.Size = new System.Drawing.Size(1392, 881);
            this.PeripheralPage.TabIndex = 8;
            this.PeripheralPage.Text = "Peripheral";
            // 
            // groupBox3
            // 
            this.groupBox3.AutoSize = true;
            this.groupBox3.Controls.Add(this.xSpiFreqbox);
            this.groupBox3.Controls.Add(this.SwitchProtocolCB);
            this.groupBox3.Controls.Add(this.AddrRadio);
            this.groupBox3.Controls.Add(this.DtrRadio);
            this.groupBox3.Controls.Add(this.xSpiCombox);
            this.groupBox3.Controls.Add(this.DqsRadio);
            this.groupBox3.Controls.Add(this.RegValueBox);
            this.groupBox3.Controls.Add(this.RegLengthBox);
            this.groupBox3.Controls.Add(this.RegAddrBox);
            this.groupBox3.Controls.Add(this.RegCombox);
            this.groupBox3.Controls.Add(this.materialButton6);
            this.groupBox3.Location = new System.Drawing.Point(4, 21);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(643, 388);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Interface Operation";
            // 
            // xSpiFreqbox
            // 
            this.xSpiFreqbox.AutoResize = false;
            this.xSpiFreqbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.xSpiFreqbox.Depth = 0;
            this.xSpiFreqbox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.xSpiFreqbox.DropDownHeight = 174;
            this.xSpiFreqbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.xSpiFreqbox.DropDownWidth = 121;
            this.xSpiFreqbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.xSpiFreqbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.xSpiFreqbox.FormattingEnabled = true;
            this.xSpiFreqbox.IntegralHeight = false;
            this.xSpiFreqbox.ItemHeight = 43;
            this.xSpiFreqbox.Items.AddRange(new object[] {
            "48",
            "24",
            "12",
            "6",
            "4",
            "2",
            "1"});
            this.xSpiFreqbox.Location = new System.Drawing.Point(284, 238);
            this.xSpiFreqbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.xSpiFreqbox.MaxDropDownItems = 4;
            this.xSpiFreqbox.MouseState = MaterialSkin.MouseState.OUT;
            this.xSpiFreqbox.Name = "xSpiFreqbox";
            this.xSpiFreqbox.Size = new System.Drawing.Size(119, 49);
            this.xSpiFreqbox.StartIndex = 0;
            this.xSpiFreqbox.TabIndex = 94;
            this.xSpiFreqbox.SelectedIndexChanged += new System.EventHandler(this.xSpiCombox_SelectedIndexChanged);
            // 
            // SwitchProtocolCB
            // 
            this.SwitchProtocolCB.AutoSize = true;
            this.SwitchProtocolCB.Depth = 0;
            this.SwitchProtocolCB.Location = new System.Drawing.Point(148, 266);
            this.SwitchProtocolCB.Margin = new System.Windows.Forms.Padding(0);
            this.SwitchProtocolCB.MouseLocation = new System.Drawing.Point(-1, -1);
            this.SwitchProtocolCB.MouseState = MaterialSkin.MouseState.HOVER;
            this.SwitchProtocolCB.Name = "SwitchProtocolCB";
            this.SwitchProtocolCB.Ripple = true;
            this.SwitchProtocolCB.Size = new System.Drawing.Size(83, 37);
            this.SwitchProtocolCB.TabIndex = 12;
            this.SwitchProtocolCB.Text = "Switch";
            this.SwitchProtocolCB.UseVisualStyleBackColor = true;
            // 
            // AddrRadio
            // 
            this.AddrRadio.AutoSize = true;
            this.AddrRadio.Checked = true;
            this.AddrRadio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AddrRadio.Depth = 0;
            this.AddrRadio.Location = new System.Drawing.Point(147, 215);
            this.AddrRadio.Margin = new System.Windows.Forms.Padding(0);
            this.AddrRadio.MouseLocation = new System.Drawing.Point(-1, -1);
            this.AddrRadio.MouseState = MaterialSkin.MouseState.HOVER;
            this.AddrRadio.Name = "AddrRadio";
            this.AddrRadio.Ripple = true;
            this.AddrRadio.Size = new System.Drawing.Size(71, 37);
            this.AddrRadio.TabIndex = 93;
            this.AddrRadio.Text = "32bit";
            this.AddrRadio.UseVisualStyleBackColor = true;
            this.AddrRadio.CheckedChanged += new System.EventHandler(this.xSpiCombox_SelectedIndexChanged);
            // 
            // DtrRadio
            // 
            this.DtrRadio.AutoSize = true;
            this.DtrRadio.Depth = 0;
            this.DtrRadio.Location = new System.Drawing.Point(31, 216);
            this.DtrRadio.Margin = new System.Windows.Forms.Padding(0);
            this.DtrRadio.MouseLocation = new System.Drawing.Point(-1, -1);
            this.DtrRadio.MouseState = MaterialSkin.MouseState.HOVER;
            this.DtrRadio.Name = "DtrRadio";
            this.DtrRadio.Ripple = true;
            this.DtrRadio.Size = new System.Drawing.Size(66, 37);
            this.DtrRadio.TabIndex = 13;
            this.DtrRadio.Text = "DTR";
            this.DtrRadio.UseVisualStyleBackColor = true;
            this.DtrRadio.CheckedChanged += new System.EventHandler(this.xSpiCombox_SelectedIndexChanged);
            // 
            // xSpiCombox
            // 
            this.xSpiCombox.AutoResize = false;
            this.xSpiCombox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.xSpiCombox.Depth = 0;
            this.xSpiCombox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.xSpiCombox.DropDownHeight = 174;
            this.xSpiCombox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.xSpiCombox.DropDownWidth = 121;
            this.xSpiCombox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.xSpiCombox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.xSpiCombox.FormattingEnabled = true;
            this.xSpiCombox.IntegralHeight = false;
            this.xSpiCombox.ItemHeight = 43;
            this.xSpiCombox.Items.AddRange(new object[] {
            "x1",
            "x4",
            "x8",
            "x16"});
            this.xSpiCombox.Location = new System.Drawing.Point(412, 238);
            this.xSpiCombox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.xSpiCombox.MaxDropDownItems = 4;
            this.xSpiCombox.MouseState = MaterialSkin.MouseState.OUT;
            this.xSpiCombox.Name = "xSpiCombox";
            this.xSpiCombox.Size = new System.Drawing.Size(148, 49);
            this.xSpiCombox.StartIndex = 0;
            this.xSpiCombox.TabIndex = 92;
            this.xSpiCombox.SelectedIndexChanged += new System.EventHandler(this.xSpiCombox_SelectedIndexChanged);
            // 
            // DqsRadio
            // 
            this.DqsRadio.AutoSize = true;
            this.DqsRadio.Depth = 0;
            this.DqsRadio.Location = new System.Drawing.Point(29, 268);
            this.DqsRadio.Margin = new System.Windows.Forms.Padding(0);
            this.DqsRadio.MouseLocation = new System.Drawing.Point(-1, -1);
            this.DqsRadio.MouseState = MaterialSkin.MouseState.HOVER;
            this.DqsRadio.Name = "DqsRadio";
            this.DqsRadio.Ripple = true;
            this.DqsRadio.Size = new System.Drawing.Size(67, 37);
            this.DqsRadio.TabIndex = 12;
            this.DqsRadio.Text = "DQS";
            this.DqsRadio.UseVisualStyleBackColor = true;
            this.DqsRadio.CheckedChanged += new System.EventHandler(this.xSpiCombox_SelectedIndexChanged);
            // 
            // RegValueBox
            // 
            this.RegValueBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RegValueBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.RegValueBox.Depth = 0;
            this.RegValueBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.RegValueBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.RegValueBox.Hint = "Value";
            this.RegValueBox.LeadingIcon = null;
            this.RegValueBox.Location = new System.Drawing.Point(29, 154);
            this.RegValueBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RegValueBox.MaxLength = 50;
            this.RegValueBox.MouseState = MaterialSkin.MouseState.OUT;
            this.RegValueBox.Multiline = false;
            this.RegValueBox.Name = "RegValueBox";
            this.RegValueBox.Size = new System.Drawing.Size(373, 36);
            this.RegValueBox.TabIndex = 91;
            this.RegValueBox.Text = "0";
            this.RegValueBox.TrailingIcon = null;
            this.RegValueBox.UseTallSize = false;
            // 
            // RegLengthBox
            // 
            this.RegLengthBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RegLengthBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.RegLengthBox.Depth = 0;
            this.RegLengthBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.RegLengthBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.RegLengthBox.Hint = "Length";
            this.RegLengthBox.LeadingIcon = null;
            this.RegLengthBox.Location = new System.Drawing.Point(209, 101);
            this.RegLengthBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RegLengthBox.MaxLength = 50;
            this.RegLengthBox.MouseState = MaterialSkin.MouseState.OUT;
            this.RegLengthBox.Multiline = false;
            this.RegLengthBox.Name = "RegLengthBox";
            this.RegLengthBox.Size = new System.Drawing.Size(193, 36);
            this.RegLengthBox.TabIndex = 90;
            this.RegLengthBox.Text = "1";
            this.RegLengthBox.TrailingIcon = null;
            this.RegLengthBox.UseTallSize = false;
            // 
            // RegAddrBox
            // 
            this.RegAddrBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RegAddrBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.RegAddrBox.Depth = 0;
            this.RegAddrBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.RegAddrBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.RegAddrBox.Hint = "Address";
            this.RegAddrBox.LeadingIcon = null;
            this.RegAddrBox.Location = new System.Drawing.Point(29, 101);
            this.RegAddrBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RegAddrBox.MaxLength = 50;
            this.RegAddrBox.MouseState = MaterialSkin.MouseState.OUT;
            this.RegAddrBox.Multiline = false;
            this.RegAddrBox.Name = "RegAddrBox";
            this.RegAddrBox.Size = new System.Drawing.Size(180, 36);
            this.RegAddrBox.TabIndex = 89;
            this.RegAddrBox.Text = "0";
            this.RegAddrBox.TrailingIcon = null;
            this.RegAddrBox.UseTallSize = false;
            // 
            // RegCombox
            // 
            this.RegCombox.AutoResize = false;
            this.RegCombox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.RegCombox.Depth = 0;
            this.RegCombox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.RegCombox.DropDownHeight = 174;
            this.RegCombox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RegCombox.DropDownWidth = 121;
            this.RegCombox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.RegCombox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.RegCombox.FormattingEnabled = true;
            this.RegCombox.IntegralHeight = false;
            this.RegCombox.ItemHeight = 43;
            this.RegCombox.Items.AddRange(new object[] {
            "Init",
            "DumpRegister",
            "Read",
            "Write",
            "ResetSOM"});
            this.RegCombox.Location = new System.Drawing.Point(29, 35);
            this.RegCombox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RegCombox.MaxDropDownItems = 4;
            this.RegCombox.MouseState = MaterialSkin.MouseState.OUT;
            this.RegCombox.Name = "RegCombox";
            this.RegCombox.Size = new System.Drawing.Size(372, 49);
            this.RegCombox.StartIndex = 0;
            this.RegCombox.TabIndex = 5;
            // 
            // materialButton6
            // 
            this.materialButton6.AutoSize = false;
            this.materialButton6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton6.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton6.Depth = 0;
            this.materialButton6.HighEmphasis = true;
            this.materialButton6.Icon = null;
            this.materialButton6.Location = new System.Drawing.Point(412, 35);
            this.materialButton6.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.materialButton6.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton6.Name = "materialButton6";
            this.materialButton6.Size = new System.Drawing.Size(148, 175);
            this.materialButton6.TabIndex = 4;
            this.materialButton6.Text = "Start";
            this.materialButton6.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton6.UseAccentColor = false;
            this.materialButton6.UseVisualStyleBackColor = true;
            this.materialButton6.Click += new System.EventHandler(this.materialButton6_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.RegPathBox);
            this.groupBox2.Controls.Add(this.LoadDefaultCB);
            this.groupBox2.Controls.Add(this.materialButton4);
            this.groupBox2.Controls.Add(this.ResetCB);
            this.groupBox2.Controls.Add(this.LoadTableBtn);
            this.groupBox2.Controls.Add(this.CrosstalkCB);
            this.groupBox2.Controls.Add(this.LoadRegFileBtn);
            this.groupBox2.Controls.Add(this.AttrbuteCB);
            this.groupBox2.Controls.Add(this.materialButton3);
            this.groupBox2.Controls.Add(this.RW_CB);
            this.groupBox2.Controls.Add(this.DefaultCB);
            this.groupBox2.Location = new System.Drawing.Point(684, 21);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(571, 344);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Register Test";
            // 
            // RegPathBox
            // 
            this.RegPathBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RegPathBox.Depth = 0;
            this.RegPathBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.RegPathBox.LeadingIcon = null;
            this.RegPathBox.Location = new System.Drawing.Point(20, 250);
            this.RegPathBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RegPathBox.MaxLength = 50;
            this.RegPathBox.MouseState = MaterialSkin.MouseState.OUT;
            this.RegPathBox.Multiline = false;
            this.RegPathBox.Name = "RegPathBox";
            this.RegPathBox.Size = new System.Drawing.Size(321, 50);
            this.RegPathBox.TabIndex = 2;
            this.RegPathBox.Text = "RegisterFile.csv";
            this.RegPathBox.TrailingIcon = null;
            // 
            // LoadDefaultCB
            // 
            this.LoadDefaultCB.AutoSize = true;
            this.LoadDefaultCB.Depth = 0;
            this.LoadDefaultCB.Location = new System.Drawing.Point(193, 190);
            this.LoadDefaultCB.Margin = new System.Windows.Forms.Padding(0);
            this.LoadDefaultCB.MouseLocation = new System.Drawing.Point(-1, -1);
            this.LoadDefaultCB.MouseState = MaterialSkin.MouseState.HOVER;
            this.LoadDefaultCB.Name = "LoadDefaultCB";
            this.LoadDefaultCB.Ripple = true;
            this.LoadDefaultCB.Size = new System.Drawing.Size(239, 37);
            this.LoadDefaultCB.TabIndex = 11;
            this.LoadDefaultCB.Text = "Get Default Value From SOM";
            this.LoadDefaultCB.UseVisualStyleBackColor = true;
            // 
            // materialButton4
            // 
            this.materialButton4.AutoSize = false;
            this.materialButton4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton4.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton4.Depth = 0;
            this.materialButton4.HighEmphasis = true;
            this.materialButton4.Icon = null;
            this.materialButton4.Location = new System.Drawing.Point(411, 251);
            this.materialButton4.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.materialButton4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton4.Name = "materialButton4";
            this.materialButton4.Size = new System.Drawing.Size(105, 59);
            this.materialButton4.TabIndex = 2;
            this.materialButton4.Text = "......";
            this.materialButton4.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton4.UseAccentColor = false;
            this.materialButton4.UseVisualStyleBackColor = true;
            this.materialButton4.Click += new System.EventHandler(this.materialButton4_Click);
            // 
            // ResetCB
            // 
            this.ResetCB.AutoSize = true;
            this.ResetCB.Depth = 0;
            this.ResetCB.Location = new System.Drawing.Point(7, 175);
            this.ResetCB.Margin = new System.Windows.Forms.Padding(0);
            this.ResetCB.MouseLocation = new System.Drawing.Point(-1, -1);
            this.ResetCB.MouseState = MaterialSkin.MouseState.HOVER;
            this.ResetCB.Name = "ResetCB";
            this.ResetCB.Ripple = true;
            this.ResetCB.Size = new System.Drawing.Size(74, 37);
            this.ResetCB.TabIndex = 10;
            this.ResetCB.Text = "Reset";
            this.ResetCB.UseVisualStyleBackColor = true;
            // 
            // LoadTableBtn
            // 
            this.LoadTableBtn.AutoSize = false;
            this.LoadTableBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.LoadTableBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.LoadTableBtn.Depth = 0;
            this.LoadTableBtn.HighEmphasis = true;
            this.LoadTableBtn.Icon = null;
            this.LoadTableBtn.Location = new System.Drawing.Point(197, 35);
            this.LoadTableBtn.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.LoadTableBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.LoadTableBtn.Name = "LoadTableBtn";
            this.LoadTableBtn.Size = new System.Drawing.Size(319, 45);
            this.LoadTableBtn.TabIndex = 2;
            this.LoadTableBtn.Text = "Load Register Table";
            this.LoadTableBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.LoadTableBtn.UseAccentColor = false;
            this.LoadTableBtn.UseVisualStyleBackColor = true;
            this.LoadTableBtn.Click += new System.EventHandler(this.materialButton3_Click_2);
            // 
            // CrosstalkCB
            // 
            this.CrosstalkCB.AutoSize = true;
            this.CrosstalkCB.Depth = 0;
            this.CrosstalkCB.Location = new System.Drawing.Point(7, 99);
            this.CrosstalkCB.Margin = new System.Windows.Forms.Padding(0);
            this.CrosstalkCB.MouseLocation = new System.Drawing.Point(-1, -1);
            this.CrosstalkCB.MouseState = MaterialSkin.MouseState.HOVER;
            this.CrosstalkCB.Name = "CrosstalkCB";
            this.CrosstalkCB.Ripple = true;
            this.CrosstalkCB.Size = new System.Drawing.Size(101, 37);
            this.CrosstalkCB.TabIndex = 9;
            this.CrosstalkCB.Text = "Crosstalk";
            this.CrosstalkCB.UseVisualStyleBackColor = true;
            // 
            // LoadRegFileBtn
            // 
            this.LoadRegFileBtn.AutoSize = false;
            this.LoadRegFileBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.LoadRegFileBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.LoadRegFileBtn.Depth = 0;
            this.LoadRegFileBtn.HighEmphasis = true;
            this.LoadRegFileBtn.Icon = null;
            this.LoadRegFileBtn.Location = new System.Drawing.Point(197, 88);
            this.LoadRegFileBtn.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.LoadRegFileBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.LoadRegFileBtn.Name = "LoadRegFileBtn";
            this.LoadRegFileBtn.Size = new System.Drawing.Size(319, 45);
            this.LoadRegFileBtn.TabIndex = 3;
            this.LoadRegFileBtn.Text = "Load Register Bit File";
            this.LoadRegFileBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.LoadRegFileBtn.UseAccentColor = false;
            this.LoadRegFileBtn.UseVisualStyleBackColor = true;
            this.LoadRegFileBtn.Click += new System.EventHandler(this.LoadRegFileBtn_Click);
            // 
            // AttrbuteCB
            // 
            this.AttrbuteCB.AutoSize = true;
            this.AttrbuteCB.Depth = 0;
            this.AttrbuteCB.Location = new System.Drawing.Point(7, 138);
            this.AttrbuteCB.Margin = new System.Windows.Forms.Padding(0);
            this.AttrbuteCB.MouseLocation = new System.Drawing.Point(-1, -1);
            this.AttrbuteCB.MouseState = MaterialSkin.MouseState.HOVER;
            this.AttrbuteCB.Name = "AttrbuteCB";
            this.AttrbuteCB.Ripple = true;
            this.AttrbuteCB.Size = new System.Drawing.Size(95, 37);
            this.AttrbuteCB.TabIndex = 8;
            this.AttrbuteCB.Text = "Attribute";
            this.AttrbuteCB.UseVisualStyleBackColor = true;
            // 
            // materialButton3
            // 
            this.materialButton3.AutoSize = false;
            this.materialButton3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton3.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton3.Depth = 0;
            this.materialButton3.HighEmphasis = true;
            this.materialButton3.Icon = null;
            this.materialButton3.Location = new System.Drawing.Point(197, 141);
            this.materialButton3.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.materialButton3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton3.Name = "materialButton3";
            this.materialButton3.Size = new System.Drawing.Size(319, 45);
            this.materialButton3.TabIndex = 4;
            this.materialButton3.Text = "Start Test";
            this.materialButton3.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton3.UseAccentColor = false;
            this.materialButton3.UseVisualStyleBackColor = true;
            this.materialButton3.Click += new System.EventHandler(this.materialButton3_Click_3);
            // 
            // RW_CB
            // 
            this.RW_CB.AutoSize = true;
            this.RW_CB.Depth = 0;
            this.RW_CB.Location = new System.Drawing.Point(5, 64);
            this.RW_CB.Margin = new System.Windows.Forms.Padding(0);
            this.RW_CB.MouseLocation = new System.Drawing.Point(-1, -1);
            this.RW_CB.MouseState = MaterialSkin.MouseState.HOVER;
            this.RW_CB.Name = "RW_CB";
            this.RW_CB.Ripple = true;
            this.RW_CB.Size = new System.Drawing.Size(59, 37);
            this.RW_CB.TabIndex = 7;
            this.RW_CB.Text = "RW";
            this.RW_CB.UseVisualStyleBackColor = true;
            // 
            // DefaultCB
            // 
            this.DefaultCB.AutoSize = true;
            this.DefaultCB.Depth = 0;
            this.DefaultCB.Location = new System.Drawing.Point(7, 26);
            this.DefaultCB.Margin = new System.Windows.Forms.Padding(0);
            this.DefaultCB.MouseLocation = new System.Drawing.Point(-1, -1);
            this.DefaultCB.MouseState = MaterialSkin.MouseState.HOVER;
            this.DefaultCB.Name = "DefaultCB";
            this.DefaultCB.Ripple = true;
            this.DefaultCB.Size = new System.Drawing.Size(131, 37);
            this.DefaultCB.TabIndex = 6;
            this.DefaultCB.Text = "Default Value";
            this.DefaultCB.UseVisualStyleBackColor = true;
            // 
            // MemoryPage
            // 
            this.MemoryPage.BackColor = System.Drawing.Color.White;
            this.MemoryPage.Controls.Add(this.materialExpansionPanel5);
            this.MemoryPage.Controls.Add(this.MaterialButton2);
            this.MemoryPage.Controls.Add(this.ProBar);
            this.MemoryPage.ImageKey = "round_swap_vert_white_24dp.png";
            this.MemoryPage.Location = new System.Drawing.Point(4, 31);
            this.MemoryPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MemoryPage.Name = "MemoryPage";
            this.MemoryPage.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MemoryPage.Size = new System.Drawing.Size(1392, 881);
            this.MemoryPage.TabIndex = 4;
            this.MemoryPage.Text = "Flash";
            // 
            // materialExpansionPanel5
            // 
            this.materialExpansionPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialExpansionPanel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialExpansionPanel5.CancelButtonText = "";
            this.materialExpansionPanel5.Controls.Add(this.materialTextBox5);
            this.materialExpansionPanel5.Controls.Add(this.materialButton1);
            this.materialExpansionPanel5.Controls.Add(this.MCU_Name);
            this.materialExpansionPanel5.Controls.Add(this.materialButton11);
            this.materialExpansionPanel5.Controls.Add(this.ImageFileBox);
            this.materialExpansionPanel5.Controls.Add(this.materialButton10);
            this.materialExpansionPanel5.Depth = 0;
            this.materialExpansionPanel5.Description = "";
            this.materialExpansionPanel5.ExpandHeight = 312;
            this.materialExpansionPanel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialExpansionPanel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialExpansionPanel5.Location = new System.Drawing.Point(31, 20);
            this.materialExpansionPanel5.Margin = new System.Windows.Forms.Padding(4, 20, 4, 20);
            this.materialExpansionPanel5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialExpansionPanel5.Name = "materialExpansionPanel5";
            this.materialExpansionPanel5.Padding = new System.Windows.Forms.Padding(32, 80, 32, 20);
            this.materialExpansionPanel5.Size = new System.Drawing.Size(1309, 312);
            this.materialExpansionPanel5.TabIndex = 95;
            this.materialExpansionPanel5.Title = "Download Image File";
            this.materialExpansionPanel5.ValidationButtonText = "";
            // 
            // materialTextBox5
            // 
            this.materialTextBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.materialTextBox5.Depth = 0;
            this.materialTextBox5.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialTextBox5.Hint = "Address";
            this.materialTextBox5.LeadingIcon = null;
            this.materialTextBox5.Location = new System.Drawing.Point(697, 131);
            this.materialTextBox5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.materialTextBox5.MaxLength = 50;
            this.materialTextBox5.MouseState = MaterialSkin.MouseState.OUT;
            this.materialTextBox5.Multiline = false;
            this.materialTextBox5.Name = "materialTextBox5";
            this.materialTextBox5.Size = new System.Drawing.Size(175, 50);
            this.materialTextBox5.TabIndex = 95;
            this.materialTextBox5.Text = "0x40000";
            this.materialTextBox5.TrailingIcon = null;
            // 
            // materialButton1
            // 
            this.materialButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.materialButton1.AutoSize = false;
            this.materialButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton1.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton1.Depth = 0;
            this.materialButton1.HighEmphasis = true;
            this.materialButton1.Icon = null;
            this.materialButton1.Location = new System.Drawing.Point(148, 122);
            this.materialButton1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.materialButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton1.Name = "materialButton1";
            this.materialButton1.Size = new System.Drawing.Size(88, 38);
            this.materialButton1.TabIndex = 94;
            this.materialButton1.Text = "Erase";
            this.materialButton1.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton1.UseAccentColor = false;
            this.materialButton1.UseVisualStyleBackColor = true;
            this.materialButton1.Visible = false;
            this.materialButton1.Click += new System.EventHandler(this.materialButton1_Click);
            // 
            // MCU_Name
            // 
            this.MCU_Name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MCU_Name.AutoResize = false;
            this.MCU_Name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.MCU_Name.Depth = 0;
            this.MCU_Name.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.MCU_Name.DropDownHeight = 174;
            this.MCU_Name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MCU_Name.DropDownWidth = 121;
            this.MCU_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.MCU_Name.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MCU_Name.FormattingEnabled = true;
            this.MCU_Name.IntegralHeight = false;
            this.MCU_Name.ItemHeight = 43;
            this.MCU_Name.Items.AddRange(new object[] {
            "Apollo4"});
            this.MCU_Name.Location = new System.Drawing.Point(963, 131);
            this.MCU_Name.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MCU_Name.MaxDropDownItems = 4;
            this.MCU_Name.MouseState = MaterialSkin.MouseState.OUT;
            this.MCU_Name.Name = "MCU_Name";
            this.MCU_Name.Size = new System.Drawing.Size(188, 49);
            this.MCU_Name.StartIndex = 0;
            this.MCU_Name.TabIndex = 93;
            // 
            // materialButton11
            // 
            this.materialButton11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.materialButton11.AutoSize = false;
            this.materialButton11.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton11.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton11.Depth = 0;
            this.materialButton11.HighEmphasis = true;
            this.materialButton11.Icon = null;
            this.materialButton11.Location = new System.Drawing.Point(1171, 69);
            this.materialButton11.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.materialButton11.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton11.Name = "materialButton11";
            this.materialButton11.Size = new System.Drawing.Size(123, 45);
            this.materialButton11.TabIndex = 92;
            this.materialButton11.Text = "...........";
            this.materialButton11.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton11.UseAccentColor = false;
            this.materialButton11.UseVisualStyleBackColor = true;
            this.materialButton11.Click += new System.EventHandler(this.materialButton11_Click);
            // 
            // ImageFileBox
            // 
            this.ImageFileBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImageFileBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ImageFileBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ImageFileBox.Depth = 0;
            this.ImageFileBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ImageFileBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ImageFileBox.Hint = "Load Flash Image File(.hex/.bin)";
            this.ImageFileBox.LeadingIcon = null;
            this.ImageFileBox.Location = new System.Drawing.Point(75, 68);
            this.ImageFileBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ImageFileBox.MaxLength = 50;
            this.ImageFileBox.MouseState = MaterialSkin.MouseState.OUT;
            this.ImageFileBox.Multiline = false;
            this.ImageFileBox.Name = "ImageFileBox";
            this.ImageFileBox.Size = new System.Drawing.Size(1083, 36);
            this.ImageFileBox.TabIndex = 88;
            this.ImageFileBox.Text = "";
            this.ImageFileBox.TrailingIcon = null;
            this.ImageFileBox.UseTallSize = false;
            // 
            // materialButton10
            // 
            this.materialButton10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.materialButton10.AutoSize = false;
            this.materialButton10.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton10.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton10.Depth = 0;
            this.materialButton10.HighEmphasis = true;
            this.materialButton10.Icon = null;
            this.materialButton10.Location = new System.Drawing.Point(1171, 126);
            this.materialButton10.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.materialButton10.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton10.Name = "materialButton10";
            this.materialButton10.Size = new System.Drawing.Size(123, 46);
            this.materialButton10.TabIndex = 89;
            this.materialButton10.Text = "Program";
            this.materialButton10.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton10.UseAccentColor = false;
            this.materialButton10.UseVisualStyleBackColor = true;
            this.materialButton10.Click += new System.EventHandler(this.materialButton10_Click);
            // 
            // MaterialButton2
            // 
            this.MaterialButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MaterialButton2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MaterialButton2.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.MaterialButton2.Depth = 0;
            this.MaterialButton2.HighEmphasis = true;
            this.MaterialButton2.Icon = global::SOM_DEVELOP_TOOL.Properties.Resources.round_add_black_24dp;
            this.MaterialButton2.Location = new System.Drawing.Point(1642, 180);
            this.MaterialButton2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.MaterialButton2.MouseState = MaterialSkin.MouseState.HOVER;
            this.MaterialButton2.Name = "MaterialButton2";
            this.MaterialButton2.Size = new System.Drawing.Size(78, 36);
            this.MaterialButton2.TabIndex = 2;
            this.MaterialButton2.Text = "Add";
            this.MaterialButton2.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.MaterialButton2.UseAccentColor = false;
            this.MaterialButton2.UseVisualStyleBackColor = true;
            this.MaterialButton2.Click += new System.EventHandler(this.MaterialButton2_Click);
            // 
            // ProBar
            // 
            this.ProBar.Depth = 0;
            this.ProBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ProBar.Location = new System.Drawing.Point(4, 872);
            this.ProBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ProBar.MouseState = MaterialSkin.MouseState.HOVER;
            this.ProBar.Name = "ProBar";
            this.ProBar.Size = new System.Drawing.Size(1384, 5);
            this.ProBar.TabIndex = 0;
            // 
            // ConfigPage
            // 
            this.ConfigPage.BackColor = System.Drawing.Color.White;
            this.ConfigPage.Controls.Add(this.materialExpansionPanel3);
            this.ConfigPage.ImageKey = "round_build_white_24dp.png";
            this.ConfigPage.Location = new System.Drawing.Point(4, 31);
            this.ConfigPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ConfigPage.Name = "ConfigPage";
            this.ConfigPage.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ConfigPage.Size = new System.Drawing.Size(1392, 881);
            this.ConfigPage.TabIndex = 2;
            this.ConfigPage.Text = "Config";
            this.ConfigPage.Click += new System.EventHandler(this.ConfigPage_Click);
            // 
            // materialExpansionPanel3
            // 
            this.materialExpansionPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialExpansionPanel3.CancelButtonText = "";
            this.materialExpansionPanel3.Controls.Add(this.JlinkSpeedCBox);
            this.materialExpansionPanel3.Controls.Add(this.IdRangeBox);
            this.materialExpansionPanel3.Controls.Add(this.label1);
            this.materialExpansionPanel3.Controls.Add(this.M7_CPU1_CB);
            this.materialExpansionPanel3.Controls.Add(this.IdRangeBtn);
            this.materialExpansionPanel3.Controls.Add(this.M7_CPU0_CB);
            this.materialExpansionPanel3.Controls.Add(this.checkBox1);
            this.materialExpansionPanel3.Controls.Add(this.IdSortCB);
            this.materialExpansionPanel3.Controls.Add(this.materialCheckBox3);
            this.materialExpansionPanel3.Controls.Add(this.materialCheckBox1);
            this.materialExpansionPanel3.Controls.Add(this.checkBox2);
            this.materialExpansionPanel3.Depth = 0;
            this.materialExpansionPanel3.Description = "Set Log Parameter Before Open ";
            this.materialExpansionPanel3.ExpandHeight = 440;
            this.materialExpansionPanel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialExpansionPanel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialExpansionPanel3.Location = new System.Drawing.Point(25, 24);
            this.materialExpansionPanel3.Margin = new System.Windows.Forms.Padding(21, 20, 21, 20);
            this.materialExpansionPanel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialExpansionPanel3.Name = "materialExpansionPanel3";
            this.materialExpansionPanel3.Padding = new System.Windows.Forms.Padding(32, 74, 32, 19);
            this.materialExpansionPanel3.Size = new System.Drawing.Size(1163, 440);
            this.materialExpansionPanel3.TabIndex = 59;
            this.materialExpansionPanel3.Title = "Log";
            this.materialExpansionPanel3.UseAccentColor = true;
            this.materialExpansionPanel3.ValidationButtonEnable = true;
            this.materialExpansionPanel3.ValidationButtonText = "";
            // 
            // JlinkSpeedCBox
            // 
            this.JlinkSpeedCBox.FormattingEnabled = true;
            this.JlinkSpeedCBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "5",
            "10",
            "20",
            "25",
            "33",
            "50"});
            this.JlinkSpeedCBox.Location = new System.Drawing.Point(599, 149);
            this.JlinkSpeedCBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.JlinkSpeedCBox.Name = "JlinkSpeedCBox";
            this.JlinkSpeedCBox.Size = new System.Drawing.Size(160, 25);
            this.JlinkSpeedCBox.TabIndex = 62;
            this.JlinkSpeedCBox.Text = "1";
            this.JlinkSpeedCBox.SelectedIndexChanged += new System.EventHandler(this.JlinkSpeedCBox_SelectedIndexChanged);
            // 
            // IdRangeBox
            // 
            this.IdRangeBox.Location = new System.Drawing.Point(164, 250);
            this.IdRangeBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.IdRangeBox.Multiline = true;
            this.IdRangeBox.Name = "IdRangeBox";
            this.IdRangeBox.Size = new System.Drawing.Size(867, 44);
            this.IdRangeBox.TabIndex = 61;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(784, 152);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 18);
            this.label1.TabIndex = 60;
            this.label1.Text = "Speed(MHz)";
            // 
            // M7_CPU1_CB
            // 
            this.M7_CPU1_CB.AutoSize = true;
            this.M7_CPU1_CB.Depth = 0;
            this.M7_CPU1_CB.Enabled = false;
            this.M7_CPU1_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.M7_CPU1_CB.Location = new System.Drawing.Point(11, 134);
            this.M7_CPU1_CB.Margin = new System.Windows.Forms.Padding(0);
            this.M7_CPU1_CB.MouseLocation = new System.Drawing.Point(-1, -1);
            this.M7_CPU1_CB.MouseState = MaterialSkin.MouseState.HOVER;
            this.M7_CPU1_CB.Name = "M7_CPU1_CB";
            this.M7_CPU1_CB.Ripple = true;
            this.M7_CPU1_CB.Size = new System.Drawing.Size(97, 37);
            this.M7_CPU1_CB.TabIndex = 59;
            this.M7_CPU1_CB.Text = "CPU1";
            this.M7_CPU1_CB.UseVisualStyleBackColor = true;
            this.M7_CPU1_CB.CheckedChanged += new System.EventHandler(this.M7_CPU1_CB_CheckedChanged);
            // 
            // IdRangeBtn
            // 
            this.IdRangeBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.IdRangeBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.IdRangeBtn.Depth = 0;
            this.IdRangeBtn.HighEmphasis = true;
            this.IdRangeBtn.Icon = null;
            this.IdRangeBtn.Location = new System.Drawing.Point(25, 250);
            this.IdRangeBtn.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.IdRangeBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.IdRangeBtn.Name = "IdRangeBtn";
            this.IdRangeBtn.Size = new System.Drawing.Size(86, 36);
            this.IdRangeBtn.TabIndex = 58;
            this.IdRangeBtn.Text = "ID Filter";
            this.IdRangeBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.IdRangeBtn.UseAccentColor = false;
            this.IdRangeBtn.UseVisualStyleBackColor = true;
            this.IdRangeBtn.Click += new System.EventHandler(this.IdRangeBtn_Click);
            // 
            // M7_CPU0_CB
            // 
            this.M7_CPU0_CB.AutoSize = true;
            this.M7_CPU0_CB.Checked = true;
            this.M7_CPU0_CB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.M7_CPU0_CB.Depth = 0;
            this.M7_CPU0_CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.M7_CPU0_CB.Location = new System.Drawing.Point(11, 88);
            this.M7_CPU0_CB.Margin = new System.Windows.Forms.Padding(0);
            this.M7_CPU0_CB.MouseLocation = new System.Drawing.Point(-1, -1);
            this.M7_CPU0_CB.MouseState = MaterialSkin.MouseState.HOVER;
            this.M7_CPU0_CB.Name = "M7_CPU0_CB";
            this.M7_CPU0_CB.Ripple = true;
            this.M7_CPU0_CB.Size = new System.Drawing.Size(97, 37);
            this.M7_CPU0_CB.TabIndex = 49;
            this.M7_CPU0_CB.Text = "CPU0";
            this.M7_CPU0_CB.UseVisualStyleBackColor = true;
            this.M7_CPU0_CB.CheckedChanged += new System.EventHandler(this.M7_CPU0_CB_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Depth = 0;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.checkBox1.Location = new System.Drawing.Point(211, 88);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(0);
            this.checkBox1.MouseLocation = new System.Drawing.Point(-1, -1);
            this.checkBox1.MouseState = MaterialSkin.MouseState.HOVER;
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Ripple = true;
            this.checkBox1.Size = new System.Drawing.Size(112, 37);
            this.checkBox1.TabIndex = 50;
            this.checkBox1.Text = "PcTime";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // IdSortCB
            // 
            this.IdSortCB.AutoSize = true;
            this.IdSortCB.Depth = 0;
            this.IdSortCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.IdSortCB.Location = new System.Drawing.Point(407, 134);
            this.IdSortCB.Margin = new System.Windows.Forms.Padding(0);
            this.IdSortCB.MouseLocation = new System.Drawing.Point(-1, -1);
            this.IdSortCB.MouseState = MaterialSkin.MouseState.HOVER;
            this.IdSortCB.Name = "IdSortCB";
            this.IdSortCB.Ripple = true;
            this.IdSortCB.Size = new System.Drawing.Size(109, 37);
            this.IdSortCB.TabIndex = 54;
            this.IdSortCB.Text = "ID_Sort";
            this.IdSortCB.UseVisualStyleBackColor = true;
            this.IdSortCB.CheckedChanged += new System.EventHandler(this.IdSortCB_CheckedChanged);
            // 
            // materialCheckBox3
            // 
            this.materialCheckBox3.AutoSize = true;
            this.materialCheckBox3.Depth = 0;
            this.materialCheckBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialCheckBox3.Location = new System.Drawing.Point(211, 134);
            this.materialCheckBox3.Margin = new System.Windows.Forms.Padding(0);
            this.materialCheckBox3.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialCheckBox3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCheckBox3.Name = "materialCheckBox3";
            this.materialCheckBox3.Ripple = true;
            this.materialCheckBox3.Size = new System.Drawing.Size(137, 37);
            this.materialCheckBox3.TabIndex = 51;
            this.materialCheckBox3.Text = "BlockMode";
            this.materialCheckBox3.UseVisualStyleBackColor = true;
            this.materialCheckBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // materialCheckBox1
            // 
            this.materialCheckBox1.AutoSize = true;
            this.materialCheckBox1.Depth = 0;
            this.materialCheckBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialCheckBox1.Location = new System.Drawing.Point(407, 88);
            this.materialCheckBox1.Margin = new System.Windows.Forms.Padding(0);
            this.materialCheckBox1.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialCheckBox1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCheckBox1.Name = "materialCheckBox1";
            this.materialCheckBox1.Ripple = true;
            this.materialCheckBox1.Size = new System.Drawing.Size(125, 37);
            this.materialCheckBox1.TabIndex = 53;
            this.materialCheckBox1.Text = "McuTime";
            this.materialCheckBox1.UseVisualStyleBackColor = true;
            this.materialCheckBox1.CheckedChanged += new System.EventHandler(this.materialCheckBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Depth = 0;
            this.checkBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.checkBox2.Location = new System.Drawing.Point(587, 88);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(0);
            this.checkBox2.MouseLocation = new System.Drawing.Point(-1, -1);
            this.checkBox2.MouseState = MaterialSkin.MouseState.HOVER;
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Ripple = true;
            this.checkBox2.Size = new System.Drawing.Size(146, 37);
            this.checkBox2.TabIndex = 52;
            this.checkBox2.Text = "ShowEnable";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // TempPage
            // 
            this.TempPage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TempPage.Controls.Add(this.HomePage);
            this.TempPage.Controls.Add(this.ScriptPage);
            this.TempPage.Controls.Add(this.PeripheralPage);
            this.TempPage.Controls.Add(this.MemoryPage);
            this.TempPage.Controls.Add(this.ConfigPage);
            this.TempPage.Controls.Add(this.tabPage1);
            this.TempPage.Depth = 0;
            this.TempPage.ImageList = this.menuIconList;
            this.TempPage.Location = new System.Drawing.Point(0, 68);
            this.TempPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TempPage.MouseState = MaterialSkin.MouseState.HOVER;
            this.TempPage.Multiline = true;
            this.TempPage.Name = "TempPage";
            this.TempPage.SelectedIndex = 0;
            this.TempPage.Size = new System.Drawing.Size(1400, 916);
            this.TempPage.TabIndex = 18;
            this.TempPage.SelectedIndexChanged += new System.EventHandler(this.materialTabControl1_SelectedIndexChanged);
            // 
            // HomePage
            // 
            this.HomePage.BackColor = System.Drawing.Color.White;
            this.HomePage.Controls.Add(this.materialDivider1);
            this.HomePage.Controls.Add(this.textBox3);
            this.HomePage.Controls.Add(this.MsgBox);
            this.HomePage.Controls.Add(this.textBox2);
            this.HomePage.Controls.Add(this.textBox1);
            this.HomePage.ImageKey = "round_assessment_white_24dp.png";
            this.HomePage.Location = new System.Drawing.Point(4, 31);
            this.HomePage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.HomePage.Name = "HomePage";
            this.HomePage.Size = new System.Drawing.Size(1392, 881);
            this.HomePage.TabIndex = 0;
            this.HomePage.Text = "Home";
            // 
            // materialDivider1
            // 
            this.materialDivider1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialDivider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider1.Depth = 0;
            this.materialDivider1.Location = new System.Drawing.Point(3, 748);
            this.materialDivider1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            this.materialDivider1.Size = new System.Drawing.Size(1349, 9);
            this.materialDivider1.TabIndex = 6;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Location = new System.Drawing.Point(1, 820);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(1343, 18);
            this.textBox3.TabIndex = 5;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            this.textBox3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ComBox_KeyDown);
            // 
            // MsgBox
            // 
            this.MsgBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MsgBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.MsgBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MsgBox.ContextMenuStrip = this.MainLogMenu;
            this.MsgBox.Depth = 0;
            this.MsgBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.MsgBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MsgBox.Location = new System.Drawing.Point(1, 4);
            this.MsgBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MsgBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.MsgBox.Name = "MsgBox";
            this.MsgBox.Size = new System.Drawing.Size(1352, 738);
            this.MsgBox.TabIndex = 0;
            this.MsgBox.Text = "";
            // 
            // MainLogMenu
            // 
            this.MainLogMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.MainLogMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem});
            this.MainLogMenu.Name = "MainLogMenu";
            this.MainLogMenu.Size = new System.Drawing.Size(116, 28);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(115, 24);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(1, 792);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(1343, 18);
            this.textBox2.TabIndex = 4;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ComBox_KeyDown);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(1, 762);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1343, 18);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ComBox_KeyDown);
            // 
            // ScriptPage
            // 
            this.ScriptPage.Controls.Add(this.MsgBox1);
            this.ScriptPage.Controls.Add(this.groupBox5);
            this.ScriptPage.ImageKey = "round_gps_fixed_white_24dp.png";
            this.ScriptPage.Location = new System.Drawing.Point(4, 31);
            this.ScriptPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ScriptPage.Name = "ScriptPage";
            this.ScriptPage.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ScriptPage.Size = new System.Drawing.Size(1392, 881);
            this.ScriptPage.TabIndex = 10;
            this.ScriptPage.Text = "CsScript";
            this.ScriptPage.UseVisualStyleBackColor = true;
            // 
            // MsgBox1
            // 
            this.MsgBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MsgBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.MsgBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MsgBox1.ContextMenuStrip = this.MainLogMenu;
            this.MsgBox1.Depth = 0;
            this.MsgBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.MsgBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MsgBox1.Location = new System.Drawing.Point(0, 4);
            this.MsgBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MsgBox1.MouseState = MaterialSkin.MouseState.HOVER;
            this.MsgBox1.Name = "MsgBox1";
            this.MsgBox1.Size = new System.Drawing.Size(1389, 660);
            this.MsgBox1.TabIndex = 97;
            this.MsgBox1.Text = "";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.AutoSize = true;
            this.groupBox5.Controls.Add(this.OpenBtn);
            this.groupBox5.Controls.Add(this.DelBtn);
            this.groupBox5.Controls.Add(this.NewBtn);
            this.groupBox5.Controls.Add(this.ScriptCBox);
            this.groupBox5.Controls.Add(this.EditBtn);
            this.groupBox5.Controls.Add(this.RunBtn);
            this.groupBox5.Location = new System.Drawing.Point(861, 648);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox5.Size = new System.Drawing.Size(515, 225);
            this.groupBox5.TabIndex = 96;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Test";
            // 
            // OpenBtn
            // 
            this.OpenBtn.AutoSize = false;
            this.OpenBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.OpenBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.OpenBtn.Depth = 0;
            this.OpenBtn.HighEmphasis = true;
            this.OpenBtn.Icon = null;
            this.OpenBtn.Location = new System.Drawing.Point(183, 144);
            this.OpenBtn.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.OpenBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.OpenBtn.Name = "OpenBtn";
            this.OpenBtn.Size = new System.Drawing.Size(148, 48);
            this.OpenBtn.TabIndex = 9;
            this.OpenBtn.Text = "Open";
            this.OpenBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.OpenBtn.UseAccentColor = false;
            this.OpenBtn.UseVisualStyleBackColor = true;
            this.OpenBtn.Click += new System.EventHandler(this.OpenBtn_Click);
            // 
            // DelBtn
            // 
            this.DelBtn.AutoSize = false;
            this.DelBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DelBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.DelBtn.Depth = 0;
            this.DelBtn.HighEmphasis = true;
            this.DelBtn.Icon = null;
            this.DelBtn.Location = new System.Drawing.Point(23, 144);
            this.DelBtn.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.DelBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.DelBtn.Name = "DelBtn";
            this.DelBtn.Size = new System.Drawing.Size(148, 45);
            this.DelBtn.TabIndex = 8;
            this.DelBtn.Text = "Delete";
            this.DelBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.DelBtn.UseAccentColor = false;
            this.DelBtn.UseVisualStyleBackColor = true;
            this.DelBtn.Click += new System.EventHandler(this.DelButton7_Click);
            // 
            // NewBtn
            // 
            this.NewBtn.AutoSize = false;
            this.NewBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.NewBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.NewBtn.Depth = 0;
            this.NewBtn.HighEmphasis = true;
            this.NewBtn.Icon = null;
            this.NewBtn.Location = new System.Drawing.Point(23, 89);
            this.NewBtn.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.NewBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.NewBtn.Name = "NewBtn";
            this.NewBtn.Size = new System.Drawing.Size(148, 48);
            this.NewBtn.TabIndex = 7;
            this.NewBtn.Text = "New";
            this.NewBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.NewBtn.UseAccentColor = false;
            this.NewBtn.UseVisualStyleBackColor = true;
            this.NewBtn.Click += new System.EventHandler(this.NewBtn_Click);
            // 
            // ScriptCBox
            // 
            this.ScriptCBox.AutoResize = false;
            this.ScriptCBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ScriptCBox.Depth = 0;
            this.ScriptCBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ScriptCBox.DropDownHeight = 174;
            this.ScriptCBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ScriptCBox.DropDownWidth = 121;
            this.ScriptCBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.ScriptCBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ScriptCBox.FormattingEnabled = true;
            this.ScriptCBox.IntegralHeight = false;
            this.ScriptCBox.ItemHeight = 43;
            this.ScriptCBox.Location = new System.Drawing.Point(23, 29);
            this.ScriptCBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ScriptCBox.MaxDropDownItems = 4;
            this.ScriptCBox.MouseState = MaterialSkin.MouseState.OUT;
            this.ScriptCBox.Name = "ScriptCBox";
            this.ScriptCBox.Size = new System.Drawing.Size(305, 49);
            this.ScriptCBox.StartIndex = 0;
            this.ScriptCBox.TabIndex = 5;
            // 
            // EditBtn
            // 
            this.EditBtn.AutoSize = false;
            this.EditBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.EditBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.EditBtn.Depth = 0;
            this.EditBtn.HighEmphasis = true;
            this.EditBtn.Icon = null;
            this.EditBtn.Location = new System.Drawing.Point(181, 89);
            this.EditBtn.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.EditBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.EditBtn.Name = "EditBtn";
            this.EditBtn.Size = new System.Drawing.Size(148, 48);
            this.EditBtn.TabIndex = 6;
            this.EditBtn.Text = "Edit";
            this.EditBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.EditBtn.UseAccentColor = false;
            this.EditBtn.UseVisualStyleBackColor = true;
            this.EditBtn.Click += new System.EventHandler(this.EditBtn_Click);
            // 
            // RunBtn
            // 
            this.RunBtn.AutoSize = false;
            this.RunBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.RunBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.RunBtn.Depth = 0;
            this.RunBtn.HighEmphasis = true;
            this.RunBtn.Icon = null;
            this.RunBtn.Location = new System.Drawing.Point(341, 29);
            this.RunBtn.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.RunBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.RunBtn.Name = "RunBtn";
            this.RunBtn.Size = new System.Drawing.Size(148, 162);
            this.RunBtn.TabIndex = 4;
            this.RunBtn.Text = "Run";
            this.RunBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.RunBtn.UseAccentColor = false;
            this.RunBtn.UseVisualStyleBackColor = true;
            this.RunBtn.Click += new System.EventHandler(this.RunBtn_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.groupBox6);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.ImageKey = "round_report_problem_white_24dp.png";
            this.tabPage1.Location = new System.Drawing.Point(4, 31);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Size = new System.Drawing.Size(1392, 881);
            this.tabPage1.TabIndex = 9;
            this.tabPage1.Text = "TEMP";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.materialLabel7);
            this.groupBox4.Controls.Add(this.materialTextBox4);
            this.groupBox4.Controls.Add(this.checkBox3);
            this.groupBox4.Controls.Add(this.materialLabel4);
            this.groupBox4.Controls.Add(this.materialTextBox1);
            this.groupBox4.Controls.Add(this.materialLabel5);
            this.groupBox4.Controls.Add(this.materialTextBox2);
            this.groupBox4.Controls.Add(this.materialLabel6);
            this.groupBox4.Controls.Add(this.materialTextBox3);
            this.groupBox4.Controls.Add(this.materialButton5);
            this.groupBox4.Location = new System.Drawing.Point(35, 221);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Size = new System.Drawing.Size(1124, 298);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Test Power";
            // 
            // materialLabel7
            // 
            this.materialLabel7.AutoSize = true;
            this.materialLabel7.Depth = 0;
            this.materialLabel7.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel7.Location = new System.Drawing.Point(9, 236);
            this.materialLabel7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.materialLabel7.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel7.Name = "materialLabel7";
            this.materialLabel7.Size = new System.Drawing.Size(52, 19);
            this.materialLabel7.TabIndex = 9;
            this.materialLabel7.Text = "Pattern";
            // 
            // materialTextBox4
            // 
            this.materialTextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.materialTextBox4.Depth = 0;
            this.materialTextBox4.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialTextBox4.LeadingIcon = null;
            this.materialTextBox4.Location = new System.Drawing.Point(8, 151);
            this.materialTextBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.materialTextBox4.MaxLength = 50;
            this.materialTextBox4.MouseState = MaterialSkin.MouseState.OUT;
            this.materialTextBox4.Multiline = false;
            this.materialTextBox4.Name = "materialTextBox4";
            this.materialTextBox4.Size = new System.Drawing.Size(373, 50);
            this.materialTextBox4.TabIndex = 8;
            this.materialTextBox4.Text = "0xFF,0xF0";
            this.materialTextBox4.TrailingIcon = null;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(997, 111);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(69, 19);
            this.checkBox3.TabIndex = 7;
            this.checkBox3.Text = "Debug";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // materialLabel4
            // 
            this.materialLabel4.AutoSize = true;
            this.materialLabel4.Depth = 0;
            this.materialLabel4.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel4.Location = new System.Drawing.Point(757, 110);
            this.materialLabel4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Size = new System.Drawing.Size(102, 19);
            this.materialLabel4.TabIndex = 6;
            this.materialLabel4.Text = "WaitTime(ms)";
            // 
            // materialTextBox1
            // 
            this.materialTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.materialTextBox1.Depth = 0;
            this.materialTextBox1.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialTextBox1.LeadingIcon = null;
            this.materialTextBox1.Location = new System.Drawing.Point(756, 25);
            this.materialTextBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.materialTextBox1.MaxLength = 50;
            this.materialTextBox1.MouseState = MaterialSkin.MouseState.OUT;
            this.materialTextBox1.Multiline = false;
            this.materialTextBox1.Name = "materialTextBox1";
            this.materialTextBox1.Size = new System.Drawing.Size(211, 50);
            this.materialTextBox1.TabIndex = 5;
            this.materialTextBox1.Text = "3000";
            this.materialTextBox1.TrailingIcon = null;
            // 
            // materialLabel5
            // 
            this.materialLabel5.AutoSize = true;
            this.materialLabel5.Depth = 0;
            this.materialLabel5.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel5.Location = new System.Drawing.Point(511, 110);
            this.materialLabel5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel5.Name = "materialLabel5";
            this.materialLabel5.Size = new System.Drawing.Size(91, 19);
            this.materialLabel5.TabIndex = 4;
            this.materialLabel5.Text = "Length(byte)";
            // 
            // materialTextBox2
            // 
            this.materialTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.materialTextBox2.Depth = 0;
            this.materialTextBox2.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialTextBox2.LeadingIcon = null;
            this.materialTextBox2.Location = new System.Drawing.Point(515, 25);
            this.materialTextBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.materialTextBox2.MaxLength = 50;
            this.materialTextBox2.MouseState = MaterialSkin.MouseState.OUT;
            this.materialTextBox2.Multiline = false;
            this.materialTextBox2.Name = "materialTextBox2";
            this.materialTextBox2.Size = new System.Drawing.Size(233, 50);
            this.materialTextBox2.TabIndex = 3;
            this.materialTextBox2.Text = "512";
            this.materialTextBox2.TrailingIcon = null;
            // 
            // materialLabel6
            // 
            this.materialLabel6.AutoSize = true;
            this.materialLabel6.Depth = 0;
            this.materialLabel6.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel6.Location = new System.Drawing.Point(9, 110);
            this.materialLabel6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.materialLabel6.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel6.Name = "materialLabel6";
            this.materialLabel6.Size = new System.Drawing.Size(66, 19);
            this.materialLabel6.TabIndex = 2;
            this.materialLabel6.Text = "Prescaler";
            // 
            // materialTextBox3
            // 
            this.materialTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.materialTextBox3.Depth = 0;
            this.materialTextBox3.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialTextBox3.LeadingIcon = null;
            this.materialTextBox3.Location = new System.Drawing.Point(8, 25);
            this.materialTextBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.materialTextBox3.MaxLength = 50;
            this.materialTextBox3.MouseState = MaterialSkin.MouseState.OUT;
            this.materialTextBox3.Multiline = false;
            this.materialTextBox3.Name = "materialTextBox3";
            this.materialTextBox3.Size = new System.Drawing.Size(373, 50);
            this.materialTextBox3.TabIndex = 1;
            this.materialTextBox3.Text = "1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 14, 18, 20, 25, 30, 40, 50, 60, 70, 80, 90, 10" +
    "0,200 ";
            this.materialTextBox3.TrailingIcon = null;
            // 
            // materialButton5
            // 
            this.materialButton5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton5.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton5.Depth = 0;
            this.materialButton5.HighEmphasis = true;
            this.materialButton5.Icon = null;
            this.materialButton5.Location = new System.Drawing.Point(997, 16);
            this.materialButton5.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.materialButton5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton5.Name = "materialButton5";
            this.materialButton5.Size = new System.Drawing.Size(67, 36);
            this.materialButton5.TabIndex = 0;
            this.materialButton5.Text = "Start";
            this.materialButton5.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton5.UseAccentColor = false;
            this.materialButton5.UseVisualStyleBackColor = true;
            this.materialButton5.Click += new System.EventHandler(this.materialButton5_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DebugCB);
            this.groupBox1.Controls.Add(this.materialLabel3);
            this.groupBox1.Controls.Add(this.TestNoBox);
            this.groupBox1.Controls.Add(this.materialLabel2);
            this.groupBox1.Controls.Add(this.TestLenBox);
            this.groupBox1.Controls.Add(this.materialLabel1);
            this.groupBox1.Controls.Add(this.TestAddrBox);
            this.groupBox1.Controls.Add(this.MRAM_TestBtn);
            this.groupBox1.Location = new System.Drawing.Point(27, 40);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(1124, 144);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MRAM Cycling Test";
            // 
            // DebugCB
            // 
            this.DebugCB.AutoSize = true;
            this.DebugCB.Location = new System.Drawing.Point(997, 111);
            this.DebugCB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DebugCB.Name = "DebugCB";
            this.DebugCB.Size = new System.Drawing.Size(69, 19);
            this.DebugCB.TabIndex = 7;
            this.DebugCB.Text = "Debug";
            this.DebugCB.UseVisualStyleBackColor = true;
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel3.Location = new System.Drawing.Point(757, 110);
            this.materialLabel3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(92, 19);
            this.materialLabel3.TabIndex = 6;
            this.materialLabel3.Text = "Test Number";
            // 
            // TestNoBox
            // 
            this.TestNoBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TestNoBox.Depth = 0;
            this.TestNoBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TestNoBox.LeadingIcon = null;
            this.TestNoBox.Location = new System.Drawing.Point(756, 25);
            this.TestNoBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TestNoBox.MaxLength = 50;
            this.TestNoBox.MouseState = MaterialSkin.MouseState.OUT;
            this.TestNoBox.Multiline = false;
            this.TestNoBox.Name = "TestNoBox";
            this.TestNoBox.Size = new System.Drawing.Size(211, 50);
            this.TestNoBox.TabIndex = 5;
            this.TestNoBox.Text = "1";
            this.TestNoBox.TrailingIcon = null;
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel2.Location = new System.Drawing.Point(511, 110);
            this.materialLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(91, 19);
            this.materialLabel2.TabIndex = 4;
            this.materialLabel2.Text = "Length(byte)";
            // 
            // TestLenBox
            // 
            this.TestLenBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TestLenBox.Depth = 0;
            this.TestLenBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TestLenBox.LeadingIcon = null;
            this.TestLenBox.Location = new System.Drawing.Point(515, 25);
            this.TestLenBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TestLenBox.MaxLength = 50;
            this.TestLenBox.MouseState = MaterialSkin.MouseState.OUT;
            this.TestLenBox.Multiline = false;
            this.TestLenBox.Name = "TestLenBox";
            this.TestLenBox.Size = new System.Drawing.Size(233, 50);
            this.TestLenBox.TabIndex = 3;
            this.TestLenBox.Text = "32*1024";
            this.TestLenBox.TrailingIcon = null;
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.Location = new System.Drawing.Point(9, 110);
            this.materialLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(58, 19);
            this.materialLabel1.TabIndex = 2;
            this.materialLabel1.Text = "Address";
            // 
            // TestAddrBox
            // 
            this.TestAddrBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TestAddrBox.Depth = 0;
            this.TestAddrBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TestAddrBox.LeadingIcon = null;
            this.TestAddrBox.Location = new System.Drawing.Point(8, 25);
            this.TestAddrBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TestAddrBox.MaxLength = 50;
            this.TestAddrBox.MouseState = MaterialSkin.MouseState.OUT;
            this.TestAddrBox.Multiline = false;
            this.TestAddrBox.Name = "TestAddrBox";
            this.TestAddrBox.Size = new System.Drawing.Size(373, 50);
            this.TestAddrBox.TabIndex = 1;
            this.TestAddrBox.Text = "0x00200000-32*1024";
            this.TestAddrBox.TrailingIcon = null;
            // 
            // MRAM_TestBtn
            // 
            this.MRAM_TestBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MRAM_TestBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.MRAM_TestBtn.Depth = 0;
            this.MRAM_TestBtn.HighEmphasis = true;
            this.MRAM_TestBtn.Icon = null;
            this.MRAM_TestBtn.Location = new System.Drawing.Point(997, 16);
            this.MRAM_TestBtn.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.MRAM_TestBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.MRAM_TestBtn.Name = "MRAM_TestBtn";
            this.MRAM_TestBtn.Size = new System.Drawing.Size(67, 36);
            this.MRAM_TestBtn.TabIndex = 0;
            this.MRAM_TestBtn.Text = "Start";
            this.MRAM_TestBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.MRAM_TestBtn.UseAccentColor = false;
            this.MRAM_TestBtn.UseVisualStyleBackColor = true;
            this.MRAM_TestBtn.Click += new System.EventHandler(this.materialButton3_Click_1);
            // 
            // menuIconList
            // 
            this.menuIconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("menuIconList.ImageStream")));
            this.menuIconList.TransparentColor = System.Drawing.Color.Transparent;
            this.menuIconList.Images.SetKeyName(0, "round_assessment_white_24dp.png");
            this.menuIconList.Images.SetKeyName(1, "round_backup_white_24dp.png");
            this.menuIconList.Images.SetKeyName(2, "round_bluetooth_white_24dp.png");
            this.menuIconList.Images.SetKeyName(3, "round_bookmark_white_24dp.png");
            this.menuIconList.Images.SetKeyName(4, "round_build_white_24dp.png");
            this.menuIconList.Images.SetKeyName(5, "round_gps_fixed_white_24dp.png");
            this.menuIconList.Images.SetKeyName(6, "round_http_white_24dp.png");
            this.menuIconList.Images.SetKeyName(7, "round_report_problem_white_24dp.png");
            this.menuIconList.Images.SetKeyName(8, "round_swap_vert_white_24dp.png");
            // 
            // MainMenu
            // 
            this.MainMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainMenu.AutoSize = false;
            this.MainMenu.BackColor = System.Drawing.Color.Transparent;
            this.MainMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.MainMenu.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MainMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fILEToolStripMenuItem,
            this.eDITToolStripMenuItem,
            this.fUNToolStripMenuItem,
            this.tESTToolStripMenuItem,
            this.vIEWToolStripMenuItem,
            this.tOOLToolStripMenuItem1,
            this.hELPToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(56, 31);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.MainMenu.Size = new System.Drawing.Size(1083, 34);
            this.MainMenu.TabIndex = 20;
            this.MainMenu.Text = "menuStrip1";
            // 
            // fILEToolStripMenuItem
            // 
            this.fILEToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.openTestToolStripMenuItem,
            this.openSciptToolStripMenuItem,
            this.debugToolStripMenuItem,
            this.publicToolStripMenuItem,
            this.paToolStripMenuItem});
            this.fILEToolStripMenuItem.Name = "fILEToolStripMenuItem";
            this.fILEToolStripMenuItem.Size = new System.Drawing.Size(64, 30);
            this.fILEToolStripMenuItem.Text = "FILE";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(224, 32);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // openTestToolStripMenuItem
            // 
            this.openTestToolStripMenuItem.Name = "openTestToolStripMenuItem";
            this.openTestToolStripMenuItem.Size = new System.Drawing.Size(224, 32);
            this.openTestToolStripMenuItem.Text = "OpenTest";
            this.openTestToolStripMenuItem.Click += new System.EventHandler(this.openTestToolStripMenuItem_Click);
            // 
            // openSciptToolStripMenuItem
            // 
            this.openSciptToolStripMenuItem.Name = "openSciptToolStripMenuItem";
            this.openSciptToolStripMenuItem.Size = new System.Drawing.Size(224, 32);
            this.openSciptToolStripMenuItem.Text = "OpenScript";
            this.openSciptToolStripMenuItem.Click += new System.EventHandler(this.openSciptToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(224, 32);
            this.debugToolStripMenuItem.Text = "Debug";
            this.debugToolStripMenuItem.Click += new System.EventHandler(this.debugToolStripMenuItem_Click);
            // 
            // publicToolStripMenuItem
            // 
            this.publicToolStripMenuItem.Name = "publicToolStripMenuItem";
            this.publicToolStripMenuItem.Size = new System.Drawing.Size(224, 32);
            this.publicToolStripMenuItem.Text = "Public";
            this.publicToolStripMenuItem.Click += new System.EventHandler(this.publicToolStripMenuItem_Click);
            // 
            // paToolStripMenuItem
            // 
            this.paToolStripMenuItem.Name = "paToolStripMenuItem";
            this.paToolStripMenuItem.Size = new System.Drawing.Size(224, 32);
            this.paToolStripMenuItem.Text = "Pack";
            this.paToolStripMenuItem.Click += new System.EventHandler(this.paToolStripMenuItem_Click);
            // 
            // eDITToolStripMenuItem
            // 
            this.eDITToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.upDataParamToolStripMenuItem,
            this.clearProjectToolStripMenuItem});
            this.eDITToolStripMenuItem.Name = "eDITToolStripMenuItem";
            this.eDITToolStripMenuItem.Size = new System.Drawing.Size(69, 30);
            this.eDITToolStripMenuItem.Text = "EDIT";
            // 
            // upDataParamToolStripMenuItem
            // 
            this.upDataParamToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aLLToolStripMenuItem,
            this.defaultToolStripMenuItem,
            this.loadHistoryToolStripMenuItem,
            this.refreshMapToolStripMenuItem});
            this.upDataParamToolStripMenuItem.Name = "upDataParamToolStripMenuItem";
            this.upDataParamToolStripMenuItem.Size = new System.Drawing.Size(231, 32);
            this.upDataParamToolStripMenuItem.Text = "UpDataParam";
            // 
            // aLLToolStripMenuItem
            // 
            this.aLLToolStripMenuItem.Name = "aLLToolStripMenuItem";
            this.aLLToolStripMenuItem.Size = new System.Drawing.Size(213, 32);
            this.aLLToolStripMenuItem.Text = "ALL";
            this.aLLToolStripMenuItem.Click += new System.EventHandler(this.aLLToolStripMenuItem_Click);
            // 
            // defaultToolStripMenuItem
            // 
            this.defaultToolStripMenuItem.Name = "defaultToolStripMenuItem";
            this.defaultToolStripMenuItem.Size = new System.Drawing.Size(213, 32);
            this.defaultToolStripMenuItem.Text = "Default";
            this.defaultToolStripMenuItem.Click += new System.EventHandler(this.defaultToolStripMenuItem_Click);
            // 
            // loadHistoryToolStripMenuItem
            // 
            this.loadHistoryToolStripMenuItem.Name = "loadHistoryToolStripMenuItem";
            this.loadHistoryToolStripMenuItem.Size = new System.Drawing.Size(213, 32);
            this.loadHistoryToolStripMenuItem.Text = "LoadHistory";
            this.loadHistoryToolStripMenuItem.Click += new System.EventHandler(this.loadHistoryToolStripMenuItem_Click);
            // 
            // refreshMapToolStripMenuItem
            // 
            this.refreshMapToolStripMenuItem.Name = "refreshMapToolStripMenuItem";
            this.refreshMapToolStripMenuItem.Size = new System.Drawing.Size(213, 32);
            this.refreshMapToolStripMenuItem.Text = "RefreshMap";
            this.refreshMapToolStripMenuItem.Click += new System.EventHandler(this.refreshMapToolStripMenuItem_Click);
            // 
            // clearProjectToolStripMenuItem
            // 
            this.clearProjectToolStripMenuItem.Name = "clearProjectToolStripMenuItem";
            this.clearProjectToolStripMenuItem.Size = new System.Drawing.Size(231, 32);
            this.clearProjectToolStripMenuItem.Text = "ClearProject";
            this.clearProjectToolStripMenuItem.Click += new System.EventHandler(this.clearProjectToolStripMenuItem_Click);
            // 
            // fUNToolStripMenuItem
            // 
            this.fUNToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadScriptToolStripMenuItem,
            this.agilentToolStripMenuItem,
            this.monitorToolStripMenuItem,
            this.loadTestCaseToolStripMenuItem,
            this.dumpFunctonToolStripMenuItem});
            this.fUNToolStripMenuItem.Name = "fUNToolStripMenuItem";
            this.fUNToolStripMenuItem.Size = new System.Drawing.Size(130, 30);
            this.fUNToolStripMenuItem.Text = "FUNCTION";
            // 
            // loadScriptToolStripMenuItem
            // 
            this.loadScriptToolStripMenuItem.Name = "loadScriptToolStripMenuItem";
            this.loadScriptToolStripMenuItem.Size = new System.Drawing.Size(234, 32);
            this.loadScriptToolStripMenuItem.Text = "LoadScript";
            this.loadScriptToolStripMenuItem.Click += new System.EventHandler(this.loadScriptToolStripMenuItem_Click);
            // 
            // agilentToolStripMenuItem
            // 
            this.agilentToolStripMenuItem.Name = "agilentToolStripMenuItem";
            this.agilentToolStripMenuItem.Size = new System.Drawing.Size(234, 32);
            this.agilentToolStripMenuItem.Text = "Agilent";
            // 
            // monitorToolStripMenuItem
            // 
            this.monitorToolStripMenuItem.Name = "monitorToolStripMenuItem";
            this.monitorToolStripMenuItem.Size = new System.Drawing.Size(234, 32);
            this.monitorToolStripMenuItem.Text = "Monitor";
            this.monitorToolStripMenuItem.Click += new System.EventHandler(this.monitorToolStripMenuItem_Click);
            // 
            // loadTestCaseToolStripMenuItem
            // 
            this.loadTestCaseToolStripMenuItem.Name = "loadTestCaseToolStripMenuItem";
            this.loadTestCaseToolStripMenuItem.Size = new System.Drawing.Size(234, 32);
            this.loadTestCaseToolStripMenuItem.Text = "LoadTestCase";
            this.loadTestCaseToolStripMenuItem.Click += new System.EventHandler(this.loadTestCaseToolStripMenuItem_Click);
            // 
            // dumpFunctonToolStripMenuItem
            // 
            this.dumpFunctonToolStripMenuItem.Name = "dumpFunctonToolStripMenuItem";
            this.dumpFunctonToolStripMenuItem.Size = new System.Drawing.Size(234, 32);
            this.dumpFunctonToolStripMenuItem.Text = "DumpFuncton";
            this.dumpFunctonToolStripMenuItem.Click += new System.EventHandler(this.dumpFunctonToolStripMenuItem_Click);
            // 
            // tESTToolStripMenuItem
            // 
            this.tESTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.toolStripMenuItem1,
            this.resetToolStripMenuItem,
            this.rTTEnableToolStripMenuItem,
            this.cPUToolStripMenuItem,
            this.deviceToolStripMenuItem,
            this.downloadToolStripMenuItem,
            this.switchToolStripMenuItem});
            this.tESTToolStripMenuItem.Name = "tESTToolStripMenuItem";
            this.tESTToolStripMenuItem.Size = new System.Drawing.Size(79, 30);
            this.tESTToolStripMenuItem.Text = "JLINK";
            this.tESTToolStripMenuItem.Click += new System.EventHandler(this.tESTToolStripMenuItem_Click);
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(202, 32);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(202, 32);
            this.toolStripMenuItem1.Text = "Disconnect";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(202, 32);
            this.resetToolStripMenuItem.Text = "TReset";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // rTTEnableToolStripMenuItem
            // 
            this.rTTEnableToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem1,
            this.generateIDTABLEToolStripMenuItem});
            this.rTTEnableToolStripMenuItem.Name = "rTTEnableToolStripMenuItem";
            this.rTTEnableToolStripMenuItem.Size = new System.Drawing.Size(202, 32);
            this.rTTEnableToolStripMenuItem.Text = "Log";
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(326, 32);
            this.openToolStripMenuItem1.Text = "Open";
            this.openToolStripMenuItem1.Click += new System.EventHandler(this.openToolStripMenuItem1_Click);
            // 
            // generateIDTABLEToolStripMenuItem
            // 
            this.generateIDTABLEToolStripMenuItem.Name = "generateIDTABLEToolStripMenuItem";
            this.generateIDTABLEToolStripMenuItem.Size = new System.Drawing.Size(326, 32);
            this.generateIDTABLEToolStripMenuItem.Text = "Generate ID_TABLE.data";
            this.generateIDTABLEToolStripMenuItem.Click += new System.EventHandler(this.generateIDTABLEToolStripMenuItem_Click);
            // 
            // cPUToolStripMenuItem
            // 
            this.cPUToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetToolStripMenuItem2,
            this.runToolStripMenuItem,
            this.haltToolStripMenuItem,
            this.statusToolStripMenuItem,
            this.regToolStripMenuItem,
            this.stackAnalysToolStripMenuItem});
            this.cPUToolStripMenuItem.Name = "cPUToolStripMenuItem";
            this.cPUToolStripMenuItem.Size = new System.Drawing.Size(202, 32);
            this.cPUToolStripMenuItem.Text = "CPU";
            // 
            // resetToolStripMenuItem2
            // 
            this.resetToolStripMenuItem2.Name = "resetToolStripMenuItem2";
            this.resetToolStripMenuItem2.Size = new System.Drawing.Size(225, 32);
            this.resetToolStripMenuItem2.Text = "Reset";
            this.resetToolStripMenuItem2.Click += new System.EventHandler(this.resetToolStripMenuItem2_Click);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(225, 32);
            this.runToolStripMenuItem.Text = "Run";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
            // 
            // haltToolStripMenuItem
            // 
            this.haltToolStripMenuItem.Name = "haltToolStripMenuItem";
            this.haltToolStripMenuItem.Size = new System.Drawing.Size(225, 32);
            this.haltToolStripMenuItem.Text = "Halt";
            this.haltToolStripMenuItem.Click += new System.EventHandler(this.haltToolStripMenuItem_Click);
            // 
            // statusToolStripMenuItem
            // 
            this.statusToolStripMenuItem.Name = "statusToolStripMenuItem";
            this.statusToolStripMenuItem.Size = new System.Drawing.Size(225, 32);
            this.statusToolStripMenuItem.Text = "Status";
            this.statusToolStripMenuItem.Click += new System.EventHandler(this.statusToolStripMenuItem_Click);
            // 
            // regToolStripMenuItem
            // 
            this.regToolStripMenuItem.Name = "regToolStripMenuItem";
            this.regToolStripMenuItem.Size = new System.Drawing.Size(225, 32);
            this.regToolStripMenuItem.Text = "Reg";
            this.regToolStripMenuItem.Click += new System.EventHandler(this.regToolStripMenuItem_Click);
            // 
            // stackAnalysToolStripMenuItem
            // 
            this.stackAnalysToolStripMenuItem.Name = "stackAnalysToolStripMenuItem";
            this.stackAnalysToolStripMenuItem.Size = new System.Drawing.Size(225, 32);
            this.stackAnalysToolStripMenuItem.Text = "StackAnalysis";
            this.stackAnalysToolStripMenuItem.Click += new System.EventHandler(this.stackAnalysToolStripMenuItem_Click);
            // 
            // deviceToolStripMenuItem
            // 
            this.deviceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sNToolStripMenuItem,
            this.pINToolStripMenuItem});
            this.deviceToolStripMenuItem.Name = "deviceToolStripMenuItem";
            this.deviceToolStripMenuItem.Size = new System.Drawing.Size(202, 32);
            this.deviceToolStripMenuItem.Text = "Device";
            // 
            // sNToolStripMenuItem
            // 
            this.sNToolStripMenuItem.Name = "sNToolStripMenuItem";
            this.sNToolStripMenuItem.Size = new System.Drawing.Size(132, 32);
            this.sNToolStripMenuItem.Text = "SN";
            this.sNToolStripMenuItem.Click += new System.EventHandler(this.sNToolStripMenuItem_Click);
            // 
            // pINToolStripMenuItem
            // 
            this.pINToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sETToolStripMenuItem,
            this.rESETToolStripMenuItem1,
            this.statusToolStripMenuItem1});
            this.pINToolStripMenuItem.Name = "pINToolStripMenuItem";
            this.pINToolStripMenuItem.Size = new System.Drawing.Size(132, 32);
            this.pINToolStripMenuItem.Text = "PIN";
            // 
            // sETToolStripMenuItem
            // 
            this.sETToolStripMenuItem.Name = "sETToolStripMenuItem";
            this.sETToolStripMenuItem.Size = new System.Drawing.Size(156, 32);
            this.sETToolStripMenuItem.Text = "SET";
            // 
            // rESETToolStripMenuItem1
            // 
            this.rESETToolStripMenuItem1.Name = "rESETToolStripMenuItem1";
            this.rESETToolStripMenuItem1.Size = new System.Drawing.Size(156, 32);
            this.rESETToolStripMenuItem1.Text = "RESET";
            // 
            // statusToolStripMenuItem1
            // 
            this.statusToolStripMenuItem1.Name = "statusToolStripMenuItem1";
            this.statusToolStripMenuItem1.Size = new System.Drawing.Size(156, 32);
            this.statusToolStripMenuItem1.Text = "State";
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ihexToolStripMenuItem,
            this.m7FlashToolStripMenuItem,
            this.apolloBootLoaderToolStripMenuItem});
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(202, 32);
            this.downloadToolStripMenuItem.Text = "Download";
            // 
            // ihexToolStripMenuItem
            // 
            this.ihexToolStripMenuItem.Name = "ihexToolStripMenuItem";
            this.ihexToolStripMenuItem.Size = new System.Drawing.Size(300, 32);
            this.ihexToolStripMenuItem.Text = "Apollo->Run In RAM";
            this.ihexToolStripMenuItem.Click += new System.EventHandler(this.ihexToolStripMenuItem_Click);
            // 
            // m7FlashToolStripMenuItem
            // 
            this.m7FlashToolStripMenuItem.Name = "m7FlashToolStripMenuItem";
            this.m7FlashToolStripMenuItem.Size = new System.Drawing.Size(300, 32);
            this.m7FlashToolStripMenuItem.Text = "Apollo->Run In Flash";
            this.m7FlashToolStripMenuItem.Click += new System.EventHandler(this.m7FlashToolStripMenuItem_Click);
            // 
            // apolloBootLoaderToolStripMenuItem
            // 
            this.apolloBootLoaderToolStripMenuItem.Name = "apolloBootLoaderToolStripMenuItem";
            this.apolloBootLoaderToolStripMenuItem.Size = new System.Drawing.Size(300, 32);
            this.apolloBootLoaderToolStripMenuItem.Text = "Apollo->BootLoader";
            this.apolloBootLoaderToolStripMenuItem.Click += new System.EventHandler(this.apolloBootLoaderToolStripMenuItem_Click);
            // 
            // switchToolStripMenuItem
            // 
            this.switchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.switchProtocolToolStripMenuItem});
            this.switchToolStripMenuItem.Name = "switchToolStripMenuItem";
            this.switchToolStripMenuItem.Size = new System.Drawing.Size(202, 32);
            this.switchToolStripMenuItem.Text = "Others";
            // 
            // switchProtocolToolStripMenuItem
            // 
            this.switchProtocolToolStripMenuItem.Name = "switchProtocolToolStripMenuItem";
            this.switchProtocolToolStripMenuItem.Size = new System.Drawing.Size(213, 32);
            this.switchProtocolToolStripMenuItem.Text = "Switch2Jlink";
            this.switchProtocolToolStripMenuItem.Click += new System.EventHandler(this.switch2jlinkToolStripMenuItem_Click);
            // 
            // vIEWToolStripMenuItem
            // 
            this.vIEWToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem1,
            this.openFileToolStripMenuItem,
            this.themeToolStripMenuItem});
            this.vIEWToolStripMenuItem.Name = "vIEWToolStripMenuItem";
            this.vIEWToolStripMenuItem.Size = new System.Drawing.Size(77, 30);
            this.vIEWToolStripMenuItem.Text = "VIEW";
            // 
            // testToolStripMenuItem1
            // 
            this.testToolStripMenuItem1.Name = "testToolStripMenuItem1";
            this.testToolStripMenuItem1.Size = new System.Drawing.Size(182, 32);
            this.testToolStripMenuItem1.Text = "ShowInf";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ArmToolStripMenuItem1,
            this.pathInfToolStripMenuItem});
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(182, 32);
            this.openFileToolStripMenuItem.Text = "OpenFile";
            // 
            // ArmToolStripMenuItem1
            // 
            this.ArmToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mAPToolStripMenuItem1,
            this.directoryToolStripMenuItem});
            this.ArmToolStripMenuItem1.Name = "ArmToolStripMenuItem1";
            this.ArmToolStripMenuItem1.Size = new System.Drawing.Size(165, 32);
            this.ArmToolStripMenuItem1.Text = "ARM";
            // 
            // mAPToolStripMenuItem1
            // 
            this.mAPToolStripMenuItem1.Name = "mAPToolStripMenuItem1";
            this.mAPToolStripMenuItem1.Size = new System.Drawing.Size(186, 32);
            this.mAPToolStripMenuItem1.Text = "MAP";
            this.mAPToolStripMenuItem1.Click += new System.EventHandler(this.mAPToolStripMenuItem1_Click);
            // 
            // directoryToolStripMenuItem
            // 
            this.directoryToolStripMenuItem.Name = "directoryToolStripMenuItem";
            this.directoryToolStripMenuItem.Size = new System.Drawing.Size(186, 32);
            this.directoryToolStripMenuItem.Text = "Directory";
            this.directoryToolStripMenuItem.Click += new System.EventHandler(this.directoryToolStripMenuItem_Click);
            // 
            // pathInfToolStripMenuItem
            // 
            this.pathInfToolStripMenuItem.Name = "pathInfToolStripMenuItem";
            this.pathInfToolStripMenuItem.Size = new System.Drawing.Size(165, 32);
            this.pathInfToolStripMenuItem.Text = "PathInf";
            this.pathInfToolStripMenuItem.Click += new System.EventHandler(this.pathInfToolStripMenuItem_Click);
            // 
            // themeToolStripMenuItem
            // 
            this.themeToolStripMenuItem.Name = "themeToolStripMenuItem";
            this.themeToolStripMenuItem.Size = new System.Drawing.Size(182, 32);
            this.themeToolStripMenuItem.Text = "Theme";
            this.themeToolStripMenuItem.Click += new System.EventHandler(this.themeToolStripMenuItem_Click);
            // 
            // tOOLToolStripMenuItem1
            // 
            this.tOOLToolStripMenuItem1.Name = "tOOLToolStripMenuItem1";
            this.tOOLToolStripMenuItem1.Size = new System.Drawing.Size(79, 30);
            this.tOOLToolStripMenuItem1.Text = "TOOL";
            // 
            // hELPToolStripMenuItem
            // 
            this.hELPToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.resetAllToolStripMenuItem,
            this.helpDocumentToolStripMenuItem});
            this.hELPToolStripMenuItem.Name = "hELPToolStripMenuItem";
            this.hELPToolStripMenuItem.Size = new System.Drawing.Size(74, 30);
            this.hELPToolStripMenuItem.Text = "HELP";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(247, 32);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // resetAllToolStripMenuItem
            // 
            this.resetAllToolStripMenuItem.Name = "resetAllToolStripMenuItem";
            this.resetAllToolStripMenuItem.Size = new System.Drawing.Size(247, 32);
            this.resetAllToolStripMenuItem.Text = "ResetAll";
            this.resetAllToolStripMenuItem.Click += new System.EventHandler(this.resetAllToolStripMenuItem_Click);
            // 
            // helpDocumentToolStripMenuItem
            // 
            this.helpDocumentToolStripMenuItem.Name = "helpDocumentToolStripMenuItem";
            this.helpDocumentToolStripMenuItem.Size = new System.Drawing.Size(247, 32);
            this.helpDocumentToolStripMenuItem.Text = "Help Document";
            this.helpDocumentToolStripMenuItem.Click += new System.EventHandler(this.helpDocumentToolStripMenuItem_Click);
            // 
            // VerLab
            // 
            this.VerLab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.VerLab.AutoSize = true;
            this.VerLab.BackColor = System.Drawing.Color.Transparent;
            this.VerLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VerLab.Location = new System.Drawing.Point(1327, 36);
            this.VerLab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.VerLab.Name = "VerLab";
            this.VerLab.Size = new System.Drawing.Size(53, 25);
            this.VerLab.TabIndex = 22;
            this.VerLab.Text = "V1.0";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.CrcCB);
            this.groupBox6.Controls.Add(this.EnhanceCB);
            this.groupBox6.Controls.Add(this.RegularCB);
            this.groupBox6.Controls.Add(this.materialLabel10);
            this.groupBox6.Controls.Add(this.RepeatBox);
            this.groupBox6.Controls.Add(this.materialLabel8);
            this.groupBox6.Controls.Add(this.TestTimeBox);
            this.groupBox6.Controls.Add(this.materialLabel9);
            this.groupBox6.Controls.Add(this.BitNumBox);
            this.groupBox6.Controls.Add(this.StartBtn);
            this.groupBox6.Location = new System.Drawing.Point(43, 546);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox6.Size = new System.Drawing.Size(1124, 144);
            this.groupBox6.TabIndex = 8;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "CRC VS SUM ";
            this.groupBox6.Enter += new System.EventHandler(this.groupBox6_Enter);
            // 
            // materialLabel8
            // 
            this.materialLabel8.AutoSize = true;
            this.materialLabel8.Depth = 0;
            this.materialLabel8.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel8.Location = new System.Drawing.Point(757, 110);
            this.materialLabel8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.materialLabel8.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel8.Name = "materialLabel8";
            this.materialLabel8.Size = new System.Drawing.Size(92, 19);
            this.materialLabel8.TabIndex = 6;
            this.materialLabel8.Text = "Test Number";
            // 
            // TestTimeBox
            // 
            this.TestTimeBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TestTimeBox.Depth = 0;
            this.TestTimeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TestTimeBox.LeadingIcon = null;
            this.TestTimeBox.Location = new System.Drawing.Point(756, 25);
            this.TestTimeBox.Margin = new System.Windows.Forms.Padding(4);
            this.TestTimeBox.MaxLength = 50;
            this.TestTimeBox.MouseState = MaterialSkin.MouseState.OUT;
            this.TestTimeBox.Multiline = false;
            this.TestTimeBox.Name = "TestTimeBox";
            this.TestTimeBox.Size = new System.Drawing.Size(211, 50);
            this.TestTimeBox.TabIndex = 5;
            this.TestTimeBox.Text = "10*10000";
            this.TestTimeBox.TrailingIcon = null;
            // 
            // materialLabel9
            // 
            this.materialLabel9.AutoSize = true;
            this.materialLabel9.Depth = 0;
            this.materialLabel9.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel9.Location = new System.Drawing.Point(511, 110);
            this.materialLabel9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.materialLabel9.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel9.Name = "materialLabel9";
            this.materialLabel9.Size = new System.Drawing.Size(54, 19);
            this.materialLabel9.TabIndex = 4;
            this.materialLabel9.Text = "BitNum";
            // 
            // BitNumBox
            // 
            this.BitNumBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BitNumBox.Depth = 0;
            this.BitNumBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.BitNumBox.LeadingIcon = null;
            this.BitNumBox.Location = new System.Drawing.Point(515, 25);
            this.BitNumBox.Margin = new System.Windows.Forms.Padding(4);
            this.BitNumBox.MaxLength = 50;
            this.BitNumBox.MouseState = MaterialSkin.MouseState.OUT;
            this.BitNumBox.Multiline = false;
            this.BitNumBox.Name = "BitNumBox";
            this.BitNumBox.Size = new System.Drawing.Size(233, 50);
            this.BitNumBox.TabIndex = 3;
            this.BitNumBox.Text = "2";
            this.BitNumBox.TrailingIcon = null;
            // 
            // StartBtn
            // 
            this.StartBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.StartBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.StartBtn.Depth = 0;
            this.StartBtn.HighEmphasis = true;
            this.StartBtn.Icon = null;
            this.StartBtn.Location = new System.Drawing.Point(997, 16);
            this.StartBtn.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.StartBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(67, 36);
            this.StartBtn.TabIndex = 0;
            this.StartBtn.Text = "Start";
            this.StartBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.StartBtn.UseAccentColor = false;
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.materialButton7_Click);
            // 
            // RepeatBox
            // 
            this.RepeatBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RepeatBox.Depth = 0;
            this.RepeatBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.RepeatBox.LeadingIcon = null;
            this.RepeatBox.Location = new System.Drawing.Point(256, 26);
            this.RepeatBox.Margin = new System.Windows.Forms.Padding(4);
            this.RepeatBox.MaxLength = 50;
            this.RepeatBox.MouseState = MaterialSkin.MouseState.OUT;
            this.RepeatBox.Multiline = false;
            this.RepeatBox.Name = "RepeatBox";
            this.RepeatBox.Size = new System.Drawing.Size(233, 50);
            this.RepeatBox.TabIndex = 7;
            this.RepeatBox.Text = "1";
            this.RepeatBox.TrailingIcon = null;
            // 
            // RegularCB
            // 
            this.RegularCB.AutoSize = true;
            this.RegularCB.Checked = true;
            this.RegularCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RegularCB.Depth = 0;
            this.RegularCB.Location = new System.Drawing.Point(9, 29);
            this.RegularCB.Margin = new System.Windows.Forms.Padding(0);
            this.RegularCB.MouseLocation = new System.Drawing.Point(-1, -1);
            this.RegularCB.MouseState = MaterialSkin.MouseState.HOVER;
            this.RegularCB.Name = "RegularCB";
            this.RegularCB.Ripple = true;
            this.RegularCB.Size = new System.Drawing.Size(127, 37);
            this.RegularCB.TabIndex = 94;
            this.RegularCB.Text = "SUM Regular";
            this.RegularCB.UseVisualStyleBackColor = true;
            // 
            // materialLabel10
            // 
            this.materialLabel10.AutoSize = true;
            this.materialLabel10.Depth = 0;
            this.materialLabel10.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel10.Location = new System.Drawing.Point(265, 110);
            this.materialLabel10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.materialLabel10.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel10.Name = "materialLabel10";
            this.materialLabel10.Size = new System.Drawing.Size(50, 19);
            this.materialLabel10.TabIndex = 8;
            this.materialLabel10.Text = "Repeat";
            // 
            // EnhanceCB
            // 
            this.EnhanceCB.AutoSize = true;
            this.EnhanceCB.Checked = true;
            this.EnhanceCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EnhanceCB.Depth = 0;
            this.EnhanceCB.Location = new System.Drawing.Point(9, 66);
            this.EnhanceCB.Margin = new System.Windows.Forms.Padding(0);
            this.EnhanceCB.MouseLocation = new System.Drawing.Point(-1, -1);
            this.EnhanceCB.MouseState = MaterialSkin.MouseState.HOVER;
            this.EnhanceCB.Name = "EnhanceCB";
            this.EnhanceCB.Ripple = true;
            this.EnhanceCB.Size = new System.Drawing.Size(133, 37);
            this.EnhanceCB.TabIndex = 95;
            this.EnhanceCB.Text = "Sum Enhance";
            this.EnhanceCB.UseVisualStyleBackColor = true;
            // 
            // CrcCB
            // 
            this.CrcCB.AutoSize = true;
            this.CrcCB.Checked = true;
            this.CrcCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CrcCB.Depth = 0;
            this.CrcCB.Location = new System.Drawing.Point(9, 103);
            this.CrcCB.Margin = new System.Windows.Forms.Padding(0);
            this.CrcCB.MouseLocation = new System.Drawing.Point(-1, -1);
            this.CrcCB.MouseState = MaterialSkin.MouseState.HOVER;
            this.CrcCB.Name = "CrcCB";
            this.CrcCB.Ripple = true;
            this.CrcCB.Size = new System.Drawing.Size(83, 37);
            this.CrcCB.TabIndex = 96;
            this.CrcCB.Text = "CRC32";
            this.CrcCB.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1404, 988);
            this.Controls.Add(this.VerLab);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.TempPage);
            this.DrawerShowIconsWhenHidden = true;
            this.DrawerTabControl = this.TempPage;
            this.FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.ActionBar_30;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(400, 231);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(0, 68, 4, 4);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "\\";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.PeripheralPage.ResumeLayout(false);
            this.PeripheralPage.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.MemoryPage.ResumeLayout(false);
            this.MemoryPage.PerformLayout();
            this.materialExpansionPanel5.ResumeLayout(false);
            this.materialExpansionPanel5.PerformLayout();
            this.ConfigPage.ResumeLayout(false);
            this.materialExpansionPanel3.ResumeLayout(false);
            this.materialExpansionPanel3.PerformLayout();
            this.TempPage.ResumeLayout(false);
            this.HomePage.ResumeLayout(false);
            this.HomePage.PerformLayout();
            this.MainLogMenu.ResumeLayout(false);
            this.ScriptPage.ResumeLayout(false);
            this.ScriptPage.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TabPage PeripheralPage;
        private TabPage MemoryPage;
        private MaterialButton MaterialButton2;
        private MaterialProgressBar ProBar;
        private TabPage ConfigPage;
        private MaterialTabControl TempPage;
        private TabPage tabPage1;
        private TabPage HomePage;
        private MenuStrip MainMenu;
        private ToolStripMenuItem fILEToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem debugToolStripMenuItem;
        private ToolStripMenuItem publicToolStripMenuItem;
        private ToolStripMenuItem eDITToolStripMenuItem;
        private ToolStripMenuItem upDataParamToolStripMenuItem;
        private ToolStripMenuItem aLLToolStripMenuItem;
        private ToolStripMenuItem defaultToolStripMenuItem;
        private ToolStripMenuItem loadHistoryToolStripMenuItem;
        private ToolStripMenuItem clearProjectToolStripMenuItem;
        private ToolStripMenuItem fUNToolStripMenuItem;
        private ToolStripMenuItem loadScriptToolStripMenuItem;
        private ToolStripMenuItem agilentToolStripMenuItem;
        private ToolStripMenuItem monitorToolStripMenuItem;
        private ToolStripMenuItem tESTToolStripMenuItem;
        private ToolStripMenuItem connectToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem resetToolStripMenuItem;
        private ToolStripMenuItem rTTEnableToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem1;
        private ToolStripMenuItem generateIDTABLEToolStripMenuItem;
        private ToolStripMenuItem cPUToolStripMenuItem;
        private ToolStripMenuItem runToolStripMenuItem;
        private ToolStripMenuItem haltToolStripMenuItem;
        private ToolStripMenuItem statusToolStripMenuItem;
        private ToolStripMenuItem stackAnalysToolStripMenuItem;
        private ToolStripMenuItem deviceToolStripMenuItem;
        private ToolStripMenuItem sNToolStripMenuItem;
        private ToolStripMenuItem pINToolStripMenuItem;
        private ToolStripMenuItem sETToolStripMenuItem;
        private ToolStripMenuItem rESETToolStripMenuItem1;
        private ToolStripMenuItem statusToolStripMenuItem1;
        private ToolStripMenuItem downloadToolStripMenuItem;
        private ToolStripMenuItem ihexToolStripMenuItem;
        private ToolStripMenuItem m7FlashToolStripMenuItem;
        private ToolStripMenuItem vIEWToolStripMenuItem;
        private ToolStripMenuItem testToolStripMenuItem1;
        private ToolStripMenuItem openFileToolStripMenuItem;
        private ToolStripMenuItem ArmToolStripMenuItem1;
        private ToolStripMenuItem mAPToolStripMenuItem1;
        private ToolStripMenuItem directoryToolStripMenuItem;
        private ToolStripMenuItem pathInfToolStripMenuItem;
        private ToolStripMenuItem themeToolStripMenuItem;
        private ToolStripMenuItem tOOLToolStripMenuItem1;
        private ToolStripMenuItem hELPToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem resetAllToolStripMenuItem;
        private ToolStripMenuItem helpDocumentToolStripMenuItem;
        private MaterialSwitch M7_CPU0_CB;
        private MaterialSwitch IdSortCB;
        private MaterialSwitch materialCheckBox1;
        private MaterialSwitch checkBox2;
        private MaterialSwitch materialCheckBox3;
        private MaterialSwitch checkBox1;
        private MaterialButton IdRangeBtn;
        private MaterialExpansionPanel materialExpansionPanel3;
        private MaterialSwitch M7_CPU1_CB;
        private Label VerLab;
        private ImageList menuIconList;
        private ContextMenuStrip MainLogMenu;
        private ToolStripMenuItem clearToolStripMenuItem;
        private Label label1;
        private MaterialButton materialButton10;
        private MaterialTextBox ImageFileBox;
        private MaterialButton materialButton11;
        private TextBox IdRangeBox;
        private TextBox textBox2;
        private TextBox textBox1;
        private ComboBox JlinkSpeedCBox;
        private MaterialExpansionPanel materialExpansionPanel5;
        private MaterialDivider materialDivider1;
        private TextBox textBox3;
        private MaterialMultiLineTextBox MsgBox;
        private ToolStripMenuItem resetToolStripMenuItem2;
        private ToolStripMenuItem loadTestCaseToolStripMenuItem;
        private MaterialComboBox MCU_Name;
        private ToolStripMenuItem switchToolStripMenuItem;
        private ToolStripMenuItem switchProtocolToolStripMenuItem;
        private ToolStripMenuItem regToolStripMenuItem;
        private MaterialButton materialButton1;
        private GroupBox groupBox1;
        private MaterialLabel materialLabel1;
        private MaterialTextBox TestAddrBox;
        private MaterialButton MRAM_TestBtn;
        private MaterialLabel materialLabel3;
        private MaterialTextBox TestNoBox;
        private MaterialLabel materialLabel2;
        private MaterialTextBox TestLenBox;
        private CheckBox DebugCB;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem paToolStripMenuItem;
        private ToolStripMenuItem openTestToolStripMenuItem;
        private GroupBox groupBox4;
        private CheckBox checkBox3;
        private MaterialLabel materialLabel4;
        private MaterialTextBox materialTextBox1;
        private MaterialLabel materialLabel5;
        private MaterialTextBox materialTextBox2;
        private MaterialLabel materialLabel6;
        private MaterialTextBox materialTextBox3;
        private MaterialButton materialButton5;
        private MaterialLabel materialLabel7;
        private MaterialTextBox materialTextBox4;
        private ToolStripMenuItem refreshMapToolStripMenuItem;
        private MaterialTextBox materialTextBox5;
        private ToolStripMenuItem apolloBootLoaderToolStripMenuItem;
        private GroupBox groupBox2;
        private MaterialTextBox RegPathBox;
        private MaterialCheckbox LoadDefaultCB;
        private MaterialButton materialButton4;
        private MaterialCheckbox ResetCB;
        private MaterialButton LoadTableBtn;
        private MaterialCheckbox CrosstalkCB;
        private MaterialButton LoadRegFileBtn;
        private MaterialCheckbox AttrbuteCB;
        private MaterialButton materialButton3;
        private MaterialCheckbox RW_CB;
        private MaterialCheckbox DefaultCB;
        private GroupBox groupBox3;
        private MaterialComboBox xSpiFreqbox;
        private MaterialCheckbox SwitchProtocolCB;
        private MaterialCheckbox AddrRadio;
        private MaterialCheckbox DtrRadio;
        private MaterialComboBox xSpiCombox;
        private MaterialCheckbox DqsRadio;
        private MaterialTextBox RegValueBox;
        private MaterialTextBox RegLengthBox;
        private MaterialTextBox RegAddrBox;
        private MaterialComboBox RegCombox;
        private MaterialButton materialButton6;
        private ToolStripMenuItem openSciptToolStripMenuItem;
        private TabPage ScriptPage;
        private GroupBox groupBox5;
        private MaterialComboBox ScriptCBox;
        private MaterialButton RunBtn;
        private MaterialMultiLineTextBox MsgBox1;
        private MaterialButton EditBtn;
        private MaterialButton NewBtn;
        private MaterialButton DelBtn;
        private MaterialButton OpenBtn;
        private ToolStripMenuItem dumpFunctonToolStripMenuItem;
        private GroupBox groupBox6;
        private MaterialLabel materialLabel8;
        private MaterialTextBox TestTimeBox;
        private MaterialLabel materialLabel9;
        private MaterialTextBox BitNumBox;
        private MaterialButton StartBtn;
        private MaterialTextBox RepeatBox;
        private MaterialCheckbox CrcCB;
        private MaterialCheckbox EnhanceCB;
        private MaterialCheckbox RegularCB;
        private MaterialLabel materialLabel10;
    }
}
