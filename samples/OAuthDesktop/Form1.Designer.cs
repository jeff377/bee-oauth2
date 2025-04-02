namespace OAuthDesktop
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
            btnGoogle = new Button();
            edtUserInfo = new TextBox();
            this.SuspendLayout();
            // 
            // btnGoogle
            // 
            btnGoogle.Location = new Point(12, 254);
            btnGoogle.Name = "btnGoogle";
            btnGoogle.Size = new Size(75, 23);
            btnGoogle.TabIndex = 0;
            btnGoogle.Text = "Google";
            btnGoogle.UseVisualStyleBackColor = true;
            btnGoogle.Click += this.btnGoogle_Click;
            // 
            // edtUserInfo
            // 
            edtUserInfo.Location = new Point(12, 12);
            edtUserInfo.Multiline = true;
            edtUserInfo.Name = "edtUserInfo";
            edtUserInfo.Size = new Size(481, 236);
            edtUserInfo.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(505, 290);
            this.Controls.Add(edtUserInfo);
            this.Controls.Add(btnGoogle);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += this.Form1_Load;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Button btnGoogle;
        private TextBox edtUserInfo;
    }
}
