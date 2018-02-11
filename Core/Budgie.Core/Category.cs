using System;
using System.Collections.Generic;
using Budgie.Core.Enums;

namespace Budgie.Core
{
    public class Category : BaseEntity, IUserEntity
    {
        public string Name { get; set; }

        public bool Recurring { get; set; }

        public DateTime? RecurringDate { get; set; }

        public decimal? RecurringValue { get; set; }

        public string Colour { get; set; }

        public CategoryType Type { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
