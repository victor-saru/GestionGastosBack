using GestionGastosBD;
using GestionGastosBD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace GestionGastosBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly GestionGastosBDContext _context;
        public readonly IConfiguration _configuration;

        public ExpensesController(GestionGastosBDContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("InsertExpense")]
        public async Task<ActionResult<string>> InsertExpense()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    var body = await reader.ReadToEndAsync();
                    dynamic jObject = JObject.Parse(body);

                    string name = jObject.name.ToString();
                    decimal cost = jObject.cost == "" ? 0 : jObject.cost;
                    string next_payment = jObject.next_payment == "" ? "" : jObject.next_payment.ToString("dd/MM/yyyy");
                    DateTime? final_payment = jObject.final_payment == "" ? null : DateTime.Parse(jObject.final_payment.ToString("dd/MM/yyyy"));
                    string id_periodicity = jObject.id_periodicity.ToString();
                    int id_user = jObject.id_user == "" ? -1 : jObject.id_user;

                    if (!string.IsNullOrEmpty(name))
                    {
                        if (cost != 0)
                        {
                            if (!string.IsNullOrEmpty(next_payment))
                            {
                                if (!string.IsNullOrEmpty(id_periodicity))
                                {
                                    if (id_user != -1)
                                    {
                                        Expenses expenses = new Expenses
                                        {
                                            name = name,
                                            cost = cost,
                                            next_payment = DateTime.Parse(next_payment),
                                            final_payment = final_payment,
                                            id_periodicity = id_periodicity,
                                            id_user = id_user
                                        };

                                        _context.Add(expenses);
                                        _context.SaveChanges();

                                        return "OK: Expense " + name + " successfully inserted.";
                                    }

                                    else return "ERROR: User is null or empty";
                                }

                                else return "ERROR: Perioficity is null or empty";
                            }

                            else return "ERROR: Next Payment is null or empty";
                        }

                        else return "ERROR: Cost is 0";
                    }

                    else return "ERROR: Name is null or empty";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost("UpdateExpense")]
        public async Task<ActionResult<string>> UpdateExpense()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    var body = await reader.ReadToEndAsync();
                    dynamic jObject = JObject.Parse(body);

                    string id_expense = jObject.id;
                    string name = jObject.name.tostring();
                    decimal cost = jObject.cost == "" ? 0 : jObject.cost;
                    string next_payment = jObject.next_payment == "" ? "" : jObject.next_payment.tostring("dd/mm/yyyy");
                    DateTime? final_payment = jObject.final_payment == "" ? null : DateTime.Parse(jObject.final_payment.tostring("dd/mm/yyyy"));
                    string id_periodicity = jObject.id_periodicity.tostring();
                    int id_user = jObject.id_user == "" ? -1 : jObject.id_user;

                    if (!string.IsNullOrEmpty(name))
                    {
                        if (cost != 0)
                        {
                            if (!string.IsNullOrEmpty(next_payment))
                            {
                                if (!string.IsNullOrEmpty(id_periodicity))
                                {
                                    if (id_user != -1)
                                    {
                                        //var expenseToUpdate = _context.Expenses.SingleOrDefault(x => x.id == expense.id);

                                        //if (expense != null)
                                        //{
                                        //    expense.name = name;
                                        //    expense.cost = cost;
                                        //    expense.next_payment = DateTime.Parse(next_payment);
                                        //    expense.final_payment = final_payment;
                                        //    expense.id_periodicity = id_periodicity;
                                        //    _context.Update(expense);
                                        //    _context.SaveChanges();

                                        //    return "OK: Expense " + name + " successfully updated.";
                                        //}

                                        //else return "ERROR: Expense " + name + " not found.";
                                        return "";
                                    }

                                    else return "ERROR: User is null or empty";
                                }

                                else return "ERROR: Perioficity is null or empty";
                            }

                            else return "ERROR: Next Payment is null or empty";
                        }

                        else return "ERROR: Cost is 0";
                    }

                    else return "ERROR: Name is null or empty";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
