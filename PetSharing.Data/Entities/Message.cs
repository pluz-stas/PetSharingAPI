using System;
using System.Collections.Generic;
using System.Text;

namespace PetSharing.Data.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public User User { get; set; }
        public int ReceiverId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
