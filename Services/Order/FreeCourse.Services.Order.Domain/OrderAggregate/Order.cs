using FreeCourse.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    public class Order:Entity,IAggregateRoot
    {
        public DateTime CreatedDate { get;private set; }
        public Address Address { get;private set; }
        public string BuyerId { get;private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems =>_orderItems;
        public Order()
        {
            
        }
        public Order(string buyerid,Address address) 
        {
            BuyerId = buyerid;
            _orderItems = new List<OrderItem>();
            CreatedDate=DateTime.Now;
            Address = address;
        }
        public void AddOrderItem(string productid,string imageurl,decimal price,string productname) 
        {
            var existproduct = _orderItems.Any(x => x.ProductId == productid);
            if (!existproduct)
            {
                var neworderitem=new OrderItem(productid,productname,imageurl,price);
                _orderItems.Add(neworderitem);
            }
        }

        public decimal GetTotalPrice=>_orderItems.Sum(x => x.Price);
    }

}
