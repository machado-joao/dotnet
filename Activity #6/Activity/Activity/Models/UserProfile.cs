using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Activity.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ProfileId { get; set; }

        public virtual User User { get; set; }

        public virtual Profile Profile { get; set; }
    }
}
