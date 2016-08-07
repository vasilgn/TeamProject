namespace TeamProject.Models
{
    public class UsersModel
    {	
        public int Id { get; set; }
		public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}

