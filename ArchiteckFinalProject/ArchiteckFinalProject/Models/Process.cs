using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.Models
{
    public class Process
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(30)]
        public string Icon { get; set; }
        [MaxLength(100)]
        public string ProcessHead { get; set; }
        [MaxLength(250)]
        public string ProcessText { get; set; }
        [MaxLength(250)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        



    }
}
