using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dumptruck.Models
{
    public class UserDTO
    {
        public string userToken { get; set; }

        public string refreshToken { get; set; }

        public string userName { get; set; }
    }
}
