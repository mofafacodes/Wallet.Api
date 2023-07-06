using System;
using System.ComponentModel.DataAnnotations;

namespace Wallet.Api.Domain
{
    public class Constant
    {
        [Key]
        public Guid Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

    }
}
