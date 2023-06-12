using System;
using System.Collections.Generic;
using Wallet.Api.Domain;

namespace Wallet.Api.Services
{
    public interface IAccountService
    {
        List<Account> GetAccounts();

        Account GetAccountById(Guid id);
    }
}

