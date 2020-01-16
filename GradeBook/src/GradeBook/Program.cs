using System;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var b = new Book("book");
            Console.WriteLine("Please enter grades to calculate score statistics, and/or q to finish.");
            var done = false;
            while (!done)
            {
                var input = Console.ReadLine();
                if (input == "q")
                {
                    break;
                }
                b.AddGrade(double.Parse(input));
            }

            var results = b.GetStatistics();
            b.ShowStatistics(results);
        }
    }
}
