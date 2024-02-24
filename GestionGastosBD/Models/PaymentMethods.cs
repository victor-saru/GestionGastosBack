using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GestionGastosBD.Models
{
    [Index(nameof(periodicity), IsUnique = true)]
    public class PaymentMethods
    {
        [Key]
        public string name { get; set; }

        [Required]
        public int periodicity { get; set; }

        
        /*
         * Anualmente 12
         * Mensualmente 1
         * Bimestralmente 2
         * Trimestralmente 3
         * Semestralmente 6
         */
    }
}
