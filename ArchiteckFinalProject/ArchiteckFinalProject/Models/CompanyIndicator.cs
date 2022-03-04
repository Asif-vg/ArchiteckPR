using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.Models
{
    public class CompanyIndicator
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
      
        [MaxLength(10)]
        public int Count { get; set; }
    }
}
