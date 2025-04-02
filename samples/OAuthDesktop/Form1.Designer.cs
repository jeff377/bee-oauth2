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
            btnFacebook = new Button();
            btnLine = new Button();
            btnAzure = new Button();
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
            // btnFacebook
            // 
            btnFacebook.Location = new Point(93, 255);
            btnFacebook.Name = "btnFacebook";
            btnFacebook.Size = new Size(75, 23);
            btnFacebook.TabIndex = 2;
            btnFacebook.Text = "Facebook";
            btnFacebook.UseVisualStyleBackColor = true;
            btnFacebook.Click += this.btnFacebook_Click;
            // 
            // btnLine
            // 
            btnLine.Location = new Point(174, 255);
            btnLine.Name = "btnLine";
            btnLine.Size = new Size(75, 23);
            btnLine.TabIndex = 3;
            btnLine.Text = "LINE";
            btnLine.UseVisualStyleBackColor = true;
            btnLine.Click += this.btnLine_Click;
            // 
            // btnAzure
            // 
            btnAzure.Location = new Point(255, 255);
            btnAzure.Name = "btnAzure";
            btnAzure.Size = new Size(75, 23);
            btnAzure.TabIndex = 4;
            btnAzure.Text = "Azure";
            btnAzure.UseVisualStyleBackColor = true;
            btnAzure.Click += this.btnAzure_Click;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(505, 290);
            this.Controls.Add(btnAzure);
            this.Controls.Add(btnLine);
            this.Controls.Add(btnFacebook);
            this.Controls.Add(edtUserInfo);
            this.Controls.Add(btnGoogle);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += this.Form1_Load;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Button btnGoogle;
        private TextBox edtUserInfo;
        private Button btnFacebook;
        private Button btnLine;
        private Button btnAzure;
    }
}
