using System;
using Persistance;
using System.Collections.Generic;
using DAL;

namespace BL
{
    public class CardBL
    {
        private CardDAL cardDAL = new CardDAL();

        public List<Card> GetAllCard()
        {
            return cardDAL.GetAllCard();
        }
    }
}