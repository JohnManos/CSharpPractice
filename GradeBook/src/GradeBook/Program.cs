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
                try
                {
                    b.AddGrade(double.Parse(input));
                }
                catch(ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch(FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            var results = b.GetStatistics();
            b.ShowStatistics(results);
        }
    }
}
