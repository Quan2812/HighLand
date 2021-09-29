using System;
using Xunit;
using DAL;
using Persistance;

namespace DALTest
{
    public class OrderDALTest
    {   
        private OrderDAL orderDAL = new OrderDAL();
        [Fact]
        public void CreateOrderTest()
        {
            Order order = new Order();
            List<Drink> drinks = new DrinkDAL().GetDrinks();
            order.OrderDrinks = drinks[0];
            string userName = "staff01";
            string userPass = "abcd1234";
            order.OrderStaff = new StaffDAL().Login(userName,userPass);
            order.OrderCard = new CardDAL().GetAllCard()[0];
            order = orderDAL.CreateOrder();
            Assert.True(order != null);
            Assert.True(order.OrderId > 0);
        }
    }
}