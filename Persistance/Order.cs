using System;
using System.Collections.Generic;

namespace Persistance
{
    public class Order
    {
        public int OrderId {set;get;}
        public string OrderDate {set;get;} 
        public Staff OrderStaff {set;get;} = new Staff();
        public Card OrderCard {set;get;} = new Card();
        public List<Drink> OrderDrinks {set;get;} = new List<Drink>();
        public bool Status {set;get;}
    }

    // public class DateTime
    // {
    //     public DateTime Day {set;get;}

    //     public DateTime DayOfWeek {set;get;}

    //     public DateTime TimeNow {set;get;}


    // }
}