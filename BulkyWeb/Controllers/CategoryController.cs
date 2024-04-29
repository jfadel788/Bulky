using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
			_db = db;
            
        }
        public IActionResult Index()
		{
			List<Category> ObjectCategoryList= _db.Categories.ToList();
			return View(ObjectCategoryList);
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
				ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the name");
			}

			if (ModelState.IsValid)
			{
				_db.Categories.Add(obj);
				_db.SaveChanges();
				TempData["sucess"] = "The Category has created sucessfuly";
				return RedirectToAction("Index");
			}
			return View();
		

		}
		public IActionResult Edit(int ? id) {
			if(id == null || id==0)
			{
				return NotFound();

			}
			Category ? categoryFromDb = _db.Categories.FirstOrDefault(c => c.Id == id);
			if(categoryFromDb ==null)
			{
				return NotFound();
			}
			return View(categoryFromDb);
		}
		[HttpPost]
		public IActionResult Edit(Category obj) {

			if (ModelState.IsValid)
			{
				_db.Categories.Update(obj);
				TempData["sucess"] = "The Category has Updated sucessfuly";
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View();

		}

		public IActionResult Delete(int id)
		{
			if (id == null || id == 0)
			{
				return NotFound();

			}
			Category? categoryFromDb = _db.Categories.FirstOrDefault(c => c.Id == id);
			if (categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb);

		}
		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(int id)
		{
			Category? obj = _db.Categories.Find(id);
			if(obj == null) { return NotFound(); }
			if (ModelState.IsValid)
			{
				_db.Categories.Remove(obj);
				_db.SaveChanges();
				TempData["sucess"]="The Category has deleted sucessfuly";
				return RedirectToAction("Index");
			}
			return View();
		}
	}
}
