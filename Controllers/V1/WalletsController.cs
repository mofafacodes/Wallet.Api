using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Wallet.Api.Contracts.V1;
using Wallet.Api.Contracts.V1.Requests;
using Wallet.Api.Contracts.V1.Response;
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

        [HttpPost(ApiRoutes.Accounts.Create)]

        public IActionResult Create([FromBody] Create accountRequest )
        {
            //mapping a requrst to a domain object because of the contract exposed to a consumers
            var account = new Account {
                Id = Guid.NewGuid().ToString(),
                Name = accountRequest.Name,
                Type = accountRequest.Type,
                AccountScheme = accountRequest.AccountScheme,
                AcountNumber = accountRequest.AcountNumber,
                Owner = accountRequest.Owner,
                PhoneNumber = accountRequest.PhoneNumber,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            if (string.IsNullOrEmpty(account.Id))
            {
                account.Id = Guid.NewGuid().ToString();
            }
            _accounts.Add(account);
            //location header specifies where the resource was created
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoutes.Accounts.GetById.Replace("{id}", account.Id);

            var response = new Response
            {
                Id = account.Id,
                Name = account.Name,
                Type = account.Type,
                AccountScheme = account.AccountScheme,
                AcountNumber = account.AcountNumber,
                Owner = account.Owner,
                PhoneNumber = account.PhoneNumber,
                CreatedAt = account.CreatedAt,
                UpdatedAt = account.UpdatedAt
            };

            return Created(locationUrl, response);
        }

    }
}
