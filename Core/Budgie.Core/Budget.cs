using System.Collections.Generic;

namespace Budgie.Core
{
    public class Budget : BaseEntity
    {
        public int Month { get; set; }

        public int Year { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Income> Incomes { get; set; }

        public virtual ICollection<Outgoing> Outgoings { get; set; }

        public virtual ICollection<Saving> Savings { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
