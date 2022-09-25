using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Activity.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        [DisplayName("User")]
        public int UserId { get; set; }

        [DisplayName("Profile")]
        public int ProfileId { get; set; }

        public virtual User User { get; set; }

        public virtual Profile Profile { get; set; }
    }
}
