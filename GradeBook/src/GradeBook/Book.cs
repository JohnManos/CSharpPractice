using System;
using System.Collections.Generic;

namespace GradeBook
{
    public class Book
    { 
        public string Name;
        private List<double> grades;

        public Book(string name)
        { 
            Name = name;
            grades = new List<double>();
        }

        public void AddGrade(double grade)
        { 
            if (grade <= 100 && grade >=0)
            {
                grades.Add(grade);
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