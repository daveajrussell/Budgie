using System;

namespace Budgie.Core
{
    public class Transaction
    {
        public int Id { get; set; }

        public double Amount { get; set; }

        public DateTime Date { get; set; }

        public int AccountId { get; set; }
        public virtual Account Account { get; set; }

        public int SubCategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}
