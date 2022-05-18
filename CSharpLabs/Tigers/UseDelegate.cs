using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    delegate double OurOperationHandler(double input);

    static class UseDelegate
    {
        public static void InvokeDelegate()
        {
            OurOperationHandler handler = null;
            Random r = new Random();
            handler += input => Math.Sqrt(input);
            handler += input => input * input;
            do
            {
                double number = r.NextDouble() * 10;
                //if (number < 5)
                    //handler += Sqrt;
                //else
                    //handler += Sqr;

                //handler(number);
                Console.WriteLine(handler(number));
            } while (Console.ReadLine() != "q");
        }
    }
}
