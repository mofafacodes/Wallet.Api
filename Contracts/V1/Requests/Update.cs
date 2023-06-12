using Wallet.Api.Domain;

namespace Wallet.Api.Contracts.V1.Requests
{
    public class Update : Constant
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
