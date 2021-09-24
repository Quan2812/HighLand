using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistance;

namespace DAL
{
    public class OrderDAL
    {
        private MySqlConnection connection = DbHelper.GetConnection();
    }
}