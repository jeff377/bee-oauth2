namespace OAuthWinForms
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGoogle = new System.Windows.Forms.Button();
            this.edtUserInfo = new System.Windows.Forms.TextBox();
            this.btnFacebook = new System.Windows.Forms.Button();
            this.btnLine = new System.Windows.Forms.Button();
            this.btnAzure = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGoogle
            // 
            this.btnGoogle.Location = new System.Drawing.Point(12, 255);
            this.btnGoogle.Name = "btnGoogle";
            this.btnGoogle.Size = new System.Drawing.Size(75, 23);
            this.btnGoogle.TabIndex = 0;
            this.btnGoogle.Text = "Google";
            this.btnGoogle.UseVisualStyleBackColor = true;
            this.btnGoogle.Click += new System.EventHandler(this.btnGoogle_Click);
            // 
            // edtUserInfo
            // 
            this.edtUserInfo.Location = new System.Drawing.Point(12, 12);
            this.edtUserInfo.Multiline = true;
            this.edtUserInfo.Name = "edtUserInfo";
            this.edtUserInfo.Size = new System.Drawing.Size(481, 237);
            this.edtUserInfo.TabIndex = 1;
            // 
            // btnFacebook
            // 
            this.btnFacebook.Location = new System.Drawing.Point(93, 255);
            this.btnFacebook.Name = "btnFacebook";
            this.btnFacebook.Size = new System.Drawing.Size(75, 23);
            this.btnFacebook.TabIndex = 2;
            this.btnFacebook.Text = "Facebook";
            this.btnFacebook.UseVisualStyleBackColor = true;
            this.btnFacebook.Click += new System.EventHandler(this.btnFacebook_Click);
            // 
            // btnLine
            // 
            this.btnLine.Location = new System.Drawing.Point(174, 255);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(75, 23);
            this.btnLine.TabIndex = 3;
            this.btnLine.Text = "LINE";
            this.btnLine.UseVisualStyleBackColor = true;
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // btnAzure
            // 
            this.btnAzure.Location = new System.Drawing.Point(255, 255);
            this.btnAzure.Name = "btnAzure";
            this.btnAzure.Size = new System.Drawing.Size(75, 23);
            this.btnAzure.TabIndex = 4;
            this.btnAzure.Text = "Azure";
            this.btnAzure.UseVisualStyleBackColor = true;
            this.btnAzure.Click += new System.EventHandler(this.btnAzure_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 290);
            this.Controls.Add(this.btnAzure);
            this.Controls.Add(this.btnLine);
            this.Controls.Add(this.btnFacebook);
            this.Controls.Add(this.edtUserInfo);
            this.Controls.Add(this.btnGoogle);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGoogle;
        private System.Windows.Forms.TextBox edtUserInfo;
        private System.Windows.Forms.Button btnFacebook;
        private System.Windows.Forms.Button btnLine;
        private System.Windows.Forms.Button btnAzure;
    }
}

