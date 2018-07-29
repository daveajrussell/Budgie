using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Budgie.Core
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<Budget> Budgets { get; set; }

        public virtual ICollection<Income> Incomes { get; set; }

        public virtual ICollection<Outgoing> Outgoings { get; set; }

        public virtual ICollection<Saving> Savings { get; set; }
    }
}