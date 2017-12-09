namespace Budgie.Framework.Facade
{
    public class Ticket
    {
        public int UserId { get; set; }

        public Ticket(int userId)
        {
            UserId = userId;
        }
    }
}
