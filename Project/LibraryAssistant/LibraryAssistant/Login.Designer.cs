namespace LibraryAssistant
{
    partial class Login
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
            this.NewUsr_lbl = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.TextBox();
            this.uName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Login_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NewUsr_lbl
            // 
            this.NewUsr_lbl.AutoSize = true;
            this.NewUsr_lbl.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Underline);
            this.NewUsr_lbl.ForeColor = System.Drawing.Color.Blue;
            this.NewUsr_lbl.Location = new System.Drawing.Point(178, 76);
            this.NewUsr_lbl.Name = "NewUsr_lbl";
            this.NewUsr_lbl.Size = new System.Drawing.Size(85, 13);
            this.NewUsr_lbl.TabIndex = 11;
            this.NewUsr_lbl.Text = "لوحة تحكم المدير";
            this.NewUsr_lbl.Click += new System.EventHandler(this.NewUsr_lbl_Click);
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(14, 53);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '•';
            this.Password.Size = new System.Drawing.Size(249, 20);
            this.Password.TabIndex = 10;
            // 
            // uName
            // 
            this.uName.Location = new System.Drawing.Point(14, 13);
            this.uName.Name = "uName";
            this.uName.Size = new System.Drawing.Size(249, 20);
            this.uName.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(291, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "كلمة المرور";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(269, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "اسم المستخدم";
            // 
            // Login_btn
            // 
            this.Login_btn.Location = new System.Drawing.Point(128, 101);
            this.Login_btn.Name = "Login_btn";
            this.Login_btn.Size = new System.Drawing.Size(75, 23);
            this.Login_btn.TabIndex = 6;
            this.Login_btn.Text = "دخول";
            this.Login_btn.UseVisualStyleBackColor = true;
            this.Login_btn.Click += new System.EventHandler(this.Login_btn_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 140);
            this.Controls.Add(this.NewUsr_lbl);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.uName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Login_btn);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تسجيل الدخول";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label NewUsr_lbl;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.TextBox uName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Login_btn;
    }
}

