using ChatRoomApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoomApp.ViewModels
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime SentOn { get; set; } = DateTime.Now;
        public int ChatRoomId { get; set; }
        public string SenderId { get; set; }
        public bool IsMyMessage { get; set; } = false;
        private Message Model { get; set; }
        public string SentOnString { get; set; } = DateTime.Now.ToString("dd/MM/yyyy h:mm:ss tt");

        public MessageViewModel()
        {
            Model = new Message();
        }

        public MessageViewModel(Message x, User user = null)
        {
            Content = x.Content;
            ChatRoomId = x.ChatRoomId;
            SentOn = x.SentOn;
            SentOnString = x.SentOn.ToString("dd/MM/yyyy h:mm:ss tt");

            if (user != null)
                if (user.Id == x.SenderId) IsMyMessage = true;

            Model = x;
        }

        public void SetModel(Message model)
        {
            Model = model;
        }

        public Message GetModel()
        {
            Model.Content = Content;
            Model.SenderId = SenderId;
            Model.SentOn = SentOn;
            Model.ChatRoomId = ChatRoomId;
            Model.Id = Id;

            return Model;
        }
    }
}
