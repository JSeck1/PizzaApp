using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace PizzaApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup to use Configuration File
            IConfiguration Config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            // Get all Settings from the Configuration File
            int Start = 1;
            int BaseCookingTime = int.Parse(Config.GetSection("BaseCookingTimeInMilliSeconds").Value);
            int NumberOfPizzas = int.Parse(Config.GetSection("NumberOfPizzas").Value);
            int Interval = int.Parse(Config.GetSection("IntervalInMilliSeconds").Value);
            string fileName = Config.GetSection("FileNameForLog").Value;

            while(Start <= NumberOfPizzas)
            {
                // Get a Random Base and Topping from Enumeration
                var random = new Random();
                var myPizzaBase = random.NextEnum<PizzaBase>();
                var myPizzaTopping = random.NextEnum<PizzaTopping>();
                
                // Create a Pizza Object passing Random Base and Topping
                var MyPizza = new Pizza(myPizzaBase, myPizzaTopping, BaseCookingTime);

                // Cook the Pizza passing a Configurable Base Cooking Time
                int TotalCookingTime = MyPizza.CookPizza();

                // Write the details of the cooked pizza to the output file
                WriteToLog(MyPizza, fileName, TotalCookingTime);

                // Set the Interval
                Task.Delay(Interval).Wait();

                Start++;
            }
        }

        static void  WriteToLog(Pizza pizza, string fileName, int TotalCookingTime)
        {
            // Get Enumeration Value for the Topping
            var enumType = typeof(PizzaTopping);
            var enumVal = pizza.Topping;
            var toppingMemberValue = EnumHelper.GetEnumMemberAttrValue(enumType, enumVal);

            // Generate String to Write to Log
            string logText = DateTime.Now.ToShortDateString() + " A " +  pizza.PizzaBase + " pizza with a topping of " + toppingMemberValue
            + " has finished cooking with a Total time of " + TotalCookingTime;

            if (!File.Exists(fileName))
            {
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.WriteLine(logText);
                }
            }
            else
            {
                StreamWriter sw = File.AppendText(fileName);
                sw.WriteLine(logText);
                sw.Close();
            }

            Console.WriteLine(logText);
        }

    }
}
