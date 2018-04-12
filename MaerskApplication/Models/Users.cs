using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaerskApplication.Models
{

    public class Users
    {
        public static bool Authenticated = false;
        public static Users CurrentUser;

        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Role { get; set; }
    }
}