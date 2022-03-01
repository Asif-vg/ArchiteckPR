using ArchiteckFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.ViewModels
{
    public class VmFaq:VmLayout
    {
        public List<Faq> Faqs { get; set; }
        public Faq Faq { get; set; }

        public List<Service> Services { get; set; }
        public List<CompanyIndicator> CompanyIndicators { get; set; }
    }
}
