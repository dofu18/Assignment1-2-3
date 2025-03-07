using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Service.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ProfileUrl { get; set; }
        public float Credits { get; set; }
        public string Meta { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
        public Guid? ParentId { get; set; }
        public string Status { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
