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
        private ResumeProcessor resumeProcessor;
        public HomeController()
        {
            resumeProcessor = new ResumeProcessor();
        }
        [HttpGet]
        [Route("GetAllResumes")]
        public IHttpActionResult GetResumes()
        {
            var pdfInput = new PdfInputReader();
            var processor = new ResumeProcessor(new JsonOutputFormatter());
            var files = Directory.GetFiles(@"C:\Tekathon 2019\Resumes").Select(Path.GetFullPath);
            var candidates = resumeProcessor.GetCandidates();
            var resumeList = new List<JObject>();
            foreach (var file in files)
            {
                IList<string> list = null;
                if (candidates.Count > 0)
                {
                    if (candidates.Find(x => x.Path == file) == null)
                    {
                        list = pdfInput.Handle(file);
                    }
                }
                else { list = pdfInput.Handle(file); }
                var output = processor.Process(list,file);
                resumeList.Add(JObject.Parse(output));
            }
            return this.Ok(resumeList);
        }
    }
}
