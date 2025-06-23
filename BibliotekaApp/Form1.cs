using System;
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
using BibliotekaApp;
using System.Diagnostics;

namespace BibliotekaApp
{
    public partial class Form1 : Form
    {
        private int userAccessLevel;
        private DatabaseHandler database = new DatabaseHandler();
        private readonly string permissionsFile = "permissions.json";
        private readonly string jwtSecret = "super_secret_key_12345678901234567890123456789012";
        public int userId;
        private bool canAdd;
        private bool canList;
        private bool canForget;
        private bool canListForgotten;
        private bool canEdit;
        private bool canBorrow;
        private bool canManagePermissions;
        private List<RoleDto> allRoles = new List<RoleDto>();

        public Form1(string token, bool admin)
        {
            InitializeComponent();
            ParseJwtToken(token);
            bool isAdmin = admin;
            System.Threading.Thread.Sleep(1000);
            if (File.Exists(permissionsFile))
            {
                string json = File.ReadAllText(permissionsFile);
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

                SetVisibleTabsForAccessLevel(userAccessLevel, isAdmin);
                try
                {
                    var role = database.CheckRole(userAccessLevel);

                    canAdd = role.dodawanie == 1;
                    canList = role.listowanie == 1;
                    canForget = role.zapominanie == 1;
                    canListForgotten = role.zapomniani == 1;
                    canEdit = role.edycja == 1;
                    canBorrow = role.wyporzyczenie == 1;
                    canManagePermissions = role.uprawnienia == 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd pobierania roli: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //LoadRolesToUI();
                //LoadUsersToUI();
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
        // Dodaj to do Form1.cs lub jako osobne metody w klasie Form1

        private void LoadRolesToUI()
        {
            var roles = database.GetAllRoles();
            listBoxRoles.DataSource = roles;
            listBoxRoles.DisplayMember = "role_name";
            listBoxRoles.ValueMember = "id";

            comboBoxRoleAssign.DataSource = roles.ToList();
            comboBoxRoleAssign.DisplayMember = "role_name";
            comboBoxRoleAssign.ValueMember = "id";
        }

        private void LoadUsersToUI()
        {
            var users = database.GetAllUsers();
            comboBoxUsers.DataSource = users;
            comboBoxUsers.DisplayMember = "Login";
            comboBoxUsers.ValueMember = "Id";
        }

        private void listBoxRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxRoles.SelectedItem is RoleDto role)
            {
                clbPermissions.SetItemChecked(0, role.dodawanie == 1);
                clbPermissions.SetItemChecked(1, role.listowanie == 1);
                clbPermissions.SetItemChecked(2, role.zapominanie == 1);
                clbPermissions.SetItemChecked(3, role.zapomniani == 1);
                clbPermissions.SetItemChecked(4, role.edycja == 1);
                clbPermissions.SetItemChecked(5, role.wyporzyczenie == 1);
                clbPermissions.SetItemChecked(6, role.uprawnienia == 1);
            }
        }

        private void btnAddRole_Click(object sender, EventArgs e)
        {
            string roleName = txtNewRoleName.Text.Trim();
            if (string.IsNullOrWhiteSpace(roleName)) return;

            int[] permissions = new int[7];
            for (int i = 0; i < clbPermissions.Items.Count; i++)
                permissions[i] = clbPermissions.GetItemChecked(i) ? 1 : 0;

            database.CreateRole(roleName, permissions[0], permissions[1], permissions[2], permissions[3], permissions[4], permissions[5], permissions[6]);
            MessageBox.Show("Dodano nową rolę.");
            LoadRolesToUI();
        }

        private void btnSaveRole_Click(object sender, EventArgs e)
        {
            if (listBoxRoles.SelectedItem is RoleDto role)
            {
                int[] permissions = new int[7];
                for (int i = 0; i < clbPermissions.Items.Count; i++)
                    permissions[i] = clbPermissions.GetItemChecked(i) ? 1 : 0;

                database.UpdateRole(role.id, role.role_name, permissions[0], permissions[1], permissions[2], permissions[3], permissions[4], permissions[5], permissions[6]);
                MessageBox.Show("Zaktualizowano rolę.");
                LoadRolesToUI();
            }
        }

        private void btnAssignRole_Click(object sender, EventArgs e)
        {
            if (comboBoxUsers.SelectedItem is UserDetailsDto user && comboBoxRoleAssign.SelectedItem is RoleDto role)
            {
                database.ChangeUserData(user.Id, accessLevel: role.id);
                MessageBox.Show($"Zmieniono rolę użytkownika {user.Login} na: {role.role_name}");
            }
        }

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
                    ClockSkew = TimeSpan.FromMinutes(5)
                }, out SecurityToken validatedToken);

                Console.WriteLine("=== JWT Claims ===");
                foreach (var claim in principal.Claims)
                {
                    Console.WriteLine($"{claim.Type}: {claim.Value}");
                }

                var userIdClaim = principal.FindFirst("userId") ?? principal.FindFirst("id");
                var accessLevelClaim = principal.FindFirst("accessLevel") ?? principal.FindFirst("access");

