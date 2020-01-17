using System;
using System.Collections.Generic;

namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args); // remember delegates are type definitions. The parameters here are part of csharp's event conventions.

    public abstract class Book // abstract classes differ from interfaces in that they may or may not specify implementation
    {
        public Book(string name)
        {
        }
        public Book(string name, string category)
        {
        }

        public abstract void AddGrade(double grade);
    }

    public class InMemoryBook : Book
    { 
        // This is a property.
        // A property is essentially a public field that implicitly wraps a private field (whether or not you declare such a field).
        // Accessing the property through "b.Name" implicitly calls a getter or setter for a "private name" field.
        // Specific getter or setter behavior can be specified within the get{} or set{}. 
        public string Name
        {
            get/*{ return name;}*/;
            /*private*/ set /*{name = value;}*/;
        }
        /*private string name;*/
        public event GradeAddedDelegate GradeAdded; // event keyword modifies an instance of a delegate. Book has a GradeAdded event as a field
        // Readonly members can be set during initialization or in the constructor, but NOWHERE else not even within this class 
        // (whereas const keyword means it can only be set once during initialization but NOT a 2nd time in the constructor)
        readonly string category;
        private List<double> grades;

        public InMemoryBook(string name) : base(name)
        { 
            Name = name;
            grades = new List<double>();
        }
        public InMemoryBook(string name, string category) : base(name, category)
        { 
            Name = name;
            this.category = category;
            this.category = "Hi"; // see this doesn't cause a compiler error, unlike const which would
            this.category = category;
            grades = new List<double>();
        }

        public override void AddGrade(double grade) // must add override keyword to implement abstract base class members
        { 
            if (grade <= 100 && grade >=0)
            {
                grades.Add(grade);
                if (GradeAdded != null) // if the caller has specified some behavior upon adding a grade ("handling" the event)
                {
                    GradeAdded(this, new EventArgs()); // do that behavior. this object instance is the sender. eventargs can be used to specify
                }
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }

        public void AddGrade(char grade)
        { 
            switch(grade)
            {
                case 'A':
                    AddGrade(90);
                    break;
                case 'B':
                    AddGrade(-80);
                    break;
                case 'C':
                    AddGrade(70);
                    break;
                case 'D':
                    AddGrade(60);
                    break;
                case 'F':
                    AddGrade(50);
                    break;
            }
        }
        public Statistics GetStatistics()
        { 
            var result = new Statistics();
            result.LowestScore = double.MaxValue;
            result.HighestScore = double.MinValue;
            result.AverageScore = 0.0;

            foreach (var grade in grades)
            { 
                if (grade < result.LowestScore)
                { 
                    result.LowestScore = grade;
                }
                if (grade > result.HighestScore)
                { 
                    result.HighestScore = grade;
                }
                result.AverageScore += grade;
            }
            result.AverageScore /= grades.Count;
            
            // Csharp's newer switch syntax (i would just use if/else here but heck why not)
            switch(result.AverageScore)
            {
                case var g when g >= 90:
                    result.Letter = 'A';
                    break;
                case var g when g >= 80:
                    result.Letter = 'B';
                    break;
                case var g when g >= 70:
                    result.Letter = 'C';
                    break;
                case var g when g >= 60:
                    result.Letter = 'D';
                    break;
                default:
                    result.Letter = 'F';
                    break;
            }

            return result;
        }

        public void ShowStatistics(Statistics stats)
        { 
            Console.WriteLine($"Lowest score: {stats.LowestScore}, Highest Score: {stats.HighestScore}, Average Score: {stats.AverageScore:F1}");
        }
    }
}