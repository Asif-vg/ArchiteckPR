using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.ViewModels
{
    public class VmChangePassword
    {

        //public string Id { get; set; }
        //public string Token { get; set; }
        //[Required, DataType(DataType.Password)]
        //public string Password { get; set; }

        //[Required, DataType(DataType.Password), Compare(nameof(Password))]
        //public string ConfirmPasswod { get; set; }

        [Required(ErrorMessage = "New password required", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        public string ResetCode { get; set; }
    }
}
