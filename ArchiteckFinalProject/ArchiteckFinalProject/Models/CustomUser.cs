using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.Models
{
    public class CustomUser:IdentityUser
    {
        [MaxLength(250)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [MaxLength(80)]
        public string FullName { get; set; }
        public DateTime CreatedDate { get; set; }
        [MaxLength(250), Required]
        public string Password { get; set; }
        [NotMapped]
        public string RoleId { get; set; }
    }
}
