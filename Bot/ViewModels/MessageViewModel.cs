using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot.ViewModels
{
    public class MessageViewModel
    {
        public string Content { get; set; }
        public int ChatRoomId { get; set; }
        public string SentOnString { get; set; } = DateTime.Now.ToString("dd/MM/yyyy h:mm:ss tt");
    }
}
