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
        
        [HttpPost("create-database")]
        public IActionResult CreateDatabase()
        {
            try
            {
               using (var connection = new SqliteConnection(databasePath))
            {
                int count = 0;
                connection.Open();
                string query = @"CREATE TABLE IF NOT EXISTS users(
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                login TEXT UNIQUE,
                password TEXT,
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
                forgotten BOOLEAN DEFAULT 0)";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                count++;
                Console.WriteLine("TABLE users created "+count+"/7");

                query = @"CREATE TABLE IF NOT EXISTS authors(
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                name TEXT)";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                count++;
                Console.WriteLine("TABLE authors created "+count+"/7");

                query = @"CREATE TABLE IF NOT EXISTS categories(
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                name TEXT)";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                count++;
                Console.WriteLine("TABLE categories created "+count+"/7");

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
                Console.WriteLine("TABLE books created "+count+"/7");

                query = @"CREATE TABLE IF NOT EXISTS book_copies(
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                book_id INTEGER,
                status TEXT)";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                count++;
                Console.WriteLine("TABLE book_copies created "+count+"/7");

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
                Console.WriteLine("TABLE loans created "+count+"/7");

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
                Console.WriteLine("TABLE reservations created "+count+"/7");
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
                    string query = @"INSERT INTO users (login, password, name, surname, city, post_number, street, property_number, apartment_number, pesel, date_of_birth, sex, email, phone_number) 
                                     VALUES (@login, @password, @name, @surname, @city, @postNumber, @street, @propertyNumber, @apartmentNumber, @pesel, @dateOfBirth, @sex, @Email, @phoneNumber)";
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
                        command.ExecuteNonQuery();
                    }
                }
                
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
                    string query = @"SELECT id, forgotten FROM users WHERE login = @login AND password = @password";
                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@login", loginDto.Login);
                        command.Parameters.AddWithValue("@password", loginDto.Password);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int userId = reader.GetInt32(0);
                                bool forgotten = reader.GetInt32(1) == 1;
                                if (!forgotten)
                                {
                                    string token = GenerateToken(userId);
                                    return Ok(new { token, forgotten });
                                }
                                else
                                {
                                    throw new Exception("User is forgotten.");
                                }
                            }
                            else
                            {
                                throw new Exception("Invalid login credentials.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(401, ex.Message);
            }
        }
         private string GenerateToken(int userId)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var rawData = $"{userId}-{DateTime.UtcNow.Ticks}";
                var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(rawData));
                return Convert.ToBase64String(bytes);
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
                    string query = @"UPDATE users SET forgotten = 1 WHERE id = @userId";
                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);
                        command.ExecuteNonQuery();
                    }
                }

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
                    string query = @"SELECT id, login, password, name, surname, city, post_number, street, property_number, apartment_number, pesel, date_of_birth, sex, email, phone_number, forgotten 
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
                            if (reader.Read())//czytelność
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
                                         phone_number = COALESCE(@phoneNumber, phone_number) 
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
                        command.ExecuteNonQuery();
                    }
                }

                return Ok("User data updated successfully.");
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
    }

    public class LoginDto
    {
        public string? Login { get; set; }
        public string? Password { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://localhost:5185");//https://5sqcn5m9-5185.euw.devtunnels.ms"
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
