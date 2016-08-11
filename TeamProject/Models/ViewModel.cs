using System.Collections.Generic;
using TeamProject.Models;

namespace TeamProject.Models
{
public class ViewModel
    {
        public IEnumerable <TeamProject.Models.IndexViewModel> IndexViewModel { get; set; }
        public IEnumerable <TeamProject.Models.UsersModel> Users { get; set; }
        public IEnumerable <TeamProject.Models.PostViewModel> Posts { get; set; }
    } 
}