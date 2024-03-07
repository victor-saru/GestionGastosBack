using GestionGastosBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionGastosBD.DTOs
{
    public class UserDto
    {
        public Errores error { get; set; }
        public Users? user { get; set; }
    }
}
