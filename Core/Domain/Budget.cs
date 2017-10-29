using System.Collections.Generic;

namespace Domain
{
    public class Budget
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<Sheet> Sheets { get; set; }
    }
}
