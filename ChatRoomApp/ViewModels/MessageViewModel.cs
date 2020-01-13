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
        private Message Model { get; set; }

        public MessageViewModel()
        {
            Model = new Message();
        }

        public MessageViewModel(Message x)
        {
            Content = x.Content;
            ChatRoomId = x.ChatRoomId;
            SentOn = x.SentOn;

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
