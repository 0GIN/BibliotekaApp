
namespace BibliotekaApp
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
        public void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
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
            nudApartmentNumber = new NumericUpDown();
            btnAddUser = new Button();
            btnDisplayUsers = new Button();
            tabControl1 = new TabControl();
            Wypożycz = new TabPage();
            Dodaj = new TabPage();
            txtHaslo = new TextBox();
            label3 = new Label();
            Lista = new TabPage();
            btnFindUserByLogin = new Button();
            txtUserLogin = new TextBox();
            label4 = new Label();
            dataGridViewUsers = new DataGridView();
            Zapomnij = new TabPage();
            label17 = new Label();
            btnForgetUser = new Button();
            txtForgetUserId = new TextBox();
            Zapomniani = new TabPage();
            label2 = new Label();
            dataGridViewForg = new DataGridView();
            btnFindForgottenUser = new Button();
            Edytuj = new TabPage();
            label1 = new Label();
            comboBoxAccessLevel = new ComboBox();
            btnSearch = new Button();
            dataGridViewUser = new DataGridView();
            txtUserIdEdit = new TextBox();
            label15 = new Label();
            btnSave = new Button();
            tabUprawnienia = new TabPage();
            label5 = new Label();
            checkedListBoxUprawnienia = new CheckedListBox();
            btnSavePermissions = new Button();
            comboBoxAccessLevels = new ComboBox();
            logoutbtn = new Button();
            labelLoggedUser = new Label();
            ((System.ComponentModel.ISupportInitialize)nudApartmentNumber).BeginInit();
            tabControl1.SuspendLayout();
            Dodaj.SuspendLayout();
            Lista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewUsers).BeginInit();
            Zapomnij.SuspendLayout();
            Zapomniani.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewForg).BeginInit();
            Edytuj.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewUser).BeginInit();
            tabUprawnienia.SuspendLayout();
            SuspendLayout();
            // 
            // txtLogin
            // 
            resources.ApplyResources(txtLogin, "txtLogin");
            txtLogin.Name = "txtLogin";
            // 
            // LoginLabel
            // 
            resources.ApplyResources(LoginLabel, "LoginLabel");
            LoginLabel.Name = "LoginLabel";
            // 
            // ImieLabel
            // 
            resources.ApplyResources(ImieLabel, "ImieLabel");
            ImieLabel.Name = "ImieLabel";
            // 
            // NazwiskoLabel
            // 
            resources.ApplyResources(NazwiskoLabel, "NazwiskoLabel");
            NazwiskoLabel.Name = "NazwiskoLabel";
            // 
            // MiastoLabel
            // 
            resources.ApplyResources(MiastoLabel, "MiastoLabel");
            MiastoLabel.Name = "MiastoLabel";
            // 
            // KodPocztowyLabel
            // 
            resources.ApplyResources(KodPocztowyLabel, "KodPocztowyLabel");
            KodPocztowyLabel.Name = "KodPocztowyLabel";
            // 
            // UlicaLabel
            // 
            resources.ApplyResources(UlicaLabel, "UlicaLabel");
            UlicaLabel.Name = "UlicaLabel";
            // 
            // NumerUlicyLabel
            // 
            resources.ApplyResources(NumerUlicyLabel, "NumerUlicyLabel");
            NumerUlicyLabel.Name = "NumerUlicyLabel";
            // 
            // NumerLokaluLabel
            // 
            resources.ApplyResources(NumerLokaluLabel, "NumerLokaluLabel");
            NumerLokaluLabel.Name = "NumerLokaluLabel";
            // 
            // PeselLabel
            // 
            resources.ApplyResources(PeselLabel, "PeselLabel");
            PeselLabel.Name = "PeselLabel";
            // 
            // DataUrodzeniaLabel
            // 
            resources.ApplyResources(DataUrodzeniaLabel, "DataUrodzeniaLabel");
            DataUrodzeniaLabel.Name = "DataUrodzeniaLabel";
            // 
            // PlecLabel
            // 
            resources.ApplyResources(PlecLabel, "PlecLabel");
            PlecLabel.Name = "PlecLabel";
            // 
            // EmailLabel
            // 
            resources.ApplyResources(EmailLabel, "EmailLabel");
            EmailLabel.Name = "EmailLabel";
            // 
            // TelefonLabel
            // 
            resources.ApplyResources(TelefonLabel, "TelefonLabel");
            TelefonLabel.Name = "TelefonLabel";
            // 
            // txtName
            // 
            resources.ApplyResources(txtName, "txtName");
            txtName.Name = "txtName";
            // 
            // txtSurname
            // 
            resources.ApplyResources(txtSurname, "txtSurname");
            txtSurname.Name = "txtSurname";
            // 
            // txtCity
            // 
            resources.ApplyResources(txtCity, "txtCity");
            txtCity.Name = "txtCity";
            // 
            // txtPostNumber
            // 
            resources.ApplyResources(txtPostNumber, "txtPostNumber");
            txtPostNumber.Name = "txtPostNumber";
            // 
            // txtStreet
            // 
            resources.ApplyResources(txtStreet, "txtStreet");
            txtStreet.Name = "txtStreet";
            // 
            // txtPropertyNumber
            // 
            resources.ApplyResources(txtPropertyNumber, "txtPropertyNumber");
            txtPropertyNumber.Name = "txtPropertyNumber";
            // 
            // txtPesel
            // 
            resources.ApplyResources(txtPesel, "txtPesel");
            txtPesel.Name = "txtPesel";
            // 
            // txtEmail
            // 
            resources.ApplyResources(txtEmail, "txtEmail");
            txtEmail.Name = "txtEmail";
            // 
            // txtPhoneNumber
            // 
            resources.ApplyResources(txtPhoneNumber, "txtPhoneNumber");
            txtPhoneNumber.Name = "txtPhoneNumber";
            // 
            // dtpDateOfBirth
            // 
            resources.ApplyResources(dtpDateOfBirth, "dtpDateOfBirth");
            dtpDateOfBirth.Name = "dtpDateOfBirth";
            // 
            // cmbSex
            // 
            resources.ApplyResources(cmbSex, "cmbSex");
            cmbSex.FormattingEnabled = true;
            cmbSex.Items.AddRange(new object[] { resources.GetString("cmbSex.Items"), resources.GetString("cmbSex.Items1") });
            cmbSex.Name = "cmbSex";
            // 
            // nudApartmentNumber
            // 
            resources.ApplyResources(nudApartmentNumber, "nudApartmentNumber");
            nudApartmentNumber.Name = "nudApartmentNumber";
            // 
            // btnAddUser
            // 
            resources.ApplyResources(btnAddUser, "btnAddUser");
            btnAddUser.Name = "btnAddUser";
            btnAddUser.Click += btnAddUser_Click;
            // 
            // btnDisplayUsers
            // 
            resources.ApplyResources(btnDisplayUsers, "btnDisplayUsers");
            btnDisplayUsers.Name = "btnDisplayUsers";
            btnDisplayUsers.UseVisualStyleBackColor = true;
            btnDisplayUsers.Click += btnDisplayUsers_Clickkk;
            // 
            // tabControl1
            // 
            resources.ApplyResources(tabControl1, "tabControl1");
            tabControl1.Controls.Add(Wypożycz);
            tabControl1.Controls.Add(Dodaj);
            tabControl1.Controls.Add(Lista);
            tabControl1.Controls.Add(Zapomnij);
            tabControl1.Controls.Add(Zapomniani);
            tabControl1.Controls.Add(Edytuj);
            tabControl1.Controls.Add(tabUprawnienia);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            // 
            // Wypożycz
            // 
            resources.ApplyResources(Wypożycz, "Wypożycz");
            Wypożycz.Name = "Wypożycz";
            Wypożycz.UseVisualStyleBackColor = true;
            // 
            // Dodaj
            // 
            resources.ApplyResources(Dodaj, "Dodaj");
            Dodaj.BackColor = Color.Transparent;
            Dodaj.Controls.Add(txtHaslo);
            Dodaj.Controls.Add(label3);
            Dodaj.Controls.Add(txtPostNumber);
            Dodaj.Controls.Add(btnAddUser);
            Dodaj.Controls.Add(txtLogin);
            Dodaj.Controls.Add(nudApartmentNumber);
            Dodaj.Controls.Add(LoginLabel);
            Dodaj.Controls.Add(ImieLabel);
            Dodaj.Controls.Add(cmbSex);
            Dodaj.Controls.Add(NazwiskoLabel);
            Dodaj.Controls.Add(dtpDateOfBirth);
            Dodaj.Controls.Add(MiastoLabel);
            Dodaj.Controls.Add(txtPhoneNumber);
            Dodaj.Controls.Add(KodPocztowyLabel);
            Dodaj.Controls.Add(txtEmail);
            Dodaj.Controls.Add(UlicaLabel);
            Dodaj.Controls.Add(txtPesel);
            Dodaj.Controls.Add(NumerUlicyLabel);
            Dodaj.Controls.Add(txtPropertyNumber);
            Dodaj.Controls.Add(NumerLokaluLabel);
            Dodaj.Controls.Add(txtStreet);
            Dodaj.Controls.Add(PeselLabel);
            Dodaj.Controls.Add(DataUrodzeniaLabel);
            Dodaj.Controls.Add(txtCity);
            Dodaj.Controls.Add(PlecLabel);
            Dodaj.Controls.Add(txtSurname);
            Dodaj.Controls.Add(EmailLabel);
            Dodaj.Controls.Add(txtName);
            Dodaj.Controls.Add(TelefonLabel);
            Dodaj.Name = "Dodaj";
            // 
            // txtHaslo
            // 
            resources.ApplyResources(txtHaslo, "txtHaslo");
            txtHaslo.Name = "txtHaslo";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // Lista
            // 
            resources.ApplyResources(Lista, "Lista");
            Lista.Controls.Add(btnFindUserByLogin);
            Lista.Controls.Add(txtUserLogin);
            Lista.Controls.Add(label4);
            Lista.Controls.Add(dataGridViewUsers);
            Lista.Controls.Add(btnDisplayUsers);
            Lista.Name = "Lista";
            Lista.UseVisualStyleBackColor = true;
            // 
            // btnFindUserByLogin
            // 
            resources.ApplyResources(btnFindUserByLogin, "btnFindUserByLogin");
            btnFindUserByLogin.Name = "btnFindUserByLogin";
            btnFindUserByLogin.UseVisualStyleBackColor = true;
            btnFindUserByLogin.Click += btnFindUserByLogin_Click;
            // 
            // txtUserLogin
            // 
            resources.ApplyResources(txtUserLogin, "txtUserLogin");
            txtUserLogin.Name = "txtUserLogin";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // dataGridViewUsers
            // 
            resources.ApplyResources(dataGridViewUsers, "dataGridViewUsers");
            dataGridViewUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewUsers.Name = "dataGridViewUsers";
            // 
            // Zapomnij
            // 
            resources.ApplyResources(Zapomnij, "Zapomnij");
            Zapomnij.BorderStyle = BorderStyle.Fixed3D;
            Zapomnij.Controls.Add(label17);
            Zapomnij.Controls.Add(btnForgetUser);
            Zapomnij.Controls.Add(txtForgetUserId);
            Zapomnij.Name = "Zapomnij";
            Zapomnij.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            resources.ApplyResources(label17, "label17");
            label17.Name = "label17";
            // 
            // btnForgetUser
            // 
            resources.ApplyResources(btnForgetUser, "btnForgetUser");
            btnForgetUser.Name = "btnForgetUser";
            btnForgetUser.UseVisualStyleBackColor = true;
            btnForgetUser.Click += btnForgetUser_Click;
            // 
            // txtForgetUserId
            // 
            resources.ApplyResources(txtForgetUserId, "txtForgetUserId");
            txtForgetUserId.Name = "txtForgetUserId";
            // 
            // Zapomniani
            // 
            resources.ApplyResources(Zapomniani, "Zapomniani");
            Zapomniani.Controls.Add(label2);
            Zapomniani.Controls.Add(dataGridViewForg);
            Zapomniani.Controls.Add(btnFindForgottenUser);
            Zapomniani.Name = "Zapomniani";
            Zapomniani.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // dataGridViewForg
            // 
            resources.ApplyResources(dataGridViewForg, "dataGridViewForg");
            dataGridViewForg.AllowUserToAddRows = false;
            dataGridViewForg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewForg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewForg.Name = "dataGridViewForg";
            // 
            // btnFindForgottenUser
            // 
            resources.ApplyResources(btnFindForgottenUser, "btnFindForgottenUser");
            btnFindForgottenUser.Name = "btnFindForgottenUser";
            btnFindForgottenUser.UseVisualStyleBackColor = true;
            btnFindForgottenUser.Click += btnFindForgottenUser_Click;
            // 
            // Edytuj
            // 
            resources.ApplyResources(Edytuj, "Edytuj");
            Edytuj.Controls.Add(label1);
            Edytuj.Controls.Add(comboBoxAccessLevel);
            Edytuj.Controls.Add(btnSearch);
            Edytuj.Controls.Add(dataGridViewUser);
            Edytuj.Controls.Add(txtUserIdEdit);
            Edytuj.Controls.Add(label15);
            Edytuj.Controls.Add(btnSave);
            Edytuj.Name = "Edytuj";
            Edytuj.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // comboBoxAccessLevel
            // 
            resources.ApplyResources(comboBoxAccessLevel, "comboBoxAccessLevel");
            comboBoxAccessLevel.AutoCompleteCustomSource.AddRange(new string[] { resources.GetString("comboBoxAccessLevel.AutoCompleteCustomSource"), resources.GetString("comboBoxAccessLevel.AutoCompleteCustomSource1"), resources.GetString("comboBoxAccessLevel.AutoCompleteCustomSource2") });
            comboBoxAccessLevel.FormattingEnabled = true;
            comboBoxAccessLevel.Items.AddRange(new object[] { resources.GetString("comboBoxAccessLevel.Items"), resources.GetString("comboBoxAccessLevel.Items1"), resources.GetString("comboBoxAccessLevel.Items2") });
            comboBoxAccessLevel.Name = "comboBoxAccessLevel";
            comboBoxAccessLevel.SelectedIndexChanged += comboBoxAccessLevel_SelectedIndexChanged;
            // 
            // btnSearch
            // 
            resources.ApplyResources(btnSearch, "btnSearch");
            btnSearch.Name = "btnSearch";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // dataGridViewUser
            // 
            resources.ApplyResources(dataGridViewUser, "dataGridViewUser");
            dataGridViewUser.AllowUserToAddRows = false;
            dataGridViewUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewUser.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewUser.Name = "dataGridViewUser";
            // 
            // txtUserIdEdit
            // 
            resources.ApplyResources(txtUserIdEdit, "txtUserIdEdit");
            txtUserIdEdit.Name = "txtUserIdEdit";
            // 
            // label15
            // 
            resources.ApplyResources(label15, "label15");
            label15.Name = "label15";
            // 
            // btnSave
            // 
            resources.ApplyResources(btnSave, "btnSave");
            btnSave.Name = "btnSave";
            btnSave.Click += btnSave_Click;
            // 
            // tabUprawnienia
            // 
            resources.ApplyResources(tabUprawnienia, "tabUprawnienia");
            tabUprawnienia.Controls.Add(label5);
            tabUprawnienia.Controls.Add(checkedListBoxUprawnienia);
            tabUprawnienia.Controls.Add(btnSavePermissions);
            tabUprawnienia.Controls.Add(comboBoxAccessLevels);
            tabUprawnienia.Name = "tabUprawnienia";
            tabUprawnienia.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // checkedListBoxUprawnienia
            // 
            resources.ApplyResources(checkedListBoxUprawnienia, "checkedListBoxUprawnienia");
            checkedListBoxUprawnienia.FormattingEnabled = true;
            checkedListBoxUprawnienia.Name = "checkedListBoxUprawnienia";
            // 
            // btnSavePermissions
            // 
            resources.ApplyResources(btnSavePermissions, "btnSavePermissions");
            btnSavePermissions.Name = "btnSavePermissions";
            btnSavePermissions.UseVisualStyleBackColor = true;
            btnSavePermissions.Click += btnSavePermissions_Click;
            // 
            // comboBoxAccessLevels
            // 
            resources.ApplyResources(comboBoxAccessLevels, "comboBoxAccessLevels");
            comboBoxAccessLevels.FormattingEnabled = true;
            comboBoxAccessLevels.Items.AddRange(new object[] { resources.GetString("comboBoxAccessLevels.Items"), resources.GetString("comboBoxAccessLevels.Items1"), resources.GetString("comboBoxAccessLevels.Items2") });
            comboBoxAccessLevels.Name = "comboBoxAccessLevels";
            comboBoxAccessLevels.SelectedIndexChanged += comboBoxAccessLevels_SelectedIndexChanged;
            // 
            // logoutbtn
            // 
            resources.ApplyResources(logoutbtn, "logoutbtn");
            logoutbtn.Name = "logoutbtn";
            logoutbtn.UseVisualStyleBackColor = true;
            logoutbtn.Click += logoutbtn_Click;
            // 
            // labelLoggedUser
            // 
            resources.ApplyResources(labelLoggedUser, "labelLoggedUser");
            labelLoggedUser.Name = "labelLoggedUser";
            labelLoggedUser.Click += labelLoggedUser_Click;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            Controls.Add(labelLoggedUser);
            Controls.Add(logoutbtn);
            Controls.Add(tabControl1);
            ForeColor = Color.CornflowerBlue;
            Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)nudApartmentNumber).EndInit();
            tabControl1.ResumeLayout(false);
            Dodaj.ResumeLayout(false);
            Dodaj.PerformLayout();
            Lista.ResumeLayout(false);
            Lista.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewUsers).EndInit();
            Zapomnij.ResumeLayout(false);
            Zapomnij.PerformLayout();
            Zapomniani.ResumeLayout(false);
            Zapomniani.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewForg).EndInit();
            Edytuj.ResumeLayout(false);
            Edytuj.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewUser).EndInit();
            tabUprawnienia.ResumeLayout(false);
            tabUprawnienia.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private void StylizeDodajTab()
        {
            // Styl dla zakładki Dodaj
            Dodaj.BackColor = Color.White;

            // Styl dla wszystkich TextBoxów
            var textBoxes = new TextBox[] { txtLogin, txtHaslo, txtName, txtSurname, txtCity, txtPostNumber, txtStreet, txtPropertyNumber, txtPesel, txtEmail, txtPhoneNumber };
            foreach (var tb in textBoxes)
            {
                tb.BorderStyle = BorderStyle.FixedSingle;
                tb.BackColor = Color.WhiteSmoke;
                tb.Font = new Font("Segoe UI", 11);
                tb.Margin = new Padding(5);
            }

            // Styl dla NumericUpDown
            nudApartmentNumber.Font = new Font("Segoe UI", 11);
            nudApartmentNumber.BackColor = Color.WhiteSmoke;
            nudApartmentNumber.BorderStyle = BorderStyle.FixedSingle;

            // Styl dla ComboBox (Płeć)
            cmbSex.Font = new Font("Segoe UI", 11);
            cmbSex.BackColor = Color.WhiteSmoke;
            cmbSex.FlatStyle = FlatStyle.Flat;

            // Styl dla DateTimePicker
            dtpDateOfBirth.Font = new Font("Segoe UI", 11);
            dtpDateOfBirth.CalendarMonthBackground = Color.WhiteSmoke;
            dtpDateOfBirth.Format = DateTimePickerFormat.Short;

            // Styl dla Buttona Dodaj
            btnAddUser.FlatStyle = FlatStyle.Flat;
            btnAddUser.FlatAppearance.BorderSize = 0;
            btnAddUser.BackColor = Color.SteelBlue;
            btnAddUser.ForeColor = Color.White;
            btnAddUser.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            //btnAddUser.Padding = new Padding(10);
            btnAddUser.Cursor = Cursors.Hand;
            btnAddUser.Height = 25;
            btnAddUser.Width = 90;

            // Styl dla wszystkich Labeli
            var labels = new Label[] { LoginLabel, label3, ImieLabel, NazwiskoLabel, MiastoLabel, KodPocztowyLabel, UlicaLabel, NumerUlicyLabel, NumerLokaluLabel, PeselLabel, DataUrodzeniaLabel, PlecLabel, EmailLabel, TelefonLabel };
            foreach (var lbl in labels)
            {
                lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                lbl.ForeColor = Color.Black;
                lbl.Margin = new Padding(3);
            }
        }

        private void ApplyModernTheme()
        {         
            // Ustawienia ogólne
            this.BackColor = Color.WhiteSmoke;

            // TabControl
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(90, 30);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            foreach (TabPage tab in tabControl1.TabPages)
            {
                tab.BackColor = Color.White;
            }

            // Wszystkie przyciski
            foreach (Control control in this.Controls)
            {
                if (control is Button btn)
                {
                    StyleButton(btn);
                }
            }

            foreach (TabPage tab in tabControl1.TabPages)
            {
                foreach (Control control in tab.Controls)
                {
                    if (control is Button btn)
                    {
                        StyleButton(btn);
                    }
                }
            }

            // DataGridView style
            StyleDataGridView(dataGridViewUsers);
            StyleDataGridView(dataGridViewForg);
            StyleDataGridView(dataGridViewUser);
        }

        private void StyleButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.FromArgb(100, 149, 237); // CornflowerBlue lekko pastelowy
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.Height = 27;
        }

        private void StyleDataGridView(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Zmienna do przechowywania całkowitej szerokości kolumn dopasowanych do zawartości
            int totalColumnWidth = 0;
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                totalColumnWidth += column.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, false);
            }

            // Warunek: Jeżeli szerokość okna jest mniejsza niż szerokość kolumn, włączamy pasek przewijania
            if (this.Width < totalColumnWidth) // Jeśli okno jest węższe niż szerokość danych
            {
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // Kolumny nie dopasowują się do szerokości
                dgv.ScrollBars = ScrollBars.Both; // Pojawi się pasek przewijania
            }
            else // Okno jest wystarczająco szerokie
            {
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Kolumny rozciągają się na całą szerokość
                dgv.ScrollBars = ScrollBars.None; // Usuwamy pasek przewijania
            }

            dgv.GridColor = Color.LightGray;
            dgv.RowHeadersVisible = false;
        }

        private void CustomizeTabControl() // -- Ustawienia wyglądu zakładek (tabs) w TabControl
        {
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(90, 20);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += (sender, e) =>
            {
                TabPage page = tabControl1.TabPages[e.Index];
                Rectangle rect = tabControl1.GetTabRect(e.Index);

                using (Brush backBrush = new SolidBrush(Color.WhiteSmoke))
                using (Brush textBrush = new SolidBrush(Color.Black))
                {
                    e.Graphics.FillRectangle(backBrush, rect);
                    StringFormat sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    e.Graphics.DrawString(page.Text, new Font("Segoe UI", 10, FontStyle.Bold), textBrush, rect, sf);
                }
            };
        }
        

        #endregion

        private Label NumerUlicyLabel;
        private Label NumerLokaluLabel;
        private Label DataUrodzeniaLabel;
        private TextBox txtStreet;
        private TextBox txtPropertyNumber;
        private TextBox txtPesel;
        private TextBox txtEmail;
        private TextBox txtPhoneNumber;
        private DateTimePicker dtpDateOfBirth;
        private ComboBox cmbSex;
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
        private TabControl tabControl1;
        private TabPage Dodaj;
        private TabPage Lista;
        private TabPage Zapomnij;
        private Button btnForgetUser;
        private TextBox txtForgetUserId;
        private TabPage Zapomniani;
        private DataGridView dataGridViewForg;
        private Button btnFindForgottenUser;
        private TabPage Edytuj;
        private Button btnSave;
        private TextBox txtUserIdEdit;
        private Label label15;
        private Label label17;
        private DataGridView dataGridViewUser;
        private Button btnSearch;
        private Label label2;
        private DataGridView dataGridViewUsers;
        private TextBox txtUserLogin;
        private Label label3;
        private TextBox txtHaslo;
        private Label label4;
        private Button btnFindUserByLogin;
        private TabPage Wypożycz;
        private ComboBox comboBoxAccessLevel;
        private Label label1;
        private TabPage tabUprawnienia;
        private Button btnSavePermissions;
        private ComboBox comboBoxAccessLevels;
        private CheckedListBox checkedListBoxUprawnienia;
        private Label label5;
        private Button logoutbtn;
        private Label labelLoggedUser;
    }
}
