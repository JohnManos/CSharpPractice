using System;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var b = new Book("book");
            b.AddGrade(59.2);
            b.AddGrade(71.11);
            var results = b.GetStatistics();
            b.ShowStatistics(results);
        }
    }
}
