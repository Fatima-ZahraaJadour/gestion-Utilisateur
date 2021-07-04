using Angularback.Models;
using Angularback.Models.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angularback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class registerController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public registerController(ApplicationContext context)
        {
            _context = context;
        }

       
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (user.Cne == "" || user.Email == "" || user.Username == "" && user.Password == "")
            {
                return Ok(new Reponse() { Status = "Error", Message = "Invalid User input" });
            }
            User newUser = new User();
            newUser.Email = user.Email;
            newUser.Password = user.Password;
            newUser.Cne = user.Cne;
            newUser.Username = user.Username;
            newUser.Role = "User";

            _context.Add(newUser);
            _context.SaveChanges();

            return Ok(new Reponse() { Status = "Success", Message = "Profile created successfuly" });

        }


        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<registerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
