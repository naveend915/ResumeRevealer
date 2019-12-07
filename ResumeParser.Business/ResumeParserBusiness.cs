
using ResumeParser.DataAccess;
using ResumeParser.Model;

namespace ResumeParser.Business
{
    public class ResumeParserBusiness
    {
        private ResumeParserData resumeParserData;

        public ResumeParserBusiness()
        {
            resumeParserData = new ResumeParserData();
        }

        public int InsertUser(User user)
        {
            return resumeParserData.InsertUser(user);
        }
    }
}
