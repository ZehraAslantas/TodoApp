using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Models;

namespace TodoApp.Repositories.Config
{
    public class TodoConfiguration : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.ToTable("Tables"); //tablo ismi Tables olsun dedik.DbSet de yapmıştık bu ismi
            
            builder.HasKey(x => x.Id); //Id primary key olsun dedik
            
            builder.Property(x => x.Title) //başlık özelliği
                .IsRequired() //zorunlu olarak dolmalı
                .HasMaxLength(200); //max 200 karakter

            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            builder.Property(x => x.Priority)
               .HasConversion<int>() //integer a çevir
               .HasDefaultValue(ToDoPriority.Low) //varsayılan değeri olsun düşük öncelik şeklinde
               .IsRequired();

            builder.Property(x => x.IsDone)
                .HasDefaultValue(false)
                .IsRequired();

            //altta 3 tane indexlenme yazdık.amacımız ilgili ifadeleri sorgulama hızımızı artırır.mesela tarihe göre süreklim sorgular
            builder.HasIndex(x => x.DueDate);
            builder.HasIndex(x => x.Priority);
            builder.HasIndex(x => x.IsDone); //tamamlandımı diye kullanıcı sorgularsa diye ekledik

            //şimdi aşağıda çekirdek dataları ekliyoruz
            var today = DateTime.Today; //gün bilgisini alıyoruz ve altta hasdata ile params yapısı koyarak virgül ile sonsuz olarak veri ekliyoruz
            builder.HasData(
                new Todo //Todo sınıfından nesne üretiyoruz ve guid ifadesi manuel verilmek zorunda
                {
                    Id=Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    //guid değerini google da "guid generator" sitelerinde oluşturup burada 1 ler yerine onu yazabilirsin
                    Title= "Alışveriş yap",
                    Description= "Süt,ekmek,yumurta",
                    Priority=ToDoPriority.Medium,
                    DueDate=today.AddDays(1),
                    IsDone=false
                },
                new Todo() {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Title = "Sunum Hazırla",
                    Description = "Pazartesi toplantısı için slaytlar", 
                    Priority = ToDoPriority.High, 
                    DueDate = today.AddDays(3), 
                    IsDone = false 
                },
                new Todo() {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Title = "Spor", 
                    Description = "30 dk koşu", 
                    Priority = ToDoPriority.Low, 
                    DueDate = today.AddDays(2), 
                    IsDone = true
                },
                new Todo() {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Title = "Araba bakımı",
                    Description = "Yağ değişimi ve filtreler", 
                    Priority = ToDoPriority.Medium, 
                    DueDate = today.AddDays(7),
                    IsDone = false
                }

                );
        
        
        }
    }
}
