using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.Pages.Todos
{

    public class IndexModel : PageModel 
        //bu sýnýftaki prop lar sayfaya yansýyor frontend kýsmýnda her biri için div ler koyup orada tasarladýk
        //sayfadaki verileri backend ile eþleþtirmek için BindProperty kullandýk
    {
        private readonly ITodoStore _store;

        [BindProperty(SupportsGet=true)]//Bind iþlemi post da gerçekleþir.get olmasý için yazarken özellikle belirtiyoruz.arama kýsmý için get 
        public String? Q { get; set; } //index.cshtml de bunu valu kýsmýna yazýyoruz

        [BindProperty(SupportsGet = true)]
        public ToDoPriority? Priority { get; set; }/*bunu yapma nedeni kullanýcý seçenekler arasýnda
                                                    * tercih yapýnca bunu hafýzada tutmak.
                                              * frontend tasarýmýndaki aramanýn yanýndaki seçeneklere enum daki low vb. deðerleri getiricez tasarým sayfasýnfa for ile*/
        [BindProperty(SupportsGet = true)] //buradaki tüm bind iþlemleri altýndaki deðiþken için
        public String? Status { get; set; } //fronend kýsmýndaki done,pending,null kýsmý için

        [BindProperty(SupportsGet = true)]
        public String? Sort { get; set; }//due_asc or due_desc deðiþkenlerini kullancaz frontendde
        public IEnumerable<Todo> Items { get; private set; } = Enumerable.Empty<Todo>();

        public IndexModel(ITodoStore store)
        {
            _store = store;
        }
        public void OnGet()
        {/*uygula butonuna basýldý.önce bu çalýþtý.sonra .cshtml dosyasýna gitti.
          
            OnGet(): Veriyi bulur, süzer, hafýzaya koyar.
       Index.cshtml'deki Razor (@): Hafýzadaki o hazýr veriyi alýp HTML etiketlerinin arasýna yerleþtirir.*/
            
            //kayýtlarý alacak ifade yani uygula kýsmýna basýnca sayfada ekrana gelecekler
            bool? isDone=Status?.ToLowerInvariant() switch {
                "done" => true,
                "pending" => false,
                _ =>null
            };
            bool? dueAsc = Sort?.ToLowerInvariant() switch
            {
                "due_desc" => false,
                _ => true
            };
            Items = _store.Search(Q,Priority,isDone,dueAsc);
        }
    }
}
