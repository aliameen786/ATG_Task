namespace ATG_Task.Models
{
    public class SaleModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;

        //public UsersModel? User { get; set; }
    }
}
