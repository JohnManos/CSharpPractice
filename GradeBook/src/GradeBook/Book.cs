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
        Statistics Statistics { get; }
        // no category field here. Interfaces cannot have fields. They are defining a contract with the implementers, not implementation. Field are considered implementation.   
        event GradeAddedDelegate GradeAdded;
        void AddGrade(double grade); // no public keyword needed because it is assumed that any implkementer has a visible member for this
    }

    public abstract class Book : IBook // abstract classes differ from interfaces in that they may or may not specify implementation
    {
        public Book(string name)
        {
            Name = name;
            statistics = new Statistics();
        }
        public Book(string name, string category)
        {
            Name = name;
            this.category = category;
            statistics = new Statistics();
        }
        public abstract string Name { get; set; }
        public virtual Statistics Statistics { get { return statistics; } }
        public abstract event GradeAddedDelegate GradeAdded;
        readonly Statistics statistics;
        readonly string category; // fields are never 'abstract' because there is no implementation to be specified. Note that Name is a property, and GradeAdded is an event, not fields.
        public abstract void AddGrade(double grade);
        public virtual void ShowStatistics() // virtual keyword means that the class specifies an implementation, but that imp can still be overriden.
        { 
            Console.WriteLine($"Lowest score: {Statistics.Lowest}, Highest Score: {Statistics.Highest}, Average Score: {Statistics.Average:F1}");
        }
    }

    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {
            statistics = new Statistics();
        }

        public override string Name { get; set; }
        public override Statistics Statistics
        {
            get
            {
                using (StreamReader sw = File.OpenText($"{Name}.txt"))
                {
                    while (sw.Peek() > -1)
                    {
                        statistics.Add(double.Parse(sw.ReadLine()));
                    }             
                }
                return statistics;
            }
        }
        public override event GradeAddedDelegate GradeAdded;
        readonly Statistics statistics;

        public override void AddGrade(double grade)
        {
            if (grade <= 100 && grade >= 0)
            {
                using (StreamWriter sw = File.AppendText($"{Name}.txt"))
                {
                    sw.WriteLine(grade);
                    if (GradeAdded != null) // if the caller has specified some behavior upon adding a grade ("handling" the event)
                    {
                        GradeAdded(this, new EventArgs()); // do that behavior. this object instance is the sender. eventargs can be used to specify
                    }
                }
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }
    }

    public class InMemoryBook : Book
    {        
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

        // This is a property.
        // A property is essentially a public field that implicitly wraps a private field (whether or not you declare such a field).
        // Accessing the property through "b.Name" implicitly calls a getter or setter for a "private name" field.
        // Specific getter or setter behavior can be specified within the get{} or set{}. 
        public override string Name
        {
            get/*{ return name;}*/;
            /*private*/ set /*{name = value;}*/;
        }
        public override event GradeAddedDelegate GradeAdded; // event keyword modifies an instance of a delegate. Book has a GradeAdded event as a field
        // Readonly members can be set during initialization or in the constructor, but NOWHERE else not even within this class 
        // (whereas const keyword means it can only be set once during initialization but NOT a 2nd time in the constructor)
        readonly string category;
        private List<double> grades;

        public override void AddGrade(double grade) // must add override keyword to implement abstract base class members
        { 
            if (grade <= 100 && grade >=0)
            {
                grades.Add(grade);
                Statistics.Add(grade);
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
    }
}