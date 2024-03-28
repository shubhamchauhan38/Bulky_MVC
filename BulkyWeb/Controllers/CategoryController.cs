using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db) 
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString()) 
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot be same as Name");
            }


            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created scuccesfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryDb = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (categoryDb == null)
            {
                return NotFound();
            }
            return View(categoryDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated scuccesfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryDb = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (categoryDb == null)
            {
                return NotFound();
            }
            return View(categoryDb);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? categoryDb = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (id == null || id == 0)
            {
                return NotFound();
            }
            _db.Categories.Remove(categoryDb);
            _db.SaveChanges();
            TempData["success"] = "Category delete scuccesfully";
            return RedirectToAction("Index");
        }

    }
}
