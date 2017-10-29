using Domain.Enums;

namespace Domain
{
    public class Account
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Balance { get; set; }

        public AccountType AccountType { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
