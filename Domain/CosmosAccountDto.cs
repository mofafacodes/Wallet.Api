using Cosmonaut.Attributes;

namespace Wallet.Api.Domain
{
    [CosmosCollection("accounts")]
    public class CosmosAccountDto : CosmosConstant
    {

        public string Name { get; set; }

        public string Type { get; set; }

        public string AccountScheme { get; set; }

        public string AcountNumber { get; set; }

        public string Email { get; set; }

        public string Owner { get; set; }

        public string PhoneNumber { get; set; }
    }
}
