using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.Pages.Todos
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Todo Todo { get; set; } = new(); //cshtl de yazdýðýmýz model olarak tanýmlanmasý gereken yer burasý

        private readonly ITodoStore _store;   //artýk get isteði üzerinden bilgileri okuyup sayfaya taþýyabiliriz(ctoru da altta oluþturduk)

        public EditModel(ITodoStore store)
        {
            _store = store;
        }

        public IActionResult OnGet(Guid id) //method voiddi.return hata verince farkettik türünü deðiþtirdik
                                            //form iþlrmini yazdýk cshtml de.þimdide ilgili görev bilgilerini repository üzerinden alacaðýz 
        {
            var item = _store.Get(id);
            if (item is null) {
                TempData["Message"] = "Kayýt Bulunamadý";
                return RedirectToPage("Index");
            }
            // id deðeri boþ deðilse nesne oluþturuyoruz
            Todo = new Todo
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                DueDate = item.DueDate,
                IsDone = item.IsDone
            };
            return Page();
        }
        // önce form sonra OnGet yazýldý.Þimdi submit butonuna týklayýnca OnPos tetiklenecek.Yani OnPost'u yazýcaz alta da
        public IActionResult OnPost() {

            if (!ModelState.IsValid) {
                return Page(); //önce kontrol ettik
            }
            //else durumunu yazýyoruz 
            var ok = _store.Update(Todo);
            /*Bu Todo nesnesi parametre olarak _store.Update(Todo) metoduna gider.

           _store içindeki eski bilgiler bu yeni Todo nesnesindeki verilerle güncellenir.*/
            if (!ok) {
                TempData["Message"] = "Güncelleme yapýlmadý.";
                return RedirectToPage("Index");
            }
            TempData["Message"] = "Görev güncellendi.";
            return RedirectToPage("Index");
            Console.WriteLine($"Gelen ID: {Todo.Id}, Baþlýk: {Todo.Title}");
        }
        //þimdide index sayfasýnda edit sayfasýna gitmek için buton ekliyoruz
    }
}

