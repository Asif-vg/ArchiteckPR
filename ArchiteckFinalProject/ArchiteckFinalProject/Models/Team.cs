using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        
        [ForeignKey("PersonPosition")]
        public int PersonPositionId { get; set; }
        public PersonPosition PersonPosition { get; set; }
        [MaxLength(250)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }



        public List<PersonToSocial> PersonToSocials { get; set; }
        [NotMapped]
        public List<int> PersonToSocialsId { get; set; }
        [NotMapped]
        public List<int> PersonSocial { get; set; }

    }
}
