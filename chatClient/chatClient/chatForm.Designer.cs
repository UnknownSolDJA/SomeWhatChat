namespace chatClient
{
    partial class chatForm
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
            this.chat_msg = new System.Windows.Forms.TextBox();
            this.enterChat = new System.Windows.Forms.Button();
            this.userPassword = new System.Windows.Forms.TextBox();
            this.usersOnline = new System.Windows.Forms.RichTextBox();
            this.chatBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.setName = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // chat_msg
            // 
            this.chat_msg.BackColor = System.Drawing.Color.Black;
            this.chat_msg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chat_msg.Enabled = false;
            this.chat_msg.ForeColor = System.Drawing.Color.Lime;
            this.chat_msg.Location = new System.Drawing.Point(12, 325);
            this.chat_msg.Name = "chat_msg";
            this.chat_msg.Size = new System.Drawing.Size(395, 13);
            this.chat_msg.TabIndex = 4;
            this.chat_msg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chat_msg_KeyDown);
            // 
            // enterChat
            // 
            this.enterChat.BackColor = System.Drawing.Color.Black;
            this.enterChat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.enterChat.ForeColor = System.Drawing.Color.Lime;
            this.enterChat.Location = new System.Drawing.Point(413, 325);
            this.enterChat.Name = "enterChat";
            this.enterChat.Size = new System.Drawing.Size(123, 23);
            this.enterChat.TabIndex = 7;
            this.enterChat.Text = "Enter";
            this.enterChat.UseVisualStyleBackColor = false;
            this.enterChat.Click += new System.EventHandler(this.enterChat_Click);
            // 
            // userPassword
            // 
            this.userPassword.BackColor = System.Drawing.Color.Black;
            this.userPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userPassword.ForeColor = System.Drawing.Color.Lime;
            this.userPassword.Location = new System.Drawing.Point(413, 299);
            this.userPassword.MaxLength = 20;
            this.userPassword.Name = "userPassword";
            this.userPassword.PasswordChar = '*';
            this.userPassword.Size = new System.Drawing.Size(123, 20);
            this.userPassword.TabIndex = 8;
            this.userPassword.UseSystemPasswordChar = true;
            this.userPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.userPassword_KeyDown);
            // 
            // usersOnline
            // 
            this.usersOnline.BackColor = System.Drawing.Color.Black;
            this.usersOnline.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.usersOnline.ForeColor = System.Drawing.Color.Lime;
            this.usersOnline.Location = new System.Drawing.Point(413, 28);
            this.usersOnline.Name = "usersOnline";
            this.usersOnline.ReadOnly = true;
            this.usersOnline.Size = new System.Drawing.Size(123, 262);
            this.usersOnline.TabIndex = 9;
            this.usersOnline.Text = "";
            // 
            // chatBox
            // 
            this.chatBox.BackColor = System.Drawing.Color.Black;
            this.chatBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chatBox.ForeColor = System.Drawing.Color.Lime;
            this.chatBox.Location = new System.Drawing.Point(12, 12);
            this.chatBox.Name = "chatBox";
            this.chatBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.chatBox.Size = new System.Drawing.Size(395, 304);
            this.chatBox.TabIndex = 10;
            this.chatBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Lime;
            this.label1.Location = new System.Drawing.Point(410, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Online:";
            // 
            // setName
            // 
            this.setName.BackColor = System.Drawing.Color.Black;
            this.setName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.setName.ForeColor = System.Drawing.Color.Lime;
            this.setName.FormattingEnabled = true;
            this.setName.Location = new System.Drawing.Point(413, 299);
            this.setName.Name = "setName";
            this.setName.Size = new System.Drawing.Size(123, 21);
            this.setName.TabIndex = 13;
            this.setName.Visible = false;
            this.setName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.setName_KeyDown);
            // 
            // chatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(642, 361);
            this.Controls.Add(this.setName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.usersOnline);
            this.Controls.Add(this.userPassword);
            this.Controls.Add(this.enterChat);
            this.Controls.Add(this.chat_msg);
            this.Name = "chatForm";
            this.Text = "С:\\\\Windows\\system32\\cmd.exe";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox chat_msg;
        private System.Windows.Forms.Button enterChat;
        public System.Windows.Forms.TextBox userPassword;
        public System.Windows.Forms.RichTextBox usersOnline;
        private System.Windows.Forms.RichTextBox chatBox;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox setName;
    }
}

