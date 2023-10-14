using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bank_Data_DLL
{
    public class Account
    {
        public int AccountId { get; set; }
        public int AccountNo { get; set; }
        public double Balance { get; set; }
        public int UserId { get; set; }
        public AccountStatus Status { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

        public enum AccountStatus
        {
            Activated,
            Deactivated
        }
    }
}
