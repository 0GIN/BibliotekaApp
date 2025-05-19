namespace BibliotekaApp
{
    partial class UserProfileForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.labelName = new Label();
            this.labelSurname = new Label();
            this.labelCity = new Label();
            this.labelStreet = new Label();
            this.labelPostNumber = new Label();
            this.labelPropertyNumber = new Label();
            this.labelApartmentNumber = new Label();
            this.labelPesel = new Label();
            this.labelDateOfBirth = new Label();
            this.labelSex = new Label();
            this.labelEmail = new Label();
            this.labelPhone = new Label();
            this.labelLogin = new Label();

            this.txtName = new TextBox();
            this.txtSurname = new TextBox();
            this.txtCity = new TextBox();
            this.txtStreet = new TextBox();
            this.txtPostNumber = new TextBox();
            this.txtPropertyNumber = new TextBox();
            this.nudApartmentNumber = new NumericUpDown();
            this.txtPesel = new TextBox();
            this.dtpDateOfBirth = new DateTimePicker();
            this.cmbSex = new ComboBox();
            this.txtEmail = new TextBox();
            this.txtPhoneNumber = new TextBox();
            this.txtLogin = new TextBox();

            this.btnChangePassword = new Button();
            this.btnSave = new Button();
            this.btnCancel = new Button();

            this.Load += new System.EventHandler(this.UserProfileForm_Load);

            ((System.ComponentModel.ISupportInitialize)(this.nudApartmentNumber)).BeginInit();
            this.SuspendLayout();

            int labelX = 20, textBoxX = 180, spacingY = 30, currentY = 20;

            void SetupControl(Control label, Control input)
            {
                label.Location = new Point(labelX, currentY);
                label.Size = new Size(150, 23);
                input.Location = new Point(textBoxX, currentY);
                input.Size = new Size(200, 23);
                currentY += spacingY;
            }

            SetupControl(labelLogin, txtLogin);
            txtLogin.ReadOnly = true;
            labelLogin.Text = "Login:";

            SetupControl(labelName, txtName);
            labelName.Text = "Imię:";

            SetupControl(labelSurname, txtSurname);
            labelSurname.Text = "Nazwisko:";

            SetupControl(labelCity, txtCity);
            labelCity.Text = "Miasto:";

            SetupControl(labelStreet, txtStreet);
            labelStreet.Text = "Ulica:";

            SetupControl(labelPostNumber, txtPostNumber);
            labelPostNumber.Text = "Kod pocztowy:";

            SetupControl(labelPropertyNumber, txtPropertyNumber);
            labelPropertyNumber.Text = "Nr budynku:";

            SetupControl(labelApartmentNumber, nudApartmentNumber);
            labelApartmentNumber.Text = "Nr mieszkania:";

            SetupControl(labelPesel, txtPesel);
            labelPesel.Text = "PESEL:";

            SetupControl(labelDateOfBirth, dtpDateOfBirth);
            labelDateOfBirth.Text = "Data urodzenia:";

            SetupControl(labelSex, cmbSex);
            labelSex.Text = "Płeć:";
            cmbSex.Items.AddRange(new object[] { "M", "K" });

            SetupControl(labelEmail, txtEmail);
            labelEmail.Text = "E-mail:";

            SetupControl(labelPhone, txtPhoneNumber);
            labelPhone.Text = "Telefon:";

            // Buttons
            btnChangePassword.Text = "Zmień hasło";
            btnChangePassword.Location = new Point(textBoxX, currentY);
            btnChangePassword.Size = new Size(200, 30);
            btnChangePassword.Click += btnChangePassword_Click;
            btnChangePassword.Click += new EventHandler(btnChangePassword_Click);

            currentY += spacingY + 10;

            btnSave.Text = "Zapisz zmiany";
            btnSave.Location = new Point(textBoxX, currentY);
            btnSave.Size = new Size(95, 30);
            btnSave.Click += btnSave_Click;

            btnCancel.Text = "Anuluj";
            btnCancel.Location = new Point(textBoxX + 105, currentY);
            btnCancel.Size = new Size(95, 30);
            btnCancel.Click += btnCancel_Click;

            // Add controls to form
            this.Controls.AddRange(new Control[]
            {
                labelLogin, txtLogin,
                labelName, txtName,
                labelSurname, txtSurname,
                labelCity, txtCity,
                labelStreet, txtStreet,
                labelPostNumber, txtPostNumber,
                labelPropertyNumber, txtPropertyNumber,
                labelApartmentNumber, nudApartmentNumber,
                labelPesel, txtPesel,
                labelDateOfBirth, dtpDateOfBirth,
                labelSex, cmbSex,
                labelEmail, txtEmail,
                labelPhone, txtPhoneNumber,
                btnChangePassword,
                btnSave, btnCancel
            });

            // Final form setup
            this.Text = "Twoje dane";
            this.ClientSize = new Size(420, currentY + 60);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            ((System.ComponentModel.ISupportInitialize)(this.nudApartmentNumber)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private Label labelName, labelSurname, labelCity, labelStreet, labelPostNumber, labelPropertyNumber, labelApartmentNumber, labelPesel, labelDateOfBirth, labelSex, labelEmail, labelPhone, labelLogin;
        private TextBox txtName, txtSurname, txtCity, txtStreet, txtPostNumber, txtPropertyNumber, txtPesel, txtEmail, txtPhoneNumber, txtLogin;
        private NumericUpDown nudApartmentNumber;
        private DateTimePicker dtpDateOfBirth;
        private ComboBox cmbSex;
        private Button btnChangePassword, btnSave, btnCancel;
    }
}
