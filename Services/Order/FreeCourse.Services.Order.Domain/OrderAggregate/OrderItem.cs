using FreeCourse.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    public class OrderItem:Entity
    {
        public string ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string ImageURL { get; private set; }
        public decimal Price { get; private set; }

        public OrderItem()
        {
            
        }
        public OrderItem(string productId, string productName, string imageURL, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            ImageURL = imageURL;
            Price = price;
        }

        public void UpdateItem(string pname,string pimageurl,decimal price)
        {
            ProductName = pname;
            ImageURL = pimageurl;
            Price = price;
        }
    }
}
