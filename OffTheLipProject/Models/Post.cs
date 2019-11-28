using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OffTheLipProject.Models
{
    public class Post
    {
        public Post() { }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Url { get; set; }
        public string UrlRedirect { get; set; }
    }
}