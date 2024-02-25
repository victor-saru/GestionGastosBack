using GestionGastosBD;
using GestionGastosBD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Text;

namespace GestionGastosBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : Controller
    {
        private readonly GestionGastosBDContext _context;
        public readonly IConfiguration _configuration;

        public ParticipantsController(GestionGastosBDContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("InsertParticipant")]
        public async Task<ActionResult<string>> InsertParticipant()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    var body = await reader.ReadToEndAsync();
                    dynamic jObject = JObject.Parse(body);

                    string name = jObject.name.ToString();
                    decimal net_monthly_salary = jObject.net_monthly_salary;
                    int paymanets = jObject.paymanets;
                    int id_user = jObject.id_user;
                    

                    if (!string.IsNullOrEmpty(name))
                    {
                        if (net_monthly_salary != 0)
                        {
                            if (paymanets != 0)
                            {
                                if(_context.Participants.SingleOrDefault(x => x.name == name && x.id_user == id_user) == null)
                                {

                                    Participants participant = new Participants
                                    {
                                        name = name,
                                        net_monthly_salary = net_monthly_salary,
                                        paymanets = paymanets,
                                        id_user = id_user
                                    };

                                    _context.Add(participant);
                                    _context.SaveChanges();

                                    return "OK: Participant " + name + " successfully inserted.";
                                }

                                else
                                    return "ERROR: Participant name is duplicated";
                            }

                            else return "ERROR: Payments is 0";
                        }

                        else return "ERROR: Net monthly salari is 0";
                    }

                    else return "ERROR: Name is null or empty";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost("UpdateParticipant")]
        public async Task<ActionResult<string>> UpdateParticipant()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    var body = await reader.ReadToEndAsync();
                    dynamic jObject = JObject.Parse(body);

                    string name = jObject.name.ToString();
                    decimal net_monthly_salary = jObject.net_monthly_salary;
                    int paymanets = jObject.paymanets;
                    int id_user = jObject.id_user;


                    if (!string.IsNullOrEmpty(name))
                    {
                        if (net_monthly_salary != 0)
                        {
                            if (paymanets != 0)
                            {
                                var participant = _context.Participants.SingleOrDefault(x => x.name == name && x.id_user == id_user);

                                if (participant != null)
                                {

                                    participant.name = name;
                                    participant.net_monthly_salary = net_monthly_salary;
                                    participant.paymanets = paymanets;
                                    
                                    _context.Update(participant);
                                    _context.SaveChanges();

                                    return "OK: Participant " + name + " successfully inserted.";
                                }

                                else
                                    return "ERROR: Participant not found";
                            }

                            else return "ERROR: Payments is 0";
                        }

                        else return "ERROR: Net monthly salari is 0";
                    }

                    else return "ERROR: Name is null or empty";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost("DeleteParticipant")]
        public async Task<ActionResult<string>> DeleteExpense()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    var body = await reader.ReadToEndAsync();
                    dynamic jObject = JObject.Parse(body);

                    string name = jObject.name.ToString();
                    int id_user = jObject.id_user;

                    var participantDelete = _context.Participants.SingleOrDefault(x => x.name == name && x.id_user == id_user);

                    if (participantDelete != null)
                    {
                        _context.Remove(participantDelete);
                        _context.SaveChanges();

                        return "OK: Participant " + participantDelete.name + " successfully deleted.";
                    }

                    else
                        return "ERROR: Participant not found";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost("GetParticipants")]
        public async Task<ActionResult<IEnumerable<Participants>>> GetParticipants()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var body = await reader.ReadToEndAsync();
                dynamic jObject = JObject.Parse(body);

                string name = jObject.name.ToString();
                int id_user = jObject.id_user;

                return await _context.Participants.Where(x => x.name == name && x.id_user == id_user)
                     .ToListAsync();
            }
        }
    }
}
