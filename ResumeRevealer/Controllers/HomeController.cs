using System.Web.Http;

namespace ResumeRevealer.Controllers
{
    public class HomeController : ApiController
    {
        //        [BasicAuthentication(ServiceName = "RestAPI")]
        [HttpGet]
        [Route("test")]
        public IHttpActionResult HasAccesstoModules()
        {
            var result = "Test message !!";
            
            return this.Ok(result);
        }
    }
}
