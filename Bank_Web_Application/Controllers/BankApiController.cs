using Bank_Data_DLL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace Bank_Web_Application.Controllers
{
    public class BankApiController : Controller
    {

        private String DataService = "http://localhost:5161/";
        private RestClient client;

        [HttpPost]
        public IActionResult Signup([FromBody] User reqBody)
        {
            client = new RestClient(DataService);
            RestRequest request = new RestRequest("api/users", Method.Post);
            request.AddBody(reqBody);
            RestResponse response = client.Execute(request);
            User user = JsonConvert.DeserializeObject<User>(response.Content);

            request = new RestRequest("api/accounts", Method.Post);
            Account account = new Account();
            account.AccountNo = (int)((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
            account.Balance = 0;
            account.UserId = user.UserId;
            account.Status = 0;
            request.AddBody(account);
            response = client.Execute(request);

            Account createdAccount = JsonConvert.DeserializeObject<Account>(response.Content);

            return new ObjectResult(createdAccount) { StatusCode = 201};
        }

        [HttpPost]
        public IActionResult Login([FromBody] User reqBody)
        {
            client = new RestClient(DataService);
            RestRequest request = new RestRequest("api/users/email/{e}", Method.Get);
            request.AddUrlSegment("e", reqBody.Email);
            RestResponse response = client.Execute(request);
            User user = JsonConvert.DeserializeObject<User>(response.Content);

            if (user != null)
            {
                if (user.Password.Equals(reqBody.Password)) 
                {
                    return new ObjectResult(user) { StatusCode = 200 };
                }
            }

            return Unauthorized();
        }

        [HttpGet]
        public User GetUser([FromQuery] string email)
        {
            client = new RestClient(DataService);
            RestRequest request = new RestRequest("api/users/email/{e}", Method.Get);
            request.AddUrlSegment("e", email);
            RestResponse response = client.Execute(request);
            User user = JsonConvert.DeserializeObject<User>(response.Content);
            return user;
        }

        [HttpGet]
        public IEnumerable<Account> GetAccounts([FromQuery] int id) 
        {
            client = new RestClient(DataService);
            RestRequest request = new RestRequest("api/accounts/holder/{i}", Method.Get);
            request.AddUrlSegment("i", id);
            RestResponse response = client.Execute(request);
            IEnumerable<Account> accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(response.Content);
            return accounts;
        }

        [HttpPost]
        public IActionResult Deposit([FromBody] Transaction transaction)
        {
            client = new RestClient(DataService);
            RestRequest request = new RestRequest("api/transactions", Method.Post);

            Transaction deposit = new Transaction();
            deposit.AccountId = transaction.AccountId;
            deposit.Amount = transaction.Amount;
            deposit.Description = transaction.Description;
            deposit.DateTime = DateTime.Now;
            deposit.Type = Transaction.TransactionType.Deposit;
            request.AddBody(deposit);

            RestResponse response = client.Execute(request);

            return new ObjectResult(deposit) { StatusCode = 201};
        }

        [HttpPost]
        public IActionResult Withdraw([FromBody] Transaction transaction)
        {
            client = new RestClient(DataService);
            RestRequest request = new RestRequest("api/transactions", Method.Post);

            Transaction withdrawal = new Transaction();
            withdrawal.AccountId = transaction.AccountId;
            withdrawal.Amount = transaction.Amount;
            withdrawal.Description = transaction.Description;
            withdrawal.DateTime = DateTime.Now;
            withdrawal.Type = Transaction.TransactionType.Withdrawal;
            request.AddBody(withdrawal);

            RestResponse response = client.Execute(request);

            return new ObjectResult(withdrawal) { StatusCode = 201 };
        }

        [HttpPost]
        public IActionResult Transfer([FromBody] Transaction transaction, [FromQuery] int to)
        {
            client = new RestClient(DataService);

            RestRequest a_request = new RestRequest("api/accounts/no/{no}", Method.Get);
            a_request.AddUrlSegment("no", to);
            RestResponse a_response = client.Execute(a_request);

            try 
            {
                Account beneficiary = JsonConvert.DeserializeObject<Account>(a_response.Content);
                Transaction deposit = new Transaction();
                deposit.AccountId = beneficiary.AccountId;
                deposit.Amount = transaction.Amount;
                deposit.Description = transaction.Description;
                deposit.DateTime = DateTime.Now;
                deposit.Type = Transaction.TransactionType.Deposit;

                Withdraw(transaction);
                Deposit(deposit);

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return new ObjectResult(transaction) { StatusCode = 201 };
        }

        [HttpGet]
        public User Search([FromQuery] string term)
        {
            client = new RestClient(DataService);
            RestRequest request = new RestRequest("api/users", Method.Get);
            RestResponse response = client.Execute(request);

            User user = JsonConvert.DeserializeObject<IEnumerable<User>>(response.Content).FirstOrDefault(u => u.Name.Equals(term));

            if(user == null) 
            {
                request = new RestRequest("api/accounts/no/{accNo}", Method.Get);
                request.AddUrlSegment("accNo", Int32.Parse(term));
                response = client.Execute(request);

                int id = JsonConvert.DeserializeObject<Account>(response.Content).UserId;

                request = new RestRequest("api/users/{id}", Method.Get);
                request.AddUrlSegment("id", id);
                response = client.Execute(request);

                user = JsonConvert.DeserializeObject<User>(response.Content);


            }


            return user;
        }

        [HttpGet]
        public IEnumerable<Transaction> GetTransactions([FromQuery] int id)
        {
            client = new RestClient(DataService);
            RestRequest request = new RestRequest("api/transactions/accId/{i}", Method.Get);
            request.AddUrlSegment("i", id);

            RestResponse response = client.Execute(request);

            IEnumerable<Transaction> transactions = JsonConvert.DeserializeObject<IEnumerable<Transaction>>(response.Content);

            return transactions;
        }

        [HttpPut]
        public User UpdateUser([FromBody] User user) 
        { 
            client = new RestClient(DataService);
            RestRequest request = new RestRequest("api/users/{id}", Method.Put);
            request.AddUrlSegment("id", user.UserId);
            request.AddBody(user);
            RestResponse response = client.Execute(request);
            return user;
        }

    }
}
