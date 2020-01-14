using System;
using System.Collections.Generic;

namespace GradeBook {
    public class Book {
        public string Name;
        private List<double> grades;

        public Book() {
            grades = new List<double>();
        }

        public void AddGrade(double grade) {
            grades.Add(grade);
        }

        public Statistics GetStatistics() {
            var result = new Statistics();
            result.LowestScore = double.MaxValue;
            result.HighestScore = double.MinValue;
            result.AverageScore = 0.0;
            foreach (var grade in grades) {
                if (grade < result.LowestScore) {
                    result.LowestScore = grade;
                }
                if (grade > result.HighestScore) {
                    result.HighestScore = grade;
                }
                result.AverageScore += grade;
            }
            result.AverageScore /= grades.Count;
            return result;
        }

        public void ShowStatistics(Statistics stats) {
            Console.WriteLine($"Lowest score: {stats.LowestScore}, Highest Score: {stats.HighestScore}, Average Score: {stats.AverageScore}");
        }
    }
}