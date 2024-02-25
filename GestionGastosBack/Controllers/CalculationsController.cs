using GestionGastosBD;
using GestionGastosBD.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text;

namespace GestionGastosBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculationsController : Controller
    {
        private readonly GestionGastosBDContext _context;
        public readonly IConfiguration _configuration;

        public CalculationsController(GestionGastosBDContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("CalculateExpenses")]
        public async Task<ActionResult<string>> CalculateExpenses()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    var body = await reader.ReadToEndAsync();
                    dynamic jObject = JObject.Parse(body);

                    int id_user = jObject.id_user;

                    var expensesList = _context.Expenses.Where(x => x.id_user == id_user).ToList();
                    var participantsList = _context.Expenses.Where(x => x.id_user == id_user).ToList();

                    CalculationsResultsDTO calculationsResults = new CalculationsResultsDTO();

                    GlobalFunctions globalFunctions = new GlobalFunctions(_context);

                    var totalAnnualExpenses = globalFunctions.CalculateTotalExpenses(expensesList);
                    calculationsResults.totalAnnualExpenses = totalAnnualExpenses;
                    calculationsResults.totalMonthlyExpenses = totalAnnualExpenses / 12;




                        return "";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
