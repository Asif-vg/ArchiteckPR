using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.Models
{
    public class Setting
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Address { get; set; }
        [MaxLength(250)]
        public string Email { get; set; }
        [MaxLength(30)]
        public string Phone { get; set; }
        [MaxLength(1000)]
        public string About { get; set; }
        [MaxLength(250)]
        public string Logo { get; set; }
        [NotMapped]
        public IFormFile LogoFile { get; set; }
        //[MaxLength(250)]
        public string Video { get; set; }
        [NotMapped]
        public IFormFile VideoFile { get; set; }
        [MaxLength(250)]
        public string Map { get; set; }
        [NotMapped]
        public IFormFile MapFile { get; set; }



    }
}
