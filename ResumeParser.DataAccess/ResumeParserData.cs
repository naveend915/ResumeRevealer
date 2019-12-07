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

        public int UpdateCandidate(Resume resume)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@name", resume.Firstname);
                return conn.executeUpdateQuery("sp_UpdateCandidate", sqlParameters);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int InsertCandidate(Resume resume)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[4];
                sqlParameters[0] = new SqlParameter("@Name", resume.Firstname);
                sqlParameters[1] = new SqlParameter("@Gender", resume.Gender);
                sqlParameters[2] = new SqlParameter("@EmailId", resume.Emailaddress);
                sqlParameters[3] = new SqlParameter("@YearsOfExperience", resume.yoe);
                sqlParameters[4] = new SqlParameter("@Designation", resume.Designation);
                sqlParameters[5] = new SqlParameter("@Skills", string.Join(",", resume.Skills));
                sqlParameters[6] = new SqlParameter("@Certifications", string.Join(",", resume.Certifications));
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
                sqlParameters[3] = new SqlParameter("@UserId", user.Id);
                conn.executeInsertQuery("usp_SaveUserCriteria", sqlParameters);
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
