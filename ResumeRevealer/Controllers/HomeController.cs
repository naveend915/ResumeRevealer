using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ResumeParser.InputReader.Pdf;
using ResumeParser.Model;
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
            string output = "";
            foreach (var file in files)
            {
                Resume resume = null;
                IList<string> list = null;
                if (candidates.Count > 0)
                {
                    var candidateResume = candidates.Find(x => x.Path == file);
                    if (candidateResume != null)
                    {
                        resume = new Resume()
                        {
                            Firstname = candidateResume.Name,
                            Gender = candidateResume.Gender,
                            Certifications = candidateResume.Certifications == null ? null : candidateResume.Certifications.Split(',').ToList(),
                            Designation = candidateResume.Designation,
                            Path = candidateResume.Path,
                            Skills = candidateResume.Skills == null ? null : candidateResume.Skills.Split(',').ToList(),
                            yoe = candidateResume.YearsOfExperience
                        };
                        output = processor.Process(list, file, resume);
                        resumeList.Add(JObject.Parse(output));
                    }
                    else
                    {
                        list = pdfInput.Handle(file);
                        output = processor.Process(list, file, resume);
                        resumeList.Add(JObject.Parse(output));
                    }
                }
                else
                {
                    list = pdfInput.Handle(file);
                    output = processor.Process(list, file, resume);
                    resumeList.Add(JObject.Parse(output));
                }
            }
            return this.Ok(resumeList);
        }

        [HttpGet]
        [Route("GetAllCandidates")]
        public IHttpActionResult GetAllCandidates()
        {
            return Ok(resumeProcessor.GetCandidates());
        }
    }
}
