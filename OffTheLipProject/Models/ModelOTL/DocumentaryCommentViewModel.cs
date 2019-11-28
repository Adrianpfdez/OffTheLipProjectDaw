using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OffTheLipProject.Models.ModelOTL
{
    public class DocumentaryCommentViewModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public string SurferName { get; set; }
        public int DocId { get; set; }
        public int NumComments { get; set; }
    }
}