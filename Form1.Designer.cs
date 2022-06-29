namespace iMesej
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.CreateRoomLabel = new System.Windows.Forms.Label();
            this.CreateRoomButton = new System.Windows.Forms.Button();
            this.RoomIPLabel = new System.Windows.Forms.Label();
            this.RoomIP = new System.Windows.Forms.TextBox();
            this.JoinRoomLabel = new System.Windows.Forms.Label();
            this.JoinRoomButton = new System.Windows.Forms.Button();
            this.ClientIPLabel = new System.Windows.Forms.Label();
            this.ClientIP = new System.Windows.Forms.TextBox();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.StatusText = new System.Windows.Forms.Label();
            this.ExportChatButton = new System.Windows.Forms.Button();
            this.MessageBox = new System.Windows.Forms.TextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.MessageField = new System.Windows.Forms.Panel();
            this.createLobbyStatus = new System.Windows.Forms.Label();
            this.RoomPort = new System.Windows.Forms.TextBox();
            this.RoomPortLabel = new System.Windows.Forms.Label();
            this.ClientPort = new System.Windows.Forms.TextBox();
            this.ClientPortLabel = new System.Windows.Forms.Label();
            this.RefreshRateTimer = new System.Windows.Forms.Timer(this.components);
            this.ClearChatButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // CreateRoomLabel
            // 
            this.CreateRoomLabel.AutoSize = true;
            this.CreateRoomLabel.BackColor = System.Drawing.SystemColors.Control;
            this.CreateRoomLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateRoomLabel.Location = new System.Drawing.Point(16, 11);
            this.CreateRoomLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.CreateRoomLabel.Name = "CreateRoomLabel";
            this.CreateRoomLabel.Size = new System.Drawing.Size(97, 17);
            this.CreateRoomLabel.TabIndex = 0;
            this.CreateRoomLabel.Text = "Create room";
            // 
            // CreateRoomButton
            // 
            this.CreateRoomButton.Location = new System.Drawing.Point(16, 31);
            this.CreateRoomButton.Margin = new System.Windows.Forms.Padding(4);
            this.CreateRoomButton.Name = "CreateRoomButton";
            this.CreateRoomButton.Size = new System.Drawing.Size(100, 28);
            this.CreateRoomButton.TabIndex = 1;
            this.CreateRoomButton.Text = "Create";
            this.CreateRoomButton.UseVisualStyleBackColor = true;
            this.CreateRoomButton.Click += new System.EventHandler(this.CreateRoomButton_Click);
            // 
            // RoomIPLabel
            // 
            this.RoomIPLabel.AutoSize = true;
            this.RoomIPLabel.Location = new System.Drawing.Point(16, 63);
            this.RoomIPLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.RoomIPLabel.Name = "RoomIPLabel";
            this.RoomIPLabel.Size = new System.Drawing.Size(72, 16);
            this.RoomIPLabel.TabIndex = 2;
            this.RoomIPLabel.Text = "IP address";
            // 
            // RoomIP
            // 
            this.RoomIP.Location = new System.Drawing.Point(16, 82);
            this.RoomIP.Margin = new System.Windows.Forms.Padding(4);
            this.RoomIP.Name = "RoomIP";
            this.RoomIP.Size = new System.Drawing.Size(132, 22);
            this.RoomIP.TabIndex = 3;
            // 
            // JoinRoomLabel
            // 
            this.JoinRoomLabel.AutoSize = true;
            this.JoinRoomLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JoinRoomLabel.Location = new System.Drawing.Point(267, 11);
            this.JoinRoomLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.JoinRoomLabel.Name = "JoinRoomLabel";
            this.JoinRoomLabel.Size = new System.Drawing.Size(79, 17);
            this.JoinRoomLabel.TabIndex = 4;
            this.JoinRoomLabel.Text = "Join room";
            // 
            // JoinRoomButton
            // 
            this.JoinRoomButton.Location = new System.Drawing.Point(271, 79);
            this.JoinRoomButton.Margin = new System.Windows.Forms.Padding(4);
            this.JoinRoomButton.Name = "JoinRoomButton";
            this.JoinRoomButton.Size = new System.Drawing.Size(72, 28);
            this.JoinRoomButton.TabIndex = 5;
            this.JoinRoomButton.Text = "Join";
            this.JoinRoomButton.UseVisualStyleBackColor = true;
            this.JoinRoomButton.Click += new System.EventHandler(this.JoinRoomButton_Click);
            // 
            // ClientIPLabel
            // 
            this.ClientIPLabel.AutoSize = true;
            this.ClientIPLabel.Location = new System.Drawing.Point(267, 31);
            this.ClientIPLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ClientIPLabel.Name = "ClientIPLabel";
            this.ClientIPLabel.Size = new System.Drawing.Size(72, 16);
            this.ClientIPLabel.TabIndex = 6;
            this.ClientIPLabel.Text = "IP address";
            // 
            // ClientIP
            // 
            this.ClientIP.Location = new System.Drawing.Point(271, 52);
            this.ClientIP.Margin = new System.Windows.Forms.Padding(4);
            this.ClientIP.Name = "ClientIP";
            this.ClientIP.Size = new System.Drawing.Size(132, 22);
            this.ClientIP.TabIndex = 7;
            this.ClientIP.TextChanged += new System.EventHandler(this.ClientIP_TextChanged);
            this.ClientIP.MouseHover += new System.EventHandler(this.ClientIP_MouseHover);
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(156, 130);
            this.StatusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(47, 16);
            this.StatusLabel.TabIndex = 8;
            this.StatusLabel.Text = "Status:";
            // 
            // StatusText
            // 
            this.StatusText.AutoSize = true;
            this.StatusText.ForeColor = System.Drawing.Color.Red;
            this.StatusText.Location = new System.Drawing.Point(211, 130);
            this.StatusText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.StatusText.Name = "StatusText";
            this.StatusText.Size = new System.Drawing.Size(90, 16);
            this.StatusText.TabIndex = 9;
            this.StatusText.Text = "Disconnected";
            // 
            // ExportChatButton
            // 
            this.ExportChatButton.Location = new System.Drawing.Point(16, 159);
            this.ExportChatButton.Margin = new System.Windows.Forms.Padding(4);
            this.ExportChatButton.Name = "ExportChatButton";
            this.ExportChatButton.Size = new System.Drawing.Size(100, 28);
            this.ExportChatButton.TabIndex = 12;
            this.ExportChatButton.Text = "Export chat";
            this.ExportChatButton.UseVisualStyleBackColor = true;
            this.ExportChatButton.Click += new System.EventHandler(this.ExportChatButton_Click);
            // 
            // MessageBox
            // 
            this.MessageBox.Location = new System.Drawing.Point(16, 587);
            this.MessageBox.Margin = new System.Windows.Forms.Padding(4);
            this.MessageBox.Name = "MessageBox";
            this.MessageBox.Size = new System.Drawing.Size(359, 22);
            this.MessageBox.TabIndex = 13;
            this.MessageBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MessageBox_KeyUp);
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(381, 586);
            this.SendButton.Margin = new System.Windows.Forms.Padding(4);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(77, 28);
            this.SendButton.TabIndex = 14;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // MessageField
            // 
            this.MessageField.AutoScroll = true;
            this.MessageField.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.MessageField.Location = new System.Drawing.Point(16, 196);
            this.MessageField.Margin = new System.Windows.Forms.Padding(4);
            this.MessageField.Name = "MessageField";
            this.MessageField.Size = new System.Drawing.Size(443, 384);
            this.MessageField.TabIndex = 15;
            this.MessageField.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.MessageField_ControlAdded);
            this.MessageField.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.MessageField_ControlRemoved);
            // 
            // createLobbyStatus
            // 
            this.createLobbyStatus.AutoSize = true;
            this.createLobbyStatus.Location = new System.Drawing.Point(123, 37);
            this.createLobbyStatus.Name = "createLobbyStatus";
            this.createLobbyStatus.Size = new System.Drawing.Size(0, 16);
            this.createLobbyStatus.TabIndex = 16;
            // 
            // RoomPort
            // 
            this.RoomPort.Location = new System.Drawing.Point(155, 82);
            this.RoomPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RoomPort.Name = "RoomPort";
            this.RoomPort.Size = new System.Drawing.Size(48, 22);
            this.RoomPort.TabIndex = 17;
            this.RoomPort.TextChanged += new System.EventHandler(this.RoomPort_TextChanged);
            this.RoomPort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RoomPort_KeyDown);
            // 
            // RoomPortLabel
            // 
            this.RoomPortLabel.AutoSize = true;
            this.RoomPortLabel.Location = new System.Drawing.Point(152, 63);
            this.RoomPortLabel.Name = "RoomPortLabel";
            this.RoomPortLabel.Size = new System.Drawing.Size(31, 16);
            this.RoomPortLabel.TabIndex = 18;
            this.RoomPortLabel.Text = "Port";
            // 
            // ClientPort
            // 
            this.ClientPort.Location = new System.Drawing.Point(411, 52);
            this.ClientPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ClientPort.Name = "ClientPort";
            this.ClientPort.Size = new System.Drawing.Size(48, 22);
            this.ClientPort.TabIndex = 19;
            this.ClientPort.TextChanged += new System.EventHandler(this.ClientPort_TextChanged);
            this.ClientPort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ClientPort_KeyDown);
            this.ClientPort.MouseHover += new System.EventHandler(this.ClientPort_MouseHover);
            // 
            // ClientPortLabel
            // 
            this.ClientPortLabel.AutoSize = true;
            this.ClientPortLabel.Location = new System.Drawing.Point(409, 33);
            this.ClientPortLabel.Name = "ClientPortLabel";
            this.ClientPortLabel.Size = new System.Drawing.Size(31, 16);
            this.ClientPortLabel.TabIndex = 20;
            this.ClientPortLabel.Text = "Port";
            // 
            // RefreshRateTimer
            // 
            this.RefreshRateTimer.Tick += new System.EventHandler(this.RefreshRateTimer_Tick);
            // 
            // ClearChatButton
            // 
            this.ClearChatButton.Location = new System.Drawing.Point(372, 159);
            this.ClearChatButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ClearChatButton.Name = "ClearChatButton";
            this.ClearChatButton.Size = new System.Drawing.Size(85, 28);
            this.ClearChatButton.TabIndex = 21;
            this.ClearChatButton.Text = "Clear chat";
            this.ClearChatButton.UseVisualStyleBackColor = true;
            this.ClearChatButton.Click += new System.EventHandler(this.ClearChatButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 626);
            this.Controls.Add(this.MessageBox);
            this.Controls.Add(this.ClearChatButton);
            this.Controls.Add(this.ClientPortLabel);
            this.Controls.Add(this.ClientPort);
            this.Controls.Add(this.RoomPortLabel);
            this.Controls.Add(this.RoomPort);
            this.Controls.Add(this.createLobbyStatus);
            this.Controls.Add(this.MessageField);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.ExportChatButton);
            this.Controls.Add(this.StatusText);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.ClientIP);
            this.Controls.Add(this.ClientIPLabel);
            this.Controls.Add(this.JoinRoomButton);
            this.Controls.Add(this.JoinRoomLabel);
            this.Controls.Add(this.RoomIP);
            this.Controls.Add(this.RoomIPLabel);
            this.Controls.Add(this.CreateRoomButton);
            this.Controls.Add(this.CreateRoomLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "iMesej";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label CreateRoomLabel;
        private System.Windows.Forms.Button CreateRoomButton;
        private System.Windows.Forms.Label RoomIPLabel;
        private System.Windows.Forms.TextBox RoomIP;
        private System.Windows.Forms.Label JoinRoomLabel;
        private System.Windows.Forms.Button JoinRoomButton;
        private System.Windows.Forms.Label ClientIPLabel;
        private System.Windows.Forms.TextBox ClientIP;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Label StatusText;
        private System.Windows.Forms.Button ExportChatButton;
        private System.Windows.Forms.TextBox MessageBox;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.Panel MessageField;
        private System.Windows.Forms.Label createLobbyStatus;
        private System.Windows.Forms.TextBox RoomPort;
        private System.Windows.Forms.Label RoomPortLabel;
        private System.Windows.Forms.TextBox ClientPort;
        private System.Windows.Forms.Label ClientPortLabel;
        private System.Windows.Forms.Timer RefreshRateTimer;
        private System.Windows.Forms.Button ClearChatButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

