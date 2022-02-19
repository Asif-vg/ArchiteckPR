using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [NotMapped]
        public List<IFormFile> ClientImageFile { get; set; }

        public List<ClientImage> ClientImages { get; set; }



    }
}
