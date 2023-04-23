using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderWeb.Data;
using OrderWeb.Models;


namespace OrderWeb.Pages.Categories
{
	[BindProperties]
	public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Category category { get; set; }
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            category = _db.Category.Find(id); 
        }

        public async Task<IActionResult> OnPost()
        {
            if(category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Category.Name", "The display order can't exactly match the name");   
            }
            if (ModelState.IsValid)
            {
                _db.Category.Update(category);
                await _db.SaveChangesAsync();
				TempData["success"] = "Category updated successfully";
				return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
