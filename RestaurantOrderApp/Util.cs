using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantOrderApp
{
    public static class Util
    {
        public static EDishType GetEDishType(string type)
        {
            object eType = EDishType.NONE;

            Enum.TryParse(typeof(EDishType), type, out eType);

            return (EDishType)eType;
        }


        public static EDishType EnumTryParse(string sType)
        {
            int iType = 0;
            int.TryParse(sType,out iType);

            if (Enum.IsDefined(typeof(EDishType), iType))
                return (EDishType)Enum.Parse(typeof(EDishType), iType.ToString());

            return EDishType.NONE;
        }
    }
}
