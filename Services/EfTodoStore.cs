using Microsoft.EntityFrameworkCore;
using TodoApp.Models;
using TodoApp.Repositories;

namespace TodoApp.Services
{
    public class EfTodoStore : ITodoStore
    {
        private readonly TodoDbContext _db;
        //bu repositories paketinden geldiği için onu üste using ekledik.  
        private readonly ILogger<EfTodoStore> _logger;
        //üstteki 2 propu seçip ctrl . ile ctor oluşturup injection yaptık

        public EfTodoStore(TodoDbContext db, ILogger<EfTodoStore> logger)
        {
            _db = db;
            _logger = logger;
        }

        public void Add(Todo todo)
        { 
            //id kontrolü şart çünkü guid de otomatik artan id yok c# ekler id yi
            if (todo.Id.Equals(Guid.Empty))
                todo.Id = Guid.NewGuid();//id yok ise bunla id ekle
            _db.Todos.Add(todo);//id varsa Todos tablosuna ekleme yap
            _db.SaveChanges(); //kaydet veritabanına
            _logger.LogInformation($"Görev eklendi: {todo.Title}");
        }

        public bool Delete(Guid id)
        {
            var entity = _db.Todos.FirstOrDefault(x => x.Id.Equals(id));

            if (entity is null)
                return false;
            _db.Todos.Remove(entity);
            _db.SaveChanges();
            _logger.LogInformation($"Görev silindi {entity.Title}");
            return true;
        }

        public Todo? Get(Guid id) => _db.Todos
            .AsNoTracking()
            .FirstOrDefault(x => x.Id.Equals(id));

        public IEnumerable<Todo> GetAll() => _db.Todos
            .AsNoTracking()
            .OrderBy(x => x.DueDate ?? DateTime.MaxValue)
            .ToList(); //listeyi todo şeklinde dönderdik
  
        public IEnumerable<Todo> Search(string? term, ToDoPriority? priority, bool? isDone, bool? dueDateAsc)
        {
            //önce bu 4 parametre veriye sahipmi sorgulayalım
            IQueryable<Todo> q =_db.Todos.AsNoTracking();

            if (!string.IsNullOrEmpty(term))
            {
                var t =term.Trim();
                q = q.Where(x => (x.Title != null && EF.Functions.Like(x.Title,$"%{t}%")) ||
                (x.Description != null && EF.Functions.Like(x.Description, $"%{t}%")));

                if (priority.HasValue) //değeri varmı bakıyoruz
                    q = q.Where(x => x.Priority.Equals(priority.Value));//varsa eğer parametreeden gelen priority değerine eşit olsun
               
                if(isDone.HasValue)
                    q = q.Where(x => x.IsDone.Equals(isDone.Value));

                q = dueDateAsc == false
                    ? q.OrderByDescending(x => x.DueDate ?? DateTime.MinValue)
                    : q.OrderBy(x => x.DueDate ?? DateTime.MaxValue);
                
            }
            return q.ToList();

        }

        public bool Update(Todo todo)
        {
            var exists = _db.Todos
                .Any(x=> x.Id.Equals(todo.Id)); //varmı kontrol

            if (!exists)
                return false;
            _db.Todos.Update(todo);
            _db.SaveChanges();
            _logger.LogInformation($"Görev güncellendi: {todo.Title}");
            return true;
        }
    }
}
