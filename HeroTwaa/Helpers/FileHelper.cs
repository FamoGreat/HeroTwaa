using OfficeOpenXml;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using Xceed.Words.NET;

namespace HeroTwaa.Helpers
{
    public class FileHelper
    {
        static FileHelper()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Set the license context
        }

        public static List<UserData> ParseExcelFile(Stream fileStream)
        {
            var users = new List<UserData>();

            using (var package = new ExcelPackage(fileStream))
            {
                var worksheet = package.Workbook.Worksheets.First();
                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    users.Add(new UserData
                    {
                        Name = worksheet.Cells[row, 1].Text,
                        Email = worksheet.Cells[row, 2].Text,
                        PhoneNumber = worksheet.Cells[row, 3].Text,
                        Role = worksheet.Cells[row, 4].Text
                    });
                }
            }

            return users;
        }

        public static List<UserData> ParsePdfFile(Stream fileStream)
        {
            var users = new List<UserData>();

            using (var pdfReader = new PdfReader(fileStream))
            using (var pdfDocument = new PdfDocument(pdfReader))
            {
                var strategy = new SimpleTextExtractionStrategy();
                for (int i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
                {
                    var text = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(i), strategy);
                    var lines = text.Split('\n');
                    foreach (var line in lines.Skip(1)) // Skip header
                    {
                        var columns = line.Split(' ');
                        if (columns.Length >= 4)
                        {
                            users.Add(new UserData
                            {
                                Name = columns[0],
                                Email = columns[1],
                                PhoneNumber = columns[2],
                                Role = columns[3]
                            });
                        }
                    }
                }
            }

            return users;
        }

        public static List<UserData> ParseDocxFile(Stream fileStream)
        {
            var users = new List<UserData>();

            using (var document = DocX.Load(fileStream))
            {
                var table = document.Tables.First();
                foreach (var row in table.Rows.Skip(1)) // Skip header
                {
                    users.Add(new UserData
                    {
                        Name = row.Cells[0].Paragraphs[0].Text,
                        Email = row.Cells[1].Paragraphs[0].Text,
                        PhoneNumber = row.Cells[2].Paragraphs[0].Text,
                        Role = row.Cells[3].Paragraphs[0].Text
                    });
                }
            }

            return users;
        }

        public class UserData
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Role { get; set; }
        }
    }
}
