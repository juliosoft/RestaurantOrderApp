using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantOrderApp
{
    public class Work : ClassBase
    {
        private string _input;
        public string _output;
        private List<Dish> _dashList;
        public Order _order;

        public Work(string input)
        {
            _input = input;
            ValidateInput();
            LoadFakeDishList();
        }
        

        /// <summary>
        /// Validate input text
        /// </summary>
        private void ValidateInput()
        {
            try
            {
                IsValid = true;
                if (string.IsNullOrEmpty(_input))
                {
                    IsValid = false;
                    Msg = "Erro: Your order can not be empty.";
                }
                else if (!_input.ToLower().StartsWith("morning") && !_input.ToLower().StartsWith("night"))
                {
                    IsValid = false;
                    Msg = "Erro: Your order must start with 'morning' or 'night' ";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Process Order
        /// </summary>
        public void ProcessOrder()
        {
            try
            {
                if (IsValid)
                {
                    string[] options = _input.Split(',');

                    string day = options[0];
                    day = day.ToLower();
                    
                    options = options.Where(o => !string.IsNullOrEmpty(o) && o.ToLower() != day).ToArray();

                    var list = (from r in options
                                group r by r.Trim() into gr
                                select new Dish()
                                {
                                    Name = FindDish(gr.Key.Trim(), day),
                                    Type = Util.EnumTryParse(gr.Key),
                                    Quantity = gr.Count(),
                                    TimeOfDay = day
                                }).ToList();

                    _order = new Order(list);
                    IsValid = _order.IsValid;
                    Msg = _order.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Load Fake data
        /// </summary>
        public void LoadFakeDishList()
        {
            _dashList = new List<Dish>();
            _dashList.Add(new Dish() { Type = EDishType.ENTREE, TimeOfDay = "Morning", Name = "Eggs" });
            _dashList.Add(new Dish() { Type = EDishType.ENTREE, TimeOfDay = "Night", Name = "Steak" });

            _dashList.Add(new Dish() { Type = EDishType.SIDE, TimeOfDay = "Morning", Name = "Toast" });
            _dashList.Add(new Dish() { Type = EDishType.SIDE, TimeOfDay = "Night", Name = "Potato" });

            _dashList.Add(new Dish() { Type = EDishType.DRINK, TimeOfDay = "Morning", Name = "Coffee" });
            _dashList.Add(new Dish() { Type = EDishType.DRINK, TimeOfDay = "Night", Name = "Wine" });

            _dashList.Add(new Dish() { Type = EDishType.DESSERT, TimeOfDay = "Night", Name = "Cake" });
        }

        /// <summary>
        /// Find Dish by type and TimeOfDay
        /// </summary>
        /// <param name="type"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public string FindDish(string type, string day )
        {
            int itype = -1;
            int.TryParse(type, out itype);
            var dish = _dashList.FirstOrDefault(p => p.Type == (EDishType)itype && p.TimeOfDay.ToLower() == day);

            if(dish == null)
            {
                return "";                
            }

            return dish.Name;
        }

    }
}