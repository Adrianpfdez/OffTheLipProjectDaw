using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OffTheLipProject.Models.ModelOTL
{
    public class Comentario
    {
        [Key]
        public int Id { get; set; }
        public string Autor { get; set; }
        public string Texto { get; set; }
    }
}