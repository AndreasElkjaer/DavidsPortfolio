using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VPport.Models
{
    public class LoginViewModel
    {
            public User User { get; set; }

            public string MyUserName { get; set; }
            public string MyPassword { get; set; }
            public string MyCryptData { get; set; }
            public string MyDeCryptData { get; set; }
            public string Errmsg { get; set; }
            public string MySubStr { get; set; }

    }
}