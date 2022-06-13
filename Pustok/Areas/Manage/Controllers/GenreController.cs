using Microsoft.AspNetCore.Mvc;
using Pustok.DAL;
using Pustok.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Areas.Manage.Controllers
{
    [Area("manage")]
    public class GenreController : Controller
    {
        private readonly PustokDbContext _context;

        public GenreController(PustokDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Genre> genres = _context.Genres.ToList();
            return View(genres);
        }
    }
}
