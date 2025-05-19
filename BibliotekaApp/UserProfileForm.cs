using System;
using System.Windows.Forms;
using BibliotekaApp; // jeśli plik helpera też jest w tym namespace

namespace BibliotekaApp
{
    public partial class UserProfileForm : Form
    {
        private readonly int userId;
        private readonly DatabaseHandler database;
        private UserDetailsDto user;

        public UserProfileForm(UserDetailsDto user)
        {
            InitializeComponent();
            this.user = user;
            this.userId = user.Id;
            this.database = new DatabaseHandler();
        }

        private void UserProfileForm_Load(object sender, EventArgs e)
        {
            user = database.FindUserById(userId);
            if (user == null)
            {
                MessageBox.Show("Nie udało się pobrać danych użytkownika.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            // Wypełnienie pól
            txtLogin.Text = user.Login;
            txtLogin.ReadOnly = true;
            txtName.Text = user.Name;
            txtSurname.Text = user.Surname;
            txtCity.Text = user.City;
            txtPostNumber.Text = user.PostNumber;
            txtStreet.Text = user.Street;
            txtPropertyNumber.Text = user.PropertyNumber;
            nudApartmentNumber.Value = user.ApartmentNumber ?? 0;
            txtPesel.Text = user.Pesel;
            dtpDateOfBirth.Value = user.DateOfBirth;
            cmbSex.SelectedItem = user.Sex?.ToString();
            txtEmail.Text = user.Email;
            txtPhoneNumber.Text = user.PhoneNumber;
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            ResetPasswordForm resetForm = new ResetPasswordForm(userId);
            resetForm.ShowDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string errorMessage;

            if (!ValidationHelper.ValidateUserData(
                txtName.Text,
                txtSurname.Text,
                txtCity.Text,
                txtPostNumber.Text,
                txtStreet.Text,
                txtPropertyNumber.Text,
                txtPesel.Text,
                dtpDateOfBirth.Value,
                cmbSex.SelectedItem?.ToString(),
                txtEmail.Text,
                txtPhoneNumber.Text,
                out errorMessage))
            {
                MessageBox.Show(errorMessage, "Błąd walidacji", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                database.ChangeUserData(
                    userId,
                    name: txtName.Text,
                    surname: txtSurname.Text,
                    city: txtCity.Text,
                    postNumber: txtPostNumber.Text,
                    street: txtStreet.Text,
                    propertyNumber: txtPropertyNumber.Text,
                    apartmentNumber: (int)nudApartmentNumber.Value,
                    pesel: txtPesel.Text,
                    dateOfBirth: dtpDateOfBirth.Value,
                    sex: cmbSex.SelectedItem?.ToString()[0],
                    email: txtEmail.Text,
                    phoneNumber: txtPhoneNumber.Text
                );

                MessageBox.Show("Dane zostały zapisane.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd zapisu danych: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            using (var resetForm = new ResetPasswordForm(userId))
            {
                var result = resetForm.ShowDialog();

                // Tylko jeśli trzeba — coś odśwież po zmianie
                if (result == DialogResult.OK)
                {
                    MessageBox.Show("Hasło zostało zmienione.");
                    // ale NIE otwieraj ResetPasswordForm ponownie!
                }
            }
        }
    }
}
