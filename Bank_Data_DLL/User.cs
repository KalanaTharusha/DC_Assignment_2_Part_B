using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bank_Data_DLL
{
    public class User
    {
        public int UserId { get; set; }
        public UserRole Role { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public string Picture { get; set; }
        public string Password { get; set; }
        public ICollection<Account> Accounts { get; set; }

        public enum UserRole
        {
            client,
            admin
        }
    }
}
