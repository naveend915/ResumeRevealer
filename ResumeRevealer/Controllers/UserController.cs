using ResumeParser.Business;
using ResumeParser.Model;
using System.Web.Mvc;

namespace ResumeRevealer.Controllers
{
    public class UserController : Controller
    {
        private ResumeParserBusiness resumeParserBusiness;
        public UserController()
        {
            resumeParserBusiness = new ResumeParserBusiness();
        }
        // POST
        [HttpPost]
        public int Register(User user)
        {
            var result = resumeParserBusiness.InsertUser(user);
            return result;
        }

    }
}
