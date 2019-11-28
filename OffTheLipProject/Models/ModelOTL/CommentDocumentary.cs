using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OffTheLipProject.Models.ModelOTL
{
    public class CommentDocumentary
    {
        [Key]
        public int Id { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }

        public virtual Documentary Documentary { get; set; }
    }
}