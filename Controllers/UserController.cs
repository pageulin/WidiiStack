using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Test_API.DAL.Service;
using Test_API.Entity;
using Test_API.Messaging;

namespace Test_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private IUserService _userService = null;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        // GET api/users
        /// <summary>
        /// Retrieves users
        /// </summary>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Users found</response>
        /// <response code="400">Users Not found</response>
        /// <response code="500">Oops! Can't find users</response>
        [HttpGet("users")]
        public async Task<IEnumerable<User>> Get()
        {
            var users = await this._userService.FindUsers();
            return users;
        }

        [HttpGet("createUser")]
        public IActionResult Create()
        {
           return View();
        }

        // GET api/values/5
        /// <summary>
        /// Get Value for specified id
        /// </summary>
        /// <param name="id">The id for the value</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult AddUser([FromBody] User user)
        {
            Console.WriteLine(user);
            string message = "";
            if(ModelState.IsValid)
            {
                message = this._userService.CreateUser(user) ? "Utilisateur " + user.login + " correctement créé" : "Erreur à la création de l'utilisateur " + user.login;
                RabbitMQSender.GetInstance().sendMessage(user.login);
            }
            ViewBag.Message = message;
            return View(user);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
