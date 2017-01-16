using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Insurer.Web.Service.Controllers
{
    [EnableCors(origins: "https://localhost:3000", headers: "*", methods: "*")]
    [Authorize]
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        [Authorize(Roles = "Administrator")]
        [Route("admin")]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            return this.Request.CreateResponse(
                        HttpStatusCode.OK);
        }
    }
}
