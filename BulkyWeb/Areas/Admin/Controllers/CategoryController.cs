using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CategoryController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public CategoryController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;

		}
		public IActionResult Index()
		{
			List<Category> ObjectCategoryList = _unitOfWork.CategoryRepository.GetAll().ToList();
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
				_unitOfWork.CategoryRepository.Add(obj);
				_unitOfWork.Save();
				TempData["sucess"] = "The Category has created sucessfuly";
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
			Category? categoryFromDb = _unitOfWork.CategoryRepository.Get(u => u.Id == id);
			if (categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb);
		}
		[HttpPost]
		public IActionResult Edit(Category obj)
		{

			if (ModelState.IsValid)
			{
				_unitOfWork.CategoryRepository.Update(obj);

				TempData["sucess"] = "The Category has Updated sucessfuly";
				_unitOfWork.Save();
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
			Category? categoryFromDb = _unitOfWork.CategoryRepository.Get(u => u.Id == id);
			if (categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb);

		}
		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(int id)
		{
			Category? obj = _unitOfWork.CategoryRepository.Get(u => u.Id == id);
			if (obj == null) { return NotFound(); }
			if (ModelState.IsValid)
			{
				_unitOfWork.CategoryRepository.Remove(obj);
				_unitOfWork.Save();
				TempData["sucess"] = "The Category has deleted sucessfuly";
				return RedirectToAction("Index");
			}
			return View();
		}
	}
}
