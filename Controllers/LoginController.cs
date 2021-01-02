using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Web.Http.Cors;
using System.IdentityModel.Tokens.Jwt;
using dumptruck.Models;
using System.Security.Cryptography;


namespace dumptruck.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/[controller]")]
    [ApiController]
    
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public ActionResult<UserDTO> Login(LoginDTO login) {
            if (login.username == "Jimmy")
            {
                string salt = "aabbcc";
                string inHash = getHash(login.password + salt);
                if (inHash == "5e5d25b59e703eddbfc53e54b8a2a680892d654222239b9fe7fae87ddea4638a")
                    Console.WriteLine("Trying to login");
                string accessToken = "Placeholder token";
                string refreshToken = "Notpresent";
                UserDTO ud = new UserDTO();
                ud.userToken = accessToken;
                ud.refreshToken = refreshToken;
                ud.userName = login.username;
                return ud;


            }

            else return NotFound();
           
        }

        public static string getHash(string input) {
            using (MD5 encryptor = MD5.Create()) {
                byte[] bytes = encryptor.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder newString = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++) {
                    newString.Append(bytes[i].ToString("x2"));
                }
                return newString.ToString();
            }
        }
    }
}
