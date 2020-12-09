using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dumptruck.Models
{
    public class User
    {
        public string userName { get; set; }
        public string userToken { get; set; }

        public string refreshToken { get; set; }
        
        //public int userId {get; set; }

    }
}
