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
            bool result = false;
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                //Lock update all tables
                command.CommandText = "lock tables Orders write, Order_details write;";
                command.ExecuteNonQuery();
                MySqlTransaction trans = connection.BeginTransaction();
                command.Transaction = trans;
                MySqlDataReader reader = null;
                try
                {
                    // Insert Order
                    command.CommandText = "insert into Orders(stat, staff_id, card_id) values (@status, @staffId, @cardId);";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@status", order.Status);
                    command.Parameters.AddWithValue("@staffId", order.OrderStaff.StaffId);
                    command.Parameters.AddWithValue("@cardId", order.OrderCard.CardId);
                    command.ExecuteNonQuery();
                    //get new Order_ID
                    command.CommandText = "select LAST_INSERT_ID() as order_id";
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        order.OrderId = reader.GetInt32("order_id");
                    }
                    reader.Close();
                    foreach (Drink drink in order.OrderDrinks)
                    {
                        //insert to Order Details
                        foreach (Size size in drink.SizeList)
                        {
                            command.CommandText = @"insert into Order_details(order_id, drink_id, size_id, quantity) values 
                            (" + order.OrderId + ", " + drink.DrinkId + ", " + size.SizeId + ", " + size.Quantity + ");";
                            command.ExecuteNonQuery();
                        }
                    }
                    trans.Commit();
                    result = true;
                }
                catch (Exception ex)
                {   
                    Console.WriteLine(ex);
                    try
                    {
                        trans.Rollback();
                    }
                    catch { }
                }
                finally
                {
                    //unlock all tables;
                    command.CommandText = "unlock tables;";
                    command.ExecuteNonQuery();
                }
            }
            catch( Exception ex) 
            { 
                Console.WriteLine(ex); 
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
    }
}