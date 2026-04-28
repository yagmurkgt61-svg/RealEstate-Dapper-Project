using Dapper;
using RealEstate_Dapper_Api.Dtos.EmployeeDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Models.Repositories.StatisticsRepositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly Context _context;
        public StatisticsRepository(Context context)
        {
            _context = context;
        }
        public int ActiveCategoryCount()
        {
            // CategoryStatus değeri true olan kayıtların sayısını getiren sorgu
            string query = "Select Count(*) From Category where CategoryStatus=1";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<int>(query);
                return values;
            }
        }

        public int ActiveEmployeeCount()
        {
            // Status değeri true olan kayıtların sayısını getiren sorgu
            string query = "Select Count(*) From Employee where Status=1";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<int>(query);
                return values;

            }
        }

        public int ApertmentCount()
        {
            // Title alanında 'Daire' geçen kayıtların sayısını getiren sorgu
            string query = "Select Count(*) From Product where Title like '%Daire%'";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<int>(query);
                return values;

            }
        }

        public decimal AverageProductPriceByRent()
        {
            // Type alanı 'Kiralık' olan kayıtların Price alanlarının ortalamasını getiren sorgu
            string query = "Select Avg(Price) From Product where Type=N'Kiralık'";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<decimal>(query);
                return values;

            }
        }

        public decimal AverageProductPriceBySale()
        {
            // Type alanı 'Satılık' olan kayıtların Price alanlarının ortalamasını getiren sorgu
            string query = "Select Avg(Price) From Product where Type=N'Satılık'";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<decimal>(query);
                return values;

            }
        }

        public int AverageRoomCount()
        {
            // RoomCount alanlarının ortalamasını getiren sorgu
            string query = "Select Avg(RoomCount) From ProductDetails";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<int>(query);
                return values;

            }
        }

        public int CategoryCount()
        {
            // Category tablosundaki kayıtların sayısını getiren sorgu
            string query = "Select Count(*) From Category";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<int>(query);
                return values;

            }
        }

        public string CategoryNameByMaxProductCount()
        {
            // En fazla ürüne sahip kategorinin adını getiren sorgu
            string query = "Select top(1) CategoryName,Count(*) From Product inner join Category On Product.ProductCategory=Category.CategoryID Group By CategoryName order by Count(*) Desc";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<string>(query);
                return values;

            }
        }

        public string CityNameByMaxProductCount()
        {
            // En fazla ürüne sahip şehrin adını getiren sorgu
            string query = "Select top(1) CityName,Count(*) as 'product_count' From Product Group By CityName order by product_count Desc";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<string>(query);
                return values;

            }
        }

        public int DifferentCityCount()
        {
            // Ürünlerin bulunduğu farklı şehirlerin sayısını getiren sorgu
            string query = "Select Count(Distinct(CityName)) From Product";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<int>(query);
                return values;

            }
        }

        public string EmployeeNameByMaxProductCount()
        {
            // Çalışanların adını ve kaç tane ürüne sahip olduğunu getiren sorgu
            string query = "Select Name,Count(*) 'product_count' From Product inner join Employee On Product.AppUserId=Employee.EmployeeID Group By Name Order By product_count Desc";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<string>(query);
                return values;

            }
        }

        public decimal LastProductPrice()
        {
            // En son eklenen ürünün fiyatını getiren sorgu
            string query = "Select Top(1) Price From Product Order By ProductID Desc";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<decimal>(query);
                return values;

            }
        }

        public string NewestBuildingYear()
        {
            // En yeni bina yılını getiren sorgu
            string query = "Select Top(1) BuildYear From ProductDetails Order By BuildYear Desc";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<string>(query);
                return values;

            }
        }

        public string OldestBuildingYear()
        {
            // En eski bina yılını getiren sorgu
            string query = "Select Top(1) BuildYear From ProductDetails Order By BuildYear Asc";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<string>(query);
                return values;

            }
        }

        public int PassiveCategoryCount()
        {
            // CategoryStatus değeri false olan kayıtların sayısını getiren sorgu
            string query = "Select Count(*) From Category Where CategoryStatus=0";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<int>(query);
                return values;

            }
            
        }

        public int ProductCount()
        {
            // Product tablosundaki kayıtların sayısını getiren sorgu
            string query = "Select Count(*) From Product";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<int>(query);
                return values;

            }
        }
    }
}
