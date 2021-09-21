using System;
using System.Collections.Generic;
using DAL;
using Persistance;

namespace BL
{
    public class SizeBL
    {
        private SizeDAL sizeDAL = new SizeDAL();

        public List<Size> GetSizeById(int drinkId)
        {
            return sizeDAL.GetSizeByID(drinkId);
        }
    }
}
