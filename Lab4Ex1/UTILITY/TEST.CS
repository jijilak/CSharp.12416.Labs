namespace Utils
{
    using System;

    /// <summary>
    ///   This is the test harness
    /// </summary>

    public class Test
    {
        public static void Main()
        {
            int x; // input value 1
            int y; // input value 2
            int greater; // result from Greater()
            Console.WriteLine("Enter first number:"); // Get input numbers

            x = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter second number:");
            y = int.Parse(Console.ReadLine());

            //Test the Greater () method
            greater = Utils.Greater(x, y);
            Console.WriteLine("The greater value is " + greater);
            //Test the Swap method
            Console.WriteLine("Before swap: " + x + "," + y);
            Utils.Swap(ref x, ref y);
            Console.WriteLine("After swap: " + x + "," + y);
        }
    }
}
