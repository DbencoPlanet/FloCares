using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FloCares.Models.Entities
{
    public class Bed
    {
        public int Id { get; set; }

        public int? BedCategoryId { get; set; }
        public BedCategory BedCategory { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Bed Capacity")]
        public string Capacity { get; set; }

        [Required]
        [Display(Name = "Bed Charge")]
        public string Charge { get; set; }

        public EntityStatus Status { get; set; }
    }
}
