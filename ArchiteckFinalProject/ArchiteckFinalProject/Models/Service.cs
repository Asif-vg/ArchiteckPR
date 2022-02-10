using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [Column(TypeName ="ntext")]
        public string Text { get; set; }
        [MaxLength(50)]
        public string Icon { get; set; }
        [MaxLength(250)]
        public string ServiceImage { get; set; }
        [NotMapped]
        public IFormFile ServiceImageFile { get; set; }

        public DateTime CreatedDate { get; set; }

        [ForeignKey("ServiceCatagory")]
        public int CategoryId { get; set; }
        public ServiceCatagory ServiceCatagory { get; set; }


        //public List<ServiceCatagory> ServiceCatagories { get; set; }
        //public List<ServiceImage> ServiceImages { get; set; }
        public List<ServiceComment> ServiceComments { get; set; }




    }
}
