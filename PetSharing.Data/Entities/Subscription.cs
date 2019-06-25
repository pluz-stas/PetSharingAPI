using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PetSharing.Data.Entities
{
    public class Subscription
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int PetId { get; set; }
        public PetProfile PetProfile { get; set; }
    }
}
