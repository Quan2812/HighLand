using System;
using Persistance;
using System.Collections.Generic;
using DAL;

namespace BL
{
    public class OrderBL
    {
        private OrderDAL orderDAL = new OrderDAL();

        public Order CreateOrder (int drinkId)
        {
            return orderDAL.CreateOrder(drinkId);
        }
    }
}