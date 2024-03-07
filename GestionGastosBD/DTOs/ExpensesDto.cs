using GestionGastosBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionGastosBD.DTOs
{
    public class ExpensesDto
    {
        public List<Expenses>? expenses { get; set; }
        public Errores error { get; set; }
    }
}
