using System;

namespace Persistance
{
    public class Size
    {
        public int SizeId {set;get;}

        public string SizeName {set;get;}
        
        public double Price {set;get;}

        public int Quantity {set;get;} = 1;
    }
}