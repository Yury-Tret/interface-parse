namespace interface_parse
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.ServicePLParseGroupBox = new System.Windows.Forms.GroupBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.WorkProgressBar = new System.Windows.Forms.ProgressBar();
            this.StartSSHButton = new System.Windows.Forms.Button();
            this.RouterProgressInfo = new System.Windows.Forms.Label();
            this.OverallProgressInfo = new System.Windows.Forms.Label();
            this.ShellTestButton = new System.Windows.Forms.Button();
            this.RouterProgress = new System.Windows.Forms.ProgressBar();
            this.OverallProgress = new System.Windows.Forms.ProgressBar();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageParsetextData = new System.Windows.Forms.TabPage();
            this.lblVersion = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ParseDelay = new System.Windows.Forms.Button();
            this.tabPageChangeTunnels = new System.Windows.Forms.TabPage();
            this.PasswordText = new System.Windows.Forms.Label();
            this.UsernameText = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.TextBox();
            this.Username = new System.Windows.Forms.TextBox();
            this.RouterAddressDescription = new System.Windows.Forms.Label();
            this.RouterAddressEnd = new System.Windows.Forms.NumericUpDown();
            this.RouterAddressStart = new System.Windows.Forms.NumericUpDown();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.txtPlain = new System.Windows.Forms.TextBox();
            this.txtCrypt = new System.Windows.Forms.TextBox();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.ServicePLParseGroupBox.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageParsetextData.SuspendLayout();
            this.tabPageChangeTunnels.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RouterAddressEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RouterAddressStart)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Parse 4-in-1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ServicePLParseGroupBox
            // 
            this.ServicePLParseGroupBox.Controls.Add(this.StartButton);
            this.ServicePLParseGroupBox.Controls.Add(this.WorkProgressBar);
            this.ServicePLParseGroupBox.Location = new System.Drawing.Point(87, 6);
            this.ServicePLParseGroupBox.Name = "ServicePLParseGroupBox";
            this.ServicePLParseGroupBox.Size = new System.Drawing.Size(95, 79);
            this.ServicePLParseGroupBox.TabIndex = 1;
            this.ServicePLParseGroupBox.TabStop = false;
            this.ServicePLParseGroupBox.Text = "service-pl-parse";
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(6, 19);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 1;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // WorkProgressBar
            // 
            this.WorkProgressBar.Location = new System.Drawing.Point(6, 48);
            this.WorkProgressBar.Name = "WorkProgressBar";
            this.WorkProgressBar.Size = new System.Drawing.Size(75, 23);
            this.WorkProgressBar.TabIndex = 0;
            // 
            // StartSSHButton
            // 
            this.StartSSHButton.Location = new System.Drawing.Point(6, 6);
            this.StartSSHButton.Name = "StartSSHButton";
            this.StartSSHButton.Size = new System.Drawing.Size(75, 23);
            this.StartSSHButton.TabIndex = 2;
            this.StartSSHButton.Text = "Start SSH";
            this.StartSSHButton.UseVisualStyleBackColor = true;
            this.StartSSHButton.Click += new System.EventHandler(this.StartSSHButton_Click);
            // 
            // RouterProgressInfo
            // 
            this.RouterProgressInfo.AutoSize = true;
            this.RouterProgressInfo.Location = new System.Drawing.Point(141, 45);
            this.RouterProgressInfo.Name = "RouterProgressInfo";
            this.RouterProgressInfo.Size = new System.Drawing.Size(0, 13);
            this.RouterProgressInfo.TabIndex = 3;
            // 
            // OverallProgressInfo
            // 
            this.OverallProgressInfo.AutoSize = true;
            this.OverallProgressInfo.Location = new System.Drawing.Point(141, 74);
            this.OverallProgressInfo.Name = "OverallProgressInfo";
            this.OverallProgressInfo.Size = new System.Drawing.Size(0, 13);
            this.OverallProgressInfo.TabIndex = 4;
            // 
            // ShellTestButton
            // 
            this.ShellTestButton.Location = new System.Drawing.Point(6, 54);
            this.ShellTestButton.Name = "ShellTestButton";
            this.ShellTestButton.Size = new System.Drawing.Size(75, 23);
            this.ShellTestButton.TabIndex = 5;
            this.ShellTestButton.Text = "ShellTest";
            this.ShellTestButton.UseVisualStyleBackColor = true;
            this.ShellTestButton.Click += new System.EventHandler(this.ShellTestButton_Click);
            // 
            // RouterProgress
            // 
            this.RouterProgress.Location = new System.Drawing.Point(8, 35);
            this.RouterProgress.Maximum = 4;
            this.RouterProgress.Name = "RouterProgress";
            this.RouterProgress.Size = new System.Drawing.Size(127, 23);
            this.RouterProgress.TabIndex = 6;
            // 
            // OverallProgress
            // 
            this.OverallProgress.Location = new System.Drawing.Point(8, 64);
            this.OverallProgress.Name = "OverallProgress";
            this.OverallProgress.Size = new System.Drawing.Size(127, 23);
            this.OverallProgress.TabIndex = 7;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageParsetextData);
            this.tabControl1.Controls.Add(this.tabPageChangeTunnels);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(495, 342);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPageParsetextData
            // 
            this.tabPageParsetextData.Controls.Add(this.btnDecrypt);
            this.tabPageParsetextData.Controls.Add(this.txtCrypt);
            this.tabPageParsetextData.Controls.Add(this.txtPlain);
            this.tabPageParsetextData.Controls.Add(this.btnEncrypt);
            this.tabPageParsetextData.Controls.Add(this.lblVersion);
            this.tabPageParsetextData.Controls.Add(this.label1);
            this.tabPageParsetextData.Controls.Add(this.ParseDelay);
            this.tabPageParsetextData.Controls.Add(this.ShellTestButton);
            this.tabPageParsetextData.Controls.Add(this.button1);
            this.tabPageParsetextData.Controls.Add(this.ServicePLParseGroupBox);
            this.tabPageParsetextData.Location = new System.Drawing.Point(4, 22);
            this.tabPageParsetextData.Name = "tabPageParsetextData";
            this.tabPageParsetextData.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageParsetextData.Size = new System.Drawing.Size(487, 316);
            this.tabPageParsetextData.TabIndex = 0;
            this.tabPageParsetextData.Text = "Parse Text Data";
            this.tabPageParsetextData.UseVisualStyleBackColor = true;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(8, 298);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(0, 13);
            this.lblVersion.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
            // 
            // ParseDelay
            // 
            this.ParseDelay.Location = new System.Drawing.Point(6, 83);
            this.ParseDelay.Name = "ParseDelay";
            this.ParseDelay.Size = new System.Drawing.Size(75, 39);
            this.ParseDelay.TabIndex = 6;
            this.ParseDelay.Text = "Parse Delays";
            this.ParseDelay.UseVisualStyleBackColor = true;
            this.ParseDelay.Click += new System.EventHandler(this.ParseDelay_Click);
            // 
            // tabPageChangeTunnels
            // 
            this.tabPageChangeTunnels.Controls.Add(this.PasswordText);
            this.tabPageChangeTunnels.Controls.Add(this.UsernameText);
            this.tabPageChangeTunnels.Controls.Add(this.Password);
            this.tabPageChangeTunnels.Controls.Add(this.Username);
            this.tabPageChangeTunnels.Controls.Add(this.RouterAddressDescription);
            this.tabPageChangeTunnels.Controls.Add(this.RouterAddressEnd);
            this.tabPageChangeTunnels.Controls.Add(this.RouterAddressStart);
            this.tabPageChangeTunnels.Controls.Add(this.StartSSHButton);
            this.tabPageChangeTunnels.Controls.Add(this.RouterProgressInfo);
            this.tabPageChangeTunnels.Controls.Add(this.OverallProgressInfo);
            this.tabPageChangeTunnels.Controls.Add(this.OverallProgress);
            this.tabPageChangeTunnels.Controls.Add(this.RouterProgress);
            this.tabPageChangeTunnels.Location = new System.Drawing.Point(4, 22);
            this.tabPageChangeTunnels.Name = "tabPageChangeTunnels";
            this.tabPageChangeTunnels.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageChangeTunnels.Size = new System.Drawing.Size(487, 316);
            this.tabPageChangeTunnels.TabIndex = 1;
            this.tabPageChangeTunnels.Text = "Change Tunnels";
            this.tabPageChangeTunnels.UseVisualStyleBackColor = true;
            // 
            // PasswordText
            // 
            this.PasswordText.AutoSize = true;
            this.PasswordText.Location = new System.Drawing.Point(142, 178);
            this.PasswordText.Name = "PasswordText";
            this.PasswordText.Size = new System.Drawing.Size(53, 13);
            this.PasswordText.TabIndex = 14;
            this.PasswordText.Text = "Password";
            this.PasswordText.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // UsernameText
            // 
            this.UsernameText.AutoSize = true;
            this.UsernameText.Location = new System.Drawing.Point(142, 152);
            this.UsernameText.Name = "UsernameText";
            this.UsernameText.Size = new System.Drawing.Size(55, 13);
            this.UsernameText.TabIndex = 13;
            this.UsernameText.Text = "Username";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(9, 175);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(127, 20);
            this.Password.TabIndex = 12;
            // 
            // Username
            // 
            this.Username.Location = new System.Drawing.Point(9, 149);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(127, 20);
            this.Username.TabIndex = 11;
            // 
            // RouterAddressDescription
            // 
            this.RouterAddressDescription.AutoSize = true;
            this.RouterAddressDescription.Location = new System.Drawing.Point(6, 106);
            this.RouterAddressDescription.Name = "RouterAddressDescription";
            this.RouterAddressDescription.Size = new System.Drawing.Size(84, 13);
            this.RouterAddressDescription.TabIndex = 10;
            this.RouterAddressDescription.Text = "Router IP scope";
            // 
            // RouterAddressEnd
            // 
            this.RouterAddressEnd.Location = new System.Drawing.Point(64, 122);
            this.RouterAddressEnd.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
            this.RouterAddressEnd.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.RouterAddressEnd.Name = "RouterAddressEnd";
            this.RouterAddressEnd.Size = new System.Drawing.Size(50, 20);
            this.RouterAddressEnd.TabIndex = 9;
            this.RouterAddressEnd.Value = new decimal(new int[] {
            147,
            0,
            0,
            0});
            // 
            // RouterAddressStart
            // 
            this.RouterAddressStart.Location = new System.Drawing.Point(8, 122);
            this.RouterAddressStart.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
            this.RouterAddressStart.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.RouterAddressStart.Name = "RouterAddressStart";
            this.RouterAddressStart.Size = new System.Drawing.Size(50, 20);
            this.RouterAddressStart.TabIndex = 8;
            this.RouterAddressStart.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(6, 128);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(75, 23);
            this.btnEncrypt.TabIndex = 9;
            this.btnEncrypt.Text = "Encrypt";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // txtPlain
            // 
            this.txtPlain.Location = new System.Drawing.Point(6, 157);
            this.txtPlain.Name = "txtPlain";
            this.txtPlain.Size = new System.Drawing.Size(100, 20);
            this.txtPlain.TabIndex = 10;
            // 
            // txtCrypt
            // 
            this.txtCrypt.Location = new System.Drawing.Point(6, 183);
            this.txtCrypt.Name = "txtCrypt";
            this.txtCrypt.Size = new System.Drawing.Size(100, 20);
            this.txtCrypt.TabIndex = 11;
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(87, 128);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(75, 23);
            this.btnDecrypt.TabIndex = 12;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 342);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ServicePLParseGroupBox.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageParsetextData.ResumeLayout(false);
            this.tabPageParsetextData.PerformLayout();
            this.tabPageChangeTunnels.ResumeLayout(false);
            this.tabPageChangeTunnels.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RouterAddressEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RouterAddressStart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox ServicePLParseGroupBox;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.ProgressBar WorkProgressBar;
        private System.Windows.Forms.Button StartSSHButton;
        private System.Windows.Forms.Label RouterProgressInfo;
        private System.Windows.Forms.Label OverallProgressInfo;
        private System.Windows.Forms.Button ShellTestButton;
        private System.Windows.Forms.ProgressBar RouterProgress;
        private System.Windows.Forms.ProgressBar OverallProgress;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageParsetextData;
        private System.Windows.Forms.TabPage tabPageChangeTunnels;
        private System.Windows.Forms.Label RouterAddressDescription;
        private System.Windows.Forms.NumericUpDown RouterAddressEnd;
        private System.Windows.Forms.NumericUpDown RouterAddressStart;
        private System.Windows.Forms.Button ParseDelay;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.Label PasswordText;
        private System.Windows.Forms.Label UsernameText;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCrypt;
        private System.Windows.Forms.TextBox txtPlain;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnDecrypt;
    }
}

