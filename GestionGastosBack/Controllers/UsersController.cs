using GestionGastosBD;
using GestionGastosBD.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text;

namespace GestionGastosBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly GestionGastosBDContext _context;
        public readonly IConfiguration _configuration;

        public UsersController(GestionGastosBDContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("InsertUser")]
        public async Task<ActionResult<string>> InsertUser()
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
                            if (_context.Users.SingleOrDefault(x => x.email.ToLower() == email) == null)
                            {
                                Users user = new Users
                                {
                                    email = jObject.email,
                                    password = jObject.password,
                                };

                                _context.Add(user);
                                _context.SaveChanges();

                                return "OK: User " + email + " successfully inserted";
                            }

                            else
                                return "ERROR: User " + email + " already exists";
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

        [HttpPost("DeleteUser")]
        public async Task<ActionResult<string>> DeleteUser()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    var body = await reader.ReadToEndAsync();
                    dynamic jObject = JObject.Parse(body);

                    string email = jObject.email.ToString().ToLower();

                    if (!string.IsNullOrEmpty(email))
                    {
                        var user = _context.Users.SingleOrDefault(x => x.email.ToLower() == email);

                        if (user != null)
                        {
                            _context.Remove(user);
                            _context.SaveChanges();

                            return "OK: User " + email + " successfully deleted";
                        }

                        else
                            return "ERROR: User " + email + " does not exists";
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


        //Change password
        [HttpPost("UpdateUser")]
        public async Task<ActionResult<string>> UpdateUser()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    var body = await reader.ReadToEndAsync();
                    dynamic jObject = JObject.Parse(body);

                    string email = jObject.email.ToString().ToLower();
                    string oldPassword = jObject.oldPassword.ToString();
                    string newPassword = jObject.newPassword.ToString();

                    if (!string.IsNullOrEmpty(email))
                    {
                        if (!string.IsNullOrEmpty(oldPassword))
                        {
                            if (!string.IsNullOrEmpty(newPassword))
                            {
                                var user = _context.Users.SingleOrDefault(x => x.email.ToLower() == email);

                                if (user != null)
                                {
                                    if (user.password == oldPassword)
                                    {
                                       user.password = newPassword;

                                        _context.Update(user);
                                        _context.SaveChanges();

                                        return "OK: User " + email + " successfully updated";
                                    }

                                    else
                                        return "ERROR: Incorrect password";
                                }

                                else
                                    return "ERROR: User " + email + " does not exists";
                            }

                            else
                                return "ERROR: New password is null or empty";
                        }

                        else
                            return "ERROR: Old password is null or empty";
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
