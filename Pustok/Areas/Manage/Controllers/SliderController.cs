using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Pustok.DAL;
using Pustok.Entities;
using Pustok.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Areas.Manage.Controllers
{
    [Area("manage")]
    public class SliderController : Controller
    {
        private readonly PustokDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(PustokDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Slider> sliders = _context.Sliders.ToList();
            return View(sliders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            if (slider.ImageFile != null)
            {
                if (slider.ImageFile.ContentType != "image/png" && slider.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "File format must be image/png or image/jpeg");
                }
                if (slider.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "File size must be less than 2MB");
                }
            }
            else
            {
                ModelState.AddModelError("ImageFile", "ImageFile is required!");
            }
          
            if (!ModelState.IsValid)
            {
                return View();
            }

            slider.Image = FileManager.Save(_env.WebRootPath, "Uploads/Slider", slider.ImageFile);


            _context.Sliders.Add(slider);
            _context.SaveChanges();


            return RedirectToAction("index");   
        }

        public IActionResult Edit(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(x => x.Id == id);
            if (slider == null)
                return RedirectToAction("error", "dashboard");

            return View(slider);
        }

        [HttpPost]
        public IActionResult Edit(Slider slider)
        {
            if (!ModelState.IsValid)
                return View();


            Slider existSl = _context.Sliders.FirstOrDefault(x => x.Id == slider.Id);
            if (existSl == null)
                return RedirectToAction("error", "dashboard");

            existSl.Title = slider.Title;
            existSl.Desc = slider.Desc;
            existSl.BtnText = slider.BtnText;
            existSl.BtnUrl = slider.BtnUrl;
            existSl.Order = slider.Order;
            existSl.Image = slider.Image;
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(X => X.Id == id);
            if (slider == null)
                return RedirectToAction("error", "dashboard");

            return View(slider);
        }

        [HttpPost]
        public IActionResult Delete(Slider slider)
        {
            Slider ExistSl = _context.Sliders.FirstOrDefault(x => x.Id == slider.Id);

            if (ExistSl == null)
                return RedirectToAction("error", "dashboard");

            _context.Sliders.Remove(ExistSl);
            _context.SaveChanges();

            return RedirectToAction("index");
        }



    }
}
