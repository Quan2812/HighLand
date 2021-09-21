using System;
using System.Collections.Generic;

namespace Persistance
{
    public class Drink
    {
        public int DrinkId {set;get;}

        public string DrinkName {set;get;}

        public bool IsActive {set;get;}
        
        public List<Size> SizeList {set;get;} = new List<Size>();
    }
}