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

        public List<Order> GetOrderToday()
        {
            return orderDAL.GetOrderToday();
        }

        public bool UpdateOrderStatus(int orderId, List<Order> listorders)
        {
            bool result = orderDAL.UpdateOrderStatus(orderId, listorders);
            return result;
        }
    }
}