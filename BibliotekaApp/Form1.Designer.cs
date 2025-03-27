
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
            label1 = new Label();
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
            ((System.ComponentModel.ISupportInitialize)nudApartmentNumber).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(135, 15);
            label1.TabIndex = 0;
            label1.Text = "Dodawanie uzytkownika";
            // 
            // txtLogin
            // 
            txtLogin.Location = new Point(73, 55);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(240, 23);
            txtLogin.TabIndex = 1;
            txtLogin.TextChanged += txtLogin_TextChanged;
            // 
            // LoginLabel
            // 
            LoginLabel.AutoSize = true;
            LoginLabel.Location = new Point(27, 58);
            LoginLabel.Name = "LoginLabel";
            LoginLabel.Size = new Size(40, 15);
            LoginLabel.TabIndex = 2;
            LoginLabel.Text = "Login:";
            LoginLabel.Click += label2_Click;
            // 
            // ImieLabel
            // 
            ImieLabel.AutoSize = true;
            ImieLabel.Location = new Point(27, 82);
            ImieLabel.Name = "ImieLabel";
            ImieLabel.Size = new Size(33, 15);
            ImieLabel.TabIndex = 3;
            ImieLabel.Text = "Imię:";
            // 
            // NazwiskoLabel
            // 
            NazwiskoLabel.AutoSize = true;
            NazwiskoLabel.Location = new Point(27, 108);
            NazwiskoLabel.Name = "NazwiskoLabel";
            NazwiskoLabel.Size = new Size(60, 15);
            NazwiskoLabel.TabIndex = 4;
            NazwiskoLabel.Text = "Nazwisko:";
            // 
            // MiastoLabel
            // 
            MiastoLabel.AutoSize = true;
            MiastoLabel.Location = new Point(27, 132);
            MiastoLabel.Name = "MiastoLabel";
            MiastoLabel.Size = new Size(46, 15);
            MiastoLabel.TabIndex = 5;
            MiastoLabel.Text = "Miasto:";
            MiastoLabel.Click += label5_Click;
            // 
            // KodPocztowyLabel
            // 
            KodPocztowyLabel.AutoSize = true;
            KodPocztowyLabel.Location = new Point(27, 156);
            KodPocztowyLabel.Name = "KodPocztowyLabel";
            KodPocztowyLabel.Size = new Size(85, 15);
            KodPocztowyLabel.TabIndex = 6;
            KodPocztowyLabel.Text = "Kod pocztowy:";
            // 
            // UlicaLabel
            // 
            UlicaLabel.AutoSize = true;
            UlicaLabel.Location = new Point(27, 180);
            UlicaLabel.Name = "UlicaLabel";
            UlicaLabel.RightToLeft = RightToLeft.No;
            UlicaLabel.Size = new Size(36, 15);
            UlicaLabel.TabIndex = 7;
            UlicaLabel.Text = "Ulica:";
            // 
            // NumerUlicyLabel
            // 
            NumerUlicyLabel.AutoSize = true;
            NumerUlicyLabel.Location = new Point(27, 205);
            NumerUlicyLabel.Name = "NumerUlicyLabel";
            NumerUlicyLabel.Size = new Size(75, 15);
            NumerUlicyLabel.TabIndex = 8;
            NumerUlicyLabel.Text = "Numer ulicy:";
            // 
            // NumerLokaluLabel
            // 
            NumerLokaluLabel.AutoSize = true;
            NumerLokaluLabel.Location = new Point(27, 229);
            NumerLokaluLabel.Name = "NumerLokaluLabel";
            NumerLokaluLabel.Size = new Size(82, 15);
            NumerLokaluLabel.TabIndex = 9;
            NumerLokaluLabel.Text = "Numer lokalu:";
            // 
            // PeselLabel
            // 
            PeselLabel.AutoSize = true;
            PeselLabel.Location = new Point(27, 253);
            PeselLabel.Name = "PeselLabel";
            PeselLabel.Size = new Size(37, 15);
            PeselLabel.TabIndex = 10;
            PeselLabel.Text = "Pesel:";
            // 
            // DataUrodzeniaLabel
            // 
            DataUrodzeniaLabel.AutoSize = true;
            DataUrodzeniaLabel.Location = new Point(27, 278);
            DataUrodzeniaLabel.Name = "DataUrodzeniaLabel";
            DataUrodzeniaLabel.Size = new Size(89, 15);
            DataUrodzeniaLabel.TabIndex = 11;
            DataUrodzeniaLabel.Text = "Data urodzenia:";
            // 
            // PlecLabel
            // 
            PlecLabel.AutoSize = true;
            PlecLabel.Location = new Point(27, 302);
            PlecLabel.Name = "PlecLabel";
            PlecLabel.Size = new Size(29, 15);
            PlecLabel.TabIndex = 12;
            PlecLabel.Text = "Płeć";
            // 
            // EmailLabel
            // 
            EmailLabel.AutoSize = true;
            EmailLabel.Location = new Point(27, 326);
            EmailLabel.Name = "EmailLabel";
            EmailLabel.Size = new Size(39, 15);
            EmailLabel.TabIndex = 13;
            EmailLabel.Text = "Email:";
            // 
            // TelefonLabel
            // 
            TelefonLabel.AutoSize = true;
            TelefonLabel.Location = new Point(27, 350);
            TelefonLabel.Name = "TelefonLabel";
            TelefonLabel.Size = new Size(48, 15);
            TelefonLabel.TabIndex = 14;
            TelefonLabel.Text = "Telefon:";
            TelefonLabel.Click += label14_Click;
            // 
            // ZapomnianyLabel
            // 
            ZapomnianyLabel.AutoSize = true;
            ZapomnianyLabel.Location = new Point(27, 376);
            ZapomnianyLabel.Name = "ZapomnianyLabel";
            ZapomnianyLabel.Size = new Size(77, 15);
            ZapomnianyLabel.TabIndex = 15;
            ZapomnianyLabel.Text = "Zapomniany:";
            // 
            // txtName
            // 
            txtName.Location = new Point(66, 79);
            txtName.Name = "txtName";
            txtName.Size = new Size(247, 23);
            txtName.TabIndex = 16;
            // 
            // txtSurname
            // 
            txtSurname.Location = new Point(93, 105);
            txtSurname.Name = "txtSurname";
            txtSurname.Size = new Size(220, 23);
            txtSurname.TabIndex = 17;
            // 
            // txtCity
            // 
            txtCity.Location = new Point(79, 129);
            txtCity.Name = "txtCity";
            txtCity.Size = new Size(234, 23);
            txtCity.TabIndex = 18;
            txtCity.TextChanged += txtCity_TextChanged;
            // 
            // txtPostNumber
            // 
            txtPostNumber.Location = new Point(118, 153);
            txtPostNumber.Name = "txtPostNumber";
            txtPostNumber.Size = new Size(195, 23);
            txtPostNumber.TabIndex = 19;
            // 
            // txtStreet
            // 
            txtStreet.Location = new Point(69, 177);
            txtStreet.Name = "txtStreet";
            txtStreet.Size = new Size(244, 23);
            txtStreet.TabIndex = 20;
            // 
            // txtPropertyNumber
            // 
            txtPropertyNumber.Location = new Point(108, 202);
            txtPropertyNumber.Name = "txtPropertyNumber";
            txtPropertyNumber.Size = new Size(205, 23);
            txtPropertyNumber.TabIndex = 21;
            // 
            // txtPesel
            // 
            txtPesel.Location = new Point(66, 250);
            txtPesel.Name = "txtPesel";
            txtPesel.Size = new Size(247, 23);
            txtPesel.TabIndex = 23;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(69, 323);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(244, 23);
            txtEmail.TabIndex = 26;
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.Location = new Point(81, 347);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Size = new Size(232, 23);
            txtPhoneNumber.TabIndex = 27;
            // 
            // dtpDateOfBirth
            // 
            dtpDateOfBirth.Location = new Point(113, 274);
            dtpDateOfBirth.Name = "dtpDateOfBirth";
            dtpDateOfBirth.Size = new Size(200, 23);
            dtpDateOfBirth.TabIndex = 29;
            dtpDateOfBirth.ValueChanged += dtpDateOfBirth_ValueChanged;
            // 
            // cmbSex
            // 
            cmbSex.FormattingEnabled = true;
            cmbSex.Items.AddRange(new object[] { "K", "M" });
            cmbSex.Location = new Point(62, 299);
            cmbSex.Name = "cmbSex";
            cmbSex.Size = new Size(70, 23);
            cmbSex.TabIndex = 30;
            cmbSex.Text = "Wybierz";
            // 
            // chkForgotten
            // 
            chkForgotten.AutoSize = true;
            chkForgotten.Location = new Point(108, 375);
            chkForgotten.Name = "chkForgotten";
            chkForgotten.Size = new Size(15, 14);
            chkForgotten.TabIndex = 31;
            chkForgotten.UseVisualStyleBackColor = true;
            // 
            // nudApartmentNumber
            // 
            nudApartmentNumber.Location = new Point(115, 227);
            nudApartmentNumber.Name = "nudApartmentNumber";
            nudApartmentNumber.Size = new Size(82, 23);
            nudApartmentNumber.TabIndex = 32;
            // 
            // btnAddUser
            // 
            btnAddUser.Location = new Point(238, 408);
            btnAddUser.Name = "btnAddUser";
            btnAddUser.Size = new Size(75, 23);
            btnAddUser.TabIndex = 33;
            btnAddUser.Text = "DODAJ";
            btnAddUser.Click += btnAddUser_Click;
            // 
            // btnDisplayUsers_Clickkk
            // 
            btnDisplayUsers.Location = new Point(472, 95);
            btnDisplayUsers.Name = "btnDisplayUsers_Clickkk";
            btnDisplayUsers.Size = new Size(75, 23);
            btnDisplayUsers.TabIndex = 34;
            btnDisplayUsers.Text = "button1";
            btnDisplayUsers.UseVisualStyleBackColor = true;
            // 
            // listBoxUsers
            // 
            // btnDisplayUsers
            // 
            btnDisplayUsers.Location = new Point(472, 95);
            btnDisplayUsers.Name = "btnDisplayUsers";
            btnDisplayUsers.Size = new Size(75, 23);
            btnDisplayUsers.TabIndex = 34;
            // Remove the incorrect line
            // Controls.Add(btnDisplayUsers_Clickkk);

            // Add the correct line
            Controls.Add(btnDisplayUsers);
            btnDisplayUsers.Text = "Wyświetl użytkowników";
            btnDisplayUsers.UseVisualStyleBackColor = true;
            btnDisplayUsers.Click += btnDisplayUsers_Clickkk;
            // 
            listBoxUsers.FormattingEnabled = true;
            listBoxUsers.Location = new Point(468, 187);
            listBoxUsers.Name = "listBoxUsers";
            listBoxUsers.Size = new Size(298, 199);
            listBoxUsers.TabIndex = 35;
            // 
            // AddUserForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(858, 443);
            Controls.Add(listBoxUsers);
            Controls.Add(btnDisplayUsers);
            Controls.Add(btnAddUser);
            Controls.Add(nudApartmentNumber);
            Controls.Add(chkForgotten);
            Controls.Add(cmbSex);
            Controls.Add(dtpDateOfBirth);
            Controls.Add(txtPhoneNumber);
            Controls.Add(txtEmail);
            Controls.Add(txtPesel);
            Controls.Add(txtPropertyNumber);
            Controls.Add(txtStreet);
            Controls.Add(txtPostNumber);
            Controls.Add(txtCity);
            Controls.Add(txtSurname);
            Controls.Add(txtName);
            Controls.Add(ZapomnianyLabel);
            Controls.Add(TelefonLabel);
            Controls.Add(EmailLabel);
            Controls.Add(PlecLabel);
            Controls.Add(DataUrodzeniaLabel);
            Controls.Add(PeselLabel);
            Controls.Add(NumerLokaluLabel);
            Controls.Add(NumerUlicyLabel);
            Controls.Add(UlicaLabel);
            Controls.Add(KodPocztowyLabel);
            Controls.Add(MiastoLabel);
            Controls.Add(NazwiskoLabel);
            Controls.Add(ImieLabel);
            Controls.Add(LoginLabel);
            Controls.Add(txtLogin);
            Controls.Add(label1);
            Name = "AddUserForm";
            Text = "Dodaj użytkownika";
            Load += AddUserForm_Load;
            ((System.ComponentModel.ISupportInitialize)nudApartmentNumber).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private Label label1;
        private TextBox txtLogin;
        private TabControl Lista;
        private ListBox listBoxUsers;
    }
}
