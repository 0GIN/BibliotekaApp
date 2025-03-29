using System;
using System.Data.Entity.Migrations.Model;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Data.Sqlite;
using SQLitePCL;

namespace BibliotekaApp
{
    public class Databasehandler
    {
        string databasePath = "Data Source=C:\\Users\\janog\\Desktop\\BibliotekaApp\\DATABASE.sqlite";
        //tworzenie bazy danych
        public void CreateDatabase()
        {
            using (SqliteConnection connection = new SqliteConnection(databasePath))
            {
                connection.Open();
                // Create table users
                string query = @"CREATE TABLE IF NOT EXISTS users(
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                login VARCHAR(30) UNIQUE,
                name VARCHAR(255),
                surname VARCHAR(255),
                city VARCHAR(255),
                post_number VARCHAR(6),
                street VARCHAR(255),
                property_number VARCHAR(10),
                apartment_number INT,
                pesel VARCHAR(11) UNIQUE,
                date_of_birth DATE,
                sex CHAR,
                email VARCHAR(255) UNIQUE,
                phone_number VARCHAR(9),
                forgotten BOOL)";
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("TABLE users created");

                // Create table authors
                query = @"CREATE TABLE IF NOT EXISTS authors(
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                name VARCHAR(255))";
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("TABLE authors created");

                // Create table categories
                query = @"CREATE TABLE IF NOT EXISTS categories(
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                name VARCHAR(255))";
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("TABLE categories created");

