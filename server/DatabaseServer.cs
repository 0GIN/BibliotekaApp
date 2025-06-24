using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.VisualBasic;


namespace biblioteka
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public class DatabaseController : ControllerBase
    {
        private readonly string databasePath = "Data Source=biblioteka.db";
        private readonly string jwtSecret = "super_secret_key_12345678901234567890123456789012"; //według preferencji
        
        [HttpPost("create-database")]
        public IActionResult CreateDatabase()
        {
            try
            {
                Console.WriteLine(DateTime.UtcNow+ " Creating database...");
                using (var connection = new SqliteConnection(databasePath))
            {
                int count = 0;
                connection.Open();
                string query = @"CREATE TABLE IF NOT EXISTS users(
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                login TEXT UNIQUE,
                password TEXT,
                password1 TEXT,
                password2 TEXT,
                password3 TEXT,
                name TEXT,
                surname TEXT,
                city TEXT,
                post_number TEXT,
                street TEXT,
                property_number TEXT,
                apartment_number INTEGER,
                pesel TEXT UNIQUE,
                date_of_birth TEXT,
                sex TEXT,
                email TEXT UNIQUE,
                phone_number TEXT,
                forgotten BOOLEAN DEFAULT 0,
                blocked BOOLEAN DEFAULT 0,
                access_level INTEGER DEFAULT 1,
                temp_password TEXT,
                failed_attempts INTEGER DEFAULT 0,
                blocked_until TEXT,
                temp_password_expire TEXT,
                admin BOOLEAN DEFAULT 0
                )";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                count++;
                Console.WriteLine("TABLE users created "+count+"/8");
                query = @"CREATE TABLE IF NOT EXISTS roles(
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                role_name TEXT,
                access_level INTEGER UNIQUE,
                dodawanie BOOLEAN,
                listowanie BOOLEAN,
                zapominanie BOOLEAN,
                zapomniani BOOLEAN,
                edycja BOOLEAN,
                wyporzyczenie BOOLEAN,
                uprawnienia BOOLEAN
                )";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                count++;
                Console.WriteLine("TABLE roles created "+count+"/8");

                query = @"INSERT INTO roles (role_name, access_level, dodawanie, listowanie, zapominanie, zapomniani, edycja, wyporzyczenie, uprawnienia) 
                                     VALUES (""uzytkownik"", 1, 0, 0, 0, 0, 0, 1, 0)";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                query = @"INSERT INTO roles (role_name, access_level, dodawanie, listowanie, zapominanie, zapomniani, edycja, wyporzyczenie, uprawnienia) 
                                     VALUES (""pracownik"", 2, 1, 1, 1, 0, 0, 1, 0)";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }

                query = @"CREATE TABLE IF NOT EXISTS authors(
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                name TEXT)";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                count++;
                Console.WriteLine("TABLE authors created "+count+"/8");

                query = @"CREATE TABLE IF NOT EXISTS categories(
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                name TEXT)";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                count++;
                Console.WriteLine("TABLE categories created "+count+"/8");

                query = @"CREATE TABLE IF NOT EXISTS books(
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                title TEXT,
                author_id INTEGER,
                category_id INTEGER,
                isbn TEXT UNIQUE,
                publish_date INTEGER)";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                count++;
                Console.WriteLine("TABLE books created "+count+"/8");

                query = @"CREATE TABLE IF NOT EXISTS book_copies(
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                book_id INTEGER,
                status TEXT)";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                count++;
                Console.WriteLine("TABLE book_copies created "+count+"/8");

                query = @"CREATE TABLE IF NOT EXISTS loans(
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                user_id INTEGER,
                book_copy_id INTEGER,
                loan_date TEXT,
                return_date TEXT,
                status TEXT)";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                count++;
                Console.WriteLine("TABLE loans created "+count+"/8");

                query = @"CREATE TABLE IF NOT EXISTS reservations(
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                user_id INTEGER,
                book_id INTEGER,
                reservation_date TEXT,
                status TEXT)";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                count++;
                Console.WriteLine("TABLE reservations created "+count+"/8");
            }
                return Ok("Database created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("add-user")]
        public IActionResult AddUser([FromBody] UserDto user)
        {
            try
            {
                using (var connection = new SqliteConnection(databasePath))
                {
                    connection.Open();
                    string query = @"INSERT INTO users (login, password, name, surname, city, post_number, street, property_number, apartment_number, pesel, date_of_birth, sex, email, phone_number, admin) 
                                     VALUES (@login, @password, @name, @surname, @city, @postNumber, @street, @propertyNumber, @apartmentNumber, @pesel, @dateOfBirth, @sex, @Email, @phoneNumber, @admin)";
                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@login", user.Login);
                        command.Parameters.AddWithValue("@password", user.Password);
                        command.Parameters.AddWithValue("@name", user.Name);
                        command.Parameters.AddWithValue("@surname", user.Surname);
                        command.Parameters.AddWithValue("@city", user.City);
                        command.Parameters.AddWithValue("@postNumber", user.PostNumber);
                        command.Parameters.AddWithValue("@street", user.Street);
                        command.Parameters.AddWithValue("@propertyNumber", user.PropertyNumber);
                        command.Parameters.AddWithValue("@apartmentNumber", user.ApartmentNumber);
                        command.Parameters.AddWithValue("@pesel", user.Pesel);
                        command.Parameters.AddWithValue("@dateOfBirth", user.DateOfBirth.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@sex", user.Sex.ToString());
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@phoneNumber", user.PhoneNumber);
                        command.Parameters.AddWithValue("@admin", user.admin.HasValue && user.admin.Value ? 1 : 0);
                        command.ExecuteNonQuery();
                    }
                }
                Console.WriteLine(DateTime.UtcNow +" User added successfully.");
                return Ok("User added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            try
            {
                using (var connection = new SqliteConnection(databasePath))
                {
                    connection.Open();
                    // Sprawdź blokadę konta
                    string checkBlockQuery = @"SELECT id, blocked, blocked_until, failed_attempts, forgotten, access_level, admin FROM users WHERE login = @login";
                    using (var checkCmd = new SqliteCommand(checkBlockQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@login", loginDto.Login);
                        using (var reader = checkCmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                Console.WriteLine(DateTime.UtcNow +" Failed login attempt: " + loginDto.Login);
                                throw new Exception("Nieprawidłowy login lub hasło");
                            }
                            int userId = reader.GetInt32(0);
                            bool blocked = reader.GetInt32(1) == 1;
                            string blockedUntilStr = reader.IsDBNull(2) ? null : reader.GetString(2);
                            int failedAttempts = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                            bool forgotten = reader.GetInt32(4) == 1;
                            int accessLevel = reader.GetInt32(5);
                            bool admin = reader.GetBoolean(6);

                            DateTime? blockedUntil = null;
                            if (!string.IsNullOrEmpty(blockedUntilStr))
                            {
                                DateTime dt;
                                if (DateTime.TryParse(blockedUntilStr, out dt))
                                    blockedUntil = dt;
                            }

                            if (blocked)
                            {
                                if (blockedUntil.HasValue && blockedUntil.Value > DateTime.UtcNow)
                                {
                                    // Zwróć komunikat o blokadzie z czasem odblokowania
                                    string msg = $"Konto zablokowane do {blockedUntil.Value.ToLocalTime():yyyy-MM-dd HH:mm:ss}";
                                    Console.WriteLine(DateTime.UtcNow + $" Login blocked for user: {loginDto.Login} until {blockedUntil}");
                                    return StatusCode(403, msg);
                                }
                                else
                                {
                                    // Odblokuj konto po upływie czasu
                                    string unblockQuery = @"UPDATE users SET blocked = 0, blocked_until = NULL, failed_attempts = 0 WHERE id = @userId";
                                    using (var unblockCmd = new SqliteCommand(unblockQuery, connection))
                                    {
                                        unblockCmd.Parameters.AddWithValue("@userId", userId);
                                        unblockCmd.ExecuteNonQuery();
                                    }
                                    blocked = false;
                                    failedAttempts = 0;
                                }
                            }


                            string loginQuery = @"SELECT temp_password, password, temp_password_expire FROM users WHERE login = @login";
                            string tempPassword = null;
                            string password = null;
                            string tempPasswordExpireStr = null;
                            using (var passCmd = new SqliteCommand(loginQuery, connection))
                            {
                                passCmd.Parameters.AddWithValue("@login", loginDto.Login);
                                using (var passReader = passCmd.ExecuteReader())
                                {
                                    if (passReader.Read())
                                    {
                                        tempPassword = passReader.IsDBNull(0) ? null : passReader.GetString(0);
                                        password = passReader.IsDBNull(1) ? null : passReader.GetString(1);
                                        tempPasswordExpireStr = passReader.IsDBNull(2) ? null : passReader.GetString(2);
                                    }
                                }
                            }


                            if (!string.IsNullOrEmpty(tempPassword) && !string.IsNullOrEmpty(tempPasswordExpireStr))
                            {
                                DateTime expire;
                                if (DateTime.TryParse(tempPasswordExpireStr, out expire) && expire < DateTime.UtcNow)
                                {
                                    string clearTempQuery = @"UPDATE users SET temp_password = NULL, temp_password_expire = NULL WHERE login = @login";
                                    using (var clearCmd = new SqliteCommand(clearTempQuery, connection))
                                    {
                                        clearCmd.Parameters.AddWithValue("@login", loginDto.Login);
                                        clearCmd.ExecuteNonQuery();
                                    }
                                    tempPassword = null;
                                }
                            }

                            bool isPasswordCorrect = (loginDto.Password == password) || (!string.IsNullOrEmpty(tempPassword) && loginDto.Password == tempPassword);

                            if (!forgotten && isPasswordCorrect)
                            {

                                string resetQuery = @"UPDATE users SET failed_attempts = 0, blocked = 0, blocked_until = NULL WHERE id = @userId";
                                using (var resetCmd = new SqliteCommand(resetQuery, connection))
                                {
                                    resetCmd.Parameters.AddWithValue("@userId", userId);
                                    resetCmd.ExecuteNonQuery();
                                }

                                bool recovery = !string.IsNullOrEmpty(tempPassword) && tempPassword == loginDto.Password;
                                Console.WriteLine(DateTime.UtcNow + " ["+userId+"] "+  "User login successfully: " + loginDto.Login);
                                string token = GenerateToken(userId, accessLevel);
                                return Ok(new { token, forgotten, recovery, admin});
                            }
                            else
                            {

                                failedAttempts++;
                                string updateFailQuery = @"UPDATE users SET failed_attempts = @fa WHERE id = @userId";
                                using (var failCmd = new SqliteCommand(updateFailQuery, connection))
                                {
                                    failCmd.Parameters.AddWithValue("@fa", failedAttempts);
                                    failCmd.Parameters.AddWithValue("@userId", userId);
                                    failCmd.ExecuteNonQuery();
                                }

                                if (failedAttempts >= 3)
                                {
                                    DateTime until = DateTime.UtcNow.AddMinutes(1);
                                    string blockQuery = @"UPDATE users SET blocked = 1, blocked_until = @until WHERE id = @userId";
                                    using (var blockCmd = new SqliteCommand(blockQuery, connection))
                                    {
                                        blockCmd.Parameters.AddWithValue("@until", until.ToString("yyyy-MM-dd HH:mm:ss"));
                                        blockCmd.Parameters.AddWithValue("@userId", userId);
                                        blockCmd.ExecuteNonQuery();
                                    }
                                    Console.WriteLine(DateTime.UtcNow + $" User {loginDto.Login} blocked for 30 minutes.");
                                    return StatusCode(403, $"Konto zostało zablokowane przez zbyt wiele prób logowania. Konto zostanie odblokowane {until.ToLocalTime():yyyy-MM-dd HH:mm:ss}");
                                }

                                Console.WriteLine(DateTime.UtcNow +" Failed login attempt: " + loginDto.Login);

                                return StatusCode(403, "Nieprawidłowy login lub hasło");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                string msg = "Nieprawidłowy login lub hasło";
                return StatusCode(403, msg);
            }
        }
        
        private string GenerateToken(int userId, int accessLevel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(jwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("userId", userId.ToString()),
                    new Claim("accessLevel", accessLevel.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpPost("logout/{userId}")]
        public IActionResult Logout(int userId)
        {
            try
            {
                Console.WriteLine(DateTime.UtcNow +" user: ["+userId+"] logged out successfully.");
                return Ok("User logged out successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("forget-user/{userId}")]
        public IActionResult ForgetUser(int userId)
        {
            try
            {
                using (var connection = new SqliteConnection(databasePath))
                {
                    connection.Open();


                    string randomString(int length)
                    {
                        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                        var rand = new Random();
                        var arr = new char[length];
                        for (int i = 0; i < length; i++)
                            arr[i] = chars[rand.Next(chars.Length)];
                        return new string(arr);
                    }

                    string randomDigits(int length)
                    {
                        var chars = "0123456789";
                        var rand = new Random();
                        var arr = new char[length];
                        for (int i = 0; i < length; i++)
                            arr[i] = chars[rand.Next(chars.Length)];
                        return new string(arr);
                    }

                    string updateQuery = @"
                        UPDATE users SET
                            login = @login,
                            password = @password,
                            password1 = @password1,
                            password2 = @password2,
                            password3 = @password3,
                            name = @name,
                            surname = @surname,
                            city = @city,
                            post_number = @postNumber,
                            street = @street,
                            property_number = @propertyNumber,
                            apartment_number = @apartmentNumber,
                            pesel = @pesel,
                            date_of_birth = @dateOfBirth,
                            sex = @sex,
                            email = @Email,
                            phone_number = @phoneNumber,
                            forgotten = 1
                        WHERE id = @userId";
                    using (var command = new SqliteCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@login", randomString(12));
                        command.Parameters.AddWithValue("@password", randomString(16));
                        command.Parameters.AddWithValue("@password1", randomString(16));
                        command.Parameters.AddWithValue("@password2", randomString(16));
                        command.Parameters.AddWithValue("@password3", randomString(16));
                        command.Parameters.AddWithValue("@name", randomString(8));
                        command.Parameters.AddWithValue("@surname", randomString(10));
                        command.Parameters.AddWithValue("@city", randomString(10));
                        command.Parameters.AddWithValue("@postNumber", randomDigits(5));
                        command.Parameters.AddWithValue("@street", randomString(10));
                        command.Parameters.AddWithValue("@propertyNumber", randomDigits(3));
                        command.Parameters.AddWithValue("@apartmentNumber", int.Parse(randomDigits(2)));
                        command.Parameters.AddWithValue("@pesel", randomDigits(11));
                        command.Parameters.AddWithValue("@dateOfBirth", DateTime.UtcNow.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@sex", "X");
                        command.Parameters.AddWithValue("@Email", randomString(10) + "@example.com");
                        command.Parameters.AddWithValue("@phoneNumber", randomDigits(9));
                        command.Parameters.AddWithValue("@userId", userId);
                        command.ExecuteNonQuery();
                    }
                }
                Console.WriteLine(DateTime.UtcNow + " User forgotten successfully.");
                return Ok("User forgotten successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("get-all-users")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = new List<object>();
                using (var connection = new SqliteConnection(databasePath))
                {
                    connection.Open();
                    string query = @"SELECT id, login, password, name, surname, city, post_number, street, property_number, apartment_number, pesel, date_of_birth, sex, email, phone_number, forgotten, access_level, temp_password
                                     FROM users WHERE forgotten = 0";
                    using (var command = new SqliteCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var user = new
                                {
                                    Id = reader.GetInt32(0),
                                    Login = reader.GetString(1),
                                    Password = reader.GetString(2),
                                    Name = reader.GetString(3),
                                    Surname = reader.GetString(4),
                                    City = reader.GetString(5),
                                    PostNumber = reader.GetString(6),
                                    Street = reader.GetString(7),
                                    PropertyNumber = reader.GetString(8),
                                    ApartmentNumber = reader.GetInt32(9),
                                    Pesel = reader.GetString(10),
                                    DateOfBirth = reader.GetString(11),
                                    Sex = reader.GetString(12),
                                    Email = reader.GetString(13),
                                    PhoneNumber = reader.GetString(14),
                                    Forgotten = reader.GetInt32(15) == 1,
                                    AccessLevel = reader.GetInt32(16),
                                    temp_password = reader.IsDBNull(17) ? null : reader.GetString(17)
                                };
                                users.Add(user);
                            }
                        }
                    }
                }
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("find-user-by-login/{login}")]
        public IActionResult FindUserByLogin(string login)
        {
            try
            {
                using (SqliteConnection connection = new SqliteConnection(databasePath))
                {
                    connection.Open();
                    string query = @"SELECT id, login, name, surname, city, post_number, street, property_number, apartment_number, pesel, date_of_birth, sex, email, phone_number, forgotten 
                                    FROM users WHERE login = @login AND forgotten = 0";
                    using (SqliteCommand command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@login", login);
                        using (SqliteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var user = new
                                {
                                    id = reader.GetInt32(0),
                                    login = reader.GetString(1),
                                    name = reader.GetString(2),
                                    surname = reader.GetString(3),
                                    city = reader.GetString(4),
                                    postNumber = reader.GetString(5),
                                    street = reader.GetString(6),
                                    propertyNumber = reader.GetString(7),
                                    apartmentNumber = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8),
                                    pesel = reader.GetString(9),
                                    dateOfBirth = reader.GetDateTime(10),
                                    sex = reader.IsDBNull(11) ? (char?)null : reader.GetString(11)[0],
                                    email = reader.GetString(12),
                                    phoneNumber = reader.GetString(13)
                                };
                                return Ok(user);
                            }
                        }
                    }
                }
                return NotFound("User not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("find-user-by-id/{id}")]
        public IActionResult FindUserById(int id)
        {
            try
            {
                using (SqliteConnection connection = new SqliteConnection(databasePath))
                {
                    connection.Open();
                    string query = @"SELECT id, login, name, surname, city, post_number, street, property_number, apartment_number, pesel, date_of_birth, sex, email, phone_number, forgotten 
                                    FROM users WHERE id = @id AND forgotten = 0";
                    using (SqliteCommand command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqliteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var user = new
                                {
                                    id = reader.GetInt32(0),
                                    login = reader.GetString(1),
                                    name = reader.GetString(2),
                                    surname = reader.GetString(3),
                                    city = reader.GetString(4),
                                    postNumber = reader.GetString(5),
                                    street = reader.GetString(6),
                                    propertyNumber = reader.GetString(7),
                                    apartmentNumber = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8),
                                    pesel = reader.GetString(9),
                                    dateOfBirth = reader.GetDateTime(10),
                                    sex = reader.IsDBNull(11) ? (char?)null : reader.GetString(11)[0],
                                    email = reader.GetString(12),
                                    phoneNumber = reader.GetString(13)
                                };
                                return Ok(user);
                            }
                        }
                    }
                }
                return NotFound("User not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("get-all-forgotten")]
        public IActionResult GetAllForgotten()
        {
            try
            {
                var users = new List<object>();
                using (var connection = new SqliteConnection(databasePath))
                {
                    connection.Open();
                    string query = @"SELECT id, login, password, name, surname, city, post_number, street, property_number, apartment_number, pesel, date_of_birth, sex, email, phone_number, forgotten 
                                     FROM users WHERE forgotten = 1";
                    using (var command = new SqliteCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var user = new
                                {
                                    Id = reader.GetInt32(0),
                                    Login = reader.GetString(1),
                                    Password = reader.GetString(2),
                                    Name = reader.GetString(3),
                                    Surname = reader.GetString(4),
                                    City = reader.GetString(5),
                                    PostNumber = reader.GetString(6),
                                    Street = reader.GetString(7),
                                    PropertyNumber = reader.GetString(8),
                                    ApartmentNumber = reader.GetInt32(9),
                                    Pesel = reader.GetString(10),
                                    DateOfBirth = reader.GetString(11),
                                    Sex = reader.GetString(12),
                                    Email = reader.GetString(13),
                                    PhoneNumber = reader.GetString(14),
                                    Forgotten = reader.GetInt32(15) == 1
                                };
                                users.Add(user);
                            }
                        }
                    }
                }
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("change-user-data/{userId}")]
        public IActionResult ChangeUserData(int userId, [FromBody] UserDto user)
        {
            try
            {
                using (var connection = new SqliteConnection(databasePath))
                {
                    connection.Open();

                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        string checkQuery = @"SELECT password, password1, password2 FROM users WHERE id = @userId";
                        using (var checkCmd = new SqliteCommand(checkQuery, connection))
                        {
                            checkCmd.Parameters.AddWithValue("@userId", userId);
                            using (var reader = checkCmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string currentPassword = reader.IsDBNull(0) ? null : reader.GetString(0);
                                    if (currentPassword == user.Password)
                                    {
                                        return StatusCode(403,"Hasło nie może być takie samo jak aktualne.");
                                    }
                                    string prev1 = reader.IsDBNull(1) ? null : reader.GetString(1);
                                    string prev2 = reader.IsDBNull(2) ? null : reader.GetString(2);
                                    if ((prev1 != null && user.Password == prev1) || (prev2 != null && user.Password == prev2))
                                    {
                                        return StatusCode(403, "Hasło musi być różne od 3 ostatnich.");
                                    }
                                }
                            }
                        }

                        string movePasswords = @"
                            UPDATE users SET 
                                password3 = password2,
                                password2 = password1,
                                password1 = password
                            WHERE id = @userId";
                        using (var moveCmd = new SqliteCommand(movePasswords, connection))
                        {
                            moveCmd.Parameters.AddWithValue("@userId", userId);
                            moveCmd.ExecuteNonQuery();
                        }
                    }

                    string query = @"UPDATE users 
                                     SET login = COALESCE(@login, login), 
                                         password = COALESCE(@password, password), 
                                         name = COALESCE(@name, name), 
                                         surname = COALESCE(@surname, surname), 
                                         city = COALESCE(@city, city), 
                                         post_number = COALESCE(@postNumber, post_number),
                                         street = COALESCE(@street, street), 
                                         property_number = COALESCE(@propertyNumber, property_number), 
                                         apartment_number = COALESCE(@apartmentNumber, apartment_number), 
                                         pesel = COALESCE(@pesel, pesel), 
                                         date_of_birth = COALESCE(@dateOfBirth, date_of_birth), 
                                         sex = COALESCE(@sex, sex), 
                                         email = COALESCE(@Email, email), 
                                         phone_number = COALESCE(@phoneNumber, phone_number),
                                         access_level = COALESCE(@accessLevel, access_level) 
                                     WHERE id = @userId";
                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);
                        command.Parameters.AddWithValue("@login", (object?)user.Login ?? DBNull.Value);
                        command.Parameters.AddWithValue("@password", (object?)user.Password ?? DBNull.Value);
                        command.Parameters.AddWithValue("@name", (object?)user.Name ?? DBNull.Value);
                        command.Parameters.AddWithValue("@surname", (object?)user.Surname ?? DBNull.Value);
                        command.Parameters.AddWithValue("@city", (object?)user.City ?? DBNull.Value);
                        command.Parameters.AddWithValue("@postNumber", (object?)user.PostNumber ?? DBNull.Value);
                        command.Parameters.AddWithValue("@street", (object?)user.Street ?? DBNull.Value);
                        command.Parameters.AddWithValue("@propertyNumber", (object?)user.PropertyNumber ?? DBNull.Value);
                        command.Parameters.AddWithValue("@apartmentNumber", (object?)user.ApartmentNumber ?? DBNull.Value);
                        command.Parameters.AddWithValue("@pesel", (object?)user.Pesel ?? DBNull.Value);
                        command.Parameters.AddWithValue("@dateOfBirth", user.DateOfBirth != default ? user.DateOfBirth.ToString("yyyy-MM-dd") : DBNull.Value);
                        command.Parameters.AddWithValue("@sex", user.Sex.HasValue ? user.Sex.ToString() : DBNull.Value);
                        command.Parameters.AddWithValue("@Email", (object?)user.Email ?? DBNull.Value);
                        command.Parameters.AddWithValue("@phoneNumber", (object?)user.PhoneNumber ?? DBNull.Value);
                        command.Parameters.AddWithValue("@accessLevel", (object?)user.AccessLevel ?? DBNull.Value);
                        command.ExecuteNonQuery();
                    }


                    if (user.recovery.GetValueOrDefault(false))
                    {
                        string recoveryQuery = @"UPDATE users SET temp_password = NULL WHERE id = @userId";
                        using (var recoveryCommand = new SqliteCommand(recoveryQuery, connection))
                        {
                            recoveryCommand.Parameters.AddWithValue("@userId", userId);
                            recoveryCommand.ExecuteNonQuery();
                        }
                    }
                }
                Console.WriteLine(DateTime.UtcNow + " id: " + userId + " user data updated successfully.");
                return Ok("User data updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.UtcNow + " Error during user data update: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("update-access-level/{userId}")]
        public IActionResult UpdateAccessLevel(int userId, [FromBody] int accessLevel)
        {
            try
            {
                using (var connection = new SqliteConnection(databasePath))
                {
                    connection.Open();
                    string query = @"UPDATE users SET access_level = @accessLevel WHERE id = @userId";
                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);
                        command.Parameters.AddWithValue("@accessLevel", accessLevel);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine(DateTime.UtcNow + " id: " + userId + " access level updated successfully.");
                            return Ok("Access level updated successfully.");
                        }
                        else
                        {

                            return NotFound("User not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.UtcNow + " Error during access level update: " + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("recover-password")]
        public IActionResult RecoverPassword([FromBody] RecoverPasswordDto dto)
        {
            try
            {
                string tempPassword = GenerateTemporaryPassword();
                string userEmail = dto.Email;
                int affected = 0;

                using (var connection = new SqliteConnection(databasePath))
                {
                    connection.Open();
                    string expireAt = DateTime.UtcNow.AddMinutes(2).ToString("yyyy-MM-dd HH:mm:ss");//zmiany czsu wygasania
                    string query = @"UPDATE users SET temp_password = @tempPassword, temp_password_expire = @expireAt WHERE email = @Email";
                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@tempPassword", tempPassword);
                        command.Parameters.AddWithValue("@expireAt", expireAt);
                        command.Parameters.AddWithValue("@Email", userEmail);
                        affected = command.ExecuteNonQuery();
                    }
                }

                if (affected > 0)
                {
                    SendTemporaryPasswordEmail(userEmail, tempPassword);
                    return Ok("E-mail z tymczasowym hasłem został wysłany.");
                }
                else
                {
                    return BadRequest("Nieprawidłowe dane");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.UtcNow + " Error during password recovery: " + ex.Message);
                return BadRequest("Nieprawidłowe dane");
            }
        }

        private string GenerateTemporaryPassword()
        {

            var upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var lower = "abcdefghijklmnopqrstuvwxyz";
            var digits = "0123456789";
            var specials = "-_!*#$";
            var random = new Random();

            char[] password = new char[10];
            for (int i = 0; i < 3; i++)
                password[i] = upper[random.Next(upper.Length)];
            for (int i = 3; i < 6; i++)
                password[i] = lower[random.Next(lower.Length)];
            for (int i = 6; i < 8; i++)
                password[i] = digits[random.Next(digits.Length)];
            for (int i = 8; i < 10; i++)
                password[i] = specials[random.Next(specials.Length)];

            for (int i = password.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                var tmp = password[i];
                password[i] = password[j];
                password[j] = tmp;
            }

            return new string(password);
        }

        private void SendTemporaryPasswordEmail(string email, string tempPassword)
        {
            try
            {
                using (var client = new System.Net.Mail.SmtpClient("smtp.gmail.com")) //według preferencji
                {
                    client.Port = 587;

                    client.Credentials = new System.Net.NetworkCredential("", ""); // adres, hasło do konta e-mail
                    client.EnableSsl = true;

                    var mail = new System.Net.Mail.MailMessage();
                    mail.From = new System.Net.Mail.MailAddress(""); // adres e-mail nadawcy
                    mail.To.Add(email);
                    mail.Subject = "Password Recovery";
                    mail.Body = $"Twoje hasło tymczasowe(zostanie usunięte po 24h): {tempPassword}";

                    client.Send(mail);
                    Console.WriteLine(DateTime.UtcNow + " Temporary password sent to: " + email);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.UtcNow + " Error sending email: " + ex.Message);
            }
        }

        [HttpGet("check-role/{accessLevel}")]
        public IActionResult CheckRole(int accessLevel)
        {
            try
            {
                using (var connection = new SqliteConnection(databasePath))
                {
                    connection.Open();
                    string query = @"SELECT id, role_name, dodawanie, listowanie, zapominanie, zapomniani, edycja, wyporzyczenie, uprawnienia FROM roles WHERE access_level = @accessLevel";
                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@accessLevel", accessLevel);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var result = new
                                {
                                    id = reader.GetInt32(0),
                                    role_name = reader.GetString(1),
                                    dodawanie = reader.GetInt32(2),
                                    listowanie = reader.GetInt32(3),
                                    zapominanie = reader.GetInt32(4),
                                    zapomniani = reader.GetInt32(5),
                                    edycja = reader.GetInt32(6),
                                    wyporzyczenie = reader.GetInt32(7),
                                    uprawnienia = reader.GetInt32(8)
                                };
                                return Ok(result);
                            }
                            else
                            {
                                return NotFound("Role not found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("add-role")]
        public IActionResult AddRole([FromBody] RoleDto role)
        {
            try
            {
                using (var connection = new SqliteConnection(databasePath))
                {
                    connection.Open();
                    string query = @"INSERT INTO roles 
                        (role_name, access_level, dodawanie, listowanie, zapominanie, zapomniani, edycja, wyporzyczenie, uprawnienia)
                        VALUES (@role_name, @access_level, @dodawanie, @listowanie, @zapominanie, @zapomniani, @edycja, @wyporzyczenie, @uprawnienia)";
                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@role_name", role.RoleName);
                        command.Parameters.AddWithValue("@access_level", role.AccessLevel);
                        command.Parameters.AddWithValue("@dodawanie", role.Dodawanie ? 1 : 0);
                        command.Parameters.AddWithValue("@listowanie", role.Listowanie ? 1 : 0);
                        command.Parameters.AddWithValue("@zapominanie", role.Zapominanie ? 1 : 0);
                        command.Parameters.AddWithValue("@zapomniani", role.Zapomniani ? 1 : 0);
                        command.Parameters.AddWithValue("@edycja", role.Edycja ? 1 : 0);
                        command.Parameters.AddWithValue("@wyporzyczenie", role.Wyporzyczenie ? 1 : 0);
                        command.Parameters.AddWithValue("@uprawnienia", role.Uprawnienia ? 1 : 0);
                        command.ExecuteNonQuery();
                    }
                }
                return Ok("Role added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("edit-role/{accessLevel}")]
        public IActionResult EditRole(int accessLevel, [FromBody] RoleDto role)
        {
            try
            {
                using (var connection = new SqliteConnection(databasePath))
                {
                    connection.Open();
                    string query = @"UPDATE roles SET 
                role_name = @role_name,
                dodawanie = @dodawanie,
                listowanie = @listowanie,
                zapominanie = @zapominanie,
                zapomniani = @zapomniani,
                edycja = @edycja,
                wyporzyczenie = @wyporzyczenie,
                uprawnienia = @uprawnienia
                WHERE access_level = @access_level";
                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@role_name", role.RoleName);
                        command.Parameters.AddWithValue("@dodawanie", role.Dodawanie ? 1 : 0);
                        command.Parameters.AddWithValue("@listowanie", role.Listowanie ? 1 : 0);
                        command.Parameters.AddWithValue("@zapominanie", role.Zapominanie ? 1 : 0);
                        command.Parameters.AddWithValue("@zapomniani", role.Zapomniani ? 1 : 0);
                        command.Parameters.AddWithValue("@edycja", role.Edycja ? 1 : 0);
                        command.Parameters.AddWithValue("@wyporzyczenie", role.Wyporzyczenie ? 1 : 0);
                        command.Parameters.AddWithValue("@uprawnienia", role.Uprawnienia ? 1 : 0);
                        command.Parameters.AddWithValue("@access_level", accessLevel);
                        int affected = command.ExecuteNonQuery();
                        if (affected > 0)
                            return Ok("Role updated successfully.");
                        else
                            return NotFound("Role not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("get-all-roles")]
        public IActionResult GetAllRoles()
        {
            try
            {
                var roles = new List<object>();
                using (var connection = new SqliteConnection(databasePath))
                {
                    connection.Open();
                    string query = @"SELECT id, role_name, access_level, dodawanie, listowanie, zapominanie, zapomniani, edycja, wyporzyczenie, uprawnienia FROM roles";
                    using (var command = new SqliteCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var role = new
                                {
                                    Id = reader.GetInt32(0),
                                    RoleName = reader.GetString(1),
                                    AccessLevel = reader.GetInt32(2),
                                    Dodawanie = reader.GetInt32(3) == 1,
                                    Listowanie = reader.GetInt32(4) == 1,
                                    Zapominanie = reader.GetInt32(5) == 1,
                                    Zapomniani = reader.GetInt32(6) == 1,
                                    Edycja = reader.GetInt32(7) == 1,
                                    Wyporzyczenie = reader.GetInt32(8) == 1,
                                    Uprawnienia = reader.GetInt32(9) == 1
                                };
                                roles.Add(role);
                            }
                        }
                    }
                }
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("delete-role/{accessLevel}")]
        public IActionResult DeleteRole(int accessLevel)
        {
            try
            {
                using (var connection = new SqliteConnection(databasePath))
                {
                    connection.Open();
                    string query = @"DELETE FROM roles WHERE access_level = @accessLevel";
                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@accessLevel", accessLevel);
                        int affected = command.ExecuteNonQuery();
                        if (affected > 0)
                            return Ok("Role deleted successfully.");
                        else
                            return NotFound("Role not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

    public class UserDto
    {
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? City { get; set; }
        public string? PostNumber { get; set; }
        public string? Street { get; set; }
        public string? PropertyNumber { get; set; }
        public int? ApartmentNumber { get; set; }
        public string? Pesel { get; set; }
        public DateTime DateOfBirth { get; set; }
        public char? Sex { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? Forgotten { get; set; }
        public int? AccessLevel { get; set; }
        public bool? recovery { get; set; }
        public bool? admin { get; set; } 
    }

    public class LoginDto
    {
        public string? Login { get; set; }
        public string? Password { get; set; }
    }

    public class RecoverPasswordDto
    {
        public string? Email { get; set; }
    }

    public class RoleDto
    {
        public string RoleName { get; set; }
        public int AccessLevel { get; set; }
        public bool Dodawanie { get; set; }
        public bool Listowanie { get; set; }
        public bool Zapominanie { get; set; }
        public bool Zapomniani { get; set; }
        public bool Edycja { get; set; }
        public bool Wyporzyczenie { get; set; }
        public bool Uprawnienia { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://localhost:5185");
                    webBuilder.ConfigureKestrel(options =>
                    {
                        options.ListenAnyIP(5185);
                    });
                })
                .Build()
                .Run();
        }
    }
}
