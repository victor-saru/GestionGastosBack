using GestionGastosBD;
using GestionGastosBD.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text;

namespace GestionGastosBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : Controller
    {
        private readonly GestionGastosBDContext _context;
        public readonly IConfiguration _configuration;

        public LogInController(GestionGastosBDContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("LogIn")]
        public async Task<ActionResult<string>> LogIn()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    var body = await reader.ReadToEndAsync();
                    dynamic jObject = JObject.Parse(body);

                    string email = jObject.email.ToString().ToLower();
                    string password = jObject.password.ToString();

                    if (!string.IsNullOrEmpty(email))
                    {
                        if (!string.IsNullOrEmpty(password))
                        {
                            var user = _context.Users.SingleOrDefault(x => x.email.ToLower() == email);

                            if (user != null)
                            {
                                if (password == user.password)
                                    return "OK: Correct login";
                                else
                                    return "ERROR: Incorrect password";
                            }

                            else
                                return "ERROR: User " + email + " does not exists";
                        }

                        else
                            return "ERROR: Password is null or empty";
                    }

                    else
                        return "ERROR: User is null or empty";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }
    }
}
