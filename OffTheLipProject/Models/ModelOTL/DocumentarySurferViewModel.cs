using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OffTheLipProject.Models.ModelOTL
{
    public class DocumentarySurferViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Url { get; set; }
        public string UrlRedirect { get; set; }
        public int Like { get; set; }
        public string SurferName { get; set; }
    }
}