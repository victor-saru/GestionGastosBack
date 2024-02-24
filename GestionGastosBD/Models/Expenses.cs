using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionGastosBD.Models
{
    public class Expenses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public decimal cost { get; set; }

        [Required] 
        public DateTime next_payment { get; set; }

        public DateTime? final_payment { get; set; }

        [Required]
        public string id_periodicity { get; set; }

        [ForeignKey("id_periodicity")]
        public virtual PaymentMethods PaymentMethods { get; set; }

        [Required]
        public int id_user { get; set; }

        [ForeignKey("id_user")]
        public virtual Users Users { get; set; }
    }

    
}
