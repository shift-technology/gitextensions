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
            this.panel5 = new System.Windows.Forms.Panel();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.label20 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cbManageType = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lblPrefixName = new System.Windows.Forms.Label();
            this.gbStart = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblDebug = new System.Windows.Forms.Label();
            this.pbResultCommand = new System.Windows.Forms.PictureBox();
            this.ttShiftFlow = new System.Windows.Forms.ToolTip(this.components);
            this.ttCommandResult = new System.Windows.Forms.ToolTip(this.components);
            this.ttDebug = new System.Windows.Forms.ToolTip(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.GroupBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.lblRunCommand = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.pnlBasedOn.SuspendLayout();
            this.gbManage.SuspendLayout();
            this.pnlManageBranch.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.gbStart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResultCommand)).BeginInit();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBasedOn
            // 
            this.pnlBasedOn.Controls.Add(this.label3);
            this.pnlBasedOn.Controls.Add(this.cbBaseBranch);
            this.pnlBasedOn.Location = new System.Drawing.Point(7, 130);
            this.pnlBasedOn.Name = "pnlBasedOn";
            this.pnlBasedOn.Size = new System.Drawing.Size(696, 37);
            this.pnlBasedOn.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Select the base branch:";
            // 
            // cbBaseBranch
            // 
            this.cbBaseBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBaseBranch.FormattingEnabled = true;
            this.cbBaseBranch.Location = new System.Drawing.Point(233, 8);
            this.cbBaseBranch.Name = "cbBaseBranch";
            this.cbBaseBranch.Size = new System.Drawing.Size(422, 24);
            this.cbBaseBranch.TabIndex = 3;
            // 
            // txtBranchName
            // 
            this.txtBranchName.Location = new System.Drawing.Point(240, 174);
            this.txtBranchName.Margin = new System.Windows.Forms.Padding(4);
            this.txtBranchName.Name = "txtBranchName";
            this.txtBranchName.Size = new System.Drawing.Size(422, 22);
            this.txtBranchName.TabIndex = 2;
            // 
            // btnCreateBranch
            // 
            this.btnCreateBranch.Location = new System.Drawing.Point(670, 173);
            this.btnCreateBranch.Margin = new System.Windows.Forms.Padding(4);
            this.btnCreateBranch.Name = "btnCreateBranch";
            this.btnCreateBranch.Size = new System.Drawing.Size(101, 29);
            this.btnCreateBranch.TabIndex = 0;
            this.btnCreateBranch.Text = "Create";
            this.btnCreateBranch.UseVisualStyleBackColor = true;
            this.btnCreateBranch.Click += new System.EventHandler(this.btnStartBranch_Click);
            // 
            // cbBranches
            // 
            this.cbBranches.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranches.FormattingEnabled = true;
            this.cbBranches.Location = new System.Drawing.Point(418, 0);
            this.cbBranches.Margin = new System.Windows.Forms.Padding(4);
            this.cbBranches.Name = "cbBranches";
            this.cbBranches.Size = new System.Drawing.Size(359, 24);
            this.cbBranches.TabIndex = 3;
            this.cbBranches.SelectedValueChanged += new System.EventHandler(this.cbBranches_SelectedValueChanged);
            // 
            // btnFinish
            // 
            this.btnFinish.Location = new System.Drawing.Point(671, 29);
            this.btnFinish.Margin = new System.Windows.Forms.Padding(4);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(74, 29);
            this.btnFinish.TabIndex = 0;
            this.btnFinish.Text = "Finish";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // gbManage
            // 
            this.gbManage.Controls.Add(this.pnlManageBranch);
            this.gbManage.Controls.Add(this.cbManageType);
            this.gbManage.Controls.Add(this.cbBranches);
            this.gbManage.Location = new System.Drawing.Point(15, 321);
            this.gbManage.Margin = new System.Windows.Forms.Padding(4);
            this.gbManage.Name = "gbManage";
            this.gbManage.Padding = new System.Windows.Forms.Padding(4);
            this.gbManage.Size = new System.Drawing.Size(785, 454);
            this.gbManage.TabIndex = 6;
            this.gbManage.TabStop = false;
            this.gbManage.Text = "Branch management:";
            // 
            // pnlManageBranch
            // 
            this.pnlManageBranch.Controls.Add(this.panel5);
            this.pnlManageBranch.Controls.Add(this.panel2);
            this.pnlManageBranch.Controls.Add(this.label11);
            this.pnlManageBranch.Controls.Add(this.panel1);
            this.pnlManageBranch.Controls.Add(this.panel4);
            this.pnlManageBranch.Controls.Add(this.comboBox1);
            this.pnlManageBranch.Controls.Add(this.button1);
            this.pnlManageBranch.Location = new System.Drawing.Point(12, 38);
            this.pnlManageBranch.Margin = new System.Windows.Forms.Padding(4);
            this.pnlManageBranch.Name = "pnlManageBranch";
            this.pnlManageBranch.Size = new System.Drawing.Size(762, 408);
            this.pnlManageBranch.TabIndex = 7;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.comboBox3);
            this.panel5.Controls.Add(this.label22);
            this.panel5.Location = new System.Drawing.Point(0, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(759, 40);
            this.panel5.TabIndex = 15;
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(125, 11);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(344, 24);
            this.comboBox3.TabIndex = 1;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(3, 14);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(99, 17);
            this.label22.TabIndex = 0;
            this.label22.Text = "Master branch";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.label19);
            this.panel2.Controls.Add(this.linkLabel3);
            this.panel2.Controls.Add(this.label20);
            this.panel2.Controls.Add(this.textBox3);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Location = new System.Drawing.Point(6, 303);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(753, 102);
            this.panel2.TabIndex = 14;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(475, 35);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(52, 17);
            this.label15.TabIndex = 19;
            this.label15.Text = "Status:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(34, 35);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(135, 17);
            this.label18.TabIndex = 17;
            this.label18.Text = "Pull request number";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(578, 35);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(54, 17);
            this.label19.TabIndex = 16;
            this.label19.Text = "label19";
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(34, 73);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(72, 17);
            this.linkLabel3.TabIndex = 13;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "linkLabel3";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(3, 6);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(288, 17);
            this.label20.TabIndex = 9;
            this.label20.Text = "Pull request to your production branch";
            // 
            // textBox3
            // 
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(184, 32);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(276, 22);
            this.textBox3.TabIndex = 2;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(671, 29);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(74, 29);
            this.button3.TabIndex = 0;
            this.button3.Text = "Finish";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(116, 17);
            this.label11.TabIndex = 13;
            this.label11.Text = "Customer branch";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.linkLabel2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.btnFinish);
            this.panel1.Location = new System.Drawing.Point(6, 195);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(753, 102);
            this.panel1.TabIndex = 11;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(475, 35);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(52, 17);
            this.label17.TabIndex = 19;
            this.label17.Text = "Status:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(34, 35);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(135, 17);
            this.label14.TabIndex = 17;
            this.label14.Text = "Pull request number";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(578, 35);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 17);
            this.label13.TabIndex = 16;
            this.label13.Text = "label13";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(34, 73);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(72, 17);
            this.linkLabel2.TabIndex = 13;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "linkLabel2";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(260, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Pull request to your master branch";
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(184, 32);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(276, 22);
            this.textBox2.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.label16);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.linkLabel1);
            this.panel4.Controls.Add(this.button2);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.textBox1);
            this.panel4.Location = new System.Drawing.Point(6, 87);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(754, 102);
            this.panel4.TabIndex = 10;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(476, 31);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(52, 17);
            this.label16.TabIndex = 17;
            this.label16.Text = "Status:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 17);
            this.label1.TabIndex = 16;
            this.label1.Text = "Pull request number";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(579, 31);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 17);
            this.label12.TabIndex = 15;
            this.label12.Text = "label12";
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(35, 76);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(72, 17);
            this.linkLabel1.TabIndex = 12;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "linkLabel1";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // button2
            // 
            this.button2.ForeColor = System.Drawing.Color.Red;
            this.button2.Location = new System.Drawing.Point(672, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(74, 29);
            this.button2.TabIndex = 11;
            this.button2.Text = "Status";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(176, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Pull request to develop";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(185, 31);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(276, 22);
            this.textBox1.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(125, 53);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(343, 24);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.SelectedValueChanged += new System.EventHandler(this.ComboBox1_SelectedValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(475, 53);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(287, 29);
            this.button1.TabIndex = 10;
            this.button1.Text = "Initialize the pull requests for your branch";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbManageType
            // 
            this.cbManageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbManageType.FormattingEnabled = true;
            this.cbManageType.Location = new System.Drawing.Point(185, 0);
            this.cbManageType.Margin = new System.Windows.Forms.Padding(4);
            this.cbManageType.Name = "cbManageType";
            this.cbManageType.Size = new System.Drawing.Size(225, 24);
            this.cbManageType.TabIndex = 3;
            this.cbManageType.SelectedValueChanged += new System.EventHandler(this.cbManageType_SelectedValueChanged);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(336, 903);
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
            this.cbType.Location = new System.Drawing.Point(185, 0);
            this.cbType.Margin = new System.Windows.Forms.Padding(4);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(225, 24);
            this.cbType.TabIndex = 3;
            this.cbType.SelectedValueChanged += new System.EventHandler(this.cbType_SelectedValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 177);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(121, 17);
            this.label10.TabIndex = 1;
            this.label10.Text = "Name the branch:";
            // 
            // lblPrefixName
            // 
            this.lblPrefixName.AutoSize = true;
            this.lblPrefixName.Location = new System.Drawing.Point(135, 177);
            this.lblPrefixName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrefixName.Name = "lblPrefixName";
            this.lblPrefixName.Size = new System.Drawing.Size(54, 17);
            this.lblPrefixName.TabIndex = 1;
            this.lblPrefixName.Text = "[prefix]/";
            this.lblPrefixName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // gbStart
            // 
            this.gbStart.Controls.Add(this.label9);
            this.gbStart.Controls.Add(this.label8);
            this.gbStart.Controls.Add(this.label7);
            this.gbStart.Controls.Add(this.label6);
            this.gbStart.Controls.Add(this.pnlBasedOn);
            this.gbStart.Controls.Add(this.cbType);
            this.gbStart.Controls.Add(this.txtBranchName);
            this.gbStart.Controls.Add(this.lblPrefixName);
            this.gbStart.Controls.Add(this.label10);
            this.gbStart.Controls.Add(this.btnCreateBranch);
            this.gbStart.Location = new System.Drawing.Point(15, 106);
            this.gbStart.Margin = new System.Windows.Forms.Padding(4);
            this.gbStart.Name = "gbStart";
            this.gbStart.Padding = new System.Windows.Forms.Padding(4);
            this.gbStart.Size = new System.Drawing.Size(786, 207);
            this.gbStart.TabIndex = 8;
            this.gbStart.TabStop = false;
            this.gbStart.Text = "Branch creation:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label9.Location = new System.Drawing.Point(153, 101);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 17);
            this.label9.TabIndex = 9;
            this.label9.Text = "label9";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label8.Location = new System.Drawing.Point(101, 37);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 17);
            this.label8.TabIndex = 8;
            this.label8.Text = "label8";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 17);
            this.label7.TabIndex = 7;
            this.label7.Text = "Context:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 17);
            this.label6.TabIndex = 5;
            this.label6.Text = "Naming convention:";
            // 
            // lblDebug
            // 
            this.lblDebug.AutoSize = true;
            this.lblDebug.Location = new System.Drawing.Point(85, 19);
            this.lblDebug.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDebug.Name = "lblDebug";
            this.lblDebug.Size = new System.Drawing.Size(316, 17);
            this.lblDebug.TabIndex = 7;
            this.lblDebug.Text = "                                                                             ";
            // 
            // pbResultCommand
            // 
            this.pbResultCommand.Location = new System.Drawing.Point(747, 0);
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
            this.panel3.Controls.Add(this.pbResultCommand);
            this.panel3.Controls.Add(this.lblRunCommand);
            this.panel3.Controls.Add(this.lblDebug);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(13, 783);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(4);
            this.panel3.Size = new System.Drawing.Size(786, 112);
            this.panel3.TabIndex = 11;
            this.panel3.TabStop = false;
            this.panel3.Text = "Result of shift flow command run";
            // 
            // txtResult
            // 
            this.txtResult.AcceptsReturn = true;
            this.txtResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtResult.Location = new System.Drawing.Point(8, 46);
            this.txtResult.Margin = new System.Windows.Forms.Padding(4);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResult.Size = new System.Drawing.Size(771, 58);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Location = new System.Drawing.Point(15, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(785, 86);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Role:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label21.Location = new System.Drawing.Point(14, 35);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(54, 17);
            this.label21.TabIndex = 1;
            this.label21.Text = "label21";
            // 
            // comboBox2
            // 
            this.comboBox2.BackColor = System.Drawing.SystemColors.Control;
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(68, 0);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 24);
            this.comboBox2.TabIndex = 0;
            this.comboBox2.SelectedValueChanged += new System.EventHandler(this.RoleSelectedValueChanged);
            // 
            // ShiftFlowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(807, 945);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.gbStart);
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
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.gbStart.ResumeLayout(false);
            this.gbStart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResultCommand)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        private void cbBranches_SelectedValueChanged(object sender, System.EventArgs e)
        {
            UpdateTargetBranches();
            UpdatePullRequestsValues();
        }

        private void UpdateTargetBranches()
        {
            var role = comboBox2.SelectedItem?.ToString();

            if (role == $"{Role.developer:G}")
            {
                panel5.Enabled = false;
                panel5.Visible = false;
                label11.Text = "Master branch";
            }
            else
            {
                panel5.Enabled = true;
                panel5.Visible = true;
                label11.Text = "Customer branch";
            }
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
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblPrefixName;
        private System.Windows.Forms.GroupBox gbStart;
        private System.Windows.Forms.ComboBox cbManageType;
        private System.Windows.Forms.Panel pnlManageBranch;
        private System.Windows.Forms.Label lblDebug;
        private System.Windows.Forms.PictureBox pbResultCommand;
        private System.Windows.Forms.ToolTip ttShiftFlow;
        private System.Windows.Forms.ToolTip ttCommandResult;
        private System.Windows.Forms.ToolTip ttDebug;
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
        private LinkLabel linkLabel2;
        private LinkLabel linkLabel1;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label13;
        private Label label12;
        private Panel panel1;
        private Label label11;
        private Label label1;
        private Label label14;
        private Label label16;
        private Label label17;
        private Panel panel2;
        private Label label15;
        private Label label18;
        private Label label19;
        private LinkLabel linkLabel3;
        private Label label20;
        private TextBox textBox3;
        private Button button3;
        private GroupBox groupBox1;
        private ComboBox comboBox2;
        private Label label21;
        private Panel panel5;
        private Label label22;
        private ComboBox comboBox3;
    }
}
