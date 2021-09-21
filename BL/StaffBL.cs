using System;
using DAL;
using Persistance;

namespace BL
{
    public class StaffBl
    {
        private StaffDal dal = new StaffDal();
        public Staff Login(string userName, string password){
            return dal.Login(userName, password);
        }
    }
}
