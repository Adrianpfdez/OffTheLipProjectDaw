using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OffTheLipProject.Models.ModelOTL
{
    public class HardwareSurferViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public string UrlRedirect { get; set; }
        public string SurferName { get; set; }
    }
}