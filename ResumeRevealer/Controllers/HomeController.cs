using ResumeParser.InputReader.Pdf;
using ResumeParser.Model;
using ResumeParser.OutputFormatter.Json;
using ResumeParser.ResumeProcessor;
using System.IO;
using System.Linq;
using System.Web.Http;

namespace ResumeRevealer.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        [Route("test")]
        public IHttpActionResult HasAccesstoModules()
        {
            var result = "Test message !!";
            var pdfInput = new PdfInputReader();
            var processor = new ResumeProcessor(new JsonOutputFormatter());
            var files = Directory.GetFiles(@"D:\Tekathon 2019\Resumes").Select(Path.GetFullPath);
            foreach (var file in files)
            {
                var list = pdfInput.Handle(file);
                var output = processor.Process(list);
                
            }
            return this.Ok(result);
        }
    }
}
