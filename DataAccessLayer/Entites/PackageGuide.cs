using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entites
{
    public class PackageGuide
    {
        public int PackageGuideId { get; set; }
        public int PackageId { get; set; }
        public virtual Package Package { get; set; }
        public int GuideId { get; set; }
        public Guide Guide { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
