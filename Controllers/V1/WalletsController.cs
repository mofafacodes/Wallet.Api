using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Wallet.Api.Contracts.V1;
using Wallet.Api.Domain;

namespace Wallet.Api.Controllers.V1
{
    public class AccountsController : Controller
    {
        private List<Account> _accounts;

        public AccountsController()
        {
            _accounts = new List<Account>();
            for (int i = 0; i < 5; i++)
            {
                _accounts.Add(new Account()
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                });
            }
        }

        [HttpGet(ApiRoutes.Accounts.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_accounts);
        }
    }
}
