using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamProject.Migrations
{
    public class DbUserConfiguration
    {
        public string User { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string UserRole { get; set; }
    }
}