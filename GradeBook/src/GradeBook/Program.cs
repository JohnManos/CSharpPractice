using System;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            Book book;
            Console.WriteLine("Would you like to enter grades in memory or to a file? \n Press m for memory, or f for file, then press enter.");
            var input = Console.ReadLine();
            while(true)
            {
                if (input == "m")
                {
                    book = new InMemoryBook("book"); // lets say we want an InMemoryBook object here, but... (see EnterGrades())
                    break;
                }
                else if (input == "f")
                {
                    book = new DiskBook("book");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid response.");
                }
            }

            book.GradeAdded += OnGradeAdded;
            // b. GradeAdded = null <-- this wopuld throw a compiler error because events cannot be directly assigned, only added or subtracted to.
            // ^^ that is because with an event you either want to subscribe or unsubscribe to/from it, assigning it would unsubscribe everyone else which would be wack yo
            EnterGrades(book);

            var results = book.GetStatistics();
            book.ShowStatistics(results);
        }

        private static void EnterGrades(IBook b) // for entering grades, we don't really care what kind of book, just that it is a book
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
