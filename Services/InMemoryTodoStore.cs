using System.Collections.Concurrent;
using TodoApp.Models;
namespace TodoApp.Services;

public class InMemoryTodoStore : ITodoStore
{
    private readonly ConcurrentDictionary<Guid, Todo> _items = new();
    private readonly ILogger<InMemoryTodoStore> _logger;

    public InMemoryTodoStore(ILogger<InMemoryTodoStore> logger)
    {
        //DI ile bağımlılık çözme
        _logger = logger;
        seed();//metot tanımla ve altta onu oluştur.çekirdek data ekliyoruz
    }

    public void Add(Todo todo)
    {
        todo.Id=todo.Id==Guid.Empty ? Guid.NewGuid() : todo.Id;
        //soru işaretinden önce bi koşul yazılır true ise soru işaretinden sonraki ilk ifade,false ise ikinci(if else mantığı)
        _items[todo.Id] = todo;//köşeli parantez olayı _items ın anahtar değer ilişkisi ile tanımlanmasından kaynaklı
        //ilgili id ile todo nesnesinin tüm özelliklerine erişir
        //"Sözlüğe git. Anahtar (Key) dolabının üzerinde "todo.Id" yazan kapağını aç ve içine değer (Value) olarak bu todo nesnesini koy."
        
        _logger.LogInformation($"Görev eklendi: {todo.Title} ({todo.Id})");
    
    }

    public bool Delete(Guid id)
    {
        var ok = _items.TryRemove(id, out var todo);//id ye sahip nesne varsa o todo da yer alacak.onu sil
        if (ok) {
            _logger.LogInformation($"Görev silindi: {todo.Title} ({todo.Id})");
        }
        return ok;
    
    }

    public Todo? Get(Guid id)
    {
       _items.TryGetValue(id, out var todo);//id ile eşleyen kayıt varmı bak todo da.
        return todo;//eğer yoksa null olur ve Todo? ile tanımlandığı için cevap null dönebiliyor.
    }

    public IEnumerable<Todo> GetAll() => _items
        //tüm kayıtları listeleyeceğiz
        .Values
        .OrderBy(x => x.DueDate ?? DateTime.MaxValue);
    //duedate ifadesi null olabilirdi değişken tanımında.eğer varsa son tarihe göre yoksa maxtarih değerine göre sıralayarak göster.

    

    public IEnumerable<Todo> Search(string? term, ToDoPriority? priority, bool? isDone, bool? dueDateAsc)
    {
       IEnumerable<Todo> q = _items.Values;
        if (!string.IsNullOrEmpty(term)) {
            var t = term.Trim();
            q = q.Where(x => (x.Title?.Contains(t, StringComparison.CurrentCultureIgnoreCase) ?? false)
            || (x.Description?.Contains(t, StringComparison.CurrentCultureIgnoreCase) ?? false));
            }
        if(priority.HasValue)//hasvalue kontrolü için soru işaretli tanımlanmış olması lazım
        {
            q = q.Where(x => x.Priority == priority.Value);
        }
        if (isDone.HasValue)//hasvalue kontrolü için soru işaretli tanımlanmış olması lazım
        {
            q = q.Where(x => x.IsDone == isDone.Value);
        }
        q = dueDateAsc == false
            ? q.OrderByDescending(x => x.DueDate ?? DateTime.MinValue)
            : q.OrderBy(x => x.DueDate ?? DateTime.MaxValue);

        return q.ToList();
    }

    public bool Update(Todo todo)
    {
        if(!_items.ContainsKey(todo.Id))
            return false;
        _items[todo.Id] = todo;//todo nun ıd değerine bağlı olarak ilgili değeri güncelle
        _logger.LogInformation($"Görev güncellendi: {todo.Title} ({todo.Id})");
        return true;

    }

    private void seed()//hazır veri ekliyoruz şuan(çekirdek data)
    {
        if (_items.Count > 0) 
            return;
        var today = DateTime.Today;//o günün tarih bilgisini aldık
        var samples = new[]//dizi tanımladık
        {
            new Todo(){Title="Alışveriş yap" ,Description="Süt,ekmek,yumurta", Priority = ToDoPriority.Medium, DueDate=today.AddDays(1), IsDone=false},
            new Todo(){Title="Sunum Hazırla" ,Description="Pazartesi toplantısı için slaytlar", Priority = ToDoPriority.High, DueDate=today.AddDays(3), IsDone=false},
            new Todo(){Title="Spor" ,Description="30 dk koşu", Priority = ToDoPriority.Low,DueDate=today.AddDays(2),IsDone=true},
            new Todo(){Title="Araba bakımı" ,Description="Yağ değişimi ve filtreler", Priority = ToDoPriority.Medium,DueDate=today.AddDays(7),IsDone=false}
        };
        foreach( var item in samples)
        {
            Add(item);
        }
    }
}
