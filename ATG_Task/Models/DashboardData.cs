namespace ATG_Task.Models
{
    public class DashboardData
    {
        public int Id { get; set; }
        public decimal TodaysMoney { get; set; }
        public decimal MoneyChangePercentage { get; set; }
        public int TodaysUsers { get; set; }
        public decimal UserChangePercentage { get; set; }
    }
}
