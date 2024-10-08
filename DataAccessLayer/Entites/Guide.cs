﻿using DataAccessLayer.Entites.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entites
{
    public class Guide
    {
        public int GuideId { get; set; }
        public string GuideName { get; set; }
        public string GuideAddress { get; set; }
        public string GuideCode { get; set; }
        public string ApplicationUserID { get; set; }
        public virtual ApplicationUser  ApplicationUser { get; set; }

        public  DateTime CreatedAt { get; set; }
        public  DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

    }
}
