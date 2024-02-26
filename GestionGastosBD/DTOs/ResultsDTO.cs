using GestionGastosBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionGastosBD.DTOs
{
    public class ResultsDTO
    {
        public decimal value { get; set; }
        
        public Participants participant { get; set; }
    }
}
