using Microsoft.EntityFrameworkCore;
using TodoApp.Repositories;
using TodoApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//veritabaný servis kaydý altta
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=(localdb)\\MSSQLLocalDB; Database=TodoAppDb; Trusted_Connnection=True; TrustServerCertificate=True";

builder.Services.AddDbContext<TodoDbContext>(options =>
options.UseSqlServer(connectionString)
);

if (builder.Environment.IsDevelopment()) {// Development ortamda ise inmemory çalýþsýn yoksada EfTodoStore olsun
    //DI-Register
    builder
        .Services
        .AddSingleton<ITodoStore, InMemoryTodoStore>();
}
else
{
    builder
        .Services
        .AddScoped<ITodoStore, EfTodoStore>();
}
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//migration kurulumundan sonra alt taraf yazýldý.bekleyen migration varsa kurala göre otomatik olarak iþletilecek

if (app.Environment.IsProduction())
{
    using var scope = app.Services.CreateScope(); //starter scope oluþturduk
    try
    {
        var db = scope
            .ServiceProvider
            .GetRequiredService<TodoDbContext>();
        db.Database.Migrate();
    }
    catch(Exception ex)
    {
        var logger = scope
            .ServiceProvider
            .GetRequiredService<ILoggerFactory>()
           .CreateLogger("Startup");

        logger.LogError(ex, "Database migration failed");
        //log yerine hata da fýrlatabilirsin
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles(); //statik dosyalarý kullanma izni verdik
                      //veritabanýnda çalýþtýrýnca sayfa düzgün geldin diye bunu ve alttaki app.MapRazorPages(); yapýsýný ekledik ve üstündekileri yorum satýrý yaptýk
app.UseRouting();

app.UseAuthorization();

/*app.MapStaticAssets();
app.MapRazorPages()//hazýr geldi bu.yönlendirme burasý
   .WithStaticAssets();*/

app.MapRazorPages();

app.Run();
