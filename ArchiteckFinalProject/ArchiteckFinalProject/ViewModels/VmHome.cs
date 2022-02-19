using ArchiteckFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.ViewModels
{
    public class VmHome:VmLayout
    {
        public List<Process> Processes { get; set; }
        public List<Service> Services { get; set; }
        public List<ServiceComment> ServiceComments { get; set; }
        public List<Project> Projects { get; set; }
        public List<Team> Teams { get; set; }
        public List<Client> Clients { get; set; }
        public About About { get; set; }
        public Solution Solution { get; set; }
        public List<HomeBanner> HomeBanners { get; set; }

    }
}
