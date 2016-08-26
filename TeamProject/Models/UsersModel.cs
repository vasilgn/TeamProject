using System;

namespace TeamProject.Models
{
    public class UsersModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Country { get; set; }
        public Role Role { get; set; }
    }

    public enum Role
    {
        Administrator,
        Member,
        Guest
    }
}

