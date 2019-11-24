﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FloCares.Models.Entities
{
    public class BloodBank
    {
        public int Id { get; set; }
        public string Age { get; set; }

        public Sex Sex { get; set; }

        public int? BloodGroupId {get;set;}
        public BloodGroup BloodGroup { get; set; }

        [Display(Name="Last Donation Date")]
        public DateTime LastDonationDate { get; set; }
    }
}
