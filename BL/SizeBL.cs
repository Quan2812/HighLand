using System;
using System.Collections.Generic;
using DAL;
using Persistance;

namespace BL
{
    public class SizeBL
    {
        private SizeDAL sizeDAL = new SizeDAL();

        public Size GetSizeById(int drinkID)
        {
            return sizeDAL.GetSizeByID(drinkID);
        }
    }
}
