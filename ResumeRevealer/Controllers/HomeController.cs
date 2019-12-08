using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ResumeParser.InputReader.Pdf;
using ResumeParser.Model;
using ResumeParser.OutputFormatter.Json;
using ResumeParser.ResumeProcessor;
using System;
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
            var candidates = resumeProcessor.GetCandidates(0);
            var resumeList = new List<JObject>();
            string output = "";
            var maxyeo = 0.0;
            var maxskillcounts = 0;
            var maxcertificationcount = 0;
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
                            Firstname = candidateResume.FirstName,
                            Lastname = candidateResume.LastName,
                            Gender = candidateResume.Gender,
                            Certifications = candidateResume.Certifications == null ? null : candidateResume.Certifications.Split(',').ToList(),
                            Designation = candidateResume.Designation,
                            Path = candidateResume.Path,
                            Skills = candidateResume.Skills == null ? null : candidateResume.Skills.Split(',').ToList(),
                            yoe = candidateResume.YearsOfExperience,
                            Emailaddress = candidateResume.EmailId                            
                        };
                        output = processor.Process(list, file, resume);
                        resumeList.Add(JObject.Parse(output));
                        if (maxyeo < Convert.ToDouble(candidateResume.YearsOfExperience))
                            maxyeo = Convert.ToDouble(candidateResume.YearsOfExperience);
                        if (maxskillcounts < candidateResume.Skills.Count())
                            maxskillcounts = candidateResume.Skills.Count();
                        if (maxcertificationcount < candidateResume.Certifications.Count())
                            maxcertificationcount = candidateResume.Certifications.Count();
                      
                    }
                    else
                    {
                        list = pdfInput.Handle(file);
                        output = processor.Process(list, file, resume);
                        var candidateData = JObject.Parse(output);
                        resumeList.Add(JObject.Parse(output));
                        if (!string.IsNullOrEmpty(candidateData["yoe"].ToString()) && maxyeo < Convert.ToDouble(candidateData["yoe"].ToString()))
                            maxyeo = Convert.ToDouble(candidateData["yoe"].ToString());
                        if (candidateData["skills"] != null && maxskillcounts < candidateData["skills"].Count())
                            maxskillcounts = candidateData["skills"].Count();
                        if (candidateData["certifications"]!=null && maxcertificationcount < candidateData["certifications"].Count())
                            maxcertificationcount = candidateData["certifications"].Count();
                    }
                }
                else
                {
                    list = pdfInput.Handle(file);
                    output = processor.Process(list, file, resume);
                    var candidateData = JObject.Parse(output);
                    resumeList.Add(JObject.Parse(output));
                    if (!string.IsNullOrEmpty(candidateData["yoe"].ToString()) && maxyeo < Convert.ToDouble(candidateData["yoe"].ToString()))
                        maxyeo = Convert.ToDouble(candidateData["yoe"].ToString());
                    if (candidateData["skills"] != null && maxskillcounts < candidateData["skills"].Count())
                        maxskillcounts = candidateData["skills"].Count();
                    if (candidateData["certifications"] != null && maxcertificationcount < candidateData["certifications"].Count())
                        maxcertificationcount = candidateData["certifications"].Count();
                }
            }

            foreach(var resume in resumeList)
            {
                var yoe = 0.0;
                var certificatecount = 0.0;
                var skillcount = 0.0;
                if(!string.IsNullOrEmpty(resume["yoe"].ToString()) && maxyeo > 0)
                {
                    yoe = Convert.ToDouble(resume["yoe"].ToString()) / maxyeo * 60;
                }
                if(resume["skills"] != null && maxskillcounts > 0)
                {
                    skillcount = Convert.ToDouble(resume["skills"].Count()) / Convert.ToDouble(maxskillcounts) * 30;
                }
                if(resume["certifications"] != null && maxcertificationcount > 0)
                {
                    certificatecount = Convert.ToDouble(resume["certifications"].Count()) / Convert.ToDouble(maxcertificationcount) * 10;
                }
                resume["rating"] = Math.Ceiling(yoe + skillcount + certificatecount);
            }
            return this.Ok(resumeList);
        }

        [HttpGet]
        [Route("GetAllCandidates")]
        public IHttpActionResult GetAllCandidates(string userId)
        {
            var maxyeo = 0.0;
            var maxskillcounts = 0;
            var candidates = resumeProcessor.GetCandidates(Convert.ToInt32(userId));
            foreach (var candidate in candidates)
            {
                if (!string.IsNullOrEmpty(candidate.YearsOfExperience) && maxyeo < Convert.ToDouble(candidate.YearsOfExperience))
                    maxyeo = Convert.ToDouble(candidate.YearsOfExperience);
                if (candidate.Skills != null && maxskillcounts < candidate.Skills.Split(',').Count())
                    maxskillcounts = candidate.Skills.Split(',').Count();
            }
                foreach (var candidate in candidates)
            {
                var yoe = 0.0;
                var skillcount = 0.0;
                if (!string.IsNullOrEmpty(candidate.YearsOfExperience) && maxyeo > 0)
                {
                    yoe = Convert.ToDouble(candidate.YearsOfExperience) / maxyeo * 60;
                }
                if (candidate.Skills != null && maxskillcounts > 0)
                {
                    skillcount = Convert.ToDouble(candidate.Skills.Split(',').Count()) / Convert.ToDouble(maxskillcounts) * 40;
                }
               
                candidate.Rating = Math.Ceiling(yoe + skillcount);
            }
            return Ok(candidates);
        }

        [HttpPost]
        [Route("ScheduleCandidate")]
        public IHttpActionResult ScheduleCandidate(Candidate candidate)
        {
            var result = resumeProcessor.ScheduleCandidate(candidate);
            return this.Ok(result);
        }
    }
}
