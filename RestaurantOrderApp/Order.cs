using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RestaurantOrderApp
{
    public class Order : ClassBase
    {
        public Order(List<Dish> dishes)
        {
            Dishes = dishes;
            ValidateOrder();
        }
        
        public List<Dish> Dishes { get; private set; }
        
        /// <summary>
        /// Validate Order
        /// </summary>
        private void ValidateOrder()
        {
            if (Dishes.Any())
            {
                foreach (var item in Dishes.OrderBy(p => p.Type))
                {
                    item.IsValid = true;
                    switch (item.Type)
                    {
                        case EDishType.ENTREE:
                            if (item.Quantity > 1)
                            {
                                ValidateQuantity(item);
                            }
                            break;
                        case EDishType.SIDE:
                            if (item.Quantity > 1 && item.TimeOfDay.ToLower() == "morning")
                            {
                                ValidateQuantity(item);
                            }
                            break;
                        case EDishType.DRINK:
                            if (item.Quantity > 1 && item.TimeOfDay.ToLower() == "night")
                            {
                                ValidateQuantity(item);
                            }
                            break;
                        case EDishType.DESSERT:
                            if (item.Quantity > 0 && item.TimeOfDay.ToLower() == "morning")
                            {
                                item.Msg = "Erro: There is no dessert for morning meals.";
                                item.IsValid = false;
                            }
                            else
                            if (item.Quantity > 1 && item.TimeOfDay.ToLower() == "morning")
                            {
                                ValidateQuantity(item);
                            }
                            break;
                        default:
                            item.IsValid = false;
                            item.Msg = "Erro: Invalid Option.";
                            break;
                    }
                }

                IsValid = !(Dishes != null && Dishes.Any(p => !p.IsValid));
            }
            else
            {
                Msg = "Please enter at least one valid option.";
                IsValid = false;
            }
        }

        /// <summary>
        /// Validate Quantity alowed
        /// </summary>
        /// <param name="item"></param>
        private static void ValidateQuantity(Dish item)
        {
            item.Msg = $"Erro: Only 1 request for {item.Name} allowed at {item.TimeOfDay}.";
            item.IsValid = false;
        }
        
        public override string ToString()
        {
            string output = string.Empty;
            int i = 1;
            
            if(Dishes != null)
            {
                foreach (var item in Dishes.OrderByDescending(p=> p.IsValid))
                {
                    output += item.ToString() + ( i < Dishes.Count ? ", " : "");
                    i++;
                }
            }

            return output;
        }
    }
}
