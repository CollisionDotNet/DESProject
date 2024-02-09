namespace DESProject
{
    partial class Form1
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
            OpenSourceFileBtn = new Button();
            OpenSourceFileLabel = new Label();
            EnterKeyTextbox = new TextBox();
            EnterKeyLabel = new Label();
            EncryptionModeGroupbox = new GroupBox();
            EncryptionModeDecryptRadioBtn = new RadioButton();
            EncryptionModeEncryptRadioBtn = new RadioButton();
            ApplyDESBtn = new Button();
            EncryptionModeGroupbox.SuspendLayout();
            SuspendLayout();
            // 
            // OpenSourceFileBtn
            // 
            OpenSourceFileBtn.Location = new Point(203, 22);
            OpenSourceFileBtn.Name = "OpenSourceFileBtn";
            OpenSourceFileBtn.Size = new Size(75, 23);
            OpenSourceFileBtn.TabIndex = 0;
            OpenSourceFileBtn.Text = "Choose";
            OpenSourceFileBtn.UseVisualStyleBackColor = true;
            OpenSourceFileBtn.Click += OpenSourceFileBtn_Click;
            // 
            // OpenSourceFileLabel
            // 
            OpenSourceFileLabel.AutoSize = true;
            OpenSourceFileLabel.Location = new Point(27, 26);
            OpenSourceFileLabel.Name = "OpenSourceFileLabel";
            OpenSourceFileLabel.Size = new Size(137, 15);
            OpenSourceFileLabel.TabIndex = 1;
            OpenSourceFileLabel.Text = "File for encoding:";
            // 
            // EnterKeyTextbox
            // 
            EnterKeyTextbox.Location = new Point(203, 62);
            EnterKeyTextbox.Name = "EnterKeyTextbox";
            EnterKeyTextbox.Size = new Size(110, 23);
            EnterKeyTextbox.TabIndex = 2;
            // 
            // EnterKeyLabel
            // 
            EnterKeyLabel.AutoSize = true;
            EnterKeyLabel.Location = new Point(27, 65);
            EnterKeyLabel.Name = "EnterKeyLabel";
            EnterKeyLabel.Size = new Size(41, 15);
            EnterKeyLabel.TabIndex = 3;
            EnterKeyLabel.Text = "Key:";
            // 
            // EncryptionModeGroupbox
            // 
            EncryptionModeGroupbox.Controls.Add(EncryptionModeDecryptRadioBtn);
            EncryptionModeGroupbox.Controls.Add(EncryptionModeEncryptRadioBtn);
            EncryptionModeGroupbox.Location = new Point(21, 104);
            EncryptionModeGroupbox.Name = "EncryptionModeGroupbox";
            EncryptionModeGroupbox.Size = new Size(143, 78);
            EncryptionModeGroupbox.TabIndex = 7;
            EncryptionModeGroupbox.TabStop = false;
            EncryptionModeGroupbox.Text = "Encoding mode:";
            // 
            // EncryptionModeDecryptRadioBtn
            // 
            EncryptionModeDecryptRadioBtn.AutoSize = true;
            EncryptionModeDecryptRadioBtn.Location = new Point(6, 47);
            EncryptionModeDecryptRadioBtn.Name = "EncryptionModeDecryptRadioBtn";
            EncryptionModeDecryptRadioBtn.Size = new Size(102, 19);
            EncryptionModeDecryptRadioBtn.TabIndex = 1;
            EncryptionModeDecryptRadioBtn.TabStop = true;
            EncryptionModeDecryptRadioBtn.Text = "Decode";
            EncryptionModeDecryptRadioBtn.UseVisualStyleBackColor = true;
            // 
            // EncryptionModeEncryptRadioBtn
            // 
            EncryptionModeEncryptRadioBtn.AutoSize = true;
            EncryptionModeEncryptRadioBtn.Location = new Point(6, 22);
            EncryptionModeEncryptRadioBtn.Name = "EncryptionModeEncryptRadioBtn";
            EncryptionModeEncryptRadioBtn.Size = new Size(89, 19);
            EncryptionModeEncryptRadioBtn.TabIndex = 0;
            EncryptionModeEncryptRadioBtn.TabStop = true;
            EncryptionModeEncryptRadioBtn.Text = "Encode";
            EncryptionModeEncryptRadioBtn.UseVisualStyleBackColor = true;
            // 
            // ApplyDESBtn
            // 
            ApplyDESBtn.Location = new Point(126, 205);
            ApplyDESBtn.Name = "ApplyDESBtn";
            ApplyDESBtn.Size = new Size(89, 23);
            ApplyDESBtn.TabIndex = 8;
            ApplyDESBtn.Text = "Apply!";
            ApplyDESBtn.UseVisualStyleBackColor = true;
            ApplyDESBtn.Click += ApplyDESBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(336, 255);
            Controls.Add(ApplyDESBtn);
            Controls.Add(EncryptionModeGroupbox);
            Controls.Add(EnterKeyLabel);
            Controls.Add(EnterKeyTextbox);
            Controls.Add(OpenSourceFileLabel);
            Controls.Add(OpenSourceFileBtn);
            Name = "Form1";
            Text = "DES algorithm";
            EncryptionModeGroupbox.ResumeLayout(false);
            EncryptionModeGroupbox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button OpenSourceFileBtn;
        private Label OpenSourceFileLabel;
        private TextBox EnterKeyTextbox;
        private Label EnterKeyLabel;
        private GroupBox EncryptionModeGroupbox;
        private RadioButton EncryptionModeEncryptRadioBtn;
        private RadioButton EncryptionModeDecryptRadioBtn;
        private Button ApplyDESBtn;
    }
}