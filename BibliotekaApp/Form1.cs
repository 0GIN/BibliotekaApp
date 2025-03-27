using System;
using System.Data.Entity;
using System.Data.SQLite;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Storage;
using SQLitePCL;

namespace BibliotekaApp
{
    public partial class AddUserForm : Form
    {
        public AddUserForm()
        {
            InitializeComponent();
            Batteries.Init(); // Przeniesienie wywo�ania Init do konstruktora
            database.CreateDatabase(); // Przeniesienie wywo�ania CreateDatabase do konstruktora
        }

        private Databasehandler database = new Databasehandler();

        private void txtLogin_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            // Pobieranie danych z p�l tekstowych
            string login = txtLogin.Text;
            string name = txtName.Text;
            string surname = txtSurname.Text;
            string city = txtCity.Text;
            string postNumber = txtPostNumber.Text;
            string street = txtStreet.Text;
            string propertyNumber = txtPropertyNumber.Text;
            int apartmentNumber = (int)nudApartmentNumber.Value;  // nudApartmentNumber to kontrolka NumericUpDown
            string pesel = txtPesel.Text;
            DateTime dateOfBirth = dtpDateOfBirth.Value;  // dtpDateOfBirth to kontrolka DateTimePicker
            char sex = cmbSex.SelectedItem.ToString()[0];  // cmbSex to ComboBox z warto�ciami "M" i "K"
            string email = txtEmail.Text;
            string phoneNumber = txtPhoneNumber.Text;
            bool forgotten = chkForgotten.Checked;  // chkForgotten to CheckBox

            // Wywo�anie metody do dodania u�ytkownika
            database.AddUser(login, name, surname, city, postNumber, street, propertyNumber, apartmentNumber, pesel, dateOfBirth, sex, email, phoneNumber, forgotten);
            MessageBox.Show("U�ytkownik zosta� dodany pomy�lnie.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Przyk�ad dla Windows Forms
        private void btnDisplayUsers_Clickkk(object sender, EventArgs e)
        {
            DisplayAllUsers();
        }

        public void DisplayAllUsers()
        {
            // Zak�adaj�c, �e masz metod�, kt�ra pobiera u�ytkownik�w z bazy danych.
            var users = database.GetAllUsers();

            // Wyczy�� ListBox przed dodaniem nowych element�w
            listBoxUsers.Items.Clear();

            // Dodaj ka�dego u�ytkownika do ListBoxa
            foreach (var user in users)
            {
                // Dodaj tekst do listy w formacie "ID: login - Imi� Nazwisko"
                listBoxUsers.Items.Add($"ID: {user.id}, Login: {user.login}, Name: {user.name}, Surname: {user.surname}");
            }
        }

        private void dtpDateOfBirth_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtCity_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddUserForm_Load(object sender, EventArgs e)
        {

        }

    }
}
