namespace RemoteControlServer.Views
{
    partial class StartServerForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartServerForm));
            panel1 = new Panel();
            StopServer = new Button();
            StartServer = new Button();
            label3 = new Label();
            portNumberNumericUpDown = new NumericUpDown();
            label2 = new Label();
            ipAddressTextBox = new TextBox();
            label1 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)portNumberNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(StopServer);
            panel1.Controls.Add(StartServer);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(portNumberNumericUpDown);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(ipAddressTextBox);
            panel1.Location = new Point(12, 21);
            panel1.Name = "panel1";
            panel1.Size = new Size(300, 181);
            panel1.TabIndex = 0;
            // 
            // StopServer
            // 
            StopServer.Enabled = false;
            StopServer.Location = new Point(158, 138);
            StopServer.Name = "StopServer";
            StopServer.Size = new Size(90, 28);
            StopServer.TabIndex = 5;
            StopServer.Text = "Стоп";
            StopServer.UseVisualStyleBackColor = true;
            StopServer.Click += Stop_Click;
            // 
            // StartServer
            // 
            StartServer.Location = new Point(43, 138);
            StartServer.Name = "StartServer";
            StartServer.Size = new Size(90, 28);
            StartServer.TabIndex = 4;
            StartServer.Text = "Старт";
            StartServer.UseVisualStyleBackColor = true;
            StartServer.Click += StartServer_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(13, 78);
            label3.Name = "label3";
            label3.Size = new Size(44, 20);
            label3.TabIndex = 3;
            label3.Text = "Порт";
            // 
            // portNumberNumericUpDown
            // 
            portNumberNumericUpDown.Location = new Point(13, 101);
            portNumberNumericUpDown.Maximum = new decimal(new int[] { 9000, 0, 0, 0 });
            portNumberNumericUpDown.Name = "portNumberNumericUpDown";
            portNumberNumericUpDown.Size = new Size(269, 26);
            portNumberNumericUpDown.TabIndex = 2;
            portNumberNumericUpDown.Value = new decimal(new int[] { 8888, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(13, 16);
            label2.Name = "label2";
            label2.Size = new Size(67, 20);
            label2.TabIndex = 1;
            label2.Text = "IP-адрес";
            // 
            // ipAdressTextBox
            // 
            ipAddressTextBox.Location = new Point(13, 39);
            ipAddressTextBox.Name = "ipAdressTextBox";
            ipAddressTextBox.ReadOnly = true;
            ipAddressTextBox.Size = new Size(269, 26);
            ipAddressTextBox.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(34, 10);
            label1.Name = "label1";
            label1.Size = new Size(176, 20);
            label1.TabIndex = 0;
            label1.Text = "Информация о сервере";
            // 
            // StartServerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(324, 214);
            Controls.Add(label1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "StartServerForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Удалённый рабочий стол";
            Load += StartServerForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)portNumberNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Button StartServer;
        private Label label3;
        private NumericUpDown portNumberNumericUpDown;
        private Label label2;
        private TextBox ipAddressTextBox;
        private Button StopServer;
    }
}