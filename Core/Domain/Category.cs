using System.Collections.Generic;

namespace Domain
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryColour { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
