using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SQLitePCL;

namespace BibliotekaApp
{
    public partial class LoginForm : Form
    {

        private Databasehandler database = new Databasehandler();
        public LoginForm()
        {
            InitializeComponent();
            Batteries.Init(); // Przeniesienie wywołania Init do konstruktora
            database.CreateDatabase(); // Przeniesienie wywołania CreateDatabase do konstruktora
        }

        
       

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text;  // Zakładając, że masz TextBox do loginu
            string password = txtPassword.Text;  // Zakładając, że masz TextBox do hasła

            try
            {
                // Wywołanie metody Login z klasy Databasehandler
                var (token, forgotten) = database.Login(login, password);

                // Jeśli dane logowania są poprawne, przenosimy do głównego formularza (Form1)
                if (!forgotten)
                {
                    // Tworzymy instancję Form1
                    AddUserForm mainForm = new AddUserForm();

                    // Ukrywamy formularz logowania i pokazujemy Form1
                    this.Hide();  // Ukrywamy formularz logowania

                    // Dodajemy zdarzenie FormClosed, które zamknie LoginForm po zamknięciu Form1
                    mainForm.FormClosed += (s, args) => this.Close();
                    mainForm.Show();  // Pokazujemy główny formularz
                }
            }
            catch (Exception ex)
            {
                // Obsługa błędów, jeśli użytkownik jest zapomniany lub dane są nieprawidłowe
                MessageBox.Show(ex.Message, "Błąd logowania", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
