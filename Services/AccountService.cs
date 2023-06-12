using System;
using System.Collections.Generic;
using System.Linq;
using Wallet.Api.Domain;

namespace Wallet.Api.Services
{
    public class AccountService : IAccountService
    {
        private List<Account> _accounts;

        public AccountService() 
        {
            _accounts = new List<Account>();
            for (int i = 0; i < 5; i++)
            {
                _accounts.Add(new Account()
                {
                    Id = Guid.NewGuid(),
                    Name = $"Account Name{i}",
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                });
            }
        }

        public Account GetAccountById(Guid id)
        {
            return _accounts.SingleOrDefault(x =>  x.Id == id);
        }

        public List<Account> GetAccounts()
        {
            return _accounts;

        }
    }
}
