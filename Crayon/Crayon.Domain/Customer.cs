using System;
using System.Collections.Generic;
using System.Text;

namespace Crayon.Crayon.Domain
{
    public class Customer
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public ICollection<Account> Accounts { get; set; } = [];
    }
}
