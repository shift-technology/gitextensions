using System.Windows.Forms;

namespace ShiftFlow
{
    partial class ShiftFlowForm
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
            this.pnlBasedOn = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.cbBaseBranch = new System.Windows.Forms.ComboBox();
            this.txtBranchName = new System.Windows.Forms.TextBox();
            this.btnCreateBranch = new System.Windows.Forms.Button();
            this.cbBranches = new System.Windows.Forms.ComboBox();
            this.btnFinish = new System.Windows.Forms.Button();
            this.gbManage = new System.Windows.Forms.GroupBox();
            this.pnlManageBranch = new System.Windows.Forms.Panel();
            this.pnlPull = new System.Windows.Forms.Panel();
            this.cbRemote = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnPull = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPrefixManage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPublish = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cbManageType = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lblPrefixName = new System.Windows.Forms.Label();
            this.gbStart = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblDebug = new System.Windows.Forms.Label();
            this.lnkShiftFlow = new System.Windows.Forms.LinkLabel();
            this.pbResultCommand = new System.Windows.Forms.PictureBox();
            this.ttShiftFlow = new System.Windows.Forms.ToolTip(this.components);
            this.ttCommandResult = new System.Windows.Forms.ToolTip(this.components);
            this.ttDebug = new System.Windows.Forms.ToolTip(this.components);
            this.lblCaptionHead = new System.Windows.Forms.Label();
            this.lblHead = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.GroupBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.lblRunCommand = new System.Windows.Forms.Label();
            this.pnlBasedOn.SuspendLayout();
            this.gbManage.SuspendLayout();
            this.pnlManageBranch.SuspendLayout();
            this.pnlPull.SuspendLayout();
            this.panel4.SuspendLayout();
            this.gbStart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResultCommand)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBasedOn
            // 
            this.pnlBasedOn.Controls.Add(this.label3);
            this.pnlBasedOn.Controls.Add(this.cbBaseBranch);
            this.pnlBasedOn.Location = new System.Drawing.Point(125, 108);
            this.pnlBasedOn.Name = "pnlBasedOn";
            this.pnlBasedOn.Size = new System.Drawing.Size(581, 37);
            this.pnlBasedOn.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Target branch";
            // 
            // cbBaseBranch
            // 
            this.cbBaseBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBaseBranch.FormattingEnabled = true;
            this.cbBaseBranch.Location = new System.Drawing.Point(123, 8);
            this.cbBaseBranch.Name = "cbBaseBranch";
            this.cbBaseBranch.Size = new System.Drawing.Size(422, 24);
            this.cbBaseBranch.TabIndex = 3;
            // 
            // txtBranchName
            // 
            this.txtBranchName.Location = new System.Drawing.Point(247, 70);
            this.txtBranchName.Margin = new System.Windows.Forms.Padding(4);
            this.txtBranchName.Name = "txtBranchName";
            this.txtBranchName.Size = new System.Drawing.Size(422, 22);
            this.txtBranchName.TabIndex = 2;
            // 
            // btnCreateBranch
            // 
            this.btnCreateBranch.Location = new System.Drawing.Point(677, 67);
            this.btnCreateBranch.Margin = new System.Windows.Forms.Padding(4);
            this.btnCreateBranch.Name = "btnCreateBranch";
            this.btnCreateBranch.Size = new System.Drawing.Size(101, 29);
            this.btnCreateBranch.TabIndex = 0;
            this.btnCreateBranch.Text = "Start!";
            this.btnCreateBranch.UseVisualStyleBackColor = true;
            this.btnCreateBranch.Click += new System.EventHandler(this.btnStartBranch_Click);
            // 
            // cbBranches
            // 
            this.cbBranches.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranches.FormattingEnabled = true;
            this.cbBranches.Location = new System.Drawing.Point(235, 10);
            this.cbBranches.Margin = new System.Windows.Forms.Padding(4);
            this.cbBranches.Name = "cbBranches";
            this.cbBranches.Size = new System.Drawing.Size(512, 24);
            this.cbBranches.TabIndex = 3;
            this.cbBranches.SelectedValueChanged += new System.EventHandler(this.cbBranches_SelectedValueChanged);
            // 
            // btnFinish
            // 
            this.btnFinish.Location = new System.Drawing.Point(414, 56);
            this.btnFinish.Margin = new System.Windows.Forms.Padding(4);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(74, 29);
            this.btnFinish.TabIndex = 0;
            this.btnFinish.Text = "Merge";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // gbManage
            // 
            this.gbManage.Controls.Add(this.pnlManageBranch);
            this.gbManage.Controls.Add(this.cbManageType);
            this.gbManage.Location = new System.Drawing.Point(16, 211);
            this.gbManage.Margin = new System.Windows.Forms.Padding(4);
            this.gbManage.Name = "gbManage";
            this.gbManage.Padding = new System.Windows.Forms.Padding(4);
            this.gbManage.Size = new System.Drawing.Size(785, 252);
            this.gbManage.TabIndex = 6;
            this.gbManage.TabStop = false;
            this.gbManage.Text = "Manage existing branches:";
            // 
            // pnlManageBranch
            // 
            this.pnlManageBranch.Controls.Add(this.pnlPull);
            this.pnlManageBranch.Controls.Add(this.panel1);
            this.pnlManageBranch.Controls.Add(this.lblPrefixManage);
            this.pnlManageBranch.Controls.Add(this.label1);
            this.pnlManageBranch.Controls.Add(this.cbBranches);
            this.pnlManageBranch.Controls.Add(this.btnPublish);
            this.pnlManageBranch.Controls.Add(this.panel4);
            this.pnlManageBranch.Location = new System.Drawing.Point(12, 38);
            this.pnlManageBranch.Margin = new System.Windows.Forms.Padding(4);
            this.pnlManageBranch.Name = "pnlManageBranch";
            this.pnlManageBranch.Size = new System.Drawing.Size(762, 206);
            this.pnlManageBranch.TabIndex = 7;
            // 
            // pnlPull
            // 
            this.pnlPull.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlPull.Controls.Add(this.cbRemote);
            this.pnlPull.Controls.Add(this.label9);
            this.pnlPull.Controls.Add(this.btnPull);
            this.pnlPull.Location = new System.Drawing.Point(112, 53);
            this.pnlPull.Margin = new System.Windows.Forms.Padding(4);
            this.pnlPull.Name = "pnlPull";
            this.pnlPull.Size = new System.Drawing.Size(154, 149);
            this.pnlPull.TabIndex = 6;
            // 
            // cbRemote
            // 
            this.cbRemote.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRemote.FormattingEnabled = true;
            this.cbRemote.Location = new System.Drawing.Point(4, 30);
            this.cbRemote.Margin = new System.Windows.Forms.Padding(4);
            this.cbRemote.Name = "cbRemote";
            this.cbRemote.Size = new System.Drawing.Size(139, 24);
            this.cbRemote.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 9);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(139, 17);
            this.label9.TabIndex = 1;
            this.label9.Text = "Remote to pull from :";
            // 
            // btnPull
            // 
            this.btnPull.Location = new System.Drawing.Point(19, 62);
            this.btnPull.Margin = new System.Windows.Forms.Padding(4);
            this.btnPull.Name = "btnPull";
            this.btnPull.Size = new System.Drawing.Size(104, 29);
            this.btnPull.TabIndex = 0;
            this.btnPull.Text = "Pull";
            this.btnPull.UseVisualStyleBackColor = true;
            this.btnPull.Click += new System.EventHandler(this.btnPull_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(189, 68);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(0, 86);
            this.panel1.TabIndex = 4;
            // 
            // lblPrefixManage
            // 
            this.lblPrefixManage.AutoSize = true;
            this.lblPrefixManage.Location = new System.Drawing.Point(130, 14);
            this.lblPrefixManage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrefixManage.Name = "lblPrefixManage";
            this.lblPrefixManage.Size = new System.Drawing.Size(54, 17);
            this.lblPrefixManage.TabIndex = 1;
            this.lblPrefixManage.Text = "[prefix]/";
            this.lblPrefixManage.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "branch:";
            // 
            // btnPublish
            // 
            this.btnPublish.Location = new System.Drawing.Point(3, 100);
            this.btnPublish.Margin = new System.Windows.Forms.Padding(4);
            this.btnPublish.Name = "btnPublish";
            this.btnPublish.Size = new System.Drawing.Size(101, 29);
            this.btnPublish.TabIndex = 0;
            this.btnPublish.Text = "Publish";
            this.btnPublish.UseVisualStyleBackColor = true;
            this.btnPublish.Click += new System.EventHandler(this.btnPublish_Click);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.button2);
            this.panel4.Controls.Add(this.button1);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.comboBox1);
            this.panel4.Controls.Add(this.textBox2);
            this.panel4.Controls.Add(this.textBox1);
            this.panel4.Controls.Add(this.btnFinish);
            this.panel4.Location = new System.Drawing.Point(268, 53);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(491, 153);
            this.panel4.TabIndex = 10;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(413, 27);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 29);
            this.button2.TabIndex = 11;
            this.button2.Text = "Merge";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(87, 100);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 29);
            this.button1.TabIndex = 10;
            this.button1.Text = "Create";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "To";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "To develop";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(34, 59);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(240, 24);
            this.comboBox1.TabIndex = 7;
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(285, 59);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(122, 22);
            this.textBox2.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(284, 30);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(123, 22);
            this.textBox1.TabIndex = 1;
            // 
            // cbManageType
            // 
            this.cbManageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbManageType.FormattingEnabled = true;
            this.cbManageType.Location = new System.Drawing.Point(246, 0);
            this.cbManageType.Margin = new System.Windows.Forms.Padding(4);
            this.cbManageType.Name = "cbManageType";
            this.cbManageType.Size = new System.Drawing.Size(225, 24);
            this.cbManageType.TabIndex = 3;
            this.cbManageType.SelectedValueChanged += new System.EventHandler(this.cbManageType_SelectedValueChanged);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(359, 656);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(104, 29);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(148, 0);
            this.cbType.Margin = new System.Windows.Forms.Padding(4);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(225, 24);
            this.cbType.TabIndex = 3;
            this.cbType.SelectedValueChanged += new System.EventHandler(this.cbType_SelectedValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 73);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(109, 17);
            this.label10.TabIndex = 1;
            this.label10.Text = "Expected name:";
            // 
            // lblPrefixName
            // 
            this.lblPrefixName.AutoSize = true;
            this.lblPrefixName.Location = new System.Drawing.Point(148, 73);
            this.lblPrefixName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrefixName.Name = "lblPrefixName";
            this.lblPrefixName.Size = new System.Drawing.Size(54, 17);
            this.lblPrefixName.TabIndex = 1;
            this.lblPrefixName.Text = "[prefix]/";
            this.lblPrefixName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // gbStart
            // 
            this.gbStart.Controls.Add(this.textBox3);
            this.gbStart.Controls.Add(this.label6);
            this.gbStart.Controls.Add(this.pnlBasedOn);
            this.gbStart.Controls.Add(this.cbType);
            this.gbStart.Controls.Add(this.txtBranchName);
            this.gbStart.Controls.Add(this.lblPrefixName);
            this.gbStart.Controls.Add(this.label10);
            this.gbStart.Controls.Add(this.btnCreateBranch);
            this.gbStart.Location = new System.Drawing.Point(15, 51);
            this.gbStart.Margin = new System.Windows.Forms.Padding(4);
            this.gbStart.Name = "gbStart";
            this.gbStart.Padding = new System.Windows.Forms.Padding(4);
            this.gbStart.Size = new System.Drawing.Size(786, 152);
            this.gbStart.TabIndex = 8;
            this.gbStart.TabStop = false;
            this.gbStart.Text = "Start branch:";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.Control;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Location = new System.Drawing.Point(146, 38);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(627, 22);
            this.textBox3.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 17);
            this.label6.TabIndex = 5;
            this.label6.Text = "Naming convention:";
            // 
            // lblDebug
            // 
            this.lblDebug.AutoSize = true;
            this.lblDebug.Location = new System.Drawing.Point(30, 662);
            this.lblDebug.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDebug.Name = "lblDebug";
            this.lblDebug.Size = new System.Drawing.Size(316, 17);
            this.lblDebug.TabIndex = 7;
            this.lblDebug.Text = "                                                                             ";
            // 
            // lnkShiftFlow
            // 
            this.lnkShiftFlow.AutoSize = true;
            this.lnkShiftFlow.Location = new System.Drawing.Point(670, 21);
            this.lnkShiftFlow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkShiftFlow.Name = "lnkShiftFlow";
            this.lnkShiftFlow.Size = new System.Drawing.Size(105, 17);
            this.lnkShiftFlow.TabIndex = 9;
            this.lnkShiftFlow.TabStop = true;
            this.lnkShiftFlow.Text = "About ShiftFlow";
            this.lnkShiftFlow.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkShiftFlow_LinkClicked);
            // 
            // pbResultCommand
            // 
            this.pbResultCommand.Location = new System.Drawing.Point(15, 12);
            this.pbResultCommand.Margin = new System.Windows.Forms.Padding(4);
            this.pbResultCommand.Name = "pbResultCommand";
            this.pbResultCommand.Size = new System.Drawing.Size(31, 31);
            this.pbResultCommand.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbResultCommand.TabIndex = 10;
            this.pbResultCommand.TabStop = false;
            // 
            // ttShiftFlow
            // 
            this.ttShiftFlow.AutoPopDelay = 10000;
            this.ttShiftFlow.InitialDelay = 0;
            this.ttShiftFlow.ReshowDelay = 0;
            // 
            // ttCommandResult
            // 
            this.ttCommandResult.AutoPopDelay = 32000;
            this.ttCommandResult.InitialDelay = 0;
            this.ttCommandResult.ReshowDelay = 0;
            this.ttCommandResult.ShowAlways = true;
            // 
            // ttDebug
            // 
            this.ttDebug.AutoPopDelay = 32000;
            this.ttDebug.InitialDelay = 0;
            this.ttDebug.ReshowDelay = 0;
            this.ttDebug.ShowAlways = true;
            // 
            // lblCaptionHead
            // 
            this.lblCaptionHead.AutoSize = true;
            this.lblCaptionHead.Location = new System.Drawing.Point(220, 21);
            this.lblCaptionHead.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCaptionHead.Name = "lblCaptionHead";
            this.lblCaptionHead.Size = new System.Drawing.Size(50, 17);
            this.lblCaptionHead.TabIndex = 1;
            this.lblCaptionHead.Text = "HEAD:";
            // 
            // lblHead
            // 
            this.lblHead.AutoSize = true;
            this.lblHead.Location = new System.Drawing.Point(271, 21);
            this.lblHead.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHead.Name = "lblHead";
            this.lblHead.Size = new System.Drawing.Size(41, 17);
            this.lblHead.TabIndex = 1;
            this.lblHead.Text = "ref/...";
            this.lblHead.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 18);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "command:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtResult);
            this.panel3.Controls.Add(this.lblRunCommand);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(15, 471);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(4);
            this.panel3.Size = new System.Drawing.Size(786, 178);
            this.panel3.TabIndex = 11;
            this.panel3.TabStop = false;
            this.panel3.Text = "Result of shift flow command run";
            // 
            // txtResult
            // 
            this.txtResult.AcceptsReturn = true;
            this.txtResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtResult.Location = new System.Drawing.Point(8, 40);
            this.txtResult.Margin = new System.Windows.Forms.Padding(4);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResult.Size = new System.Drawing.Size(771, 130);
            this.txtResult.TabIndex = 2;
            this.txtResult.Text = " -";
            // 
            // lblRunCommand
            // 
            this.lblRunCommand.AutoSize = true;
            this.lblRunCommand.Location = new System.Drawing.Point(75, 18);
            this.lblRunCommand.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRunCommand.Name = "lblRunCommand";
            this.lblRunCommand.Size = new System.Drawing.Size(13, 17);
            this.lblRunCommand.TabIndex = 1;
            this.lblRunCommand.Text = "-";
            // 
            // ShiftFlowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(816, 695);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.pbResultCommand);
            this.Controls.Add(this.lnkShiftFlow);
            this.Controls.Add(this.gbStart);
            this.Controls.Add(this.lblHead);
            this.Controls.Add(this.lblDebug);
            this.Controls.Add(this.lblCaptionHead);
            this.Controls.Add(this.gbManage);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ShiftFlowForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ShiftFlow";
            this.pnlBasedOn.ResumeLayout(false);
            this.pnlBasedOn.PerformLayout();
            this.gbManage.ResumeLayout(false);
            this.pnlManageBranch.ResumeLayout(false);
            this.pnlManageBranch.PerformLayout();
            this.pnlPull.ResumeLayout(false);
            this.pnlPull.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.gbStart.ResumeLayout(false);
            this.gbStart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResultCommand)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void cbBranches_SelectedValueChanged(object sender, System.EventArgs e)
        {
            UpdatePullRequestsValues();
        }

        private void ComboBox1_SelectedValueChanged(object sender, System.EventArgs e)
        {
            UpdatePullRequestsValues();
        }

        #endregion
        private System.Windows.Forms.TextBox txtBranchName;
        private System.Windows.Forms.Button btnCreateBranch;
        private System.Windows.Forms.ComboBox cbBranches;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.GroupBox gbManage;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cbRemote;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnPull;
        private System.Windows.Forms.Button btnPublish;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblPrefixName;
        private System.Windows.Forms.GroupBox gbStart;
        private System.Windows.Forms.ComboBox cbManageType;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPrefixManage;
        private System.Windows.Forms.Panel pnlPull;
        private System.Windows.Forms.Panel pnlManageBranch;
        private System.Windows.Forms.Label lblDebug;
        private System.Windows.Forms.LinkLabel lnkShiftFlow;
        private System.Windows.Forms.PictureBox pbResultCommand;
        private System.Windows.Forms.ToolTip ttShiftFlow;
        private System.Windows.Forms.ToolTip ttCommandResult;
        private System.Windows.Forms.ToolTip ttDebug;
        private System.Windows.Forms.Label lblCaptionHead;
        private System.Windows.Forms.Label lblHead;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox panel3;
        private System.Windows.Forms.Label lblRunCommand;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Panel pnlBasedOn;
        private System.Windows.Forms.ComboBox cbBaseBranch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private ComboBox comboBox1;
        private Label label4;
        private Label label5;
        private Button button1;
        private Button button2;
        private Label label6;
        private TextBox textBox3;
    }
}
