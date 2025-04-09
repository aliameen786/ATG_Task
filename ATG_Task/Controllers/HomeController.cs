using System.Data;
using System.Diagnostics;
using ATG_Task.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ATG_Task.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, ApplicationDbContext context)
        {
            _logger = logger;
            _configuration = configuration;
            _context = context;
        }

        public IActionResult Index()
        {
            // Fetching data from the database
            //var dashboardData = new DashboardData
            //{
            //    TodaysMoney = _context.Sales.Where(s => s.SaleDate == DateTime.Today).Sum(s => s.Amount),
            //    TodaysUsers = _context.Users.Where(u => u.CreatedAt == DateTime.Today).Count(),
            //    MoneyChangePercentage = CalculateMoneyChangePercentage(),
            //    UserChangePercentage = CalculateUserChangePercentage()
            //};
            return View();
        }

        private decimal CalculateMoneyChangePercentage()
        {
            var lastWeekMoney = _context.Sales.Where(s => s.SaleDate == DateTime.Today.AddDays(-7)).Sum(s => s.Amount);
            var todayMoney = _context.Sales.Where(s => s.SaleDate == DateTime.Today).Sum(s => s.Amount);

            if (lastWeekMoney == 0) return 0;
            return ((todayMoney - lastWeekMoney) / lastWeekMoney) * 100;
        }

        private decimal CalculateUserChangePercentage()
        {
            var lastMonthUsers = _context.Users.Where(u => u.CreatedAt.Month == DateTime.Today.AddMonths(-1).Month).Count();
            var todayUsers = _context.Users.Where(u => u.CreatedAt.Month == DateTime.Today.Month).Count();

            if (lastMonthUsers == 0) return 0;
            return ((todayUsers - lastMonthUsers) / lastMonthUsers) * 100;
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult TableView()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }


        [HttpGet]
        public IActionResult SearchProducts(string searchTerm)
        {
            List<OrderModel> products = new List<OrderModel>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SearchProducts", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@searchTerm", searchTerm ?? "");

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        products.Add(new OrderModel
                        {
                            ProductId = Convert.ToInt32(reader["ProductId"]),
                            ProductName = reader["ProductName"].ToString(),
                            Category = reader["Category"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"])
                        });
                    }
                    conn.Close();
                }
            }

            return Json(products); // Return JSON for AJAX
        }


    }
}
