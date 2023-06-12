using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Wallet.Api.Contracts.V1;
using Wallet.Api.Contracts.V1.Requests;
using Wallet.Api.Contracts.V1.Response;
using Wallet.Api.Domain;
using Wallet.Api.Services;

namespace Wallet.Api.Controllers.V1
{
    public class AccountsController : Controller
    {
        private readonly IAccountService _accounts;

        public AccountsController(IAccountService accounts)
        {
            _accounts = accounts;
        }

        [HttpGet(ApiRoutes.Accounts.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_accounts.GetAccounts());
        }

        [HttpGet(ApiRoutes.Accounts.GetById)]
        public IActionResult GetById([FromRoute]Guid id)
        {
            var account = _accounts.GetAccountById(id);
            if(account == null)
            {
                return NotFound();
            }
            var respone = new Response
            {
                Id = account.Id,
                Name = account.Name,
                CreatedAt = account.CreatedAt,
                UpdatedAt = account.UpdatedAt,

            };

            return Ok(respone);
        }

        [HttpPost(ApiRoutes.Accounts.Create)]
        public IActionResult Create([FromBody] Create accountRequest )
        {
            //mapping a requrst to a domain object because of the contract exposed to a consumers
            var account = new Account {
                Id = Guid.NewGuid(),
                Name = accountRequest.Name,
                Type = accountRequest.Type,
                AccountScheme = accountRequest.AccountScheme,
                AcountNumber = accountRequest.AcountNumber,
                Email = accountRequest.Email,
                Owner = accountRequest.Owner,
                PhoneNumber = accountRequest.PhoneNumber,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            if (account.Id != Guid.Empty)
            {
                account.Id = Guid.NewGuid();
            }
            _accounts.GetAccounts().Add(account);
            //location header specifies where the resource was created
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoutes.Accounts.GetById.Replace("{id}", account.Id.ToString());

            var response = new Response
            {
                Id = account.Id,
                Name = account.Name,
                Type = account.Type,
                AccountScheme = account.AccountScheme,
                AcountNumber = account.AcountNumber,
                Email = account.Email,
                Owner = account.Owner,
                PhoneNumber = account.PhoneNumber,
                CreatedAt = account.CreatedAt,
                UpdatedAt = account.UpdatedAt
            };

            return Created(locationUrl, response);
        }

    }
}
