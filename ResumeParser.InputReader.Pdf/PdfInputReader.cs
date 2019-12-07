using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeParser.InputReader.Pdf
{
    public class PdfInputReader
    {
        public bool CanHandle(string location)
        {
            return location.EndsWith("pdf");
        }

        public IList<string> Handle(string location)
        {
            var lines = new List<string>();

            if (!File.Exists(location))
            {
                throw new ArgumentException("File not exists: " + location);
            }

            using (var pdfReader = new PdfReader(location))
            {
                for (var page = 1; page <= pdfReader.NumberOfPages; page++)
                {
                    var strategy = new SimpleTextExtractionStrategy();
                    var currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                    currentText = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                    lines.AddRange(currentText.Split(new[] { "\n" }, StringSplitOptions.None));
                }
            }

            return lines;
        }
    }
}
