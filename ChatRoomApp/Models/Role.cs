using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoomApp.Models
{
    public class Role : IdentityRole
    {
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
