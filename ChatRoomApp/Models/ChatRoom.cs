﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoomApp.Models
{
    public class ChatRoom
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
