using System;
using Xunit;

namespace GradeBook.Test
{
    public class BookTests
    {           
        [Fact]
        public void BookCalculatesAnAverageScore()
        {
            //arrange
            var book = new Book("book");
            book.AddGrade(64.5);
            book.AddGrade(86);
            book.AddGrade(75);
            
            //act
            var result = book.GetStatistics();

            //assert
            Assert.Equal(75.2, result.AverageScore, 1); // third arg indicates float precision
        }

        [Fact]
        public void BookCalculatesAnAverageLetterGrade()
        {
            //arrange
            var book = new Book("book");
            book.AddGrade(64.5);
            book.AddGrade(85.5);
            book.AddGrade(75);
            
            //act
            var result = book.GetStatistics();

            //assert
            Assert.Equal('C', result.Letter);
        }
    }
}
