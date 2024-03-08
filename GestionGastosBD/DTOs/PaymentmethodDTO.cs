using GestionGastosBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionGastosBD.DTOs
{
    public class PaymentmethodDTO
    {
        public List<PaymentMethods>? PaymentMethodsList{ get; set; }
        public Errores Errores { get; set; }
    }
}
