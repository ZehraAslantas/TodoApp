using Microsoft.EntityFrameworkCore;
using TodoApp.Models;
using TodoApp.Repositories.Config;

namespace TodoApp.Repositories;

public class TodoDbContext(DbContextOptions<TodoDbContext> options) 
    : DbContext(options) //bunu yazdık şimdi appsettings.json da yazıcaz.(ConnectionString)
{
    /*TodoDbContext newlendiğinde options ifadesi aracılığıyla 
     * appsettings.json daki connection strings ifadesini alıp,çözümleyip
     * base clasa(DbContext e göndericez ve bunu için de program.cs de servis kaydı yapıyoruz)*/
    public DbSet<Todo> Todos{ get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoDbContext).Assembly);
        /*Projeye eklediğin tüm konfigürasyon sınıflarını tek tek elle yazmak yerine,
        tek bir satırla otomatik olarak bulup DbContext'e kaydetmektir.*/
        base.OnModelCreating(modelBuilder);
    }

}
