using System;

namespace TeamProject.Models
{
    public class UsersModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Role Role { get; set; }
    }

    public enum Role
    {
        Administrator,
        Member,
        Guest
    }
}

