using System.Collections.Generic;

namespace Domain
{
    public class Sheet
    {
        public int Id { get; set; }

        public int Month { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}