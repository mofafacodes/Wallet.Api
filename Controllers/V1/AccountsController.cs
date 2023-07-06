using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _accounts.GetAccountsAsync());
        }

        [HttpGet(ApiRoutes.Accounts.GetById)]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var account = await _accounts.GetAccountByIdAsync(id);
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

        [HttpDelete(ApiRoutes.Accounts.Delete)]
        public async Task<IActionResult> Delele([FromRoute] Guid id)
        {
          
            var deleted = await _accounts.DeleteAccountAsync(id);

            if (deleted)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPut(ApiRoutes.Accounts.Update)]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] Update accountUpdate)
        {
            var account = new Account
            {
                Id = id,
                Name = accountUpdate.Name,
                CreatedAt = accountUpdate.CreatedAt,
                UpdatedAt = accountUpdate.UpdatedAt,
            };

            var updated = await _accounts.UpdateAccountAsync(account);

            if (updated)
            {
                return Ok(account);
            }

            return NotFound();
        }

        [HttpPost(ApiRoutes.Accounts.Create)]
        public async Task<IActionResult> Create([FromBody] Create accountRequest )
        {
            //mapping a requrst to a domain object because of the contract exposed to a consumers
            var account = new Account {
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
            await _accounts.CreateAccountAsync(account);
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
