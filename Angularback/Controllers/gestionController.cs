using Angularback.Models;
using Angularback.Models.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace  Angularback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class gestionController : ControllerBase
    {

        private readonly ApplicationContext _context;
        private readonly IConfiguration _configuration;

        public gestionController(ApplicationContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
       

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var idUser = HttpContext.User.Claims.Where(x => x.Type == "userId").SingleOrDefault();
            var role = _context.Users.Where(x => x.Id == int.Parse(idUser.Value)).Select(p => p.Role).SingleOrDefault();

            if (role == "User")
            {
                return Ok(new Reponse() { Status = "Error", Message = "Not Authorized" });
            }
            else if(role== "admin")
            {
                var users = _context.Users.Where(usr => usr.Role == "User").ToList();
                return Ok(new { usrs = users });
            }
            return Ok(new Reponse() { Status = "Error", Message = "Not Authorized" });
        }



    }
}
