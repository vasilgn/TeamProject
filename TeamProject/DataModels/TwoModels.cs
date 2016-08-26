using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TeamProject.DataModels
{
    public class TwoModels
    {
        public PostVideo PostVideo { get; set; }
        public Post Post{ get; set; }

    }
}