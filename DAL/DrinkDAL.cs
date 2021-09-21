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
                        command.CommandText = "select sizes.size_id, size_name, price from drink_sizes inner join sizes on drink_sizes.size_id = sizes.size_id where drink_id =" + drink.DrinkId ;
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Size size = new Size();
                            size.SizeId = reader.GetInt32("sizes.sizes_id");
                            size.SizeName = reader.GetString("size_name");
                            size.Price = reader.GetDouble("price");
                            drink.SizeList.Add(size);
                        }
                        reader.Close();
                    }
                }
                catch
                {
                    
                }
                
                finally
                {
                    connection.Close();
                }
            }
            return listdrink;
        }

        public List<Drink> GetDrinkById(int drinkId)
        {
            List<Drink> listdrink = null;
            lock(connection){
            try{
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = @"select d.drink_id, d.drink_name, s.size_id, s.size_name, ds.price 
                                        from drinks d inner join drink_sizes ds 
                                        on d.drink_id = ds.drink_id 
                                        inner join sizes s 
                                        on s.size_id = ds.size_id
                                        where d.drink_id = " + drinkId +" and is_active = true;;";
                MySqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    Drink drink = new Drink();
                    drink.DrinkId = reader.GetInt32("drink_id");
                    drink.DrinkName = reader.GetString("drink_name");
                    drink.IsActive = true;
                    Size size = new Size();
                    size.SizeId = reader.GetInt32("size_id");
                    size.SizeName = reader.GetString("size_name");
                    size.Price = reader.GetDouble("price");
                    drink.SizeList.Add(size);
                }
                reader.Close();
                connection.Close();
            }catch
            {
                
            }
            finally
            {
                connection.Close();
            }
            }
            return listdrink;
        } 
    }
}