                if (userIdClaim == null || accessLevelClaim == null)
                {
                    MessageBox.Show("Token nie zawiera wymaganych danych (userId lub accessLevel)", "Błąd JWT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                int userId = int.Parse(userIdClaim.Value);
                int accessLevel = int.Parse(accessLevelClaim.Value);
                return (userId, accessLevel);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd JWT: " + ex.Message, "Token", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void HideSensitiveColumns(DataGridView grid)
        {
            if (userAccessLevel < 2)
            {
                if (grid.Columns.Contains("AccessLevel"))
                {
                    grid.Columns["AccessLevel"]!.Visible = false; // Use null-forgiving operator
                }

                if (grid.Columns.Contains("Password"))
                {
                    grid.Columns["Password"]!.Visible = false; // Use null-forgiving operator
                }
            }
            else
            {
                if (grid.Columns.Contains("AccessLevel"))
                {
                    grid.Columns["AccessLevel"]!.Visible = true; // Use null-forgiving operator
                    grid.Columns["AccessLevel"].SortMode = DataGridViewColumnSortMode.Automatic;
                }

                if (grid.Columns.Contains("Password"))
                {
                    grid.Columns["Password"]!.Visible = true; // Use null-forgiving operator
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
                this.Hide(); // ukryj aktualną formę
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
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
        // =============================
        // Funkcje użytkowe i walidacyjne
        // =============================
        private void SetVisibleTabsForAccessLevel(int accessLevel, bool isAdmin)
        {
            if (!isAdmin)
            {
                var role = database.CheckRole(accessLevel);

                // Ukrywamy wszystkie zakładki na początku
                tabControl1.TabPages.Remove(Wypożycz);
                tabControl1.TabPages.Remove(Dodaj);
                tabControl1.TabPages.Remove(Lista);
                tabControl1.TabPages.Remove(Zapomnij);
                tabControl1.TabPages.Remove(Zapomniani);
                tabControl1.TabPages.Remove(Edytuj);
                tabControl1.TabPages.Remove(tabUprawnienia);

                // Dodajemy tylko te, do których użytkownik ma uprawnienia
                if (role.wyporzyczenie == 1) tabControl1.TabPages.Add(Wypożycz);
                if (role.dodawanie == 1) tabControl1.TabPages.Add(Dodaj);
                if (role.listowanie == 1) tabControl1.TabPages.Add(Lista);
                if (role.zapominanie == 1) tabControl1.TabPages.Add(Zapomnij);
                if (role.zapomniani == 1) tabControl1.TabPages.Add(Zapomniani);
                if (role.edycja == 1) tabControl1.TabPages.Add(Edytuj);
                if (role.uprawnienia == 1) tabControl1.TabPages.Add(tabUprawnienia);
            }
        }

        // =============================
        // Funkcje operujące na bazie danych
        // =============================


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

        private void DisplayAllUsersInUserGrid()
        {
            var users = database.GetAllUsers();
            DataTable userTable = new DataTable();

            userTable.Columns.Add("ID", typeof(int));
            userTable.Columns.Add("Login");
            userTable.Columns.Add("Imię");
            userTable.Columns.Add("Nazwisko");
            userTable.Columns.Add("Rola", typeof(int));

            foreach (var user in users)
            {
                userTable.Rows.Add(user.Id, user.Login, user.Name, user.Surname, user.AccessLevel);
            }

            dataGridViewUser.DataSource = userTable;
            dataGridViewUser.AllowUserToAddRows = false;
            dataGridViewUser.ReadOnly = true;

            // Dodaj przycisk "Edytuj"
            if (!dataGridViewUser.Columns.Contains("Edit"))
            {
                var buttonColumn = new DataGridViewButtonColumn
                {
                    Name = "Edit",
                    HeaderText = "Akcje",
                    Text = "Edytuj",
                    UseColumnTextForButtonValue = true
                };
                dataGridViewUser.Columns.Add(buttonColumn);
            }

            // Ukryj inne kolumny jeśli istnieją
            foreach (DataGridViewColumn col in dataGridViewUser.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void dataGridViewUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewUser.Columns[e.ColumnIndex].Name == "Edit" && e.RowIndex >= 0)
            {
                int userId = Convert.ToInt32(dataGridViewUser.Rows[e.RowIndex].Cells["ID"].Value);
                var user = database.FindUserById(userId);
                if (user != null)
                {
                    var profileForm = new UserProfileForm(user);
                    profileForm.ShowDialog();

                    // Odśwież dane po edycji
                    DisplayAllUsersInUserGrid();
                }
            }
        }
        public void DisplayUsersDependingOnLogin() // -- Wyświetlenie użytkowników w zależności od loginu
        {
            string login = txtSearch.Text.Trim();
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

        private void btnProfile_Click(object sender, EventArgs e)
        {
            var user = database.FindUserById(userId);
            if (user != null)
            {
                var form = new UserProfileForm(user);
                form.ShowDialog();
            }
        }
        private void btnSearchUser_Click(object sender, EventArgs e)
        {
            string value = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(value))
            {
                DisplayAllUsers();
                return;
            }

            string filter = comboBoxSearchBy.SelectedItem.ToString();
            List<UserDetailsDto> result = new();

            var users = database.GetAllUsers();

            switch (filter)
            {
                case "Login":
                    result = users.Where(u => u.Login.Contains(value, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case "Imię":
                    result = users.Where(u => u.Name.Contains(value, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case "Nazwisko":
                    result = users.Where(u => u.Surname.Contains(value, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case "PESEL":
                    result = users.Where(u => u.Pesel.Contains(value)).ToList();
                    break;
            }

            DataTable userTable = CreateUserDataTable();
            foreach (var user in result)
            {
                AddUserRow(userTable, user);
            }

            dataGridViewUsers.DataSource = userTable;
            SetPolishColumnHeaders(dataGridViewUsers);
            StyleDataGridView(dataGridViewUsers);
            dataGridViewUsers.AllowUserToAddRows = false;
            dataGridViewUsers.ReadOnly = true;
        }
        private void Lista_Enter(object sender, EventArgs e)
        {
            DisplayAllUsers();
        }

        private void Edytuj_Enter(object sender, EventArgs e)
        {
            DisplayAllUsersInUserGrid();
        }

        private void Zapomniani_Enter(object sender, EventArgs e)
        {
            btnFindForgottenUser.PerformClick(); // jeśli chcesz wywołać kliknięcie
        }

    }
}
