using System;
using System.IO;
using ResumeParser.Model;
using ResumeParser.Model.Exceptions;
using ResumeParser.ResumeProcessor.Parsers;
using ResumeParser.ResumeProcessor.Helpers;
using System.Collections.Generic;
using ResumeParser.DataAccess;
using System.Data;
using System.Linq;
using System.Data.SqlClient;

namespace ResumeParser.ResumeProcessor
{
    public class ResumeProcessor
    {
        private readonly IOutputFormatter _outputFormatter;
        private readonly IInputReader _inputReaders;
        private ResumeParserData resumeParserData;

        public ResumeProcessor()
        {
            resumeParserData = new ResumeParserData();
        }
        public ResumeProcessor(IOutputFormatter outputFormatter)
        {
            if (outputFormatter == null)
            {
                throw new ArgumentNullException("outputFormatter");
            }
            resumeParserData = new ResumeParserData();
            _outputFormatter = outputFormatter;
            //IInputReaderFactory inputReaderFactory = new InputReaderFactory(new ConfigFileApplicationSettingsAdapter());
            //_inputReaders = inputReaderFactory.LoadInputReaders();
        }

        public string Process(string location)
        {
            try
            {
                var rawInput = _inputReaders.ReadIntoList(location);

                var sectionExtractor = new SectionExtractor();
                var sections = sectionExtractor.ExtractFrom(rawInput);

                IResourceLoader resourceLoader = new CachedResourceLoader(new ResourceLoader());
                var resumeBuilder = new ResumeBuilder(resourceLoader);
                var resume = resumeBuilder.Build(sections);

                var formatted = _outputFormatter.Format(resume);

                return formatted;
            }
            catch (IOException ex)
            {
                throw new ResumeParserException("There's a problem accessing the file, it might still being opened by other application", ex);
            }
        }

        public string Process(IList<string> rawInput,string path,Resume resumeObj)
        {
            if (resumeObj == null)
            {
                var sectionExtractor = new SectionExtractor();
                var sections = sectionExtractor.ExtractFrom(rawInput);

                IResourceLoader resourceLoader = new CachedResourceLoader(new ResourceLoader());
                var resumeBuilder = new ResumeBuilder(resourceLoader);
                var resume = resumeBuilder.Build(sections);

                resume.Skills = resume.Skills.Distinct().ToList();
                //resumeParserData.InsertCandidate(resume, path);
                var formatted = _outputFormatter.Format(resume);
                return formatted;
            }
            else
            {
                return _outputFormatter.Format(resumeObj);
            }
        }
      
        public List<Candidate> GetCandidates()
        {
            List<Candidate> candidates = new List<Candidate>();
            try
            {
                var dataTable = resumeParserData.GetCandidates();
                foreach (DataRow dr in dataTable.Rows)
                {
                    Candidate candidate = new Candidate();
                    candidate.Id = dr.IsNull("Id") ? 0 : int.Parse(dr["Id"].ToString());
                    candidate.FirstName = dr.IsNull("FirstName") ? "" : dr["FirstName"].ToString();
                    candidate.LastName = dr.IsNull("LastName") ? "" : dr["LastName"].ToString();
                    candidate.EmailId = dr.IsNull("EmailId") ? "" : dr["EmailId"].ToString();
                    candidate.Gender = dr.IsNull("Gender") ? "" : dr["Gender"].ToString();
                    candidate.Path = dr.IsNull("Path") ? "" : dr["Path"].ToString();
                    candidate.Designation = dr.IsNull("Designation") ? "" : dr["Designation"].ToString();
                    candidate.YearsOfExperience = dr.IsNull("YearsOfExperience") ? "" : dr["YearsOfExperience"].ToString();
                    candidate.Status = dr.IsNull("Status") ? "" : dr["Status"].ToString();
                    candidate.Skills = dr.IsNull("Skills") ? "" : dr["Skills"].ToString();
                    candidate.ScheduleDateTime = dr.IsNull("ScheduleDateTime") ? new DateTime() : Convert.ToDateTime(dr["ScheduleDateTime"]);
                    candidate.LastUpdatedDate = dr.IsNull("LastUpdatedDate") ? new DateTime() : Convert.ToDateTime(dr["LastUpdatedDate"]);
                    candidate.CreatedDate = dr.IsNull("CreatedDate") ? new DateTime() : Convert.ToDateTime(dr["CreatedDate"]);
                    candidate.Certifications = dr.IsNull("Certifications") ? "" : dr["Certifications"].ToString();
                    candidate.L1Comments = dr.IsNull("L1Comments") ? "" : dr["L1Comments"].ToString();
                    candidate.L2Comments = dr.IsNull("L2Comments") ? "" : dr["L2Comments"].ToString();
                    candidate.L1UserId = dr.IsNull("L1UserId") ? 0 : int.Parse(dr["L1UserId"].ToString());
                    candidate.L2UserId = dr.IsNull("L2UserId") ? 0 : int.Parse(dr["L2UserId"].ToString());
                    candidates.Add(candidate);
                }
                return candidates;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int ScheduleCandidate(Candidate candidate)
        {
            return resumeParserData.ScheduleCandidate(candidate);
        }
    }
   
}
