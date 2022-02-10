using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.Models
{
    public class PersonToSocial
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Team")]
        public int TeamId { get; set; }
        public Team Team { get; set; }
        [ForeignKey("PersonSocial")]
        public int PersonSocialId { get; set; }
        public PersonSocial PersonSocial { get; set; }

    }
}
