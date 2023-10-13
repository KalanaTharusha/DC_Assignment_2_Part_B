using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bank_Data_DLL
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public TransactionType Type { get; set; }
        public double Amount { get; set; }
        public DateTime DateTime { get; set; }
        public int AccountId { get; set; }
        [JsonIgnore]
        public Account Account { get; set; }

        public enum TransactionType
        {
            Deposit,
            Withdrawal
        }
    }
}
