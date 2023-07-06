using Cosmonaut;
using Cosmonaut.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Wallet.Api.Domain;

namespace Wallet.Api.Services
{
    public class CosmosAccountService : IAccountService
    {
        private readonly ICosmosStore<CosmosAccountDto> _cosmosStore;

        public CosmosAccountService(ICosmosStore<CosmosAccountDto> cosmosStore)
        {
            _cosmosStore = cosmosStore;
        }
    
        public async Task<bool> CreateAccountAsync(Account account)
        {
            var cosmosAccount = new CosmosAccountDto
            {
                Id = Guid.NewGuid().ToString(),
                Name = account.Name,
                Type = account.Type,
                AccountScheme = account.AccountScheme,
                AcountNumber = account.AcountNumber,
                Email = account.Email,
                Owner = account.Owner,
                PhoneNumber = account.PhoneNumber,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

           var response = await _cosmosStore.AddAsync(cosmosAccount);
            account.Id = Guid.Parse(cosmosAccount.Id);
            return response.IsSuccess;
        }

        public async Task<bool> DeleteAccountAsync(Guid id)
        {
            //fairly very simple
            var response = await _cosmosStore.RemoveByIdAsync(id.ToString(), id.ToString());
            return response.IsSuccess;
        }

        public async Task<Account> GetAccountByIdAsync(Guid id)
        {
            //thi is where cosmos dbb really performs
            var account = await _cosmosStore.FindAsync(id.ToString(), id.ToString());
            return account == null ? null : new Account
            {
                Id = Guid.Parse(account.Id),
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
        }

        public async Task<List<Account>> GetAccountsAsync()
        {
           var accounts = await _cosmosStore.Query().ToListAsync();

            return accounts.Select(x => new Account
            {
                Id = Guid.Parse(x.Id),
                Name = x.Name,
                Type = x.Type,
                AccountScheme = x.AccountScheme,
                AcountNumber = x.AcountNumber,
                Email = x.Email,
                Owner = x.Owner,
                PhoneNumber = x.PhoneNumber,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            }).ToList();
        }

        public async Task<bool> UpdateAccountAsync(Account accountToUpdate)
        {
            var cosmosAccount = new CosmosAccountDto
            {
                Id = accountToUpdate.Id.ToString(),
                Name = accountToUpdate.Name,
                Type = accountToUpdate.Type,
                AccountScheme = accountToUpdate.AccountScheme,
                AcountNumber = accountToUpdate.AcountNumber,
                Email = accountToUpdate.Email,
                Owner = accountToUpdate.Owner,
                PhoneNumber = accountToUpdate.PhoneNumber,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            var response = await _cosmosStore.UpdateAsync(cosmosAccount);
            return response.IsSuccess;
        }
    }
}
