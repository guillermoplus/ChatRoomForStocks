using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot.Services;
using Bot.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace Bot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private readonly ISenderService _senderService;
        private readonly IReceiverService _receiverService;

        public BotController(SenderService senderService, ReceiverService receiverService)
        {
            _receiverService = receiverService;
            _senderService = senderService;
        }

        // GET api/values/5
        [HttpGet]
        public ActionResult<MessageViewModel> Get()
        {
            return new MessageViewModel();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] MessageViewModel model)
        {
            if (!model.Content.Contains("/stock="))
            {
                throw new Exception("El mensaje no contiene ningún comando válido");
            }

            model.Command = Utilities.getCommand(model.Content);

            _senderService.SendMessage(model);
        }
    }
}
