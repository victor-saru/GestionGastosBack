using GestionGastosBD;
using GestionGastosBD.DTOs;
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
        public async Task<ActionResult<UserDto>> LogIn()
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
                                {
                                     return new UserDto { error = new Errores { Code = "OK", Message = "Correct login" }, user = user };
                                     
                                }
                                    
                                else
                                    return new UserDto { error = new Errores { Code = "ERROR", Message = "Incorrect password"}, user = null};
                                }

                            else
                                return new UserDto { error = new Errores { Code = "ERROR", Message = "User " + email + " does not exists" }, user = null };
                        }

                        else
                            return new UserDto { error = new Errores { Code = "ERROR", Message = "Password is null or empty" }, user = null };
                    }

                    else
                        return new UserDto { error = new Errores { Code = "ERROR", Message = "User is null or empty" }, user = null };
                }
            }
            catch (Exception ex)
            {
                return new UserDto { error = new Errores { Code = "ERROR", Message = ex.Message }, user = null };
            }
            
        }
    }
}
