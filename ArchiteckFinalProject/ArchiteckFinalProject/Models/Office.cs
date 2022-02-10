using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.Models
{
    public class Office
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Address { get; set; }
        [MaxLength(200)]
        public string Location { get; set; }
        [MaxLength(30)]
        public string Phone1 { get; set; }
        [MaxLength(30)]
        public string Phone2 { get; set; }
        [MaxLength(250)]
        public string Email1 { get; set; }
        [MaxLength(250)]
        public string Email2 { get; set; }
        [MaxLength(250)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

    }
}
