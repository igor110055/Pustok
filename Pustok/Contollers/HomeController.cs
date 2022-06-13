using Microsoft.AspNetCore.Mvc;
using Pustok.DAL;
using Pustok.Entities;
using Pustok.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Contollers
{
    public class HomeController : Controller
    {
        private readonly PustokDbContext _context;

        public HomeController(PustokDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            List<Slider> sliders = _context.Sliders.ToList();
            List<Feature> features = _context.Featuress.ToList();
            HomeViewModels homeVM = new HomeViewModels()
            {
                Sliders = sliders,
                Features = features
            };

            return View(homeVM);
        }
    }
}
