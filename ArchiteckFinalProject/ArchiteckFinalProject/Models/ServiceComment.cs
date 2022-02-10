using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.Models
{
    public class ServiceComment
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(250), Required]
        public string Name { get; set; }
        [MaxLength(250), Required]
        public string Email { get; set; }
        [MaxLength(100), Required]
        public string Subject { get; set; }
        [MaxLength(4000), Required]
        public string Context { get; set; }
        [MaxLength(250)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public DateTime CreatedDate { get; set; }


        [ForeignKey("Service")]
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        [ForeignKey("ParrentComment")]
        public int? ParentId { get; set; }
        public ServiceComment ParrentComment { get; set; }



    }
}
