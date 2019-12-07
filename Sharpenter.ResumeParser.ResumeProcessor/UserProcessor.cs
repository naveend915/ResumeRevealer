using ResumeParser.DataAccess;
using ResumeParser.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ResumeParser.ResumeProcessor
{
    public class UserProcessor
    {
        private ResumeParserData userData;

        public UserProcessor()
        {
            userData = new ResumeParserData();
        }

        public bool InsertUser(User user)
        {
            return userData.InsertUser(user);
        }

        public User GetValidUser(string userName, string password)
        {
            User user = null;
            try
            {
                var dataTable = userData.GetUser(userName, password);
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

        public List<IdTextDTO> GetInterviewers()
        {
            var listOfInteviewers = new List<IdTextDTO>();
            try
            {
                var dataTable = userData.GetInterviewers();
                foreach (DataRow dr in dataTable.Rows)
                {
                    var interviewer = new IdTextDTO();
                    interviewer.Id = dr.IsNull("Id") ? 0 : int.Parse(dr["Id"].ToString());
                    interviewer.Name = dr.IsNull("Name") ? "" : dr["Name"].ToString();
                    listOfInteviewers.Add(interviewer);
                }
                return listOfInteviewers;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool SaveUserSaveUserCriteria(User user)
        {
            return userData.SaveUserSaveUserCriteria(user);
        }
    }
}
