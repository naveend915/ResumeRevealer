using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ResumeParser.InputReader.Pdf;
using ResumeParser.OutputFormatter.Json;
using ResumeParser.ResumeProcessor;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;

namespace ResumeRevealer.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        [Route("GetAllResumes")]
        public IHttpActionResult GetResumes()
        {
            var pdfInput = new PdfInputReader();
            var processor = new ResumeProcessor(new JsonOutputFormatter());
            var files = Directory.GetFiles(@"D:\Tekathon 2019\Resumes").Select(Path.GetFullPath);
            var resumeList = new List<JObject>();
            foreach (var file in files)
            {
                var list = pdfInput.Handle(file);
                var output = processor.Process(list);
                resumeList.Add(JObject.Parse(output));
            }
            return this.Ok(resumeList);
        }
    }
}
