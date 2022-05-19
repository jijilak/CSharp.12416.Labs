using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhatDay1
{
    enum monthName
    {
        January, February, March, April, May, June, July, August, September, October, November, December
    }
    class WhatDay
    {
        static void Main()
        {
            Console.WriteLine("Please enter a day number between 1 and 365: ");
            string line = Console.ReadLine();
            int dayNum = int.Parse(line);
            int monthNum = 0;
            foreach (int daysInMonth in DaysInMonths)
            {
                if (dayNum <= daysInMonth)
                {
                    break;
                }
                else
                {
                    dayNum -= daysInMonth;
                    monthNum++;
                }
            }
            monthName temp = (monthName)monthNum;
            string monthName = temp.ToString();
            //
            // Add code here
            //

            ////////////////////////////////////////////////////////////////////
            ////// Uncomment the code below  before Calculating           //////
            /////  the month and day pair from a day number               //////
            ////////////////////////////////////////////////////////////////////

            // // January
            // if (dayNum <= 31)
            // {
            //     goto End;
            // }
            // else
            // {
            //     dayNum -= 31;
            //     monthNum++;
            // }

            // // February
            // if (dayNum <= 28)
            // {
            //     goto End;
            // }
            // else
            // {
            //     dayNum -= 28;
            //     monthNum++;
            // }

            // // March
            // if (dayNum <= 31)
            // {
            //     goto End;
            // }
            // else
            // {
            //     dayNum -= 31;
            //     monthNum++;
            // }

            // // April
            // if (dayNum <= 30)
            // {
            //     goto End;
            // }
            // else
            // {
            //     dayNum -= 30;
            //     monthNum++;
            // }

            // // May
            // if (dayNum <= 31)
            // {
            //     goto End;
            // }
            // else
            // {
            //     dayNum -= 31;
            //     monthNum++;
            // }

            // // June
            // if (dayNum <= 30)
            // {
            //     goto End;
            // }
            // else
            // {
            //     dayNum -= 30;
            //     monthNum++;
            // }

            // // July
            // if (dayNum <= 31)
            // {
            //     goto End;
            // }
            // else
            // {
            //     dayNum -= 31;
            //     monthNum++;
            // }

            // // August
            // if (dayNum <= 31)
            // {
            //     goto End;
            // }
            // else
            // {
            //     dayNum -= 31;
            //     monthNum++;
            // }

            // // September
            // if (dayNum <= 30)
            // {
            //     goto End;
            // }
            // else
            // {
            //     dayNum -= 30;
            //     monthNum++;
            // }

            // // October
            // if (dayNum <= 31)
            // {
            //     goto End;
            // }
            // else
            // {
            //     dayNum -= 31;
            //     monthNum++;
            // }

            // // November
            // if (dayNum <= 30)
            // {
            //     goto End;
            // }
            // else
            // {
            //     dayNum -= 30;
            //     monthNum++;
            // }

            // // December
            // if (dayNum <= 31)
            // {
            //     goto End;
            // }
            // else
            // {
            //     dayNum -= 31;
            //     monthNum++;
            // }

            //if (dayNum > 365)
            //{

            //}
            // else
            //     Console.WriteLine("Error date!");

            //////////////////////////////////////////////////////////////
            // TODO: Implement if statements for November and December ///
            //////////////////////////////////////////////////////////////


            //////////////////////////////////////////////////////////////
            // TODO:												  ////
            //		Add an identifer label End below and              ////
            //		declare string variable							  ////
            //////////////////////////////////////////////////////////////

            //End:
            //monthName temp = (monthName)monthNum;
            //string monthName = temp.ToString();

            //////////Lab3Ex1/////////////
            //string monthName;
            //switch (monthNum)
            //{
            //    case 0:
            //        monthName = "January"; break;
            //    case 1:
            //        monthName = "February"; break;
            //    case 2:
            //        monthName = "March"; break;
            //    case 3:
            //        monthName = "April"; break;
            //    case 4:
            //        monthName = "May"; break;
            //    case 5:
            //        monthName = "June"; break;
            //    case 6:
            //        monthName = "July"; break;
            //    case 7:
            //        monthName = "August"; break;
            //    case 8:
            //        monthName = "September"; break;
            //    case 9:
            //        monthName = "October"; break;
            //    case 10:
            //        monthName = "November"; break;
            //    case 11:
            //        monthName = "December"; break;
            //    default:
            //        monthName = "not done yet"; break;
            //}
            //////////Lab3Ex1/////////////


            //Console.WriteLine("{0} {1}", dayNum, monthName);
            Console.WriteLine("{0} {1}", monthName, dayNum);

            //////////////////////////////////////////////////////////////////
            // TODO: Implement the case 10 and case 11 for November         //
            //       and December											//
            //       Add a default label									//
            //////////////////////////////////////////////////////////////////

        }
        // Don't modify anything below here
        static System.Collections.ICollection DaysInMonths = new int[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    }
}
