using ArchiteckFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.ViewModels
{
    public class VmProject:VmLayout
    {
        public List<Project> Projects { get; set; }
        public Project Project { get; set; }
        public List<ProjectArchiteck> ProjectArchitecks { get; set; }



    }
}
