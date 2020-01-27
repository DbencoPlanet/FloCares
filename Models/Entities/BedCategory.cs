using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FloCares.Models.Entities
{
    public class BedCategory
    {
        public int Id { get; set; }

        [Display(Name="Bed Number")]
        public int BedNumber { get; set; }

        [Display(Name = "Bed Name")]
        public string BedName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Status")]
        public EntityStatus Status { get; set; }

    }
}
