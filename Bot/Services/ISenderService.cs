using Bot.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot.Services
{
    interface ISenderService
    {
        bool SendMessage(MessageViewModel message);
    }
}
