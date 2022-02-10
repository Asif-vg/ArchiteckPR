using ArchiteckFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.ViewModels
{
    public class VmService:VmLayout
    {
        public List<Service> Services { get; set; }
        public Service Service { get; set; }
        public List<ServiceComment> ServiceComments { get; set; }
        public ServiceComment ServiceComment { get; set; }

        public List<ServiceComment> ReplyComments { get; set; }


    }
}
