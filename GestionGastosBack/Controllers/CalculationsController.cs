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
        public async Task<ActionResult<CalculationsResultsDTO>> CalculateExpenses()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    var body = await reader.ReadToEndAsync();
                    dynamic jObject = JObject.Parse(body);

                    int id_user = jObject.id_user;

                    var expensesList = _context.Expenses.Where(x => x.id_user == id_user).ToList();
                    var participantsList = _context.Participants.Where(x => x.id_user == id_user).ToList();

                    CalculationsResultsDTO calculationsResults = new CalculationsResultsDTO();

                    GlobalFunctions globalFunctions = new GlobalFunctions(_context);

                    var totalAnnualExpenses = globalFunctions.CalculateTotalAnnualExpenses(expensesList);
                    calculationsResults.totalAnnualExpenses = totalAnnualExpenses;
                    calculationsResults.totalMonthlyExpenses = totalAnnualExpenses / 12;
                    calculationsResults.annualIncome = globalFunctions.CalculateTotalAnnualIncome(participantsList);
                    calculationsResults.monthlyIncome = globalFunctions.CalculateTotalMonthlyIncome(participantsList);
                    calculationsResults.monthlyIncomeSamePayments = calculationsResults.annualIncome / 12;



                    calculationsResults.participantsAnnualIncome = globalFunctions.CalculateParticipantsAnnualIncome(participantsList);


                    calculationsResults.participantsMonthlyIncome = globalFunctions.CalculateParticipantsMonthlyIncome(participantsList);
                    calculationsResults.annualCelarMoney = calculationsResults.annualIncome - calculationsResults.totalAnnualExpenses;
                    calculationsResults.monthlyCelarMoney = calculationsResults.monthlyIncome - calculationsResults.totalMonthlyExpenses;

                    var equalAnnualParticipationAndLeftover = globalFunctions.CalculateEqualAnnualParticipation(calculationsResults.totalAnnualExpenses, participantsList);
                    calculationsResults.annualEqualParticipation = equalAnnualParticipationAndLeftover.Item1;
                    calculationsResults.annualEqualParticipationLeftover = equalAnnualParticipationAndLeftover.Item2;

                    var equalMonthlyParticipationAndLeftover = globalFunctions.CalculateEqualMonthlyParticipation(calculationsResults.totalAnnualExpenses, participantsList);
                    calculationsResults.monthlyEqualParticipation = equalMonthlyParticipationAndLeftover.Item1;
                    calculationsResults.monthlyEqualParticipationLeftover = equalMonthlyParticipationAndLeftover.Item2;

                    var percentageAnualParticipationAndLeftover = globalFunctions.CalculatePercentageAnnualParticipation(calculationsResults.totalAnnualExpenses, participantsList);
                    calculationsResults.annualPercentageParticipation = percentageAnualParticipationAndLeftover.Item1;
                    calculationsResults.annualPercentageParticipationLeftover = percentageAnualParticipationAndLeftover.Item2;

                    var percentageMonthlyParticipationAndLeftover = globalFunctions.CalculatePercentageMonthlyParticipation(calculationsResults.totalAnnualExpenses, participantsList);
                    calculationsResults.monthlyPercentageParticipation = percentageMonthlyParticipationAndLeftover.Item1;
                    calculationsResults.monthlyPercentageParticipationLeftover = percentageMonthlyParticipationAndLeftover.Item2;



                    return calculationsResults;
                }
            }
            catch (Exception ex)
            {
                return new CalculationsResultsDTO();
            }
        }
    }
}
