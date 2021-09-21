using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistance;

namespace DAL
{
    public class SizeDAL
    {   
        private MySqlConnection connection = DbHelper.GetConnection();
        public List<Size> GetSizeByID(int drinkId)
        {   
            List<Size> listsize = new List<Size>();
            lock(connection)
            {
                try{
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = @"select sizes.size_id, size_name, price 
                                        from drink_sizes inner join sizes 
                                        on drink_sizes.size_id = sizes.size_id 
                                        where drink_id=" + drinkId + ";";
                    MySqlDataReader reader = command.ExecuteReader();
                    while(reader.Read()){
                        Size size = new Size();
                        size.SizeId = reader.GetInt32("size_id");
                        size.SizeName = reader.GetString("size_name");
                        size.Price = reader.GetDouble("price");
                        listsize.Add(size);
                    }
                    reader.Close();
                    connection.Close();
                }
                catch{}
                finally
                {
                    connection.Close();
                }
            }
            return listsize;
        }
    }        
}