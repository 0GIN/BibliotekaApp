using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Windows.Forms;
using Microsoft.IdentityModel.Tokens;

namespace BibliotekaApp
{
    public partial class ResetPasswordForm : Form
    {
        private readonly int userId;
        private readonly DatabaseHandler database = new DatabaseHandler();



        public ResetPasswordForm(int userId)
        {
            InitializeComponent();
            this.userId = userId;


        }

        
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Wprowadź oba pola hasła.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Hasła się nie zgadzają.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                database.ChangeUserData(userId, password: newPassword);
                MessageBox.Show("Hasło zostało zmienione pomyślnie.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
