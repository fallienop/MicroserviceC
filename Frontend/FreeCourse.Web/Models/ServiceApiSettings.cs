namespace FreeCourse.Web.Models
{
    public class ServiceApiSettings
    {
        public string IdentityBaseURL { get; set; }
        public string GatewayURl { get; set; }

        public string PhotoStockURL { get; set; }
        public ServiceApi Catalog { get; set; }
    }

    public class ServiceApi
    { 
         public string Path { get; set;}
    
    }
}
