
# Asp.Net Core 7 MVC E Ticaret Projesi

TR

ASP.NET Core 7 ve MVC mimarisiyle yapılmış e-ticaret projesi. Bu proje, modern bir alışveriş deneyimi sunmak üzere tasarlanmıştır.

Projenin temel özellikleri arasında katmanlı mimari yapısı, güvenli kimlik doğrulama, alışveriş sepeti yönetimi ve ürün adeti artışında Ajax kullanımı bulunmaktadır. Kullanıcı eğer giriş yapmamış ise sepeti sunucu hafızasında tutuluyor. Eğer kullanıcı giriş yaparsa sepet veri tabanında tutuluyor.

EN

E-commerce Project Built with ASP.NET Core 7 and MVC Architecture. This project is designed to provide a modern shopping experience, developed using ASP.NET Core 7 and MVC architecture.

Key features of our project include a layered architectural structure, secure identity authentication, shopping cart management, and the use of Ajax for increasing product quantities. If the user is not logged in, the cart is stored in the server's memory. Once the user logs in, the cart is stored in the database.


## Kullanılan Teknolojiler Ve Mimari / Technologies Used and Architecture

- Katmanlı Mimari / Onion Pattern
- Asp.Net Core 7
- Ajax
- N Tier Architecture (Entity Layer, Service Layer, DataAccess Layer, UI Layer)
- Identity Authorization
- Entity Framework Core
- MS Sql
- Generic Repository
- Unit Of Work
- LINQ
- JavaScript
- Bootstrap
  
## Kullanım / Usage



- TR -> Projeyi çalıştırmak için UI katmanındaki appsettings.json dosyasındaki ConnConnectionStrings altındaki "ConnStr" kısmını kendinize göre ayarlamanız gerekmektedir

- EN -> To run the project, configure the "ConnStr" section under ConnConnectionStrings in the appsettings.json file in the UI layer according to your environment.


```json
{
  "ConnectionStrings": {
    "ConnStr": "Bu kısmı kendinize göre ayarlamanız gereklidir"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

```

  
