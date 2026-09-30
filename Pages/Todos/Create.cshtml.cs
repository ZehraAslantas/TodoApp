using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.Pages.Todos
{
    public class CreateModel : PageModel
    {
        private readonly ITodoStore _store;
        //yeni bir kaynak oluþturduk ve altta ctor ile injektion yaptýk
        public CreateModel(ITodoStore store)
        {
            _store = store;
        }

        [BindProperty] //post olacaðý için ger gibi ekstra belirtmene gerek yok
        public Todo Todo { get; set; } = new(); //bunu oluþturduk ki @Model.Todo olarak .cshtml de çaðýrýyoruz
        public void OnGet()
        {

        }
        public IActionResult OnPost() {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _store.Add(Todo);
            TempData["Message"] = "Görev Eklendi";
            return RedirectToAction("Index");
        }
    }
}
