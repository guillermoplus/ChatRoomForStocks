using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoomApp.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(MAX)")]
        public string Content { get; set; }
        public DateTime SentDate { get; set; } = DateTime.Now;

        public string SenderId { get; set; }
        public User Sender { get; set; }

        public int ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; }
    }
}
