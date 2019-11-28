using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OffTheLipProject.Models.ModelOTL
{
    public class Surfer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Natinality { get; set; }
        public bool Competitor { get; set; }
        public StanceEnum Stance { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Hardware> Hardwares { get; set; }
        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<Documentary> Documentaries { get; set; }
    }
}