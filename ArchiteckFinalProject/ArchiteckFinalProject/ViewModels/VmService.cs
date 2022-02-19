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
        public List<Process> Processes { get; set; }
        public List<Project> Projects { get; set; }
        public Service Service { get; set; }
        public List<ServiceComment> ServiceComments { get; set; }
        public ServiceComment ServiceComment { get; set; }

        public List<ServiceComment> ReplyComments { get; set; }
        public List<ServiceCatagory> ServiceCatagories { get; set; }

        public VmSearch Search { get; set; }


    }
}
