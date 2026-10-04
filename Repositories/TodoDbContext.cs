using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.Repositories;

public class TodoDbContext(DbContextOptions<TodoDbContext> options) 
    : DbContext(options)
{
    public DbSet<Todo> Todos{ get; set; }

}
