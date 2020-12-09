using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Web.Http.Cors;
using dumptruck.Models;

namespace dumptruck.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/[controller]")]
    [ApiController]
    
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public String Login(User u){
            Console.WriteLine("Trying to login");
            string accessToken = "DummyToken";
            return accessToken;
        }
    }
}
