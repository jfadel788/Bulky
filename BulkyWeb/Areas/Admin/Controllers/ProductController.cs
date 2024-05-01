using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAcess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            List<Product> ObjectProductList = _unitOfWork.ProductRepository.GetAll().ToList();
            return View(ObjectProductList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {
            

            if (ModelState.IsValid)
            {
                _unitOfWork.ProductRepository.Add(obj);
                _unitOfWork.Save();
                TempData["sucess"] = "The Product has created sucessfuly";
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
            Product? ProductFromDb = _unitOfWork.ProductRepository.Get(u => u.Id == id);
            if (ProductFromDb == null)
            {
                return NotFound();
            }
            return View(ProductFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.ProductRepository.Update(obj);

                TempData["sucess"] = "The Product has Updated sucessfuly";
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
            Product? ProductFromDb = _unitOfWork.ProductRepository.Get(u => u.Id == id);
            if (ProductFromDb == null)
            {
                return NotFound();
            }
            return View(ProductFromDb);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            Product? obj = _unitOfWork.ProductRepository.Get(u => u.Id == id);
            if (obj == null) { return NotFound(); }
            if (ModelState.IsValid)
            {
                _unitOfWork.ProductRepository.Remove(obj);
                _unitOfWork.Save();
                TempData["sucess"] = "The Product has deleted sucessfuly";
                return RedirectToAction("Index");
            }
            return View();
        }


    }
}
