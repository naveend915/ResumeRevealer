using Newtonsoft.Json;
using ResumeParser.Business;
using ResumeParser.Model;
using System.Web.Http;

namespace ResumeRevealer.Controllers
{
    public class UserController : ApiController
    {
        private ResumeParserBusiness resumeParserBusiness;
        public UserController()
        {
            resumeParserBusiness = new ResumeParserBusiness();
        }
        // POST
        [HttpPost]
        [Route("Register")]
        public bool Register(User user)
        {
            return resumeParserBusiness.InsertUser(user);
        }
        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login(User user)
        {
            User userObj = null;
            userObj = resumeParserBusiness.GetValidUser(user.Name,user.Password);
            return Ok(userObj);
        }

        [HttpGet]
        [Route("GetInterviewers")]
        public IHttpActionResult GetInterviewers()
        {
            var result = resumeParserBusiness.GetInterviewers();
            return Ok(result);
        }

        
        [HttpPost]
        [Route("SaveUserSaveUserCriteria")]
        public IHttpActionResult SaveUserSaveUserCriteria(User user)
        {
            var result = resumeParserBusiness.SaveUserSaveUserCriteria(user);
            return Ok(result);
        }
    }
}
