using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BibliotekaApp;

namespace BibliotekaApp
{
    public static class ValidationHelper
    {
        public static bool ValidateUserData(
                string name, string surname, string city, string postNumber,
                string street, string propertyNumber, string pesel,
                DateTime dateOfBirth, string sexText, string email, string phoneNumber, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 2 || !name.All(char.IsLetter))
            {
                errorMessage = "Imię musi mieć co najmniej 2 litery i zawierać tylko litery.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(surname) || surname.Length < 2 || !surname.All(char.IsLetter))
            {
                errorMessage = "Nazwisko musi mieć co najmniej 2 litery i zawierać tylko litery.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(city) || city.Length < 2)
            {
                errorMessage = "Miasto musi mieć co najmniej 2 znaki.";
                return false;
            }

            if (!Regex.IsMatch(postNumber, @"^\d{2}-\d{3}$"))
            {
                errorMessage = "Kod pocztowy musi być w formacie 00-000.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(street))
            {
                errorMessage = "Ulica nie może być pusta.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(propertyNumber))
            {
                errorMessage = "Numer budynku nie może być pusty.";
                return false;
            }

            if (dateOfBirth >= DateTime.Today)
            {
                errorMessage = "Data urodzenia musi być wcześniejsza niż dzisiaj.";
                return false;
            }

            if (string.IsNullOrEmpty(sexText) || (sexText != "M" && sexText != "K"))
            {
                errorMessage = "Wybierz płeć (M lub K).";
                return false;
            }

            if (!IsPeselValid(pesel, dateOfBirth, sexText[0], out string peselErrors))
            {
                errorMessage = peselErrors;
                return false;
            }

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.Contains("."))
            {
                errorMessage = "Niepoprawny adres e-mail.";
                return false;
            }

            if (!Regex.IsMatch(phoneNumber, @"^\d{9}$"))
            {
                errorMessage = "Numer telefonu musi mieć 9 cyfr.";
                return false;
            }

            errorMessage = null;
            return true;
        }

        public static bool IsPeselValid(string pesel, DateTime birthDate, char sex, out string detailedError)
        {
            detailedError = "";
            var errors = new List<string>();

            if (pesel.Length != 11 || !pesel.All(char.IsDigit))
            {
                errors.Add("PESEL musi zawierać dokładnie 11 cyfr.");
                detailedError = string.Join(" ", errors);
                return false;
            }

            // Cyfra kontrolna
            int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            int controlSum = 0;
            for (int i = 0; i < 10; i++)
            {
                controlSum += (pesel[i] - '0') * weights[i];
            }
            int lastDigit = (10 - (controlSum % 10)) % 10;
            if (lastDigit != (pesel[10] - '0'))
            {
                errors.Add("Nieprawidłowa cyfra kontrolna PESEL.");
            }

            // Data urodzenia z PESEL
            int year = int.Parse(pesel.Substring(0, 2));
            int month = int.Parse(pesel.Substring(2, 2));
            int day = int.Parse(pesel.Substring(4, 2));

            int century = 1900;
            if (month >= 1 && month <= 12)
                century = 1900;
            else if (month >= 21 && month <= 32) { century = 2000; month -= 20; }
            else if (month >= 81 && month <= 92) { century = 1800; month -= 80; }
            else if (month >= 41 && month <= 52) { century = 2100; month -= 40; }
            else if (month >= 61 && month <= 72) { century = 2200; month -= 60; }
            else
            {
                errors.Add("Nieprawidłowy miesiąc w PESEL.");
                detailedError = string.Join(" ", errors);
                return false;
            }

            DateTime parsedDate;
            try
            {
                parsedDate = new DateTime(century + year, month, day);
                if (parsedDate.Date != birthDate.Date)
                {
                    errors.Add("Data urodzenia nie zgadza się z PESEL.");
                }
            }
            catch
            {
                errors.Add("Nieprawidłowa data w PESEL.");
            }

            // Płeć
            int genderDigit = pesel[9] - '0';
            char genderFromPesel = (genderDigit % 2 == 1) ? 'M' : 'K';
            if (char.ToUpper(genderFromPesel) != char.ToUpper(sex))
            {
                errors.Add("Płeć nie zgadza się z PESEL.");
            }

            detailedError = string.Join(" ", errors);
            return errors.Count == 0;
        }

        public static bool ValidatePassword(string password, out string errorMessage)
        {
            errorMessage = null;

            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            {
                errorMessage = "Hasło musi mieć co najmniej 8 znaków.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(password) || password.Length > 15)
            {
                errorMessage = "Hasło musi mieć co najmniej 8 znaków.";
                return false;
            }

            if (!password.Any(char.IsUpper))
            {
                errorMessage = "Hasło musi zawierać co najmniej jedną wielką literę.";
                return false;
            }

            if (!password.Any(char.IsLower))
            {
                errorMessage = "Hasło musi zawierać co najmniej jedną małą literę.";
                return false;
            }

            if (!password.Any(char.IsDigit))
            {
                errorMessage = "Hasło musi zawierać co najmniej jedną cyfrę.";
                return false;
            }

            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                errorMessage = "Hasło musi zawierać co najmniej jeden znak specjalny.";
                return false;
            }

            return true;
        }

    }
}
