﻿using System.Collections.Generic;

namespace Budgie.Core
{
    public class SubCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}