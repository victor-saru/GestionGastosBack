using GestionGastosBD;
using GestionGastosBD.DTOs;
using GestionGastosBD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Text;

namespace GestionGastosBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodsController : ControllerBase
    {
        private readonly GestionGastosBDContext _context;
        public readonly IConfiguration _configuration;

        public PaymentMethodsController(GestionGastosBDContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("GetPaymentMethods")]
        public async Task<ActionResult<PaymentmethodDTO>> GetPaymentMethods()
        {
            var paymentMethodsList = _context.PaymentMethods.ToList();
            if (paymentMethodsList.Any()) 
                return new PaymentmethodDTO { PaymentMethodsList = paymentMethodsList, Errores = new Errores { Code = "OK", Message = "All payment methods" }}; 
            
            else
               return new PaymentmethodDTO { PaymentMethodsList = null, Errores = new Errores { Code = "ERROR", Message = "There aren't payment methods" }};
        }
    }
}
