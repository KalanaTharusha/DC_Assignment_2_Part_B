using Microsoft.EntityFrameworkCore;
using Bank_Data_DLL;

namespace Bank_Data_Web_Service.Data
{
    public class DBManager : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = Bank.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<User> users = new List<User>();
            List<Account> accounts = new List<Account>();

            User user = new User();
            user.UserId = 1;
            user.Name = "admin";
            user.Role = Bank_Data_DLL.User.UserRole.admin;
            user.Email = "admin@email.com";
            user.Address = "admin address";
            user.Phone = 0710000001;
            user.Picture = "admin picture url";
            user.Password = "adminpass";
            users.Add(user);

            user = new User();
            user.UserId = 2;
            user.Name = "user1";
            user.Role = Bank_Data_DLL.User.UserRole.client;
            user.Email = "user1@email.com";
            user.Address = "user1 address";
            user.Phone = 0710000002;
            user.Picture = "user1 picture url";
            user.Password = "user1pass";
            users.Add(user);

            user = new User();
            user.UserId = 3;
            user.Name = "user2";
            user.Role = Bank_Data_DLL.User.UserRole.client;
            user.Email = "user2@email.com";
            user.Address = "user2 address";
            user.Phone = 0710000002;
            user.Picture = "user2 picture url";
            user.Password = "user2pass";
            users.Add(user);

            Account account = new Account();
            account.AccountId = 1;
            account.AccountNo = 6786887;
            account.Status = Bank_Data_DLL.Account.AccountStatus.Activated;
            account.Balance = 99999.00;
            account.UserId = 2;
            accounts.Add(account);
            

            account = new Account();
            account.AccountId = 2;
            account.AccountNo = 2454567;
            account.Status = Bank_Data_DLL.Account.AccountStatus.Activated;
            account.Balance = 4354.00;
            account.UserId = 2;
            accounts.Add(account);

            account = new Account();
            account.AccountId = 3;
            account.AccountNo = 567577;
            account.Status = Bank_Data_DLL.Account.AccountStatus.Activated;
            account.Balance = 13214.00;
            account.UserId = 3;
            accounts.Add(account);

            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Account>().HasData(accounts);

        }

        public DbSet<Bank_Data_DLL.User>? User { get; set; }

        public DbSet<Bank_Data_DLL.Account>? Account { get; set; }

        public DbSet<Bank_Data_DLL.Transaction>? Transaction { get; set; }

        public DbSet<Bank_Data_DLL.Log>? Log { get; set; }
    }
}
