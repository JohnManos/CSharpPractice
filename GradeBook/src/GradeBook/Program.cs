using System;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var b = new Book("book");
            b.GradeAdded += OnGradeAdded;
            // b. GradeAdded = null <-- this wopuld throw a compiler error because events cannot be directly assigned, only added or subtracted to.
            // ^^ that is because with an event you either want to subscribe or unsubscribe to/from it, assigning it would unsubscribe everyone else which would be wack yo

            EnterGrades(b);

            var results = b.GetStatistics();
            b.ShowStatistics(results);
        }

        private static void EnterGrades(Book b)
        {
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
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void OnGradeAdded(object sender, EventArgs eventArgs)
        {
            Console.WriteLine("A grade was added.");
        }
    }
}
