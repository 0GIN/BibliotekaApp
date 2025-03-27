using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaApp
{
    internal class Class1
    {
        private Databasehandler database = new Databasehandler();

        public void DisplayAllUsers()
        {
            var users = database.GetAllUsers();
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.id}, Login: {user.login}, Name: {user.name}, Surname: {user.surname}");
            }
        }
    }
}
