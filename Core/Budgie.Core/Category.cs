using System.Collections.Generic;
using Budgie.Core.Enums;

namespace Budgie.Core
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public string CategoryColour { get; set; }

        public CategoryType CategoryType { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
