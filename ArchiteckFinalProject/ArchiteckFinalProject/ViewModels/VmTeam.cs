using ArchiteckFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.ViewModels
{
    public class VmTeam :VmLayout
    {
        public List<Team> Teams { get; set; }
        public List<Service> Services { get; set; }

    }
}
