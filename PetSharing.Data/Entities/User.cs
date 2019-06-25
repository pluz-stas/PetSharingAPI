using System;
using System.Collections.Generic;
using System.Text;

namespace PetSharing.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public virtual List<Subscription> Subscriptions { get; set; }
        public virtual List<Message> Messages { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<PetProfile> PetProfiles { get; set; }
    }
}
