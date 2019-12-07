using ResumeParser.DataAccess;
using ResumeParser.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace ResumeParser.Business
{
    public class ResumeParserBusiness
    {
        private ResumeProcessor resumeParserData;

        public ResumeParserBusiness()
        {
            resumeParserData = new ResumeParserData();
        }

        public bool InsertUser(User user)
        {
            return resumeParserData.InsertUser(user);
        }
        
        public User GetValidUser(string userName,string password)
        {
            User user = null;
            try
            {
                var dataTable = resumeParserData.GetUser(userName, password);
                foreach (DataRow dr in dataTable.Rows)
                {
                    user = new User();
                    user.Id = dr.IsNull("Id") ? 0 : int.Parse(dr["Id"].ToString());
                    user.Name = dr.IsNull("Name") ? "" : dr["Name"].ToString();
                    user.Role = dr.IsNull("Role") ? "" : dr["Role"].ToString();
                    user.EmailId = dr.IsNull("EmailId") ? "" : dr["EmailId"].ToString();
                    user.FilterCriteria = dr.IsNull("FilterCriteria") ? "" : dr["FilterCriteria"].ToString();
                    user.DefaultScreen = dr.IsNull("DefaultScreen") ? "" : dr["DefaultScreen"].ToString();
                }
                return user;
            }
            catch (Exception e)
            {
                throw e;
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
                    candidate.Name = dr.IsNull("Name") ? "" : dr["Name"].ToString();
                    candidate.EmailId = dr.IsNull("EmailId") ? "" : dr["EmailId"].ToString();
                    candidate.Gender = dr.IsNull("Gender") ? "" : dr["Gender"].ToString();
                    candidate.Designation = dr.IsNull("Designation") ? "" : dr["Designation"].ToString();
                    candidate.YearsOfExperience = dr.IsNull("YearsOfExperience") ? 0 : int.Parse(dr["YearsOfExperience"].ToString());
                    candidate.Status = dr.IsNull("Status") ? "" : dr["Status"].ToString();
                    candidate.Skills = dr.IsNull("Skills") ? "" : dr["Skills"].ToString();
                    candidate.ScheduleDateTime = dr.IsNull("ScheduleDateTime") ? new DateTime() : Convert.ToDateTime(dr["ScheduleDateTime"]);
                    candidate.LastUpdatedDate = dr.IsNull("LastUpdatedDate") ? new DateTime() : Convert.ToDateTime(dr["LastUpdatedDate"]);
                    candidate.CreatedDate= dr.IsNull("CreatedDate") ? new DateTime() : Convert.ToDateTime(dr["CreatedDate"]);
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
    }
}
