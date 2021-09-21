using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistance;

namespace DAL
{
    public class SizeDAL
    {   
        private MySqlConnection connection;
        public SizeDAL()
        {
            connection =DbHelper.GetConnection();
        }
        public Size GetSizeByID(int drinkID)
        {   
            Size size = null;
            lock(connection){
            try{
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = @"select sizes.size_id, size_name, price 
                                      from drink_sizes inner join sizes 
                                      on drink_sizes.size_id = sizes.size_id 
                                      where drink_id=" + drinkID + ";";
                MySqlDataReader reader = command.ExecuteReader();
                if(reader.Read()){
                    size = new Size();
                    size.SizeId = reader.GetInt32("size_id");
                    size.SizeName = reader.GetString("size_name");
                    size.Price = reader.GetDouble("price");
                }
                reader.Close();
                connection.Close();
            }catch(Exception ex){
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }
            }
            return size;
        }
    }        
}