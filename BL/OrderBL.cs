using System;
using Persistance;
using System.Collections.Generic;
using DAL;

namespace BL
{
    public class OrderBL
    {
        private OrderDAL orderDAL = new OrderDAL();

        public bool CreateOrder(Order order)
        {
            bool result = orderDAL.CreateOrder(order);
            return result;
        }
    }
}