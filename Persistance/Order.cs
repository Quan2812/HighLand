using System;
using System.Collections.Generic;

namespace Persistance
{
    public class Order
    {
        public int OrderId {set;get;}
        public DateTime OrderDate {set;get;}
        public Staff OrderStaff {set;get;} = new Staff();
        public Card OrderCard {set;get;} = new Card();
        public Drink OrderDrink {set;get;} = new Drink();
        public int Status {set;get;}
    }
}