                // Create table books
                query = @"CREATE TABLE IF NOT EXISTS books(
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                title VARCHAR(255),
                author_id INTAGER,
                category_id INTAGER,
                isbn VARCHAR(13) unique,
                publish_date INTAGER)";
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("TABLE books created");

                // Create table book_copies
                query = @"CREATE TABLE IF NOT EXISTS book_copies(
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                book_id INTAGER,
                status VARCHAR(255))";
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("TABLE book_copies created");

                // Create table loans
                query = @"CREATE TABLE IF NOT EXISTS loans(
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                user_id INTAGER,
                book_copy_id INTAGER,
                loan_date DATE,
                return_date DATE,
                status VARCHAR(255))";
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("TABLE loans created");

                // Create table reservations
                query = @"CREATE TABLE IF NOT EXISTS reservations(
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                user_id INTAGER,
                book_id INTAGER,
                reservation_date DATE,
                status VARCHAR(255))";
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("TABLE reservations created");

                connection.Close();
            }
        }
        //dodawanie użytkownika
        public void AddUser(string login, string name, string surname, string city, string postNumber, string street, string propertyNumber, int apartmentNumber, string pesel, DateTime dateOfBirth, char sex, string email, string phoneNumber, bool forgotten)
        {
            using (SqliteConnection connection = new SqliteConnection(databasePath))
            {
                connection.Open();
                string query = @"INSERT INTO users (login, name, surname, city, post_number, street, property_number, apartment_number, pesel, date_of_birth, sex, email, phone_number, forgotten) 
                                 VALUES (@login, @name, @surname, @city, @postNumber, @street, @propertyNumber, @apartmentNumber, @pesel, @dateOfBirth, @sex, @Email, @phoneNumber, @forgotten)";
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@surname", surname);
                    command.Parameters.AddWithValue("@city", city);
                    command.Parameters.AddWithValue("@postNumber", postNumber);
                    command.Parameters.AddWithValue("@street", street);
                    command.Parameters.AddWithValue("@propertyNumber", propertyNumber);
                    command.Parameters.AddWithValue("@apartmentNumber", apartmentNumber);
                    command.Parameters.AddWithValue("@pesel", pesel);
                    command.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
                    command.Parameters.AddWithValue("@sex", sex);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                    command.Parameters.AddWithValue("@forgotten", forgotten);
                    command.ExecuteNonQuery();
                }
            }
        }
        //logowanie użytkownika
        public (string token, bool forgotten) Login(string login, string password)
        {
            using (SqliteConnection connection = new SqliteConnection(databasePath))
            {
                connection.Open();
                string query = @"SELECT id, forgotten FROM users WHERE login = @login AND password = @password";
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", password);
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int userId = reader.GetInt32(0);
                            bool forgotten = reader.GetBoolean(1);
                            if (forgotten == false)
                            {
                                string token = GenerateToken(userId);
                                return (token, forgotten);
                            }
                            else
                            {
                                throw new Exception("user zapomniany");
                            }
                        }
                        else
                        {
                            throw new Exception("złe dane logowania");
                        }
                    }
                }
            }
        }
        //generowanie tokena
        private string GenerateToken(int userId)
        {
            using (var sha256 = SHA256.Create())
            {
                var rawData = $"{userId}-{DateTime.UtcNow.Ticks}";
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                return Convert.ToBase64String(bytes);
            }
        }
        //zapomnienie użytkownika
        public void ForgetUser(int userId)
        {
            using (SqliteConnection connection = new SqliteConnection(databasePath))
            {
                connection.Open();
                string query = @"UPDATE users SET forgotten = 1 WHERE id = @userId";
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.ExecuteNonQuery();
                }
            }
        }
        //lista użytkowników
        public List<(int id, string login, string name, string surname, string city, string postNumber, string street, string propertyNumber, int apartmentNumber, string pesel, DateTime dateOfBirth, char sex, string email, string phoneNumber, bool forgotten)> GetAllUsers()
        {
            var users = new List<(int, string, string, string, string, string, string, string, int, string, DateTime, char, string, string, bool)>();

            using (SqliteConnection connection = new SqliteConnection(databasePath))
            {
                connection.Open();
                string query = @"SELECT id, login, name, surname, city, post_number, street, property_number, apartment_number, pesel, date_of_birth, sex, email, phone_number, forgotten 
                                 FROM users WHERE forgotten = 0";
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string login = reader.GetString(1);
                            string name = reader.GetString(2);
                            string surname = reader.GetString(3);
                            string city = reader.GetString(4);
                            string postNumber = reader.GetString(5);
                            string street = reader.GetString(6);
                            string propertyNumber = reader.GetString(7);
                            int apartmentNumber = reader.GetInt32(8);
                            string pesel = reader.GetString(9);
                            DateTime dateOfBirth = reader.GetDateTime(10);
                            char sex = reader.GetString(11)[0];
                            string email = reader.GetString(12);
                            string phoneNumber = reader.GetString(13);
                            bool forgotten = reader.GetBoolean(14);

                            users.Add((id, login, name, surname, city, postNumber, street, propertyNumber, apartmentNumber, pesel, dateOfBirth, sex, email, phoneNumber, forgotten));
                        }
                    }
                }
            }
            return users;
        }
        //wyszukiwanie urzytkownika po loginie
        public (int id, string login, string name, string surname, string city, string postNumber, string street, string propertyNumber, int apartmentNumber, string pesel, DateTime dateOfBirth, char sex, string email, string phoneNumber)? FindUserByLogin(string login)
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
                            int id = reader.GetInt32(0);
                            string userLogin = reader.GetString(1);
                            string name = reader.GetString(2);
                            string surname = reader.GetString(3);
                            string city = reader.GetString(4);
                            string postNumber = reader.GetString(5);
                            string street = reader.GetString(6);
                            string propertyNumber = reader.GetString(7);
                            int apartmentNumber = reader.GetInt32(8);
                            string pesel = reader.GetString(9);
                            DateTime dateOfBirth = reader.GetDateTime(10);
                            char sex = reader.GetString(11)[0];
                            string email = reader.GetString(12);
                            string phoneNumber = reader.GetString(13);


                            return (id, userLogin, name, surname, city, postNumber, street, propertyNumber, apartmentNumber, pesel, dateOfBirth, sex, email, phoneNumber);
                        }
                    }
                }
            }
            return null;
        }
        //wyszukiwanie zapomnianego użytkownika
        public (int id, string login, string name, string surname, string city, string postNumber, string street, string propertyNumber, int apartmentNumber, string pesel, DateTime dateOfBirth, char sex, string email, string phoneNumber)? FindForgottenUserByLogin(string login)
        {
            using (SqliteConnection connection = new SqliteConnection(databasePath))
            {
                connection.Open();
                string query = @"SELECT id, login, name, surname, city, post_number, street, property_number, apartment_number, pesel, date_of_birth, sex, email, phone_number, forgotten 
                                 FROM users WHERE login = @login AND forgotten = 1";
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@login", login);
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string userLogin = reader.GetString(1);
                            string name = reader.GetString(2);
                            string surname = reader.GetString(3);
                            string city = reader.GetString(4);
                            string postNumber = reader.GetString(5);
                            string street = reader.GetString(6);
                            string propertyNumber = reader.GetString(7);
                            int apartmentNumber = reader.GetInt32(8);
                            string pesel = reader.GetString(9);
                            DateTime dateOfBirth = reader.GetDateTime(10);
                            char sex = reader.GetString(11)[0];
                            string email = reader.GetString(12);
                            string phoneNumber = reader.GetString(13);


                            return (id, userLogin, name, surname, city, postNumber, street, propertyNumber, apartmentNumber, pesel, dateOfBirth, sex, email, phoneNumber);
                        }
                    }
                }
            }
            return null;
        }
        //wyszukiwanie użytkownika po id
        public (int id, string login, string name, string surname, string city, string postNumber, string street, string propertyNumber, int apartmentNumber, string pesel, DateTime dateOfBirth, char sex, string email, string phoneNumber)? FindUserById(int id)
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
                            string userLogin = reader.GetString(1);
                            string name = reader.GetString(2);
                            string surname = reader.GetString(3);
                            string city = reader.GetString(4);
                            string postNumber = reader.GetString(5);
                            string street = reader.GetString(6);
                            string propertyNumber = reader.GetString(7);
                            int apartmentNumber = reader.GetInt32(8);
                            string pesel = reader.GetString(9);
                            DateTime dateOfBirth = reader.GetDateTime(10);
                            char sex = reader.GetString(11)[0];
                            string email = reader.GetString(12);
                            string phoneNumber = reader.GetString(13);


                            return (id, userLogin, name, surname, city, postNumber, street, propertyNumber, apartmentNumber, pesel, dateOfBirth, sex, email, phoneNumber);
                        }
                    }
                }
            }
            return null;
        }

    }
}