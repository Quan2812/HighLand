using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistance;

namespace DAL
{
    public class OrderDAL
    {
        private MySqlConnection connection = DbHelper.GetConnection();

        public bool CreateOrder(Order order)
        {
            if (order == null || order.OrderDrinks == null || order.OrderDrinks.Count == 0)
            {
                return false;
            }
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                //Lock update all tables
                command.CommandText = "lock tables Orders write, Order_details write;";
                command.ExecuteNonQuery();
                MySqlTransaction trans = connection.BeginTransaction();
                command.Transaction = trans;
                MySqlDataReader reader =null;


            }
            catch (System.Exception)
            {
                
                throw;
            }
            return false;
        }
    }
}