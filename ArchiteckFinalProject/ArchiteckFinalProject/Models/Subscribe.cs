using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.Models
{
    public class Subscribe
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Email { get; set; }
        [NotMapped]
        public string Controller { get; set; }

        public DateTime CreatedDate { get; set; }


    }
}
