using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.ViewModels
{
    public class VmSearch
    {
        public string searchData { get; set; }
        public int? catId { get; set; }
        public int? arcId { get; set; }
        public int? id { get; set; }
        public int? page { get; set; }
    }
}
