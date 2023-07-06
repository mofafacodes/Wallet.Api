using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wallet.Api.Domain;

namespace Wallet.Api.Services
{
    public interface IAccountService
    {
        //changing all methods to async because entity framework supports async in its extension methods to retrieve data
        //IO operations require async
        Task<List<Account>> GetAccountsAsync();

        Task<Account> GetAccountByIdAsync(Guid id);

        Task<bool> UpdateAccountAsync(Account accountToUpdate);

        Task<bool> DeleteAccountAsync(Guid id);

        Task<bool> CreateAccountAsync(Account account);
    }
}

