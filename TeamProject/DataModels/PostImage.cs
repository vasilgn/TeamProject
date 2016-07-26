using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamProject.DataModels
{
    public class PostImage : Image
    {
        public virtual Post Post { get; set; }
    }
}