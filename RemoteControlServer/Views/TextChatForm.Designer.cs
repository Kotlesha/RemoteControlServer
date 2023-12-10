namespace RemoteControlClient.Views
{
    partial class TextChatForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextChatForm));
            chat = new ListBox();
            textInput = new TextBox();
            Send = new Button();
            SuspendLayout();
            // 
            // chat
            // 
            chat.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            chat.FormattingEnabled = true;
            chat.ItemHeight = 19;
            chat.Location = new Point(12, 12);
            chat.Name = "chat";
            chat.Size = new Size(787, 460);
            chat.TabIndex = 0;
            // 
            // textInput
            // 
            textInput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textInput.Location = new Point(12, 482);
            textInput.Multiline = true;
            textInput.Name = "textInput";
            textInput.ScrollBars = ScrollBars.Vertical;
            textInput.Size = new Size(654, 57);
            textInput.TabIndex = 1;
            // 
            // Send
            // 
            Send.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Send.Location = new Point(690, 482);
            Send.Name = "Send";
            Send.Size = new Size(109, 61);
            Send.TabIndex = 2;
            Send.Text = "Отправить";
            Send.UseVisualStyleBackColor = true;
            Send.Click += Send_Click;
            // 
            // TextChatForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(811, 551);
            Controls.Add(Send);
            Controls.Add(textInput);
            Controls.Add(chat);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(829, 596);
            Name = "TextChatForm";
            Text = "Текстовый чат";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox chat;
        private TextBox textInput;
        private Button Send;
    }
}