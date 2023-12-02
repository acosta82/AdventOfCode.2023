using System;

namespace Advent._2023.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Enter the Advent Calendar day: ");

            var dateTime = DateTime.Now;
            var allowEnter = dateTime.Month == 12 && dateTime.Day <= 24;

            if (allowEnter)
            {
                System.Console.WriteLine($"[Enter for {dateTime.Day}]");
            }

            var day = System.Console.ReadLine();

            if(allowEnter && string.IsNullOrEmpty(day))
                day = dateTime.Day.ToString();

            IAdventCalendarDay calendarDay;

            switch (day)
            {
                case "1":
                    calendarDay = new Day1();
                    break;
                case "2":
                    calendarDay = new Day2();
                    break;
                default:
                    return;
            }

            calendarDay.Run();

            System.Console.ReadKey();
        }
    }
}