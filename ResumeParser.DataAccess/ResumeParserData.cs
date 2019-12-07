using ResumeParser.Model;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ResumeParser.DataAccess
{
    public class ResumeParserData
    {
        private DBConnection conn;

        public ResumeParserData()
        {
            conn = new DBConnection();
        }

        public DataTable GetCandidates()
        {
            try
            {
                return conn.executeSelectQuery("usp_GetCandidates");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataTable GetInterviewers()
        {
            try
            {
                return conn.executeSelectQuery("usp_GetInterviewers");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataTable GetFavoriteCandidate(int userId) {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@UserId", userId);
                return conn.executeSelectQuery("usp_GetFavoriteCandidate", sqlParameters);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    
        public int UpdateCandidate(Resume resume)
        public int ScheduleCandidate(Candidate candidate)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[8];
                sqlParameters[0] = new SqlParameter("@Path", candidate.Path);
                sqlParameters[1] = new SqlParameter("@Status", candidate.Status);
                sqlParameters[2] = new SqlParameter("@ScheduleDateTime", candidate.ScheduleDateTime);
                sqlParameters[3] = new SqlParameter("@L1Comments", candidate.L1Comments);
                sqlParameters[4] = new SqlParameter("@L1UserId", candidate.L1UserId);
                sqlParameters[5] = new SqlParameter("@L2Comments", candidate.L2Comments);
                sqlParameters[6] = new SqlParameter("@L2UserId", candidate.L2UserId);
                sqlParameters[7] = new SqlParameter("@LastUpdatedBy", candidate.LastUpdatedBy);

                return conn.executeUpdateQuery("sp_UpdateCandidate", sqlParameters);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int InsertCandidate(Resume resume,string path)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[10];
                sqlParameters[0] = new SqlParameter("@FirstName", resume.Firstname);
                sqlParameters[1] = new SqlParameter("@Gender", resume.Gender);
                sqlParameters[2] = new SqlParameter("@EmailId", resume.Emailaddress);
                sqlParameters[3] = new SqlParameter("@YearsOfExperience", string.IsNullOrWhiteSpace(resume.yoe) ? "" : resume.yoe);
                sqlParameters[4] = new SqlParameter("@Designation", resume.Designation);
                sqlParameters[5] = new SqlParameter("@Skills", resume.Skills == null ? "" :  string.Join(",", resume.Skills));
                sqlParameters[6] = new SqlParameter("@Certifications", resume.Certifications == null ? "" : string.Join(",", resume.Certifications));
                sqlParameters[7] = new SqlParameter("@Path", string.IsNullOrWhiteSpace(path) ? "" : path);
                sqlParameters[8] = new SqlParameter("@UserId", 1);
                sqlParameters[9] = new SqlParameter("@LastName", resume.Lastname);

                return conn.executeUpdateQuery("usp_InsertCandidate", sqlParameters);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool InsertUser(User user)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[6];
                sqlParameters[0] = new SqlParameter("@name", string.IsNullOrWhiteSpace(user.Name) ? "" : user.Name);
                sqlParameters[1] = new SqlParameter("@Password", string.IsNullOrWhiteSpace(user.Password) ? "" : user.Password);
                sqlParameters[2] = new SqlParameter("@Role", string.IsNullOrWhiteSpace(user.Role) ? "" : user.Role);
                sqlParameters[3] = new SqlParameter("@EmailId", string.IsNullOrWhiteSpace(user.EmailId) ? "" : user.EmailId);
                sqlParameters[4] = new SqlParameter("@FilterCriteria", string.IsNullOrWhiteSpace(user.FilterCriteria) ? "" : user.FilterCriteria);
                sqlParameters[5] = new SqlParameter("@DefaultScreen", string.IsNullOrWhiteSpace(user.DefaultScreen) ? "" : user.DefaultScreen);
                conn.executeInsertQuery("usp_InsertUser", sqlParameters);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool SaveUserSaveUserCriteria(User user)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];
                sqlParameters[0] = new SqlParameter("@FilterCriteria", string.IsNullOrWhiteSpace(user.FilterCriteria) ? "" : user.FilterCriteria);
                sqlParameters[1] = new SqlParameter("@DefaultScreen", string.IsNullOrWhiteSpace(user.DefaultScreen) ? "" : user.DefaultScreen);
                sqlParameters[2] = new SqlParameter("@UserId", user.Id);
                conn.executeInsertQuery("usp_SaveUserCriteria", sqlParameters);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool SaveIsFavoriteCandidate(string userId, string emailId, bool isFavorite)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];
                sqlParameters[0] = new SqlParameter("@IsFavourite", isFavorite);
                sqlParameters[1] = new SqlParameter("@UserId", userId);
                sqlParameters[2] = new SqlParameter("@EmailId", emailId);
                conn.executeInsertQuery("usp_SaveIsFavoriteCandidate", sqlParameters);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataTable GetUser(string userName,string password)
        {           
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@name", userName);
                sqlParameters[1] = new SqlParameter("@Password", password);
                return conn.executeSelectQuery("usp_GetUser", sqlParameters);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
