using System;
using System.Collections.Generic;
using Wallet.Api.Domain;

namespace Wallet.Api.Services
{
    public interface IAccountService
    {
        List<Account> GetAccounts();

        Account GetAccountById(Guid id);

        bool UpdateAccount(Account accountToUpdate);

        bool DeleteAccount(Guid id);
    }
}

