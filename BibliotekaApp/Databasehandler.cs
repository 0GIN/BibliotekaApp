using System.DirectoryServices;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace BibliotekaApp
{
    public class DatabaseHandler
    {


        private readonly string apiBaseUrl = "http://localhost:5185";
        //private readonly string apiBaseUrl = "https://kpxzrf19-5185.euw.devtunnels.ms";
        //private readonly string apiBaseUrl = "https://5sqcn5m9-5185.euw.devtunnels.ms";

        public RoleDto CheckRole(int accessLevel)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{apiBaseUrl}/check-role/{accessLevel}").Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to get role: {response.ReasonPhrase}");
                }
                var responseContent = response.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<RoleDto>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        public void Logout(int userId)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsync($"{apiBaseUrl}/logout/{userId}", null).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to logout: {response.ReasonPhrase}");
                }
            }
        }
        public void AddUser(string login, string password, string name, string surname, string city, string postNumber, string street, string propertyNumber, int apartmentNumber, string pesel, DateTime dateOfBirth, char sex, string email, string phoneNumber)
        {
            using (var client = new HttpClient())
            {
                var user = new
                {
                    Login = login,
                    Password = password,
                    Name = name,
                    Surname = surname,
                    City = city,
                    PostNumber = postNumber,
                    Street = street,
                    PropertyNumber = propertyNumber,
                    ApartmentNumber = apartmentNumber,
                    Pesel = pesel,
                    DateOfBirth = dateOfBirth,
                    Sex = sex,
                    Email = email,
                    PhoneNumber = phoneNumber
                };
                var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
                var response = client.PostAsync($"{apiBaseUrl}/add-user", content).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to add user: {response.ReasonPhrase}");
                }
            }
        }

        public (string token, bool forgotten, bool recovery, bool admin) Login(string login, string password)
        {
            using (var client = new HttpClient())
            {
                var loginDto = new { Login = login, Password = password };
                var content = new StringContent(JsonSerializer.Serialize(loginDto), Encoding.UTF8, "application/json");
                var response = client.PostAsync($"{apiBaseUrl}/login", content).Result;

                if (!response.IsSuccessStatusCode)
                {
                    if ((int)response.StatusCode == 403)
                    {
                        var errorMsg = response.Content.ReadAsStringAsync().Result;
                        throw new Exception(errorMsg); // Przekazujemy komunikat z serwera
                    }
                    throw new Exception($"Login failed: {response.ReasonPhrase}");
                }

                using var doc = JsonDocument.Parse(response.Content.ReadAsStringAsync().Result);
                var root = doc.RootElement;

                string token = root.GetProperty("token").GetString();
                bool forgotten = root.GetProperty("forgotten").GetBoolean();
                bool recovery = root.GetProperty("recovery").GetBoolean();
                bool admin = root.GetProperty("admin").GetBoolean();

                return (token, forgotten, recovery, admin);
            }
        }

        public void ForgetUser(int userId)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsync($"{apiBaseUrl}/forget-user/{userId}", null).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to forget user: {response.ReasonPhrase}");
                }
            }
        }

        public List<UserDetailsDto> GetAllUsers()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{apiBaseUrl}/get-all-users").Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to get all users: {response.ReasonPhrase}");
                }
                var responseContent = response.Content.ReadAsStringAsync().Result;

                // Deserializacja odpowiedzi na listę UserDetailsDto
                return JsonSerializer.Deserialize<List<UserDetailsDto>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        public UserDetailsDto? FindUserByLogin(string login)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{apiBaseUrl}/find-user-by-login/{login}").Result;
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var responseContent = response.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrWhiteSpace(responseContent))
                {
                    return null;
                }

                var user = JsonSerializer.Deserialize<UserDetailsDto>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return user;
            }
        }

        public UserDetailsDto? FindUserById(int id)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{apiBaseUrl}/find-user-by-id/{id}").Result;
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var responseContent = response.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrWhiteSpace(responseContent))
                {
                    return null;
                }

                var user = JsonSerializer.Deserialize<UserDetailsDto>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return user;
            }
        }
        public void ChangeUserData(int userId, string? login = null, string? password = null, string? name = null, string? surname = null, string? city = null, string? postNumber = null, string? street = null, string? propertyNumber = null, int? apartmentNumber = null, string? pesel = null, DateTime? dateOfBirth = null, char? sex = null, string? email = null, string? phoneNumber = null, int? accessLevel = null, bool? recovery = null)
        {
            using (var client = new HttpClient())
            {
                var user = new
                {
                    Login = login,
                    Password = password,
                    Name = name,
                    Surname = surname,
                    City = city,
                    PostNumber = postNumber,
                    Street = street,
                    PropertyNumber = propertyNumber,
                    ApartmentNumber = apartmentNumber,
                    Pesel = pesel,
                    DateOfBirth = dateOfBirth?.ToString("yyyy-MM-dd"),
                    Sex = sex?.ToString(),
                    Email = email,
                    PhoneNumber = phoneNumber,
                    AccessLevel = accessLevel,
                    recovery
                };
                var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
                var response = client.PutAsync($"{apiBaseUrl}/change-user-data/{userId}", content).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to change user data: {response.ReasonPhrase}");
                }
            }
        }
        public List<UserDetailsDto> GetAllForgotten()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{apiBaseUrl}/get-all-forgotten").Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to get all users: {response.ReasonPhrase}");
                }
                var responseContent = response.Content.ReadAsStringAsync().Result;

                // Deserializacja odpowiedzi na listę UserDetailsDto
                return JsonSerializer.Deserialize<List<UserDetailsDto>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        public void RecoverPassword(string email)
        {
            using (var client = new HttpClient())
            {
                var dto = new { Email = email };
                var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
                var response = client.PostAsync($"{apiBaseUrl}/recover-password", content).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to recover password: {response.ReasonPhrase}");
                }
            }
        }
    }
}

public class RoleDto
{
    public int id { get; set; }
    public string role_name { get; set; }
    public int dodawanie { get; set; }
    public int listowanie { get; set; }
    public int zapominanie { get; set; }
    public int zapomniani { get; set; }
    public int edycja { get; set; }
    public int wyporzyczenie { get; set; }
    public int uprawnienia { get; set; }
}
    
    public class UserDetailsDto
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string PostNumber { get; set; }
        public string Street { get; set; }
        public string PropertyNumber { get; set; }
        public int? ApartmentNumber { get; set; }
        public string Pesel { get; set; }
        public DateTime DateOfBirth { get; set; }
        public char? Sex { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int AccessLevel { get; set; }
        public bool recovery { get; set; }
}

