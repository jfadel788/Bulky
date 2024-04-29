using Bulky_Razor2.Data;
using Bulky_Razor2.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bulky_Razor2.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<Category> CategoryList { get; set; }
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
            
        }
        public void OnGet()
        {
            CategoryList= _db.Categories.ToList();
        }
    }
}
