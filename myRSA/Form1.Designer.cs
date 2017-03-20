namespace myRSA
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
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnClearPlain = new System.Windows.Forms.Button();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.btnClearCipher = new System.Windows.Forms.Button();
            this.txtPlain = new System.Windows.Forms.TextBox();
            this.txtCipher = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(38, 37);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(75, 23);
            this.btnEncrypt.TabIndex = 0;
            this.btnEncrypt.Text = "Encrypt";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // btnClearPlain
            // 
            this.btnClearPlain.Location = new System.Drawing.Point(250, 37);
            this.btnClearPlain.Name = "btnClearPlain";
            this.btnClearPlain.Size = new System.Drawing.Size(75, 23);
            this.btnClearPlain.TabIndex = 1;
            this.btnClearPlain.Text = "Clear";
            this.btnClearPlain.UseVisualStyleBackColor = true;
            this.btnClearPlain.Click += new System.EventHandler(this.btnClearPlain_Click);
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(524, 37);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(75, 23);
            this.btnDecrypt.TabIndex = 2;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            // 
            // btnClearCipher
            // 
            this.btnClearCipher.Location = new System.Drawing.Point(736, 37);
            this.btnClearCipher.Name = "btnClearCipher";
            this.btnClearCipher.Size = new System.Drawing.Size(75, 23);
            this.btnClearCipher.TabIndex = 3;
            this.btnClearCipher.Text = "Clear";
            this.btnClearCipher.UseVisualStyleBackColor = true;
            this.btnClearCipher.Click += new System.EventHandler(this.btnClearCipher_Click);
            // 
            // txtPlain
            // 
            this.txtPlain.Location = new System.Drawing.Point(38, 101);
            this.txtPlain.Multiline = true;
            this.txtPlain.Name = "txtPlain";
            this.txtPlain.Size = new System.Drawing.Size(287, 308);
            this.txtPlain.TabIndex = 4;
            // 
            // txtCipher
            // 
            this.txtCipher.Location = new System.Drawing.Point(524, 101);
            this.txtCipher.Multiline = true;
            this.txtCipher.Name = "txtCipher";
            this.txtCipher.Size = new System.Drawing.Size(287, 308);
            this.txtCipher.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 459);
            this.Controls.Add(this.txtCipher);
            this.Controls.Add(this.txtPlain);
            this.Controls.Add(this.btnClearCipher);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.btnClearPlain);
            this.Controls.Add(this.btnEncrypt);
            this.Name = "Form1";
            this.Text = "Public Key Encryption";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnClearPlain;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.Button btnClearCipher;
        private System.Windows.Forms.TextBox txtPlain;
        private System.Windows.Forms.TextBox txtCipher;
    }
}

