namespace RealEstate_Dapper_UI.Dtos.ProductDtos
{
    public class ResultProductDto
    {
        
            public int productID { get; set; }
            public string title { get; set; }
            public decimal price { get; set; }
            public string city { get; set; }
            public string district { get; set; }
            public object categoryName { get; set; }
            public string CoverImage { get; set; }
            public string type { get; set; }
            public string adress { get; set; }
        public bool dealOfTheDay { get; set; }
    }

    }

