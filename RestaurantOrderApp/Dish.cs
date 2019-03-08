using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantOrderApp 
{
    public class Dish : ClassBase
    {
        public EDishType Type { get; set; }
        public string Name { get; set; }
        public string TimeOfDay { get; set; }
        public int Quantity { get; set; }
        
        public override string ToString() {
            if(IsValid)
            {
                if(Quantity > 1)
                    return string.Format("{0}(x{1})", Name, Quantity);
                else
                    return Name;
            }
            else
            {
                return Msg;
            }
        }
    }
}
