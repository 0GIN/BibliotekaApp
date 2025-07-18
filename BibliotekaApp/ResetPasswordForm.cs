﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Windows.Forms;
using Microsoft.IdentityModel.Tokens;

namespace BibliotekaApp
{
    public partial class ResetPasswordForm : Form
    {
        private int userId;
        private DatabaseHandler database = new DatabaseHandler();



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
                MessageBox.Show("Wprowadź hasła w oba pola.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Hasła się nie zgadzają.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidationHelper.ValidatePassword(newPassword, out string passwordError))
            {
                MessageBox.Show(passwordError, "Błąd walidacji hasła", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var user = database.FindUserById(userId);
                if (user == null)
                {
                    MessageBox.Show("Nie znaleziono użytkownika.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                database.ChangeUserData(
                    userId,
                    password: newPassword,
                    login: user.Login,
                    name: user.Name,
                    surname: user.Surname,
                    city: user.City,
                    postNumber: user.PostNumber,
                    street: user.Street,
                    propertyNumber: user.PropertyNumber,
                    apartmentNumber: user.ApartmentNumber,
                    pesel: user.Pesel,
                    dateOfBirth: user.DateOfBirth,
                    sex: user.Sex,
                    email: user.Email,
                    phoneNumber: user.PhoneNumber,
                    recovery: true
                );

                MessageBox.Show("Hasło zostało zmienione pomyślnie.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.DialogResult = DialogResult.OK;
                this.Close();  
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hasło musi byc różne od 3 ostatnich.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ResetPasswordForm_Load(object sender, EventArgs e)
        {

        }
    }
}
