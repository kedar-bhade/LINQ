using LINQ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LINQ.Tests
{
    public class BookControllerTests
    {
        [Fact]
        public void GetBook_ReturnsBook_WhenBookExists()
        {
            var options = new DbContextOptionsBuilder<BookContext>()
                .UseInMemoryDatabase("GetBook_ReturnsBook_WhenBookExists")
                .Options;
            using (var context = new BookContext(options))
            {
                context.Books.Add(new Book { Id = 1, Title = "Test" });
                context.SaveChanges();
            }
            using (var context = new BookContext(options))
            {
                var controller = new BookController(context);
                var result = controller.GetBook(1);
                var book = Assert.IsType<Book>(result.Value);
                Assert.Equal("Test", book.Title);
            }
        }

        [Fact]
        public void GetBook_ReturnsNotFound_WhenBookMissing()
        {
            var options = new DbContextOptionsBuilder<BookContext>()
                .UseInMemoryDatabase("GetBook_ReturnsNotFound_WhenBookMissing")
                .Options;
            using var context = new BookContext(options);
            var controller = new BookController(context);
            var result = controller.GetBook(1);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_AddsBookAndSaves()
        {
            var mockSet = new Mock<DbSet<Book>>();
            var mockContext = new Mock<BookContext>(new DbContextOptions<BookContext>());
            mockContext.Setup(m => m.Books).Returns(mockSet.Object);
            var controller = new BookController(mockContext.Object);
            var book = new Book { Title = "New" };
            await controller.Create(book);
            mockSet.Verify(m => m.Add(book), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
