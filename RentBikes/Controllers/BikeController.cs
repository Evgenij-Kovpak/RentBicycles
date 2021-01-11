using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentBikes.Models;
using System.Diagnostics;
using RentBikes.Models.AppDbContext;
using RentBikes.Models.ViewModel;
using Microsoft.Extensions.Hosting.Internal;

namespace RentBikes.Controllers
{
    public class BikeController : Controller
    {
        private readonly BikeDbContext _db;
        
        [BindProperty]
        public BikeViewModel BikeVM { get; set; }

        public BikeController(BikeDbContext db)
        {
            _db = db;
            
            BikeVM = new BikeViewModel()
            {
                Makes = _db.Makes.ToList(),
                Models = _db.Models.ToList(),
                Bike = new Models.Bike()
            };
        }
        public IActionResult Index()
        {
            var Bikes = _db.Bikes.Include(m => m.Make).Include(m=>m.Model);
            return View(Bikes.ToList());
        }

        public IActionResult Create()
        {
            return View(BikeVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost()
        {
            if (!ModelState.IsValid)
            {
                BikeVM.Makes = _db.Makes.ToList();
                BikeVM.Models = _db.Models.ToList();
                return View(BikeVM);
            }
            _db.Bikes.Add(BikeVM.Bike);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        //public IActionResult Edit(int id)
        //{
        //    ModelVM.Model = _db.Models.Include(m => m.Make).SingleOrDefault(m => m.Id == id);
        //    if (ModelVM.Model == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(ModelVM);
        //}

        //[HttpPost, ActionName("Edit")]
        //public IActionResult EditPost()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(ModelVM);
        //    }
        //    _db.Update(ModelVM.Model);
        //    _db.SaveChanges();
        //    return RedirectToAction(nameof(Index));
        //}

        //[HttpPost]
        //public IActionResult Delete(int id)
        //{
        //    Model model = _db.Models.Find(id);
        //    if (model == null)
        //    {
        //        return NotFound();
        //    }
        //    _db.Models.Remove(model);
        //    _db.SaveChanges();
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
