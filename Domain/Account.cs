using System;

namespace Wallet.Api.Domain
{
    public class Account : Constant
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string AccountScheme { get; set; }

        public string AcountNumber { get; set; }

        public string Owner { get; set;}

        public string PhoneNumber { get; set; }

    }
}
