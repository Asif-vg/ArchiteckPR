using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.Models
{
    public class About
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(4000)]
        public string Text { get; set; }
        [MaxLength(250)]
        public string FounderName { get; set; }
        [MaxLength(250)]
        public string Position { get; set; }
        [MaxLength(250)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [MaxLength(250)]
        public string SignImage { get; set; }
        [NotMapped]
        public IFormFile SignImageFile { get; set; }



    }
}
