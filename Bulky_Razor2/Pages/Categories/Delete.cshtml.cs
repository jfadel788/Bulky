using Bulky_Razor2.Data;
using Bulky_Razor2.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bulky_Razor2.Pages.Categories
{
	[BindProperties]
	public class DeleteModel : PageModel
	{
		private readonly ApplicationDbContext _db;
		[BindProperty]
		public Category? Category { get; set; }
		public DeleteModel(ApplicationDbContext db)
		{
			_db = db;

		}
		public void OnGet(int? id)
		{
			if (id != null && id != 0)
			{
				Category = _db.Categories.Find(id);
			}
		}
		public IActionResult OnPost()
		{

			if (ModelState.IsValid)
			{
				_db.Categories.Remove(Category);
				//TempData["sucess"] = "The Category has Updated sucessfuly";
				_db.SaveChanges();
				return RedirectToPage("Index");

			}
			return Page();


		}
	}
}