﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Order : BaseEntity
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Stree { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string OrderStatus { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public Order()
        {
            this.OrderItems = new List<OrderItem>();
        }
    }
}
