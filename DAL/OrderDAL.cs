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
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public List<Order> GetOrderToday()
        {
            List<Order> listorders = new List<Order>();
            lock (connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = @"select order_id, order_date, stat, o.staff_id, card_id, s.staff_name
                                            from Orders o inner join staffs s on s.staff_id = o.staff_id 
                                            where (day(order_date) - day(now())) = 0;";
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Order order = new Order();
                        order.OrderId = reader.GetInt32("order_id");
                        order.OrderDate = reader.GetDateTime("order_date");
                        order.Status = reader.GetBoolean("stat");
                        order.OrderCard.CardId = reader.GetInt32("card_id");
                        order.OrderStaff.StaffName = reader.GetString("staff_name");
                        listorders.Add(order);
                    }
                    reader.Close();
                    foreach (Order order in listorders)
                    {
                        command.CommandText = @"select order_id, d.drink_id, d.drink_name, s.size_id, s.size_name, quantity, ds.price from Order_details od
                                                inner join Drinks d on d.drink_id = od.drink_id
                                                inner join Sizes s on s.size_id = od.size_id
                                                inner join Drink_Sizes ds on  ds.drink_id = od.drink_id and ds.size_id = od.size_id
                                                where order_id = " + order.OrderId + ";";
                        reader = command.ExecuteReader();
                        while(reader.Read())
                        {
                            Drink drink = new Drink();
                            drink.DrinkName = reader.GetString("drink_name");
                            Size size = new Size();
                            size.SizeName = reader.GetString("size_name");
                            size.Quantity = reader.GetInt32("quantity");
                            size.Price = reader.GetDouble("price");
                            drink.SizeList.Add(size);
                            order.OrderDrinks.Add(drink);
                        }
                        reader.Close();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return listorders;
        }

        public bool UpdateOrderStatus(int orderId)
        {
            bool result = false;
            lock (connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = @"UPDATE Orders
                                            SET stat = false WHERE order_id = " + orderId + "; ";
                    command.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                finally
                {
                    connection.Close();
                }
            }
            return result;
        }
    }
}