using AccountsService.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AccountsService.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        public Repo repo;

        public AccountController()
        {
            repo = new Repo();
        }

        [HttpGet]
        public Task<Account> Get(string name, string password)
        {
            return repo.Find(name, password);
        }

        [HttpPost]
        public Task<Account> Create(string name, string password)
        {
            return repo.Create(name, password);
        }

        [HttpPost]
        [Route("credit/{accountId}/{amount}")]
        public async Task<Account> Credit(string accountId, double amount)
        {
            var account = await repo.Get(accountId);
            account.Balance += amount;

            account.Transactions.Add(new TransactionBuilder()
                .BuildWithAmount(amount)
                .BuildWithType("credit")
                .BuildWithBalance(account.Balance)
                .Build());

            return await repo.Update(account);
        }

        [HttpPost]
        [Route("debit")]
        public async Task<Account> Debit(string id, double amount)
        {
            var account = await repo.Get(id);
            account.Balance -= amount;

            account.Transactions.Add(new TransactionBuilder()
                .BuildWithAmount(amount)
                .BuildWithType("debit")
                .BuildWithBalance(account.Balance)
                .Build());

            return await repo.Update(account);
        }

        [HttpGet]
        [Route("printPassBook")]
        public async Task<string> PrintPassBook(string id, string target)
        {
            var account = await repo.Get(id);

            if (target == "passbook")
            {
                var header = string.Format("{0,-20} | {1,-10} | {2,-10} | {3,-10}\n", "Date", "Type", "Amount", "Balance");
                string passbook = @"" + header + @"";
                foreach (var item in account.Transactions)
                {
                    passbook += string.Format("{0,-20} | {1,-10} | {2,-10} | {3,-10}\n", item.Date, item.Type, item.Amount, item.Balance);
                }
                return passbook;
            }
            else if (target == "statement")
            {

                var header = string.Format("{0,-20} | {1,-10} | {2,-10} | {3,-10}\n", "Date", "Type", "Amount", "Balance");
                string statement = @"Aurora Bank
Statement of Accounts
Account Number: " + account.id + @"
Account Name: " + account.Name + @"
Balance: " + account.Balance + @"
Opening Date: " + account.OpeningDate + @"

" + header + @"
";
                foreach (var item in account.Transactions)
                {
                    statement += string.Format("{0,-20} | {1,-10} | {2,-10} | {3,-10}\n", item.Date, item.Type, item.Amount, item.Balance);
                }
                return statement;
            }

            return null;
        }
    }
}
