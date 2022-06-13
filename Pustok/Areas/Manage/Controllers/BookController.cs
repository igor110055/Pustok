using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class BookController : Controller
    {
        private readonly PustokDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BookController(PustokDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Book> books = _context.Books.Include(x=>x.Author).ToList();
            return View(books);
        }
        public IActionResult Create()
        {
            ViewBag.Authors = _context.Authors.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Book book)
        {
            
            if (ModelState.IsValid)
            {
                if (book.PosterImage != null)
                {
                    BookImage bookImage = new BookImage()
                    {
                        Name = FileManager.Save(_env.WebRootPath, "uploads/book", book.PosterImage),
                        PosterStatus = true
                    };
                    book.BookImages.Add(bookImage);
                }
                else
                {
                    ModelState.AddModelError("PosterImage", "Book image Required");
                    ViewBag.Authors = _context.Authors.ToList();
                    return View();
                }
                if (book.ImageFiles != null)
                {
                    foreach (IFormFile file in book.ImageFiles)
                    {
                        BookImage bookImage = new BookImage()
                        {
                            Name = FileManager.Save(_env.WebRootPath, "uploads/book", file)
                        };
                        book.BookImages.Add(bookImage);
                    }
                }
                _context.Books.Add(book);
                _context.SaveChanges();
                return RedirectToAction("index");
            }
            else
            {
                ViewBag.Authors = _context.Authors.ToList();
                return View();
            }
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Authors = _context.Authors.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                var existsBook = _context.Books.Include(x => x.BookImages).FirstOrDefault(x => x.Id == book.Id);
                if (book.BookImages != null)
                {
                    for(var i=0; i<existsBook.BookImages.Count; i++)
                    {
                        var image = existsBook.BookImages[i];
                        FileManager.Delete(_env.WebRootPath, "uploads/book", image.Name);
                        _context.BookImages.Remove(image);
                    }
                    foreach (IFormFile file in book.ImageFiles)
                    {
                        BookImage bookImage = new BookImage()
                        {
                            Name=FileManager.Save(_env.WebRootPath , "uploads/book" , file)
                        };
                        book.BookImages.Add(bookImage);
                    }
                }
                existsBook.Name = book.Name;
                existsBook.PageSize = book.PageSize;
                existsBook.SalePrice = book.SalePrice;
                existsBook.DiscountPercent = book.DiscountPercent;
                existsBook.IsAvailable = book.IsAvailable;
                existsBook.AuthorId = book.AuthorId;
                return RedirectToAction("index");
            }
        }
    }
}
