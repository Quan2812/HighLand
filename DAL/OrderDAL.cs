using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistance;

namespace DAL
{
    public class OrderDAL
    {
        private MySqlConnection connection = DbHelper.GetConnection();

        public Order CreateOrder(int drinkId)
        {
            Order order = null;

            lock(connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = @"select drink_id, drink_name, is_active from drinks
                                            where drink_id = " + drinkId +" and is_active = true;";
                    MySqlDataReader reader = command.ExecuteReader();
                    if(reader.Read())
                    {
                        Drink drink = new Drink();
                        order = new Order();
                        drink.DrinkId = reader.GetInt32("drink_id");
                        drink.DrinkName = reader.GetString("drink_name");
                        order.OrderDrink = drink;
                    }
                    reader.Close();
                }
                catch
                {
                    
                }
                
                finally
                {
                    connection.Close();
                }
            }

            return order;
        }
    }
}