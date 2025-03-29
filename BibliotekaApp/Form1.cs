using System;
using System.Data;
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

        private Databasehandler database = new Databasehandler();
        public AddUserForm()
        {
            InitializeComponent();
            Batteries.Init(); // Przeniesienie wywo³ania Init do konstruktora
            database.CreateDatabase(); // Przeniesienie wywo³ania CreateDatabase do konstruktora
        }




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
            // Pobieranie danych z pól tekstowych
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
            char sex = cmbSex.SelectedItem.ToString()[0];  // cmbSex to ComboBox z wartoœciami "M" i "K"
            string email = txtEmail.Text;
            string phoneNumber = txtPhoneNumber.Text;
            bool forgotten = chkForgotten.Checked;  // chkForgotten to CheckBox

            // Wywo³anie metody do dodania u¿ytkownika
            database.AddUser(login, name, surname, city, postNumber, street, propertyNumber, apartmentNumber, pesel, dateOfBirth, sex, email, phoneNumber, forgotten);
            MessageBox.Show("U¿ytkownik zosta³ dodany pomyœlnie.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Przyk³ad dla Windows Forms
        private void btnDisplayUsers_Clickkk(object sender, EventArgs e)
        {
            DisplayAllUsers();
        }

        public void DisplayAllUsers()
        {
            // Zak³adaj¹c, ¿e masz metodê, która pobiera u¿ytkowników z bazy danych.
            var users = database.GetAllUsers();

            // Wyczyœæ ListBox przed dodaniem nowych elementów
            listBoxUsers.Items.Clear();

            // Dodaj ka¿dego u¿ytkownika do ListBoxa
            foreach (var user in users)
            {
                // Dodaj tekst do listy w formacie "ID: login - Imiê Nazwisko"
                listBoxUsers.Items.Add($"ID: {user.id}, Login: {user.login}, Imiê: {user.name}, Nazwisko: {user.surname}");
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

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void btnFindUser_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtUserId.Text, out int userId))  // txtUserId to TextBox do wprowadzenia ID
            {
                var user = database.FindUserById(userId);

                if (user.HasValue)
                {
                    DataTable userDataTable = new DataTable();
                    userDataTable.Columns.Add("ID");
                    userDataTable.Columns.Add("Login");
                    userDataTable.Columns.Add("Name");
                    userDataTable.Columns.Add("Surname");
                    userDataTable.Columns.Add("City");
                    userDataTable.Columns.Add("Post Number");
                    userDataTable.Columns.Add("Street");
                    userDataTable.Columns.Add("Property Number");
                    userDataTable.Columns.Add("Apartment");
                    userDataTable.Columns.Add("Pesel");
                    userDataTable.Columns.Add("Date of Birth");
                    userDataTable.Columns.Add("Sex");
                    userDataTable.Columns.Add("Email");
                    userDataTable.Columns.Add("Phone Number");

                    userDataTable.Rows.Add(user.Value.id, user.Value.login, user.Value.name, user.Value.surname, user.Value.city,
                        user.Value.postNumber, user.Value.street, user.Value.propertyNumber, user.Value.apartmentNumber,
                        user.Value.pesel, user.Value.dateOfBirth.ToShortDateString(), user.Value.sex, user.Value.email, user.Value.phoneNumber);

                    // Ustawienie DataGridView
                    dataGridView1.DataSource = userDataTable;

                    // Automatyczne dostosowanie szerokoœci kolumn
                    dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                    // Ustawienia stylu
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
                    dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                    dataGridView1.DefaultCellStyle.BackColor = Color.White;
                    dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
                    dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10);
                    dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);

                    // Zmiana kolorów wierszy
                    dataGridView1.RowsDefaultCellStyle.BackColor = Color.LightBlue;
                    dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
                }
                else
                {
                    MessageBox.Show("U¿ytkownik nie zosta³ znaleziony.", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("WprowadŸ poprawne ID u¿ytkownika.", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnForgetUser_Click(object sender, EventArgs e)
        {
            // Pobranie ID u¿ytkownika z TextBox
            if (int.TryParse(txtForgetUserId.Text, out int userId))  // txtForgetUserId to TextBox, w którym wprowadzasz ID
            {
                try
                {
                    // Wywo³anie metody ForgetUser z klasy Databasehandler
                    database.ForgetUser(userId);

                    // Informacja o pomyœlnym zapomnieniu u¿ytkownika
                    MessageBox.Show("U¿ytkownik zosta³ zapomniany.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Obs³uga b³êdów
                    MessageBox.Show($"Wyst¹pi³ b³¹d: {ex.Message}", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // B³¹d, jeœli ID jest niepoprawne
                MessageBox.Show("WprowadŸ poprawne ID u¿ytkownika.", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnFindUserByLogin_Click(object sender, EventArgs e)
        {
            // Pobranie loginu u¿ytkownika z TextBox
            string login = txtUserLogin.Text.Trim();  // txtUserLogin to TextBox, w którym u¿ytkownik wpisuje login

            if (!string.IsNullOrEmpty(login))  // SprawdŸ, czy login nie jest pusty
            {
                // Wywo³anie metody FindUserByLogin
                var user = database.FindUserByLogin(login);

                if (user.HasValue)
                {
                    // Tworzymy DataTable, aby wyœwietliæ dane w DataGridView
                    DataTable userDataTable = new DataTable();
                    userDataTable.Columns.Add("ID");
                    userDataTable.Columns.Add("Login");
                    userDataTable.Columns.Add("Name");
                    userDataTable.Columns.Add("Surname");
                    userDataTable.Columns.Add("City");
                    userDataTable.Columns.Add("Post Number");
                    userDataTable.Columns.Add("Street");
                    userDataTable.Columns.Add("Property Number");
                    userDataTable.Columns.Add("Apartment");
                    userDataTable.Columns.Add("Pesel");
                    userDataTable.Columns.Add("Date of Birth");
                    userDataTable.Columns.Add("Sex");
                    userDataTable.Columns.Add("Email");
                    userDataTable.Columns.Add("Phone Number");

                    // Dodanie danych u¿ytkownika do DataTable
                    userDataTable.Rows.Add(user.Value.id, user.Value.login, user.Value.name, user.Value.surname, user.Value.city,
                        user.Value.postNumber, user.Value.street, user.Value.propertyNumber, user.Value.apartmentNumber,
                        user.Value.pesel, user.Value.dateOfBirth.ToShortDateString(), user.Value.sex, user.Value.email, user.Value.phoneNumber);

                    // Wyœwietlenie danych w DataGridView
                    dataGridView2.DataSource = userDataTable;  // dataGridView1 to kontrolka, w której wyœwietlamy dane
                }
                else
                {
                    MessageBox.Show("U¿ytkownik o podanym loginie nie zosta³ znaleziony.", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("WprowadŸ poprawny login u¿ytkownika.", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnFindForgottenUser_Click(object sender, EventArgs e)
        {
            // Pobranie loginu u¿ytkownika z TextBox
            string login = txtUserLoginForg.Text.Trim();  // txtUserLogin to TextBox, w którym u¿ytkownik wpisuje login

            if (!string.IsNullOrEmpty(login))  // SprawdŸ, czy login nie jest pusty
            {
                // Wywo³anie metody FindForgottenUserByLogin
                var user = database.FindForgottenUserByLogin(login);

                if (user.HasValue)
                {
                    // Tworzymy DataTable, aby wyœwietliæ dane w DataGridView
                    DataTable userDataTable = new DataTable();
                    userDataTable.Columns.Add("ID");
                    userDataTable.Columns.Add("Login");
                    userDataTable.Columns.Add("Name");
                    userDataTable.Columns.Add("Surname");
                    userDataTable.Columns.Add("City");
                    userDataTable.Columns.Add("Post Number");
                    userDataTable.Columns.Add("Street");
                    userDataTable.Columns.Add("Property Number");
                    userDataTable.Columns.Add("Apartment");
                    userDataTable.Columns.Add("Pesel");
                    userDataTable.Columns.Add("Date of Birth");
                    userDataTable.Columns.Add("Sex");
                    userDataTable.Columns.Add("Email");
                    userDataTable.Columns.Add("Phone Number");

                    // Dodanie danych u¿ytkownika do DataTable
                    userDataTable.Rows.Add(user.Value.id, user.Value.login, user.Value.name, user.Value.surname, user.Value.city,
                        user.Value.postNumber, user.Value.street, user.Value.propertyNumber, user.Value.apartmentNumber,
                        user.Value.pesel, user.Value.dateOfBirth.ToShortDateString(), user.Value.sex, user.Value.email, user.Value.phoneNumber);

                    // Wyœwietlenie danych w DataGridView
                    dataGridViewForg.DataSource = userDataTable;  // dataGridView1 to kontrolka, w której wyœwietlamy dane
                }
                else
                {
                    MessageBox.Show("Zapomniany u¿ytkownik o podanym loginie nie zosta³ znaleziony.", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("WprowadŸ poprawny login u¿ytkownika.", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


    }
}
