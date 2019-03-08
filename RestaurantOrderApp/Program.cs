using System;
using System.Collections.Generic;

namespace RestaurantOrderApp
{
    /// <summary>
    /// Program RestaurantOrderApp
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                bool stop = false;

                while (!stop)
                {
                    Console.Write("Enter your Order or 0 for exit: ");
                    string options = Console.ReadLine();
                    Console.Clear();

                    if (options == "0")
                    {
                        stop = true;
                        break;
                    }

                    var work = new Work(options);
                    work.ProcessOrder();

                    if (work.IsValid)
                    {
                        Console.WriteLine(work._order.ToString());
                    }
                    else
                    {
                        Console.WriteLine(work.Msg);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }              
    }
}
