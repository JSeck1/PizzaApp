using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace PizzaApp
{
    /// <summary>
    /// Main Pizza Class
    /// </summary>
    public class Pizza
    {
        public int BaseCookingTime { get; set; } 
        public PizzaBase PizzaBase { get; set; }
        public PizzaTopping Topping { get; set; }
        
        public Pizza(PizzaBase BaseName, PizzaTopping ToppingName, int BaseCookingTime)
        {
            this.PizzaBase = BaseName;
            this.Topping = ToppingName;
            this.BaseCookingTime = BaseCookingTime;
        }

        /// <summary>
        /// Cooks the Pizza and Returns Total Cooking Time
        /// </summary>
        /// <returns>Total Cooking Time</returns>
        public int CookPizza() 
        {
            int totalBaseCookingTime = GetBaseCookingTime();
            int totalToppingCookingTime = GetToppingCookingTime();
            int totalCookingTime = totalBaseCookingTime + totalToppingCookingTime;

            // Set the delay to simulate cooking.
            Task.Delay(totalCookingTime).Wait();
            return totalCookingTime;
        }

        /// <summary>
        /// This gets the base cooking time dependent on type of pizza base.
        /// </summary>
        /// <returns></returns>
        private int GetBaseCookingTime()
        {
            switch (this.PizzaBase)
            {
                case PizzaBase.DeepPan:
                    return Convert.ToInt32(this.BaseCookingTime * 2);
                case PizzaBase.StuffCrust:
                    return Convert.ToInt32(this.BaseCookingTime * 1.5);
                case PizzaBase.ThinAndCrispy:
                    return this.BaseCookingTime;
                default:
                    throw new ArgumentOutOfRangeException(nameof(PizzaBase), PizzaBase, null);
            }
        }

        /// <summary>
        /// This gets the Topping Cooking time based on the Enumeration Member Value
        /// </summary>
        /// <returns></returns>
        private int GetToppingCookingTime() 
        {
            var enumType = typeof(PizzaTopping);
            var enumVal = this.Topping;
            var toppingMemberValue = EnumHelper.GetEnumMemberAttrValue(enumType, enumVal);
            return toppingMemberValue.Length * 100;
        }
    }

    // Have used an Enum Member Annotation on the Topping as I am unsure whether the string "Ham and Mushroom" needs to include the spaces in the string as 
    // part of the cooking time.  In this case, I decided it does but can be changed here easily if not ??
    public enum PizzaTopping
    {
       [EnumMember(Value = "Ham and Mushroom")]
       HamAndMushroom,
       [EnumMember(Value = "Pepperoni")]
       Pepperoni,
       [EnumMember(Value = "Vegetable")]
       Vegetable,
    }

    public enum PizzaBase
    {
       DeepPan,
       StuffCrust,
       ThinAndCrispy,
    }
    

}