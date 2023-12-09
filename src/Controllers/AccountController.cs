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
            return await repo.Update(account);
        }

        [HttpPost]
        [Route("debit")]
        public async Task<Account> Debit(string id, double amount)
        {
            var account = await repo.Get(id);
            account.Balance -= amount;
            return await repo.Update(account);
        }
    }
}
