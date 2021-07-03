using System;
using System.Collections.Generic;
using System.Linq;

namespace eShop.CoreBusiness.Models
{
    public class Order
    {
        public Order()
        {
            LineItems = new List<OrderLineItem>();
        }

        public int? OrderId { get; set; }                   // the number of the Order
        public DateTime? DatePlaced { get; set; }
        public DateTime? DateProcessing { get; set; }
        public DateTime? DateProcessed { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerStateProvince { get; set; }
        public string CustomerCountry { get; set; }
        public string AdminUser { get; set; }       
        public List<OrderLineItem> LineItems { get; set; }  // each line Item will have a product in it
        public string UniqueId { get; set; }                // primary key of the Order

        public void AddProduct(int productId, int qty, double price)
        {
            var item = LineItems.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                item.Quantity += qty;
            }
            else
                LineItems.Add(new OrderLineItem { ProductId = productId, Quantity = qty, Price = price });
        }

        public void RemoveProduct(int productId)
        {   
            var item = LineItems.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
                LineItems.Remove(item);
        }        
    }
}
