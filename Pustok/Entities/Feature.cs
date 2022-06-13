using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Entities
{
    public class Feature
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Icon { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(50)]
        public string Desc { get; set; }
    }
}
