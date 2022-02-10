using ArchiteckFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.ViewModels
{
    public class VmContact: VmLayout
    {

        public Message Contact { get; set; }
        public List<Office> Offices { get; set; }


    }
}
