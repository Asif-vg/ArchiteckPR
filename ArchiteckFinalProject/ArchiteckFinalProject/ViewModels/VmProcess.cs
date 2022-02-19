using ArchiteckFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.ViewModels
{
    public class VmProcess:VmLayout
    {
        public List<Process> Processes { get; set; }
        public List<Service> Services { get; set; }
        public List<Project> Projects { get; set; }
    }
}
