using AccountsService.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AccountsService
{
    public class TransactionBuilder
    {
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public double Balance { get; set; }

        public TransactionBuilder BuildWithAmount(double Amount)
        {
            this.Amount = Amount;
            return this;
        }
        public TransactionBuilder BuildWithType(string Type)
        {
            this.Type = Type;
            return this;
        }
        public TransactionBuilder BuildWithBalance(double Balance)
        {
            this.Balance = Balance;
            return this;
        }

        public Transaction Build()
        {
            return new Transaction
            {
                Amount = this.Amount,
                Date = DateTime.Now,
                Type = this.Type,
                Balance = this.Balance
            };
        }

    }
}