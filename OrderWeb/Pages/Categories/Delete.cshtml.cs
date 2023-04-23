using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OrderWeb.Data;
using OrderWeb.Models;


namespace OrderWeb.Pages.Categories
{
	[BindProperties]
	public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Category category { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            category = _db.Category.Find(id); 
        }

        public async Task<IActionResult> OnPost()
        {
           var categoryFromDb = _db.Category.Find(category.Id);
           if(categoryFromDb != null)
           {
	        	_db.Category.Remove(categoryFromDb);
				await _db.SaveChangesAsync();
				TempData["success"] = "Category deleted successfully";
				return RedirectToPage("Index");

		    }
            return Page();
        }
    }
}
