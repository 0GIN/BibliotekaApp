using SQLitePCL;

namespace BibliotekaApp
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            btnLogin = new Button();
            txtLogin = new TextBox();
            txtPassword = new TextBox();
            lblLogin = new Label();
            lblPassword = new Label();
            lvlRecover = new Label();
            SuspendLayout();
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.CornflowerBlue;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(186, 183);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(122, 35);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Zaloguj";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // txtLogin
            // 
            txtLogin.Location = new Point(40, 55);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(240, 25);
            txtLogin.TabIndex = 1;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(40, 120);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(240, 25);
            txtPassword.TabIndex = 3;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // lblLogin
            // 
            lblLogin.AutoSize = true;
            lblLogin.Location = new Point(40, 30);
            lblLogin.Name = "lblLogin";
            lblLogin.Size = new Size(46, 19);
            lblLogin.TabIndex = 0;
            lblLogin.Text = "Login:";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(40, 95);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(47, 19);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "Hasło:";
            // 
            // lvlRecover
            // 
            lvlRecover.AutoSize = true;
            lvlRecover.Cursor = Cursors.Hand;
            lvlRecover.Font = new Font("Segoe UI", 8F);
            lvlRecover.ForeColor = SystemColors.ButtonShadow;
            lvlRecover.Location = new Point(196, 148);
            lvlRecover.Name = "lvlRecover";
            lvlRecover.Size = new Size(84, 13);
            lvlRecover.TabIndex = 5;
            lvlRecover.Text = "Odzyskaj hasło";
            lvlRecover.Click += lvlRecover_Click_1;
            // 
            // LoginForm
            // 
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(320, 230);
            Controls.Add(lvlRecover);
            Controls.Add(lblLogin);
            Controls.Add(txtLogin);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(btnLogin);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Logowanie";
            Load += LoginForm_Load_1;
            ResumeLayout(false);
            PerformLayout();
        }




        #endregion

        private Button btnLogin;
        private TextBox txtLogin;
        private TextBox txtPassword;
        private Label lblLogin;
        private Label lblPassword;
        private Label lvlRecover;
    }


}