using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OffTheLipProject.Models.ModelOTL
{
    public class SurferViewModel
    {
        public Surfer Surfer { get; set; }
        public List<Documentary> Documentaries { get; set; }
        public List<Hardware> Hardwares { get; set; }
        public List<News> News { get; set; }
    }
}