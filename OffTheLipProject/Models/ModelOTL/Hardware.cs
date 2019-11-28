using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OffTheLipProject.Models.ModelOTL
{
    public class Hardware
    {
        public Hardware()
        {
            Surfers = new HashSet<Surfer>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Surfer> Surfers { get; set; }
        public virtual ICollection<CommentHardware> CommentHardwares { get; set; }
    }
}