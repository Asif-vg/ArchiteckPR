using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        [Column(TypeName = "ntext")]
        public string Text { get; set; }
        [MaxLength(100)]
        public string Architeck { get; set; }
        [MaxLength(250)]
        public string Client { get; set; }
        [MaxLength(250)]
        public string Contractor { get; set; }
        [MaxLength(250)]
        public string OurRole { get; set; }
        public DateTime CreatedDate { get; set; }
        [MaxLength(250)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
