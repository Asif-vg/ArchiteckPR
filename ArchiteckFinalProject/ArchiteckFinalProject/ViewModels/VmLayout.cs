using ArchiteckFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.ViewModels
{
    public class VmLayout
    {
        public List<Social> Socials { get; set; }
        public Setting Setting { get; set; }
        public Banner Banner { get; set; }
        public List<Project> Projects { get; set; }
        public Project Project { get; set; }

        public VmSearch Search { get; set; }
    }
}
