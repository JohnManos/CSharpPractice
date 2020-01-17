using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args); // remember delegates are type definitions. The parameters here are part of csharp's event conventions.

    public interface IBook // interfaces are pure - they contain no implementation. they specify what kinds of things any Book should do
    {
        string Name { get; set; }     
        // no category field here. Interfaces cannot have fields. They are defining a contract with the implementers, not implementation. Field are considered implementation.   
        event GradeAddedDelegate GradeAdded;
        void AddGrade(double grade); // no public keyword needed because it is assumed that any implkementer has a visible member for this
        Statistics GetStatistics();
    }

    public abstract class Book : IBook // abstract classes differ from interfaces in that they may or may not specify implementation
    {
        public Book(string name)
        {
            Name = name;
        }
        public Book(string name, string category)
        {
            Name = name;
            this.category = category;
        }
        public abstract string Name { get; set; }
        readonly string category; // fields are never 'abstract' because there is no implementation to be specified. Note that Name is a property, and GradeAdded is an event, not fields.
        public abstract event GradeAddedDelegate GradeAdded;
        public abstract void AddGrade(double grade);
        public abstract Statistics GetStatistics();
        public virtual void ShowStatistics(Statistics stats) // virtual keyword means that the class specifies an implementation, but that imp can still be overriden.
        { 
            Console.WriteLine($"Lowest score: {stats.LowestScore}, Highest Score: {stats.HighestScore}, Average Score: {stats.AverageScore:F1}");
        }
    }

    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {
            /*if (!File.Exists(name))
            {
                fs = File.Open(name, FileMode.OpenOrCreate); // this opens it for reading or writing while also creating the file if it does not exist
            }*/
        }
        public override string Name { get; set; }
        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            if (grade <= 100 && grade >= 0)
            {
                StreamWriter sw = File.AppendText($"{Name}.txt");
                sw.Write((char) grade);
                sw.WriteLine();
                if (GradeAdded != null) // if the caller has specified some behavior upon adding a grade ("handling" the event)
                {
                    GradeAdded(this, new EventArgs()); // do that behavior. this object instance is the sender. eventargs can be used to specify
                }
                sw.Close();
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }
        
        public override Statistics GetStatistics()
        {
            List<double> grades = new List<double>();
            StreamReader sw = File.OpenText($"{Name}.txt");
            while (sw.Peek() != '\n')
            {
                //List<int> buf = new List<int>();
                grades.Add(sw.Read());
            }

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
    }

    public class InMemoryBook : Book
    { 
        // This is a property.
        // A property is essentially a public field that implicitly wraps a private field (whether or not you declare such a field).
        // Accessing the property through "b.Name" implicitly calls a getter or setter for a "private name" field.
        // Specific getter or setter behavior can be specified within the get{} or set{}. 
        public override string Name
        {
            get/*{ return name;}*/;
            /*private*/ set /*{name = value;}*/;
        }
        /*private string name;*/
        public override event GradeAddedDelegate GradeAdded; // event keyword modifies an instance of a delegate. Book has a GradeAdded event as a field
        // Readonly members can be set during initialization or in the constructor, but NOWHERE else not even within this class 
        // (whereas const keyword means it can only be set once during initialization but NOT a 2nd time in the constructor)
        readonly string category;
        private List<double> grades;

        public InMemoryBook(string name) : base(name)
        { 
            grades = new List<double>();
        }
        public InMemoryBook(string name, string category) : base(name, category)
        { 
            this.category = "Hi"; // see modifying a readonly in the constuctor doesn't cause a compiler error, unlike const which would
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
        
        public override Statistics GetStatistics()
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
    }
}