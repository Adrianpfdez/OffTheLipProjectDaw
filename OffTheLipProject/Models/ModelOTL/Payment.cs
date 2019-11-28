using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OffTheLipProject.Models.ModelOTL
{
    public class Payment
    {
        [ForeignKey("Cart")]
        public int Id { get; set; }
        public float Amount { get; set; }
        public string Currency { get; set; }

        public virtual ICollection<PaymentMethod> PaymentMethods { get; set; }

        public virtual Cart Cart { get; set; }
    }
}