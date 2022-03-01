using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.ViewModels
{
    public class VmForgotPassword
    {
        [Required]
        public string Email { get; set; }
    }
}
