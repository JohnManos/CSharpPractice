using System;
using Xunit;

namespace GradeBook.Test
{
    public class TypeTests
    {    

        [Fact]
        public void GetBookReturnsDifferentObjects()
        {
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");

            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 2", book2.Name);
        }

        [Fact]
        public void TwoVarsCanReferenceSameObject()
        {
            var book1 = GetBook("Book 1");
            var book2 = book1;

            Assert.True(Object.ReferenceEquals(book1, book2));
        }

        [Fact]
        public void CanSetNameByReference()
        {
            var book1 = GetBook("Book 1");

            SetBookName(book1, "New Name");

            Assert.Equal("New Name", book1.Name);
        }

        [Fact]
        public void CSharpIsPassByValue()
        {
            var book1 = GetBook("Book 1");

            GetBookSetName(book1, "New Name");

            Assert.Equal("Book 1", book1.Name);
        }

        [Fact]
        public void CSharpCanPassByRef()
        {
            var book1 = GetBook("Book 1");

            GetBookSetName(ref book1, "New Name");

            Assert.Equal("New Name", book1.Name);
        }

        [Fact]
        public void StringsBehaveLikeValueTypes()
        {
            string name = "John";
            var upper = MakeUpperCase(name);

            Assert.Equal("John", name);
            Assert.Equal("JOHN", upper);
        }

        public delegate string WriteLogDelegate(string logMessage);
        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            WriteLogDelegate log;

            log = new WriteLogDelegate(ReturnMessage);
            // alternatively...
            /*log = ReturnMessage*/

            var result = log("Hello!");
            Assert.Equal("Hello!", result);
        }

        int count = 0;
        [Fact]
        public void WriteLogDelegateCanPointToMultipleMethods()
        {
            WriteLogDelegate log = ReturnMessage;

            log += ReturnMessage;
            log += IncrementCount;
            
            var result = log("Hello!");
            Assert.Equal(3, count);
        }

        string ReturnMessage(string message)
        {
            count++;
            return message;
        }

        string IncrementCount(string mynamedontmatter)
        {
            count++;
            return mynamedontmatter;
        }

        private string MakeUpperCase(string param)
        {
            return param.ToUpper();
        }

        private InMemoryBook GetBook(string name)
        {
            return new InMemoryBook(name);
        }

        private void SetBookName(InMemoryBook book, string name)
        {
            book.Name = name;
        }

        private void GetBookSetName(InMemoryBook book, string name)
        {
            book =  new InMemoryBook(name);
            book.Name = name;
        }

        private void GetBookSetName(ref InMemoryBook book, string name)
        {
            book =  new InMemoryBook(name);
        }
    }
}
