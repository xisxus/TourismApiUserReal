using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class PackageGuideDTO
    {
        public int PackageGuideId { get; set; }
        public int PackageId { get; set; }
        public int GuideId { get; set; }
    }
}
