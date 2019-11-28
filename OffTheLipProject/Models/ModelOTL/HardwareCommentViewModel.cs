using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OffTheLipProject.Models.ModelOTL
{
    public class HardwareCommentViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public string SurferName { get; set; }
        public int DocId { get; set; }
        public int NumComments { get; set; }
    }
}