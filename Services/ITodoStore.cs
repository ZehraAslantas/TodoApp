using TodoApp.Models;

namespace TodoApp.Services;

public interface ITodoStore
{
    IEnumerable<Todo> GetAll();
    //Todo clasını kullanacak ve ordaki verilerin hepsini alacak.Todo clasını kullanmak için üste using TodoApp.Models yapısı eklendi.Çünkü o models klasörünün sınıfı kullanılacak(ContactApp projesiyle aynı mantık)
    IEnumerable<Todo> Search(String? term, ToDoPriority? priority,bool? isDone,bool? dueDateAsc );
    Todo? Get(Guid id);
    void Add(Todo todo);
    bool Update(Todo todo);
    bool Delete(Guid id);

}

