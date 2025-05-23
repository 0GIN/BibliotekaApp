﻿using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SQLitePCL;
using System.IO;
using System.Text.Json;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic.ApplicationServices;
using BibliotekaApp; // jeśli plik helpera też jest w tym namespace



namespace BibliotekaApp
{
    public partial class Form1 : Form
    {
        private int userAccessLevel;
        private DatabaseHandler database = new DatabaseHandler();
        private readonly string permissionsFile = "permissions.json";
        private readonly string jwtSecret = "super_secret_key_12345678901234567890123456789012";
        public int userId;
        public Form1(string token)
        {
            InitializeComponent();
            ParseJwtToken(token);
            checkedListBoxUprawnienia.Items.AddRange(new string[]
            {
            "Dodaj", "Lista", "Zapomnij", "Zapomniani", "Edytuj", "Wypożycz", "Uprawnienia"
            });
            System.Threading.Thread.Sleep(1000);
            if (File.Exists(permissionsFile))
            {
                string json = File.ReadAllText(permissionsFile);
                accessLevelTabs = JsonSerializer.Deserialize<Dictionary<int, List<string>>>(json);
                labelLoggedUser.Text = $"Zalogowany jako: {loggedInLogin}";
            }
            Batteries.Init();
            this.WindowState = FormWindowState.Maximized;
            var result = ParseJwtToken(token);

            if (result != null)
            {
                //int userId = result.Value.userId;
                userAccessLevel = result.Value.accessLevel;
                userId = result.Value.userId;

                var user = database.FindUserById(userId);
                if (user != null)
                {
                    loggedInLogin = user.Login;
                    labelLoggedUser.Text = $"Zalogowany jako: {loggedInLogin}";
                }

                SetVisibleTabsForAccessLevel(userAccessLevel);
            }
            else
            {
                MessageBox.Show("Nieprawidłowy token!", "Błąd logowania", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Application.Exit();
            }

            dataGridViewUser.CellContentClick += dataGridViewUser_CellContentClick;
            CustomizeTabControl();
            ApplyModernTheme();
            StylizeDodajTab();
            DisplayAllUsers();
            DisplayUsersDependingOnLogin();
            DisplayAllUsersInUserGrid();

        }

        // =============================
        // Metody inicjalizujące i ustawiające
        // =============================

        public (int userId, int accessLevel)? ParseJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(jwtSecret);

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var userId = int.Parse(principal.FindFirst("userId").Value);
                var accessLevel = int.Parse(principal.FindFirst("accessLevel").Value);
                return (userId, accessLevel);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Błąd JWT: " + ex.Message, "Token", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void HideSensitiveColumns(DataGridView grid)
        {
            if (userAccessLevel < 2)
            {
                if (grid.Columns.Contains("AccessLevel"))
                {
                    grid.Columns["AccessLevel"].Visible = false;
                }

                if (grid.Columns.Contains("Password"))
                {
                    grid.Columns["Password"].Visible = false;
                }
            }
            else
            {
                if (grid.Columns.Contains("AccessLevel"))
                {
                    grid.Columns["AccessLevel"].Visible = true;
                    grid.Columns["AccessLevel"].SortMode = DataGridViewColumnSortMode.Automatic;
                }

                if (grid.Columns.Contains("Password"))
                {
                    grid.Columns["Password"].Visible = true;
                    grid.Columns["Password"].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
        }


        private void SetPolishColumnHeaders(DataGridView dgv) // -- Ustawianie polskich nagłówków kolumn w DataGridView
        {
            if (dgv.Columns.Contains("Login")) dgv.Columns["Login"].HeaderText = "Login";
            if (dgv.Columns.Contains("Password")) dgv.Columns["Password"].HeaderText = "Hasło";
            if (dgv.Columns.Contains("Name")) dgv.Columns["Name"].HeaderText = "Imię";
            if (dgv.Columns.Contains("Surname")) dgv.Columns["Surname"].HeaderText = "Nazwisko";
            if (dgv.Columns.Contains("City")) dgv.Columns["City"].HeaderText = "Miasto";
            if (dgv.Columns.Contains("PostNumber")) dgv.Columns["PostNumber"].HeaderText = "Kod pocztowy";
            if (dgv.Columns.Contains("Street")) dgv.Columns["Street"].HeaderText = "Ulica";
            if (dgv.Columns.Contains("PropertyNumber")) dgv.Columns["PropertyNumber"].HeaderText = "Nr budynku";
            if (dgv.Columns.Contains("ApartmentNumber")) dgv.Columns["ApartmentNumber"].HeaderText = "Nr mieszkania";
            if (dgv.Columns.Contains("Pesel")) dgv.Columns["Pesel"].HeaderText = "PESEL";
            if (dgv.Columns.Contains("DateOfBirth")) dgv.Columns["DateOfBirth"].HeaderText = "Data urodzenia";
            if (dgv.Columns.Contains("Sex")) dgv.Columns["Sex"].HeaderText = "Płeć";
            if (dgv.Columns.Contains("Email")) dgv.Columns["Email"].HeaderText = "Email";
            if (dgv.Columns.Contains("PhoneNumber")) dgv.Columns["PhoneNumber"].HeaderText = "Telefon";
            if (dgv.Columns.Contains("AccessLevel")) dgv.Columns["AccessLevel"].HeaderText = "Poziom dostępu";
        }

        private DataTable CreateUserDataTable() // -- Tworzenie pustej tabeli użytkowników
        {
            DataTable userTable = new DataTable();
            userTable.Columns.Add("ID", typeof(int));
            userTable.Columns.Add("Login");
            userTable.Columns.Add("Name");
            userTable.Columns.Add("Surname");
            userTable.Columns.Add("City");
            userTable.Columns.Add("PostNumber");
            userTable.Columns.Add("Street");
            userTable.Columns.Add("PropertyNumber");
            userTable.Columns.Add("ApartmentNumber", typeof(int));
            userTable.Columns.Add("PESEL");
            userTable.Columns.Add("DateofBirth", typeof(DateTime));
            userTable.Columns.Add("Sex");
            userTable.Columns.Add("Email");
            userTable.Columns.Add("PhoneNumber");
            userTable.Columns.Add("AccessLevel", typeof(int));
            return userTable;
        }

        private void AddUserRow(DataTable table, dynamic user) // -- Dodanie nowego wiersza użytkownika do tabeli
        {
            table.Rows.Add(
                user.Id,
                user.Login,
                user.Name,
                user.Surname,
                user.City,
                user.PostNumber,
                user.Street,
                user.PropertyNumber,
                user.ApartmentNumber,
                user.Pesel,
                user.DateOfBirth,
                user.Sex,
                user.Email,
                user.PhoneNumber,
                user.AccessLevel
            );
        }

        private string loggedInLogin;

        // =============================
        // Obsługa przycisków (eventy)
        // =============================
        //private int userId;
        private void logoutbtn_Click(object sender, EventArgs e)
        {

            var result = MessageBox.Show(
                "Czy na pewno chcesz się wylogować?",
                "Wylogowanie",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    database.Logout(userId); // <-- Upewnij się, że masz ten ID w polu klasy
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd podczas wylogowania: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                userAccessLevel = 0; // Resetuj poziom dostępu
                accessLevelTabs.Clear(); // Wyczyść uprawnienia
                this.Hide(); // ukryj aktualną formę
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
            }
        }

        private void comboBoxAccessLevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(comboBoxAccessLevels.SelectedItem.ToString(), out int selectedLevel))
            {
                // Wyczyść wszystkie zaznaczenia
                for (int i = 0; i < checkedListBoxUprawnienia.Items.Count; i++)
                {
                    checkedListBoxUprawnienia.SetItemChecked(i, false);
                }

                // Zaznacz te, które pasują do poziomu
                if (accessLevelTabs.TryGetValue(selectedLevel, out var allowedTabs))
                {
                    for (int i = 0; i < checkedListBoxUprawnienia.Items.Count; i++)
                    {
                        string tabName = checkedListBoxUprawnienia.Items[i].ToString();
                        if (allowedTabs.Contains(tabName))
                        {
                            checkedListBoxUprawnienia.SetItemChecked(i, true);
                        }
                    }
                }
            }
        }

        private void btnSavePermissions_Click(object sender, EventArgs e)
        {
            if (int.TryParse(comboBoxAccessLevels.SelectedItem.ToString(), out int level))
            {
                var selectedTabs = checkedListBoxUprawnienia.CheckedItems.Cast<string>().ToList();
                accessLevelTabs[level] = selectedTabs;

                string json = JsonSerializer.Serialize(accessLevelTabs);
                File.WriteAllText(permissionsFile, json);

                MessageBox.Show("Uprawnienia zapisane!");

                if (level == userAccessLevel)
                {
                    SetVisibleTabsForAccessLevel(userAccessLevel);
                }
            }
        }

        private List<TabPage> allTabs = new List<TabPage>();

        private void btnAddUser_Click(object sender, EventArgs e) // -- Dodanie nowego użytkownika po kliknięciu przycisku
        {
            string login = txtLogin.Text.Trim();
            string password = txtHaslo.Text.Trim();
            string name = txtName.Text.Trim();
            string surname = txtSurname.Text.Trim();
            string city = txtCity.Text.Trim();
            string postNumber = txtPostNumber.Text.Trim();
            string street = txtStreet.Text.Trim();
            string propertyNumber = txtPropertyNumber.Text.Trim();
            int apartmentNumber = (int)nudApartmentNumber.Value;
            string pesel = txtPesel.Text.Trim();
            DateTime dateOfBirth = dtpDateOfBirth.Value;
            string sexText = cmbSex.SelectedItem?.ToString();
            string email = txtEmail.Text.Trim();
            string phoneNumber = txtPhoneNumber.Text.Trim();

            string errorMessage;
            bool isValid = ValidationHelper.ValidateUserData(
                name, surname, city, postNumber, street, propertyNumber, pesel, dateOfBirth, sexText, email, phoneNumber,
                out errorMessage
            );


            if (!isValid)
            {
                MessageBox.Show(errorMessage, "Błąd walidacji", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            char sex = sexText[0];
            database.AddUser(login, password, name, surname, city, postNumber, street, propertyNumber, apartmentNumber, pesel, dateOfBirth, sex, email, phoneNumber);
            MessageBox.Show("Użytkownik został dodany pomyślnie.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDisplayUsers_Clickkk(object sender, EventArgs e) // -- Wyświetlenie wszystkich użytkowników
        {
            DisplayAllUsers();
        }

        private void btnForgetUser_Click(object sender, EventArgs e) // -- Zapominanie użytkownika
        {
            if (int.TryParse(txtForgetUserId.Text, out int userId))
            {
                try
                {
                    database.ForgetUser(userId);
                    MessageBox.Show("Użytkownik został zapomniany.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Wprowadź poprawne ID użytkownika.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnFindUserByLogin_Click(object sender, EventArgs e) // -- Wyświetlanie użytkowników w zależności od loginu
        {
            DisplayUsersDependingOnLogin();
        }

        private void btnFindForgottenUser_Click(object sender, EventArgs e) // -- Wyświetlenie wszystkich zapomnianych użytkowników
        {
            var forgottenUsers = database.GetAllForgotten();

            if (forgottenUsers.Count > 0)
            {
                DataTable userTable = CreateUserDataTable();

                foreach (var user in forgottenUsers)
                {
                    AddUserRow(userTable, user);
                }

                dataGridViewForg.DataSource = userTable;
                SetPolishColumnHeaders(dataGridViewForg);
                StyleDataGridView(dataGridViewForg);
                dataGridViewForg.AllowUserToAddRows = false;
                dataGridViewForg.ReadOnly = true;
            }
            else
            {
                MessageBox.Show("Brak zapomnianych użytkowników w bazie.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridViewForg.DataSource = null;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e) // -- Wyszukiwanie użytkownika po ID i pokazanie w tabeli
        {
            if (string.IsNullOrWhiteSpace(txtUserIdEdit.Text))
            {
                DisplayAllUsersInUserGrid();
                return;
            }

            if (int.TryParse(txtUserIdEdit.Text.Trim(), out int id))
            {
                var user = database.FindUserById(id);

                if (user != null)
                {
                    var u = user;
                    DataTable dt = CreateUserDataTable();
                    AddUserRow(dt, u);

                    dataGridViewUser.DataSource = dt;
                    dataGridViewUser.AllowUserToAddRows = false;
                    dataGridViewUser.ReadOnly = false;
                    HideSensitiveColumns(dataGridViewForg);
                }
                else
                {
                    MessageBox.Show("Użytkownik nie znaleziony. Wyświetlam wszystkich.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DisplayAllUsersInUserGrid();
                }
            }
            else
            {
                MessageBox.Show("Wpisz poprawne ID (liczbę całkowitą).", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSave_Click(object sender, EventArgs e) // -- Zapisanie zmian użytkowników po edycji
        {
            if (dataGridViewUser.Rows.Count == 0)
            {
                MessageBox.Show("Brak danych do zapisania.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int savedUsersCount = 0;

            foreach (DataGridViewRow row in dataGridViewUser.Rows)
            {
                if (row.IsNewRow) continue;

                UserDetailsDto editedUser = new UserDetailsDto
                {
                    Id = Convert.ToInt32(row.Cells["ID"].Value),
                    Login = row.Cells["Login"].Value?.ToString(),
                    Password = row.Cells["Password"].Value?.ToString(),
                    Name = row.Cells["Name"].Value?.ToString(),
                    Surname = row.Cells["Surname"].Value?.ToString(),
                    City = row.Cells["City"].Value?.ToString(),
                    PostNumber = row.Cells["PostNumber"].Value?.ToString(),
                    Street = row.Cells["Street"].Value?.ToString(),
                    PropertyNumber = row.Cells["PropertyNumber"].Value?.ToString(),
                    ApartmentNumber = Convert.ToInt32(row.Cells["ApartmentNumber"].Value),
                    Pesel = row.Cells["Pesel"].Value?.ToString(),
                    DateOfBirth = Convert.ToDateTime(row.Cells["DateOfBirth"].Value),
                    Sex = row.Cells["Sex"].Value?.ToString()[0],
                    Email = row.Cells["Email"].Value?.ToString(),
                    PhoneNumber = row.Cells["PhoneNumber"].Value?.ToString(),
                    AccessLevel = Convert.ToInt32(row.Cells["AccessLevel"].Value)
                };

                var originalUser = database.FindUserById(editedUser.Id);

                if (originalUser != null)
                {
                    if (!UsersAreEqual(editedUser, originalUser))
                    {
                        UpdateUser(editedUser);
                        savedUsersCount++;
                    }
                }
            }

            MessageBox.Show($"Zapisano zmiany dla {savedUsersCount} użytkownika(ów)!", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DisplayAllUsersInUserGrid();
        }

        private void comboBoxAccessLevel_SelectedIndexChanged(object sender, EventArgs e) // -- Filtrowanie użytkowników w DataGridView według roli
        {
            string selectedRole = comboBoxAccessLevel.SelectedItem.ToString();
            DataTable dt = (DataTable)dataGridViewUser.DataSource;

            if (dt == null)
                return;

            if (selectedRole == "Wszyscy")
            {
                dt.DefaultView.RowFilter = ""; // Pokaż wszystkich
            }
            else if (selectedRole == "Admini")
            {
                dt.DefaultView.RowFilter = "AccessLevel = 2";
            }
            else if (selectedRole == "Pracownicy")
            {
                dt.DefaultView.RowFilter = "AccessLevel = 1";
            }
            else if (selectedRole == "Użytkownicy")
            {
                dt.DefaultView.RowFilter = "AccessLevel = 0";
            }
        }

        // =============================
        // Funkcje użytkowe i walidacyjne
        // =============================
        public void SetVisibleTabsForAccessLevel(int level)
        {
            if (allTabs == null || allTabs.Count == 0)
            {
                allTabs = tabControl1.TabPages.Cast<TabPage>().ToList(); // ratunkowo
            }


            if (accessLevelTabs.TryGetValue(level, out var allowedTabTexts))
            {
                string currentTabText = tabControl1.SelectedTab?.Text;

                tabControl1.TabPages.Clear();
                TabPage selectedTabToRestore = null;

                foreach (var tab in allTabs)
                {
                    if (allowedTabTexts.Contains(tab.Text))
                    {
                        tabControl1.TabPages.Add(tab);
                        if (tab.Text == currentTabText)
                            selectedTabToRestore = tab;
                    }
                }

                if (selectedTabToRestore != null)
                {
                    tabControl1.SelectedTab = selectedTabToRestore;
                }

                if (tabControl1.TabPages.Count == 0)
                {
                    MessageBox.Show("Brak dostępnych zakładek dla tego poziomu uprawnień.");
                }
            }
        }

        private bool UsersAreEqual(UserDetailsDto edited, UserDetailsDto original) // -- Porównanie edytowanego użytkownika z oryginalnym
        {
            return
                edited.Login == original.Login &&
                edited.Password == original.Password &&
                edited.Name == original.Name &&
                edited.Surname == original.Surname &&
                edited.City == original.City &&
                edited.PostNumber == original.PostNumber &&
                edited.Street == original.Street &&
                edited.PropertyNumber == original.PropertyNumber &&
                (edited.ApartmentNumber ?? 0) == (original.ApartmentNumber ?? 0) &&
                edited.Pesel == original.Pesel &&
                edited.DateOfBirth == original.DateOfBirth &&
                (edited.Sex ?? 'M') == (original.Sex ?? 'M') &&
                edited.Email == original.Email &&
                edited.PhoneNumber == original.PhoneNumber &&
                edited.AccessLevel == original.AccessLevel;
        }

        // =============================
        // Funkcje operujące na bazie danych
        // =============================

        private Dictionary<int, List<string>> accessLevelTabs = new Dictionary<int, List<string>>
            {
                { 0, new List<string> { "Dodaj", "Lista" } },
                { 1, new List<string> { "Dodaj", "Lista", "Zapomnij", "Zapomniani", "Edytuj" } },
                { 2, new List<string> { "Dodaj", "Lista", "Zapomnij", "Zapomniani", "Edytuj", "Wypożycz" } }
            };

        private Dictionary<string, string> tabTextToName = new Dictionary<string, string>
            {
                { "Dodaj", "Dodaj" },
                { "Lista", "Lista" },
                { "Zapomnij", "Zapomnij" },
                { "Zapomniani", "Zapomniani" },
                { "Edytuj", "Edytuj" },
                { "Wypożycz", "Wypożycz" },
                { "Uprawnienia", "tabUprawnienia" } // jeśli tworzona dynamicznie

            };

        public void DisplayAllUsers() // -- Wyświetlenie wszystkich użytkowników
        {
            var users = database.GetAllUsers();
            DataTable userTable = CreateUserDataTable();

            foreach (var user in users)
            {
                AddUserRow(userTable, user);
            }

            dataGridViewUsers.DataSource = userTable;
            SetPolishColumnHeaders(dataGridViewUsers);
            StyleDataGridView(dataGridViewUsers);
            dataGridViewUsers.AllowUserToAddRows = false;
            dataGridViewUsers.ReadOnly = true;
            HideSensitiveColumns(dataGridViewForg);
        }

        private void DisplayAllUsersInUserGrid() // -- Wyświetlenie wszystkich użytkowników w DataGridView
        {
            var users = database.GetAllUsers();
            DataTable userTable = CreateUserDataTable();

            foreach (var user in users)
            {
                AddUserRow(userTable, user);
            }

            dataGridViewUser.DataSource = userTable;
            SetPolishColumnHeaders(dataGridViewUser);
            StyleDataGridView(dataGridViewUser);
            dataGridViewUser.AllowUserToAddRows = false;
            dataGridViewUser.ReadOnly = false;
            HideSensitiveColumns(dataGridViewForg);
            if (!dataGridViewUser.Columns.Contains("Password"))
            {
                var buttonColumn = new DataGridViewButtonColumn
                {
                    Name = "Password",
                    HeaderText = "Hasło",
                    Text = "Zmień hasło",
                    UseColumnTextForButtonValue = true
                };
                dataGridViewUser.Columns.Add(buttonColumn);
            }
        }

        private void dataGridViewUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewUser.Columns[e.ColumnIndex].Name == "Password" && e.RowIndex >= 0)
            {
                int userId = Convert.ToInt32(dataGridViewUser.Rows[e.RowIndex].Cells["ID"].Value);
                using var form = new ResetPasswordForm(userId);
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    MessageBox.Show("Hasło zostało zmienione.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        public void UpdateUser(UserDetailsDto user) // -- Aktualizacja danych użytkownika
        {
            try
            {
                database.ChangeUserData(
                    userId: user.Id,
                    login: user.Login,
                    password: user.Password,
                    name: user.Name,
                    surname: user.Surname,
                    city: user.City,
                    postNumber: user.PostNumber,
                    street: user.Street,
                    propertyNumber: user.PropertyNumber,
                    apartmentNumber: user.ApartmentNumber ?? 0,
                    pesel: user.Pesel,
                    dateOfBirth: user.DateOfBirth,
                    sex: user.Sex ?? 'M',
                    email: user.Email,
                    phoneNumber: user.PhoneNumber,
                    accessLevel: user.AccessLevel
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas aktualizacji użytkownika: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DisplayUsersDependingOnLogin() // -- Wyświetlenie użytkowników w zależności od loginu
        {
            string login = txtUserLogin.Text.Trim();
            DataTable userTable = CreateUserDataTable();

            if (string.IsNullOrEmpty(login))
            {
                var users = database.GetAllUsers();
                foreach (var user in users)
                {
                    AddUserRow(userTable, user);
                }
            }
            else
            {
                var user = database.FindUserByLogin(login);

                if (user != null)
                {
                    AddUserRow(userTable, user);
                }
            }

            dataGridViewUsers.DataSource = userTable;
            SetPolishColumnHeaders(dataGridViewUsers);
            StyleDataGridView(dataGridViewUsers);
            dataGridViewUsers.AllowUserToAddRows = false;
            dataGridViewUsers.ReadOnly = true;
            HideSensitiveColumns(dataGridViewUsers);
        }

        private void labelLoggedUser_Click(object sender, EventArgs e)
        {

        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            var user = database.FindUserById(userId);
            if (user != null)
            {
                var form = new UserProfileForm(user);
                form.ShowDialog();
            }
        }
    }
}
