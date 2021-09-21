using System;
using System.Collections.Generic;

namespace Persistance
{
    public class Order
    {
        public int OrderId {set;get;}
        public DateTime OrderDate {set;get;}
        public Staff OrderStaff {set;get;}
        public Card OrderCard {set;get;}
        public Drink OrderDrink {set;get;}
        public int Status {set;get;}
    }
}