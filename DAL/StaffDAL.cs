using System;
using MySql.Data.MySqlClient;
using Persistance;

namespace DAL
{
    public class StaffDal
    {
        // public static int USER_ERROR = -1;
        // public static int ACCOUNT_WRONG = 0;
        // public static int ACCOUNT_EXIST = 1;
        // 0: account wrong
        // 1: ok
        // -1: can't connect to db or error
        private MySqlConnection connection = DbHelper.GetConnection();
        public Staff Login(string userName, string password)
        {
            Staff staff = null;
            lock(connection){
            try{
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "select staff_id, user_account, staff_name, staff_address, staff_phone, staff_age, role from Staffs where user_account=@userName and user_password=@userPass and is_active=1;";
                command.Parameters.AddWithValue("@userName", userName);
                command.Parameters.AddWithValue("@userPass", Md5Algorithms.CreateMD5(password));
                MySqlDataReader reader = command.ExecuteReader();
                if(reader.Read()){
                    staff = new Staff();
                    staff.StaffId = reader.GetInt32("staff_id");
                    staff.UserAccount = GetString(reader, 1);
                    staff.StaffName = GetString(reader, 2);
                    staff.StaffAddress = GetString(reader,3);
                    staff.Role = reader.GetInt32("role");
                    staff.IsActive = true;
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
            return staff;
        }
        private string GetString(MySqlDataReader reader, int columnNo){
            if(reader.IsDBNull(columnNo)){
                return ""; 
            }else{
                return reader.GetString(columnNo);
            }
        }
    }
}
