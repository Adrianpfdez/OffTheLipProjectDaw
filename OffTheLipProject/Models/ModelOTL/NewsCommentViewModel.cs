using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OffTheLipProject.Models.ModelOTL
{
    public class NewsCommentViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Image { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public int DocId { get; set; }
        public string SurferName { get; set; }
        public int NumComments { get; set; }
    }
}