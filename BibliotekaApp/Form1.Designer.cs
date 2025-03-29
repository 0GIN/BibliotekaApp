
namespace BibliotekaApp
{
    partial class AddUserForm
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
        public void InitializeComponent()
        {
            txtLogin = new TextBox();
            LoginLabel = new Label();
            ImieLabel = new Label();
            NazwiskoLabel = new Label();
            MiastoLabel = new Label();
            KodPocztowyLabel = new Label();
            UlicaLabel = new Label();
            NumerUlicyLabel = new Label();
            NumerLokaluLabel = new Label();
            PeselLabel = new Label();
            DataUrodzeniaLabel = new Label();
            PlecLabel = new Label();
            EmailLabel = new Label();
            TelefonLabel = new Label();
            ZapomnianyLabel = new Label();
            txtName = new TextBox();
            txtSurname = new TextBox();
            txtCity = new TextBox();
            txtPostNumber = new TextBox();
            txtStreet = new TextBox();
            txtPropertyNumber = new TextBox();
            txtPesel = new TextBox();
            txtEmail = new TextBox();
            txtPhoneNumber = new TextBox();
            dtpDateOfBirth = new DateTimePicker();
            cmbSex = new ComboBox();
            chkForgotten = new CheckBox();
            nudApartmentNumber = new NumericUpDown();
            btnAddUser = new Button();
            btnDisplayUsers = new Button();
            listBoxUsers = new ListBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tabPage5 = new TabPage();
            dataGridView2 = new DataGridView();
            btnFindUserByLogin = new Button();
            txtUserLogin = new TextBox();
            tabPage3 = new TabPage();
            dataGridView1 = new DataGridView();
            btnFindUser = new Button();
            txtUserId = new TextBox();
            label1 = new Label();
            tabPage4 = new TabPage();
            btnForgetUser = new Button();
            txtForgetUserId = new TextBox();
            tabPage6 = new TabPage();
            dataGridViewForg = new DataGridView();
            btnFindForgottenUser = new Button();
            txtUserLoginForg = new TextBox();
            ((System.ComponentModel.ISupportInitialize)nudApartmentNumber).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tabPage4.SuspendLayout();
            tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewForg).BeginInit();
            SuspendLayout();
            // 
            // txtLogin
            // 
            txtLogin.Location = new Point(57, 12);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(240, 23);
            txtLogin.TabIndex = 1;
            txtLogin.TextChanged += txtLogin_TextChanged;
            // 
            // LoginLabel
            // 
            LoginLabel.AutoSize = true;
            LoginLabel.Location = new Point(11, 15);
            LoginLabel.Name = "LoginLabel";
            LoginLabel.Size = new Size(40, 15);
            LoginLabel.TabIndex = 2;
            LoginLabel.Text = "Login:";
            LoginLabel.Click += label2_Click;
            // 
            // ImieLabel
            // 
            ImieLabel.AutoSize = true;
            ImieLabel.Location = new Point(11, 39);
            ImieLabel.Name = "ImieLabel";
            ImieLabel.Size = new Size(33, 15);
            ImieLabel.TabIndex = 3;
            ImieLabel.Text = "Imię:";
            // 
            // NazwiskoLabel
            // 
            NazwiskoLabel.AutoSize = true;
            NazwiskoLabel.Location = new Point(11, 65);
            NazwiskoLabel.Name = "NazwiskoLabel";
            NazwiskoLabel.Size = new Size(60, 15);
            NazwiskoLabel.TabIndex = 4;
            NazwiskoLabel.Text = "Nazwisko:";
            // 
            // MiastoLabel
            // 
            MiastoLabel.AutoSize = true;
            MiastoLabel.Location = new Point(11, 89);
            MiastoLabel.Name = "MiastoLabel";
            MiastoLabel.Size = new Size(46, 15);
            MiastoLabel.TabIndex = 5;
            MiastoLabel.Text = "Miasto:";
            MiastoLabel.Click += label5_Click;
            // 
            // KodPocztowyLabel
            // 
            KodPocztowyLabel.AutoSize = true;
            KodPocztowyLabel.Location = new Point(11, 113);
            KodPocztowyLabel.Name = "KodPocztowyLabel";
            KodPocztowyLabel.Size = new Size(85, 15);
            KodPocztowyLabel.TabIndex = 6;
            KodPocztowyLabel.Text = "Kod pocztowy:";
            // 
            // UlicaLabel
            // 
            UlicaLabel.AutoSize = true;
            UlicaLabel.Location = new Point(11, 137);
            UlicaLabel.Name = "UlicaLabel";
            UlicaLabel.RightToLeft = RightToLeft.No;
            UlicaLabel.Size = new Size(36, 15);
            UlicaLabel.TabIndex = 7;
            UlicaLabel.Text = "Ulica:";
            // 
            // NumerUlicyLabel
            // 
            NumerUlicyLabel.AutoSize = true;
            NumerUlicyLabel.Location = new Point(11, 162);
            NumerUlicyLabel.Name = "NumerUlicyLabel";
            NumerUlicyLabel.Size = new Size(75, 15);
            NumerUlicyLabel.TabIndex = 8;
            NumerUlicyLabel.Text = "Numer ulicy:";
            // 
            // NumerLokaluLabel
            // 
            NumerLokaluLabel.AutoSize = true;
            NumerLokaluLabel.Location = new Point(11, 186);
            NumerLokaluLabel.Name = "NumerLokaluLabel";
            NumerLokaluLabel.Size = new Size(82, 15);
            NumerLokaluLabel.TabIndex = 9;
            NumerLokaluLabel.Text = "Numer lokalu:";
            // 
            // PeselLabel
            // 
            PeselLabel.AutoSize = true;
            PeselLabel.Location = new Point(11, 210);
            PeselLabel.Name = "PeselLabel";
            PeselLabel.Size = new Size(37, 15);
            PeselLabel.TabIndex = 10;
            PeselLabel.Text = "Pesel:";
            // 
            // DataUrodzeniaLabel
            // 
            DataUrodzeniaLabel.AutoSize = true;
            DataUrodzeniaLabel.Location = new Point(11, 235);
            DataUrodzeniaLabel.Name = "DataUrodzeniaLabel";
            DataUrodzeniaLabel.Size = new Size(89, 15);
            DataUrodzeniaLabel.TabIndex = 11;
            DataUrodzeniaLabel.Text = "Data urodzenia:";
            // 
            // PlecLabel
            // 
            PlecLabel.AutoSize = true;
            PlecLabel.Location = new Point(11, 259);
            PlecLabel.Name = "PlecLabel";
            PlecLabel.Size = new Size(29, 15);
            PlecLabel.TabIndex = 12;
            PlecLabel.Text = "Płeć";
            // 
            // EmailLabel
            // 
            EmailLabel.AutoSize = true;
            EmailLabel.Location = new Point(11, 283);
            EmailLabel.Name = "EmailLabel";
            EmailLabel.Size = new Size(39, 15);
            EmailLabel.TabIndex = 13;
            EmailLabel.Text = "Email:";
            // 
            // TelefonLabel
            // 
            TelefonLabel.AutoSize = true;
            TelefonLabel.Location = new Point(11, 307);
            TelefonLabel.Name = "TelefonLabel";
            TelefonLabel.Size = new Size(48, 15);
            TelefonLabel.TabIndex = 14;
            TelefonLabel.Text = "Telefon:";
            TelefonLabel.Click += label14_Click;
            // 
            // ZapomnianyLabel
            // 
            ZapomnianyLabel.AutoSize = true;
            ZapomnianyLabel.Location = new Point(11, 333);
            ZapomnianyLabel.Name = "ZapomnianyLabel";
            ZapomnianyLabel.Size = new Size(77, 15);
            ZapomnianyLabel.TabIndex = 15;
            ZapomnianyLabel.Text = "Zapomniany:";
            // 
            // txtName
            // 
            txtName.Location = new Point(50, 36);
            txtName.Name = "txtName";
            txtName.Size = new Size(247, 23);
            txtName.TabIndex = 16;
            // 
            // txtSurname
            // 
            txtSurname.Location = new Point(77, 62);
            txtSurname.Name = "txtSurname";
            txtSurname.Size = new Size(220, 23);
            txtSurname.TabIndex = 17;
            // 
            // txtCity
            // 
            txtCity.Location = new Point(63, 86);
            txtCity.Name = "txtCity";
            txtCity.Size = new Size(234, 23);
            txtCity.TabIndex = 18;
            txtCity.TextChanged += txtCity_TextChanged;
            // 
            // txtPostNumber
            // 
            txtPostNumber.Location = new Point(102, 110);
            txtPostNumber.Name = "txtPostNumber";
            txtPostNumber.Size = new Size(195, 23);
            txtPostNumber.TabIndex = 19;
            // 
            // txtStreet
            // 
            txtStreet.Location = new Point(53, 134);
            txtStreet.Name = "txtStreet";
            txtStreet.Size = new Size(244, 23);
            txtStreet.TabIndex = 20;
            // 
            // txtPropertyNumber
            // 
            txtPropertyNumber.Location = new Point(92, 159);
            txtPropertyNumber.Name = "txtPropertyNumber";
            txtPropertyNumber.Size = new Size(205, 23);
            txtPropertyNumber.TabIndex = 21;
            // 
            // txtPesel
            // 
            txtPesel.Location = new Point(50, 207);
            txtPesel.Name = "txtPesel";
            txtPesel.Size = new Size(247, 23);
            txtPesel.TabIndex = 23;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(53, 280);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(244, 23);
            txtEmail.TabIndex = 26;
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.Location = new Point(65, 304);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Size = new Size(232, 23);
            txtPhoneNumber.TabIndex = 27;
            // 
            // dtpDateOfBirth
            // 
            dtpDateOfBirth.Location = new Point(97, 231);
            dtpDateOfBirth.Name = "dtpDateOfBirth";
            dtpDateOfBirth.Size = new Size(200, 23);
            dtpDateOfBirth.TabIndex = 29;
            dtpDateOfBirth.ValueChanged += dtpDateOfBirth_ValueChanged;
            // 
            // cmbSex
            // 
            cmbSex.FormattingEnabled = true;
            cmbSex.Items.AddRange(new object[] { "K", "M" });
            cmbSex.Location = new Point(46, 256);
            cmbSex.Name = "cmbSex";
            cmbSex.Size = new Size(70, 23);
            cmbSex.TabIndex = 30;
            cmbSex.Text = "Wybierz";
            // 
            // chkForgotten
            // 
            chkForgotten.AutoSize = true;
            chkForgotten.Location = new Point(92, 332);
            chkForgotten.Name = "chkForgotten";
            chkForgotten.Size = new Size(15, 14);
            chkForgotten.TabIndex = 31;
            chkForgotten.UseVisualStyleBackColor = true;
            // 
            // nudApartmentNumber
            // 
            nudApartmentNumber.Location = new Point(99, 184);
            nudApartmentNumber.Name = "nudApartmentNumber";
            nudApartmentNumber.Size = new Size(82, 23);
            nudApartmentNumber.TabIndex = 32;
            // 
            // btnAddUser
            // 
            btnAddUser.Location = new Point(222, 365);
            btnAddUser.Name = "btnAddUser";
            btnAddUser.Size = new Size(75, 23);
            btnAddUser.TabIndex = 33;
            btnAddUser.Text = "DODAJ";
            btnAddUser.Click += btnAddUser_Click;
            // 
            // btnDisplayUsers
            // 
            btnDisplayUsers.Location = new Point(6, 6);
            btnDisplayUsers.Name = "btnDisplayUsers";
            btnDisplayUsers.Size = new Size(75, 23);
            btnDisplayUsers.TabIndex = 34;
            btnDisplayUsers.Text = "Wyświetl użytkowników";
            btnDisplayUsers.UseVisualStyleBackColor = true;
            btnDisplayUsers.Click += btnDisplayUsers_Clickkk;
            // 
            // listBoxUsers
            // 
            listBoxUsers.FormattingEnabled = true;
            listBoxUsers.Location = new Point(6, 33);
            listBoxUsers.Name = "listBoxUsers";
            listBoxUsers.Size = new Size(299, 364);
            listBoxUsers.TabIndex = 35;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage5);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage6);
            tabControl1.Location = new Point(3, 7);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(696, 431);
            tabControl1.TabIndex = 36;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(txtPostNumber);
            tabPage1.Controls.Add(btnAddUser);
            tabPage1.Controls.Add(txtLogin);
            tabPage1.Controls.Add(nudApartmentNumber);
            tabPage1.Controls.Add(LoginLabel);
            tabPage1.Controls.Add(chkForgotten);
            tabPage1.Controls.Add(ImieLabel);
            tabPage1.Controls.Add(cmbSex);
            tabPage1.Controls.Add(NazwiskoLabel);
            tabPage1.Controls.Add(dtpDateOfBirth);
            tabPage1.Controls.Add(MiastoLabel);
            tabPage1.Controls.Add(txtPhoneNumber);
            tabPage1.Controls.Add(KodPocztowyLabel);
            tabPage1.Controls.Add(txtEmail);
            tabPage1.Controls.Add(UlicaLabel);
            tabPage1.Controls.Add(txtPesel);
            tabPage1.Controls.Add(NumerUlicyLabel);
            tabPage1.Controls.Add(txtPropertyNumber);
            tabPage1.Controls.Add(NumerLokaluLabel);
            tabPage1.Controls.Add(txtStreet);
            tabPage1.Controls.Add(PeselLabel);
            tabPage1.Controls.Add(DataUrodzeniaLabel);
            tabPage1.Controls.Add(txtCity);
            tabPage1.Controls.Add(PlecLabel);
            tabPage1.Controls.Add(txtSurname);
            tabPage1.Controls.Add(EmailLabel);
            tabPage1.Controls.Add(txtName);
            tabPage1.Controls.Add(TelefonLabel);
            tabPage1.Controls.Add(ZapomnianyLabel);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(688, 403);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Dodaj";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(listBoxUsers);
            tabPage2.Controls.Add(btnDisplayUsers);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(688, 403);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Lista";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            tabPage5.Controls.Add(dataGridView2);
            tabPage5.Controls.Add(btnFindUserByLogin);
            tabPage5.Controls.Add(txtUserLogin);
            tabPage5.Location = new Point(4, 24);
            tabPage5.Name = "tabPage5";
            tabPage5.Size = new Size(688, 403);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "Wyszukaj Login";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(5, 41);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(680, 359);
            dataGridView2.TabIndex = 2;
            // 
            // btnFindUserByLogin
            // 
            btnFindUserByLogin.Location = new Point(132, 12);
            btnFindUserByLogin.Name = "btnFindUserByLogin";
            btnFindUserByLogin.Size = new Size(75, 23);
            btnFindUserByLogin.TabIndex = 1;
            btnFindUserByLogin.Text = "button1";
            btnFindUserByLogin.UseVisualStyleBackColor = true;
            btnFindUserByLogin.Click += btnFindUserByLogin_Click;
            // 
            // txtUserLogin
            // 
            txtUserLogin.Location = new Point(14, 12);
            txtUserLogin.Name = "txtUserLogin";
            txtUserLogin.Size = new Size(100, 23);
            txtUserLogin.TabIndex = 0;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(dataGridView1);
            tabPage3.Controls.Add(btnFindUser);
            tabPage3.Controls.Add(txtUserId);
            tabPage3.Controls.Add(label1);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(688, 403);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Wyszukaj ID";
            tabPage3.UseVisualStyleBackColor = true;
            tabPage3.Click += tabPage3_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(5, 35);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(680, 365);
            dataGridView1.TabIndex = 4;
            // 
            // btnFindUser
            // 
            btnFindUser.Location = new Point(207, 6);
            btnFindUser.Name = "btnFindUser";
            btnFindUser.Size = new Size(75, 23);
            btnFindUser.TabIndex = 2;
            btnFindUser.Text = "button1";
            btnFindUser.UseVisualStyleBackColor = true;
            btnFindUser.Click += btnFindUser_Click;
            // 
            // txtUserId
            // 
            txtUserId.Location = new Point(101, 6);
            txtUserId.Name = "txtUserId";
            txtUserId.Size = new Size(100, 23);
            txtUserId.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(5, 9);
            label1.Name = "label1";
            label1.Size = new Size(90, 15);
            label1.TabIndex = 0;
            label1.Text = "Wyszukaj po ID:";
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(btnForgetUser);
            tabPage4.Controls.Add(txtForgetUserId);
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Size = new Size(688, 403);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Zapomnij";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnForgetUser
            // 
            btnForgetUser.Location = new Point(172, 31);
            btnForgetUser.Name = "btnForgetUser";
            btnForgetUser.Size = new Size(75, 23);
            btnForgetUser.TabIndex = 1;
            btnForgetUser.Text = "button1";
            btnForgetUser.UseVisualStyleBackColor = true;
            btnForgetUser.Click += btnForgetUser_Click;
            // 
            // txtForgetUserId
            // 
            txtForgetUserId.Location = new Point(41, 32);
            txtForgetUserId.Name = "txtForgetUserId";
            txtForgetUserId.Size = new Size(100, 23);
            txtForgetUserId.TabIndex = 0;
            // 
            // tabPage6
            // 
            tabPage6.Controls.Add(dataGridViewForg);
            tabPage6.Controls.Add(btnFindForgottenUser);
            tabPage6.Controls.Add(txtUserLoginForg);
            tabPage6.Location = new Point(4, 24);
            tabPage6.Name = "tabPage6";
            tabPage6.Size = new Size(688, 403);
            tabPage6.TabIndex = 5;
            tabPage6.Text = "Zapomniani ";
            tabPage6.UseVisualStyleBackColor = true;
            // 
            // dataGridViewForg
            // 
            dataGridViewForg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewForg.Location = new Point(5, 44);
            dataGridViewForg.Name = "dataGridViewForg";
            dataGridViewForg.Size = new Size(680, 356);
            dataGridViewForg.TabIndex = 2;
            // 
            // btnFindForgottenUser
            // 
            btnFindForgottenUser.Location = new Point(157, 3);
            btnFindForgottenUser.Name = "btnFindForgottenUser";
            btnFindForgottenUser.Size = new Size(75, 23);
            btnFindForgottenUser.TabIndex = 1;
            btnFindForgottenUser.Text = "button1";
            btnFindForgottenUser.UseVisualStyleBackColor = true;
            btnFindForgottenUser.Click += btnFindForgottenUser_Click;
            // 
            // txtUserLoginForg
            // 
            txtUserLoginForg.Location = new Point(39, 3);
            txtUserLoginForg.Name = "txtUserLoginForg";
            txtUserLoginForg.Size = new Size(100, 23);
            txtUserLoginForg.TabIndex = 0;
            // 
            // AddUserForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(711, 450);
            Controls.Add(tabControl1);
            Name = "AddUserForm";
            Text = "Dodaj użytkownika";
            Load += AddUserForm_Load;
            ((System.ComponentModel.ISupportInitialize)nudApartmentNumber).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage5.ResumeLayout(false);
            tabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            tabPage6.ResumeLayout(false);
            tabPage6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewForg).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label NumerUlicyLabel;
        private Label NumerLokaluLabel;
        private Label DataUrodzeniaLabel;
        private Label ZapomnianyLabel;
        private TextBox txtStreet;
        private TextBox txtPropertyNumber;
        private TextBox txtPesel;
        private TextBox txtEmail;
        private TextBox txtPhoneNumber;
        private DateTimePicker dtpDateOfBirth;
        private ComboBox cmbSex;
        private CheckBox chkForgotten;
        private NumericUpDown nudApartmentNumber;
        private Button btnAddUser;
        // Ensure the correct line is present
        private Button btnDisplayUsers;
        private TextBox txtPostNumber;
        private TextBox txtCity;
        private TextBox txtSurname;
        private TextBox txtName;
        private Label TelefonLabel;
        private Label EmailLabel;
        private Label PlecLabel;
        private Label PeselLabel;
        private Label UlicaLabel;
        private Label KodPocztowyLabel;
        private Label MiastoLabel;
        private Label NazwiskoLabel;
        private Label ImieLabel;
        private Label LoginLabel;
        private TextBox txtLogin;
        //private TabControl Lista;
        private ListBox listBoxUsers;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private Button btnFindUser;
        private TextBox txtUserId;
        private Label label1;
        private DataGridView dataGridView1;
        private TabPage tabPage4;
        private Button btnForgetUser;
        private TextBox txtForgetUserId;
        private TabPage tabPage5;
        private TextBox txtUserLogin;
        private DataGridView dataGridView2;
        private Button btnFindUserByLogin;
        private TabPage tabPage6;
        private DataGridView dataGridViewForg;
        private Button btnFindForgottenUser;
        private TextBox txtUserLoginForg;
    }
}
