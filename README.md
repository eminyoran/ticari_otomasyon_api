# Ticari Otomasyon API

.NET Core Web API ile geliştirilmiş ticari otomasyon sistemi backend API'si.

## Özellikler

### ✅ Tamamlanan Modüller

1. **Cari Yönetimi**
   - Cari listesi, detay, oluşturma, güncelleme, silme
   - Cari hareketleri ve ekstre takibi

2. **Ürün Yönetimi**
   - Ürün listesi, detay, CRUD işlemleri
   - Stok takibi

3. **Fatura Yönetimi**
   - Fatura listesi, detay, CRUD işlemleri
   - Fatura kalemleri yönetimi
   - KDV hesaplamaları

4. **Kasa Yönetimi**
   - Kasa listesi, detay, CRUD işlemleri
   - Bakiye takibi

5. **Banka Yönetimi**
   - Banka hesap listesi, detay, CRUD işlemleri
   - Bakiye takibi

6. **Kullanıcı Yönetimi**
   - JWT tabanlı kimlik doğrulama
   - Kullanıcı kayıt ve giriş

## Teknolojiler

- **.NET 8.0**: Framework
- **Entity Framework Core 9.0**: ORM
- **PostgreSQL**: Veritabanı
- **Dapper**: Performanslı SQL sorguları için
- **JWT Bearer Authentication**: Güvenli API erişimi
- **BCrypt**: Şifre hashleme
- **Swagger/OpenAPI**: API dokümantasyonu

## Kurulum

### Gereksinimler

- .NET 8.0 SDK veya üzeri
- PostgreSQL 12+ veya Docker ile PostgreSQL
- Visual Studio 2022 veya VS Code

### Adımlar

1. Projeyi klonlayın:
```bash
git clone https://github.com/eminyoran/ticari_otomasyon_api.git
cd OtomasyonApi
```

2. Yapılandırma dosyasını oluşturun:
   - `appsettings.json.example` dosyasını kopyalayıp `appsettings.json` olarak kaydedin
   - `appsettings.json` dosyasında `ConnectionStrings:DefaultConnection` değerini güncelleyin
   - JWT Key'i güvenli bir değerle değiştirin
   - **Not**: `appsettings.json` dosyası git'e eklenmez (güvenlik için)

3. NuGet paketlerini geri yükleyin:
```bash
dotnet restore
```

4. Veritabanı migration'larını uygulayın:
```bash
dotnet ef database update
```

5. Uygulamayı çalıştırın:
```bash
dotnet run
```

API `https://localhost:5001` veya `http://localhost:5000` adresinde çalışacaktır.

## Veritabanı Yapılandırması

### PostgreSQL Bağlantı String Örneği

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=ticari_otomasyon;Username=postgres;Password=your_password"
  }
}
```

### Migration Komutları

```bash
# Yeni migration oluşturma
dotnet ef migrations add MigrationName

# Migration'ları veritabanına uygulama
dotnet ef database update

# Migration'ı geri alma
dotnet ef database update PreviousMigrationName
```

## API Endpoints

### Kimlik Doğrulama

- `POST /api/users/login` - Kullanıcı girişi
- `POST /api/users/register` - Yeni kullanıcı kaydı

### Cari

- `GET /api/cari` - Cari listesi
- `GET /api/cari/{id}` - Cari detay
- `POST /api/cari` - Yeni cari oluştur
- `PUT /api/cari/{id}` - Cari güncelle
- `DELETE /api/cari/{id}` - Cari sil

### Ürün

- `GET /api/urun` - Ürün listesi
- `GET /api/urun/{id}` - Ürün detay
- `POST /api/urun` - Yeni ürün oluştur
- `PUT /api/urun/{id}` - Ürün güncelle
- `DELETE /api/urun/{id}` - Ürün sil

### Fatura

- `GET /api/fatura` - Fatura listesi
- `GET /api/fatura/{id}` - Fatura detay
- `POST /api/fatura` - Yeni fatura oluştur
- `PUT /api/fatura/{id}` - Fatura güncelle
- `DELETE /api/fatura/{id}` - Fatura sil

### Kasa

- `GET /api/kasa` - Kasa listesi
- `GET /api/kasa/{id}` - Kasa detay
- `POST /api/kasa` - Yeni kasa oluştur
- `PUT /api/kasa/{id}` - Kasa güncelle
- `DELETE /api/kasa/{id}` - Kasa sil

### Banka

- `GET /api/banka` - Banka listesi
- `GET /api/banka/{id}` - Banka detay
- `POST /api/banka` - Yeni banka hesabı oluştur
- `PUT /api/banka/{id}` - Banka hesabı güncelle
- `DELETE /api/banka/{id}` - Banka hesabı sil

## Swagger Dokümantasyonu

Uygulama çalışırken Swagger UI'ya şu adresten erişebilirsiniz:
- `https://localhost:5001/swagger` (HTTPS)
- `http://localhost:5000/swagger` (HTTP)

## CORS Yapılandırması

API, Flutter Web uygulaması için CORS politikası ile yapılandırılmıştır. Production ortamında CORS ayarlarını daha kısıtlayıcı hale getirmeniz önerilir.

## Proje Yapısı

```
OtomasyonApi/
├── Controllers/          # API Controller'ları
│   ├── CariController.cs
│   ├── UrunController.cs
│   ├── FaturaController.cs
│   ├── KasaController.cs
│   ├── BankaController.cs
│   └── UsersController.cs
├── Data/                 # Veritabanı context
│   ├── AppDbContext.cs
│   └── DapperContext.cs
├── DTOs/                 # Data Transfer Objects
│   ├── CariDto.cs
│   ├── UrunDto.cs
│   └── ...
├── Migrations/           # Entity Framework migrations
├── Models/               # Entity modelleri
│   ├── Cari.cs
│   ├── Urun.cs
│   ├── Fatura.cs
│   └── ...
├── Repositories/         # Data access layer
│   ├── UserRepository.cs
│   └── CariHareketRepository.cs
├── Services/             # Business logic layer
│   ├── CariService.cs
│   ├── UrunService.cs
│   └── ...
├── Utils/                # Yardımcı sınıflar
│   └── PasswordHasher.cs
├── Program.cs            # Uygulama giriş noktası
└── appsettings.json      # Konfigürasyon
```

## Güvenlik Notları

⚠️ **Önemli**: 
- `appsettings.Development.json` ve `appsettings.Production.json` dosyaları `.gitignore`'da olmalıdır
- Production ortamında JWT secret key'i güvenli bir şekilde saklanmalıdır
- Connection string'lerde şifreler environment variable olarak tutulmalıdır
- HTTPS kullanılması önerilir

## Frontend Entegrasyonu

Bu API, [Ticari Otomasyon Mobile](https://github.com/eminyoran/ticari_otomasyon_mobile) Flutter uygulaması ile kullanılmak üzere geliştirilmiştir.

API base URL'ini Flutter uygulamasında `lib/core/config/api_config.dart` dosyasında yapılandırabilirsiniz.

## Geliştirme

### Test

```bash
# Testleri çalıştırma (eğer test projesi varsa)
dotnet test
```

### Build

```bash
# Release build
dotnet build -c Release

# Publish
dotnet publish -c Release -o ./publish
```

## Lisans

Bu proje özel bir projedir.

