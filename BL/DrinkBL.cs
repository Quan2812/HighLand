using System;
using Persistance;
using System.Collections.Generic;
using DAL;
namespace BL
{   
    public class DrinkBL
    {   
        private DrinkDAL drinkDAL = new DrinkDAL();
        public List<Drink> GetDrinks()
        {
            return drinkDAL.GetDrinks();
        } 
    }
}