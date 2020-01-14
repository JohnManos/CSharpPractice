using System;
using Xunit;

namespace GradeBook.Test {
    public class BookTests {    
        
        [Fact]
        public void Test1() {
            //arrange
            var book = new Book();
            book.AddGrade(69.2);
            book.AddGrade(85);
            book.AddGrade(12.57);
            
            //act
            var result = book.GetStatistics();

            //assert
            Assert.Equal(55.59, result.AverageScore, 1);
        }
    }
}
