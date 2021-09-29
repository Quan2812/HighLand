using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistance;

namespace DAL
{
    public class DrinkDAL
    {
        private MySqlConnection connection = DbHelper.GetConnection();
        public List<Drink> GetDrinks()
        {
            List<Drink> listdrink = new List<Drink>();
            lock(connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "select drink_id, drink_name from drinks where is_active = 1";
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Drink drink = new Drink();
                        drink.DrinkId = reader.GetInt32("drink_id");
                        drink.DrinkName = reader.GetString("drink_name");
                        listdrink.Add(drink);
                    }
                    reader.Close();
                    foreach (Drink drink in listdrink)
                    {
                        command.CommandText = @"select sizes.size_id sizeId, size_name, price from drink_sizes 
                                            inner join sizes on drink_sizes.size_id = sizes.size_id 
                                            where drink_id =" + drink.DrinkId ;
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {   
                            Size size = new Size();
                            size.SizeId = reader.GetInt32("sizeId");
                            size.SizeName = reader.GetString("size_name");
                            size.Price = reader.GetDouble("price");
                            drink.SizeList.Add(size);
                        }
                        reader.Close();
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
                
                finally
                {
                    connection.Close();
                }
            }
            return listdrink;
        } 

        public Drink GetDrinkById(int drinkId)
        {   
            Drink drink = null;
            lock(connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "select drink_id, drink_name from drinks where drink_id = "+ drinkId +" and is_active = 1;";
                    MySqlDataReader reader = command.ExecuteReader();
                    if(reader.Read())
                    {
                        drink = new Drink();
                        drink.DrinkId = reader.GetInt32("drink_id");
                        drink.DrinkName = reader.GetString("drink_name");
                    }
                    reader.Close();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
                
                finally
                {
                    connection.Close();
                }
            }
            return drink;
        }
    }
}