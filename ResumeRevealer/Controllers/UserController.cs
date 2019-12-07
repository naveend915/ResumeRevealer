using ResumeParser.Model;
using ResumeParser.ResumeProcessor;
using System.Web.Http;

namespace ResumeRevealer.Controllers
{
    public class UserController : ApiController
    {
        private UserProcessor userProcessor;

        public UserController()
        {
            userProcessor = new UserProcessor();
        }
        // POST
        [HttpPost]
        [Route("Register")]
        public bool Register(User user)
        {
            return userProcessor.InsertUser(user);
        }
        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login(User user)
        {
            User userObj = null;
            userObj = userProcessor.GetValidUser(user.Name,user.Password);
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
