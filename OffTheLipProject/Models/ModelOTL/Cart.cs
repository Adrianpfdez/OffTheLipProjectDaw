using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OffTheLipProject.Models.ModelOTL
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<Hardware> Hardwares { get; set; }

        public virtual Payment Payment { get; set; }
    }
}