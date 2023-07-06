using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallet.Api.Data;
using Wallet.Api.Domain;

namespace Wallet.Api.Services
{
    public class AccountService : IAccountService
    {
        //injecting Db as dependency here
        private readonly DataContext _dataContext;

        public AccountService(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }

        public async Task<bool> CreateAccountAsync(Account account)
        {
           await _dataContext.Accounts.AddAsync(account);

           var created = await _dataContext.SaveChangesAsync();

           return created > 0;
        }

      

        public async Task<Account> GetAccountByIdAsync(Guid id)
        {
            return await _dataContext.Accounts.SingleOrDefaultAsync(x =>  x.Id == id);
        }

        public async Task<List<Account>> GetAccountsAsync()
        {
            return await _dataContext.Accounts.ToListAsync();

        }

        public async Task<bool> UpdateAccountAsync(Account accountToUpdate)
        {
            _dataContext.Accounts.Update(accountToUpdate);

            var updated = await _dataContext.SaveChangesAsync();
            
            return updated > 0;
        }

        public async Task<bool> DeleteAccountAsync(Guid id)
        {
            var account = await GetAccountByIdAsync(id);

            if(account == null)
            {
                return false;
            }

            _dataContext.Accounts.Remove(account);

            var deleted = await _dataContext.SaveChangesAsync();

            return deleted > 0;
        }
    }
}
