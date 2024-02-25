using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionGastosBD.Models
{
    public class Participants
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public decimal net_monthly_salary { get; set; }

        [Required]
        public int paymanets { get; set; }

        [Required]
        public int id_user { get; set; }

        [ForeignKey("id_user")]
        public virtual Users Users { get; set; }
    }
}
