using Pustok.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Models
{
    public class HomeViewModels
    {
        public List<Slider> Sliders { get; set; } 
        public List<Feature> Features { get; set; }
    }
}
