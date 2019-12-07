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


        public int UpdateCandidate(Resume resume)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@name", resume.FirstName);
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
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@name", resume.FirstName);
                return conn.executeUpdateQuery("sp_InsertCandidate", sqlParameters);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int InsertUser(User user)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[6];
                sqlParameters[0] = new SqlParameter("@name", user.Name);
                sqlParameters[1] = new SqlParameter("@Password", user.Password);
                sqlParameters[2] = new SqlParameter("@Role", user.Role);
                sqlParameters[3] = new SqlParameter("@EmailId", user.EmailId);
                sqlParameters[4] = new SqlParameter("@FilterCriteria", user.FilterCriteria);
                sqlParameters[5] = new SqlParameter("@DefaultScreen", user.DefaultScreen);
                return conn.executeUpdateQuery("sp_InsertUser", sqlParameters);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
