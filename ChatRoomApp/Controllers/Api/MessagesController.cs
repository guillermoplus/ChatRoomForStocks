using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatRoomApp.Data;
using ChatRoomApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChatRoomApp.Controllers.Api
{
    [Route("api/messages")]
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public MessagesController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/<controller>
        //[HttpGet]
        //public IEnumerable<Message> Get()
        //{

        //}

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult GetMessagesByChatRoom(int id)
        {
            var messages = _context.Messages
                .Where(x => x.ChatRoomId == id)
                .OrderByDescending(x => x.SentOn)
                .Take(50)
                .ToList();

            return Ok(messages);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody] Message model)
        {
            try
            {
                model.SenderId = _userManager.GetUserId(User);

                if (ModelState.IsValid)
                {
                    _context.Messages.Add(model);
                    _context.SaveChangesAsync();
                }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest("No fue posible enviar el mensaje");
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
