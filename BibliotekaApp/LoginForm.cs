using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using SQLitePCL;
using Microsoft.VisualBasic;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Web;

namespace BibliotekaApp
{
    public partial class LoginForm : Form
    {
        private DatabaseHandler database = new DatabaseHandler();
        private readonly string jwtSecret = "super_secret_key_12345678901234567890123456789012";
        public LoginForm()
        {

            InitializeComponent();
            Batteries.Init(); 
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Text;

            try
            {
                var (token, forgotten, recovery, admin) = database.Login(login, password);

                if (recovery)
                {
                    var user = database.FindUserByLogin(login);
                    if (user != null)
                    {
                        this.Hide();
                        ResetPasswordForm resetForm = new ResetPasswordForm(user.Id);
                        resetForm.ShowDialog();
                        this.Show();
                        return;
                    }
                }

                if (!forgotten)
                {
                    this.Hide();
                    Form1 mainForm = new Form1(token, admin);
                    mainForm.FormClosed += (s, args) => this.Close();
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show("Użytkownik jest oznaczony jako zapomniany.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd logowania", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void lvlRecover_Click_1(object sender, EventArgs e)
        {
            RecoverPasswordForm recoverForm = new RecoverPasswordForm();
            recoverForm.ShowDialog();
        }

        private void LoginForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
