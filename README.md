# TodoApp - ASP.NET Core Razor Pages

ASP.NET Core Razor Pages mimarisi kullanılarak geliştirilmiş, tam fonksiyonlu bir CRUD (Create, Read, Update, Delete) görev yönetim uygulaması.

## 🚀 Özellikler
- **Tam CRUD Desteği:** Görev ekleme, listeleme, detaylı düzenleme ve silme.
- **Sunucu Taraflı Doğrulama (Server-Side Validation):** `ModelState` ve Tag Helper'lar ile veri bütünlüğü.
- **Modüler Yapı:** Yeniden kullanılabilir formlar için Razor Partial View (`_TodoForm`) kullanımı.
- **Soyutlanmış Veri Katmanı:** Gevşek bağlılık (loose coupling) için `ITodoStore` arayüzü ve `InMemoryTodoStore` uygulaması.
- **Kullanıcı Dostu Arayüz:** Bootstrap ile duyarlı (responsive) tasarım.

## 🛠️ Kullanılan Teknolojiler
- .NET 8 / C#
- ASP.NET Core Razor Pages
- Bootstrap 5