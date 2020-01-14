using System;
using System.Collections.Generic;

namespace GradeBook {
    public class Book {
        private List<double> Grades;

        public Book() {
            Grades = new List<double>();
        }

        public void AddGrade(double grade) {
            Grades.Add(grade);
        }

        public Statistics GetStatistics() {
            var result = new Statistics();
            result.LowestScore = double.MaxValue;
            result.HighestScore = double.MinValue;
            result.AverageScore = 0.0;
            foreach (var grade in Grades) {
                if (grade < result.LowestScore) {
                    result.LowestScore = grade;
                }
                if (grade > result.HighestScore) {
                    result.HighestScore = grade;
                }
                result.AverageScore += grade;
            }
            result.AverageScore /= Grades.Count;
            return result;
        }

        public void ShowStatistics(Statistics stats) {
            Console.WriteLine($"Lowest score: {stats.LowestScore}, Highest Score: {stats.HighestScore}, Average Score: {stats.AverageScore}");
        }
    }
}