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

        public List<User> GetInterviewers()
        {
            var listOfInteviewers = new List<User>();
            try
            {
                var dataTable = userData.GetInterviewers();
                foreach (DataRow dr in dataTable.Rows)
                {
                    var interviewer = new User();
                    interviewer.Id = dr.IsNull("Id") ? 0 : int.Parse(dr["Id"].ToString());
                    interviewer.Name = dr.IsNull("Name") ? "" : dr["Name"].ToString();
                    interviewer.Role = dr.IsNull("Role") ? "" : dr["Role"].ToString();
                    interviewer.EmailId = dr.IsNull("EmailId") ? "" : dr["EmailId"].ToString();
                    listOfInteviewers.Add(interviewer);
                }
                return listOfInteviewers;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<IdTextDTO> GetFavoriteCandidate(int userId)
        {
            var listOfCandidates = new List<IdTextDTO>();
            try
            {
                var dataTable = userData.GetFavoriteCandidate(userId);
                foreach (DataRow dr in dataTable.Rows)
                {
                    var candidate = new IdTextDTO();
                    candidate.Id = dr.IsNull("CandidateId") ? 0 : int.Parse(dr["CandidateId"].ToString());
                    candidate.Text = dr.IsNull("IsFavourite") ? "" : dr["IsFavourite"].ToString();
                    listOfCandidates.Add(candidate);
                }
                return listOfCandidates;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool SaveUserCriteria(User user)
        {
            return userData.SaveUserSaveUserCriteria(user);
        }

        public bool SaveIsFavoriteCandidate(string userId, string emailId, bool isFavorite)
        {
            return userData.SaveIsFavoriteCandidate(userId, emailId, isFavorite);
        }
    }
}
