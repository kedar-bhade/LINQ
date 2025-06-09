using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ClosedXML.Excel;
using LibraryApi;
using LibraryApi.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExcelFunction
{
    public class ProcessExcelFunction
    {
        private readonly LibraryContext _context;
        private readonly ILogger _logger;

        public ProcessExcelFunction(LibraryContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<ProcessExcelFunction>();
        }

        [Function("ProcessExcel")]
        public async Task<HttpResponseData> Run([
            HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req)
        {
            if (req.Body == null)
            {
                var bad = req.CreateResponse(HttpStatusCode.BadRequest);
                await bad.WriteStringAsync("Request must contain an Excel file in the body.");
                return bad;
            }

            using var ms = new MemoryStream();
            await req.Body.CopyToAsync(ms);
            ms.Position = 0;

            using var workbook = new XLWorkbook(ms);
            var ws = workbook.Worksheet(1);
            var rows = ws.RowsUsed().Skip(1); // assume first row is headers
            foreach (var row in rows)
            {
                var authorName = row.Cell(1).GetValue<string>();
                var bookTitle = row.Cell(2).GetValue<string>();

                var author = await _context.Authors.FirstOrDefaultAsync(a => a.Name == authorName);
                if (author == null)
                {
                    author = new Author { Name = authorName };
                    _context.Authors.Add(author);
                    await _context.SaveChangesAsync();
                }

                var book = new Book { Title = bookTitle, AuthorId = author.Id };
                _context.Books.Add(book);
            }

            await _context.SaveChangesAsync();

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync("Excel processed");
            return response;
        }
    }
}
