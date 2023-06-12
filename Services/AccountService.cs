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

        public bool DeleteAccount(Guid id)
        {
            var account = GetAccountById(id);

            _accounts.Remove(account);

            return true;
        }

        public Account GetAccountById(Guid id)
        {
            return _accounts.SingleOrDefault(x =>  x.Id == id);
        }

        public List<Account> GetAccounts()
        {
            return _accounts;

        }

        public bool UpdateAccount(Account accountToUpdate)
        {
            var existingAccount = GetAccountById(accountToUpdate.Id) != null;

            if(!existingAccount)
            {
                return false;
            }

            var index = _accounts.FindIndex(x => x.Id == accountToUpdate.Id);
            _accounts[index] = accountToUpdate;
            return true;
        }
    }
}
