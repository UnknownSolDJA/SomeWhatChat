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
            this.chatBox = new System.Windows.Forms.TextBox();
            this.chat_msg = new System.Windows.Forms.TextBox();
            this.chat_send = new System.Windows.Forms.Button();
            this.enterChat = new System.Windows.Forms.Button();
            this.userPassword = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // chatBox
            // 
            this.chatBox.BackColor = System.Drawing.Color.Black;
            this.chatBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chatBox.Enabled = false;
            this.chatBox.ForeColor = System.Drawing.Color.Lime;
            this.chatBox.Location = new System.Drawing.Point(12, 12);
            this.chatBox.Multiline = true;
            this.chatBox.Name = "chatBox";
            this.chatBox.ReadOnly = true;
            this.chatBox.Size = new System.Drawing.Size(395, 307);
            this.chatBox.TabIndex = 3;
            this.chatBox.Text = "Enter your password in form below";
            this.chatBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chatBox_KeyDown);
            // 
            // chat_msg
            // 
            this.chat_msg.BackColor = System.Drawing.Color.Black;
            this.chat_msg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chat_msg.Enabled = false;
            this.chat_msg.ForeColor = System.Drawing.Color.Lime;
            this.chat_msg.Location = new System.Drawing.Point(12, 325);
            this.chat_msg.Name = "chat_msg";
            this.chat_msg.Size = new System.Drawing.Size(395, 20);
            this.chat_msg.TabIndex = 4;
            this.chat_msg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chat_msg_KeyDown);
            // 
            // chat_send
            // 
            this.chat_send.BackColor = System.Drawing.Color.Black;
            this.chat_send.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chat_send.ForeColor = System.Drawing.Color.Lime;
            this.chat_send.Location = new System.Drawing.Point(413, 320);
            this.chat_send.Name = "chat_send";
            this.chat_send.Size = new System.Drawing.Size(123, 23);
            this.chat_send.TabIndex = 5;
            this.chat_send.Text = "Send";
            this.chat_send.UseVisualStyleBackColor = false;
            this.chat_send.Click += new System.EventHandler(this.chat_send_Click);
            // 
            // enterChat
            // 
            this.enterChat.BackColor = System.Drawing.Color.Black;
            this.enterChat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.enterChat.ForeColor = System.Drawing.Color.Lime;
            this.enterChat.Location = new System.Drawing.Point(413, 296);
            this.enterChat.Name = "enterChat";
            this.enterChat.Size = new System.Drawing.Size(123, 23);
            this.enterChat.TabIndex = 7;
            this.enterChat.Text = "Enter room";
            this.enterChat.UseVisualStyleBackColor = false;
            this.enterChat.Click += new System.EventHandler(this.enterChat_Click);
            // 
            // userPassword
            // 
            this.userPassword.BackColor = System.Drawing.SystemColors.Desktop;
            this.userPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userPassword.ForeColor = System.Drawing.Color.Lime;
            this.userPassword.Location = new System.Drawing.Point(413, 270);
            this.userPassword.Name = "userPassword";
            this.userPassword.Size = new System.Drawing.Size(123, 20);
            this.userPassword.TabIndex = 8;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.Black;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.ForeColor = System.Drawing.Color.Lime;
            this.richTextBox1.Location = new System.Drawing.Point(413, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(123, 252);
            this.richTextBox1.TabIndex = 9;
            this.richTextBox1.Text = "Currently online:";
            // 
            // chatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(548, 361);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.userPassword);
            this.Controls.Add(this.enterChat);
            this.Controls.Add(this.chat_send);
            this.Controls.Add(this.chat_msg);
            this.Controls.Add(this.chatBox);
            this.Name = "chatForm";
            this.Text = "OnlineChat";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox chatBox;
        private System.Windows.Forms.TextBox chat_msg;
        private System.Windows.Forms.Button chat_send;
        private System.Windows.Forms.Button enterChat;
        public System.Windows.Forms.TextBox userPassword;
        public System.Windows.Forms.RichTextBox richTextBox1;
    }
}

