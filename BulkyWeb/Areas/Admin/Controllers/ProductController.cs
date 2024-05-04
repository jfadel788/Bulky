using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAcess.Data;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            List<Product> ObjectProductList = _unitOfWork.ProductRepository.GetAll().ToList();
         
            return View(ObjectProductList);
        }
        public IActionResult Upsert(int?id)
        {
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()

                }),
                Product = new Product()
            };
            if(id==null || id == 0)
            {
                //create
				return View(productVM);
			}
            else
            {
				productVM.Product = _unitOfWork.ProductRepository.Get(u => u.Id == id);
                return View(productVM);

            }
         
			
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productVm, IFormFile ?file)
        {


            if (ModelState.IsValid)
            {
                string wwwRootpath = _webHostEnvironment.WebRootPath;
                if(file!=null)
                {
                    string fileName= Guid.NewGuid().ToString() +Path.GetExtension(file.FileName);
                    string productPath= Path.Combine(wwwRootpath, @"images\product");
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStream);

                    }
                    productVm.Product.ImageUrl=@"\images\product\"+fileName; 
                }
                _unitOfWork.ProductRepository.Add(productVm.Product);
                _unitOfWork.Save();
                TempData["sucess"] = "The Product has created sucessfuly";
                return RedirectToAction("Index");
            }
            else
            {
                productVm.CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()

                });
            }	
            return View(productVm);
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
