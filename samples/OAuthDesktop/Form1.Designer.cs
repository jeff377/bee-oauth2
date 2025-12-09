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
            btnAuth0 = new Button();
            btnOkta = new Button();
            SuspendLayout();
            // 
            // btnGoogle
            // 
            btnGoogle.Location = new Point(12, 254);
            btnGoogle.Name = "btnGoogle";
            btnGoogle.Size = new Size(75, 23);
            btnGoogle.TabIndex = 0;
            btnGoogle.Text = "Google";
            btnGoogle.UseVisualStyleBackColor = true;
            btnGoogle.Click += btnGoogle_Click;
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
            btnFacebook.Click += btnFacebook_Click;
            // 
            // btnLine
            // 
            btnLine.Location = new Point(174, 255);
            btnLine.Name = "btnLine";
            btnLine.Size = new Size(75, 23);
            btnLine.TabIndex = 3;
            btnLine.Text = "LINE";
            btnLine.UseVisualStyleBackColor = true;
            btnLine.Click += btnLine_Click;
            // 
            // btnAzure
            // 
            btnAzure.Location = new Point(255, 255);
            btnAzure.Name = "btnAzure";
            btnAzure.Size = new Size(75, 23);
            btnAzure.TabIndex = 4;
            btnAzure.Text = "Azure";
            btnAzure.UseVisualStyleBackColor = true;
            btnAzure.Click += btnAzure_Click;
            // 
            // btnAuth0
            // 
            btnAuth0.Location = new Point(336, 255);
            btnAuth0.Name = "btnAuth0";
            btnAuth0.Size = new Size(75, 23);
            btnAuth0.TabIndex = 5;
            btnAuth0.Text = "Auth0";
            btnAuth0.UseVisualStyleBackColor = true;
            btnAuth0.Click += btnAuth0_Click;
            //
            // btnOkta
            //
            btnOkta.Location = new Point(417, 255);
            btnOkta.Name = "btnOkta";
            btnOkta.Size = new Size(75, 23);
            btnOkta.TabIndex = 6;
            btnOkta.Text = "Okta";
            btnOkta.UseVisualStyleBackColor = true;
            btnOkta.Click += btnOkta_Click;
            //
            // Form1
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(505, 290);
            Controls.Add(btnOkta);
            Controls.Add(btnAuth0);
            Controls.Add(btnAzure);
            Controls.Add(btnLine);
            Controls.Add(btnFacebook);
            Controls.Add(edtUserInfo);
            Controls.Add(btnGoogle);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnGoogle;
        private TextBox edtUserInfo;
        private Button btnFacebook;
        private Button btnLine;
        private Button btnAzure;
        private Button btnAuth0;
        private Button btnOkta;
    }
}
