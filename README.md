# 🛒 ASP.NET Core 9.0 ve PostgreSQL ile Ogani E-Ticaret Sitesi
Bu repository, M&Y Yazılım Akademi bünyesinde yaptığım on altıncı proje olan ASP.NET Core 9.0 ve PostgreSQL ile Ogani E-Ticaret Sitesi projesini içermektedir. Bu eğitimde bana yol gösteren Murat Yücedağ'a çok teşekkür ederim.

Bu proje, ASP.NET Core 9.0 ve PostgreSQL kullanılarak geliştirilmiş, temel e-ticaret işlevlerini barındıran modern bir web uygulamasıdır. Proje tek katmanlı bir yapıda, folder structure prensiplerine uygun olarak tasarlanmış ve gereksiz sınıflar kullanılmadan sade, okunabilir bir mimari anlayışıyla geliştirilmiştir. Geliştirme sürecinde performans, sürdürülebilirlik ve kod okunabilirliği ön planda tutulmuştur.

Sistem, sipariş istatistikleri, ürün listeleme, satış tahmini ve veri analitiği gibi modülleri bir araya getirir. Ayrıca, ML.NET ile satış tahmini gibi yapay zekâ destekli özellikleriyle, klasik stok yönetimi uygulamalarının ötesinde bir kullanıcı deneyimi sunar.

Veri tabanı olarak tamamen ücretsiz olan PostgreSQL üzerinde ilişkisel tablolar tasarlanmış ve Ürünler, Kategoriler, Siparişler, Müşteriler gibi temel entity’ler için dinamik veri yapıları oluşturulmuştur. Bu sayede proje sadece bir demo değil, gerçek bir sektörel uygulamaya dönüştürülebilecek nitelikte güçlü bir temel kazanmıştır. Projede eksiklikler muhakkak vardır. Bu bir eğitim projesidir.

---

### ⚙️ Proje Özellikleri
- 🧩 **Veritabanı Yapısı:** Category, Product, Customer, Order ve Log tabloları PostgreSQL üzerinde yapılandırıldı.
- 🐘 **PostgreSQL Entegrasyonu:** Tüm veriler PostgreSQL üzerinde saklanmakta, güvenli CRUD işlemleri yapılmaktadır.
- 🍎 **Ürün Verisi:** Yapay Zeka yardımıyla oluşturulan 250 adet yiyecek ürünü (meyve, sebze, içecek, tatlı, tuzlu vb.) veritabanına insert sorgusu olarak eklendi.
- 👤 **Müşteri Verisi:**  Yapay Zeka kullanılarak hazırlanan 500 adet Türkçe müşteri verisi PostgreSQL’e aktarıldı.
- 📦 **Sipariş Verisi:** 100.000 adet sipariş verisi CSV dosyası olarak hazırlanıp sisteme yüklendi.
- 🧠 **Admin Paneli:** CRUD işlemlerinin yapılabildiği modern bir yönetim paneli oluşturuldu.
- 📊 **Dashboard & Analitik:** Admin panelinde widget’lar, istatistik kartları, tablo ve grafikler içeren bir dashboard yer almakta.
- 🍳 **Yemek Öneri Özelliği (AI Integration):** Kullanıcı elindeki malzemeleri girerek Rapid API üzerinden yapay zekâ destekli yemek önerileri alabiliyor.
- 💬 **WhatsApp Entegrasyonu:** Ana Sayfada bulunan telefon numarasından WhatsApp Web üzerinden iletişim başlatılabiliyor.
- 📈 **Sipariş Tahmin Analizi:** ML.NET kullanılarak 2025 verilerine göre 2026’nın ilk 3 ayına ait şehir bazlı sipariş tahmini yapılmakta.
- 🥇 **Müşteri Segmentasyonu:**<br>
Altın Müşteriler: 210+ sipariş<br>
Gümüş Müşteriler: 180-209 sipariş<br>
Bronz Müşteriler: 179 dan daha az sipariş<br>
Bu segment dağılımı dashboard üzerinde pie chart olarak gösterilmektedir.
- 🗺️ **Harita Entegrasyonu:** Leaflet kullanılarak Türkiye haritası üzerinde şehir bazlı sipariş yoğunluğu heatmap olarak gösterilmektedir.
Şehir seçildiğinde toplam sipariş sayısı, ortalama sipariş tutarı ve en çok tercih edilen kategori bilgileri görüntülenir.
- 🧾 **Loglama Sistemi:** Admin panelindeki her CRUD işlemi (ekleme, silme, güncelleme) Log tablosuna kaydedilir.
Log tablosunda şu sütunlar bulunur: LogId, UserName, ActionType, Entity, Description, Date.<br>
Son işlemler Admin Paneli'nde “Yapılan İşlemler” bölümünde listelenir.

---

## 🚀 Kullandığım Teknolojiler

- 💻 ASP.NET Core 9.0 (MVC) - Modern .NET altyapısı ve güçlü backend yapısı
- 🐘 PostgreSQL - 	İlişkisel veritabanı yönetimi
- 💎 Entity Framework Core - ORM aracı ile veritabanı işlemleri
- 🔄 AutoMapper - Entity–DTO dönüşümleri için
- 🤖 ML.NET - Satış tahmini algoritmaları için
- 🌐 RapidAPI - AI destekli sohbet entegrasyonu
- 🧱 Tek Katmanlı Mimari - Temiz, modüler ve ölçeklenebilir yapı
- 🧼 Clean Code Prensipleri & Folder Structure Düzeni
- 🧩 ViewComponent - Tekrarlayan UI bileşenlerinin yönetimi
- 🎨 HTML5, CSS3, Bootstrap, JavaScript - Modern ve responsive UI tasarımı

---

## 🧭 Proje Bölümleri

### 🏠 Ana Sayfa
Kullanıcılar bu bölümde:
- E-Ticaret sitesinde yer alan ürünleri kategori bazında görüntüleyebilir.
- Ürünleri kategoriye ve fiyat aralığına göre filtreleyebilir.
- Ürün detaylarını inceleyebilir ve ürünlerle ilgili bilgilere kolayca ulaşabilir.
- Bize Ulaşın bölümünden yöneticiye mesaj gönderebilir.

### 🧮 Admin Paneli
Yönetici bu panelde:
- Kategori, ürün, müşteri ve sipariş gibi varlıklar üzerinde CRUD (Create, Read, Update, Delete) işlemleri gerçekleştirebilir.
- Sipariş verilerini analiz ederek tahminleme (ML.NET ile) yapabilir.
- Sipariş istatistiklerini Türkiye haritası üzerinden görüntüleyebilir.
- Girilen malzemelere göre yemek önerileri almak için Rapid API entegrasyonunu kullanabilir.

---

## 💡 Genel Değerlendirme
Ogani E-Ticaret Sitesi, klasik bir e-ticaret sitesi projesinin ötesinde; AI destekli tahmin ve modern katmanlı mimarisi ile sektörel düzeyde bir altyapı sunar.
Proje eğitim amaçlı olarak geliştirilmiştir, ancak mevcut mimarisi ile gerçek bir işletmede uygulanabilir düzeydedir.

---

## 🖼️ Projeden Ekran Görüntüleri

### ➡️ Ana Sayfa
<div align="center">
  <img src="https://github.com/melihcolak0/OganiECommerce/blob/08f36fe3dae5060d23bc6e1395bbe9102e6af9ef/ss/screencapture-localhost-7292-Default-Index-2025-10-25-15_34_56.png" alt="image alt">
</div>
<div align="center">
  <img src="https://github.com/melihcolak0/OganiECommerce/blob/08f36fe3dae5060d23bc6e1395bbe9102e6af9ef/ss/screencapture-localhost-7292-Default-Products-2025-10-25-15_35_54.png" alt="image alt">
</div>
<div align="center">
  <img src="https://github.com/melihcolak0/OganiECommerce/blob/08f36fe3dae5060d23bc6e1395bbe9102e6af9ef/ss/screencapture-localhost-7292-Default-ProductDetail-73-2025-10-25-15_35_38.png" alt="image alt">
</div>
<div align="center">
  <img src="https://github.com/melihcolak0/OganiECommerce/blob/08f36fe3dae5060d23bc6e1395bbe9102e6af9ef/ss/screencapture-localhost-7292-Default-Contact-2025-10-25-15_36_57.png" alt="image alt">
</div>
<br/>

### ➡️ Admin Paneli
<div align="center">
  <img src="https://github.com/melihcolak0/OganiECommerce/blob/08f36fe3dae5060d23bc6e1395bbe9102e6af9ef/ss/screencapture-localhost-7292-Dashboard-Index-2025-10-25-15_38_09.png" alt="image alt">
</div>
<div align="center">
  <img src="https://github.com/melihcolak0/OganiECommerce/blob/08f36fe3dae5060d23bc6e1395bbe9102e6af9ef/ss2/screencapture-localhost-7292-Category-Index-2025-10-25-20_16_36.png" alt="image alt">
</div>
<div align="center">
  <img src="https://github.com/melihcolak0/OganiECommerce/blob/08f36fe3dae5060d23bc6e1395bbe9102e6af9ef/ss2/screencapture-localhost-7292-Customer-Index-2025-10-25-20_18_35.png" alt="image alt">
</div>
<div align="center">
  <img src="https://github.com/melihcolak0/OganiECommerce/blob/08f36fe3dae5060d23bc6e1395bbe9102e6af9ef/ss/screencapture-localhost-7292-Customer-CreateCustomer-2025-10-25-15_39_24.png" alt="image alt">
</div>
<div align="center">
  <img src="https://github.com/melihcolak0/OganiECommerce/blob/08f36fe3dae5060d23bc6e1395bbe9102e6af9ef/ss/screencapture-localhost-7292-Customer-UpdateCustomer-255-2025-10-25-15_39_46.png" alt="image alt">
</div>
<div align="center">
  <img src="https://github.com/melihcolak0/OganiECommerce/blob/08f36fe3dae5060d23bc6e1395bbe9102e6af9ef/ss2/screencapture-localhost-7292-Product-Index-2025-10-25-20_19_22.png" alt="image alt">
</div>
<div align="center">
  <img src="https://github.com/melihcolak0/OganiECommerce/blob/08f36fe3dae5060d23bc6e1395bbe9102e6af9ef/ss2/screencapture-localhost-7292-Order-Index-2025-10-25-20_19_10.png" alt="image alt">
</div>
<div align="center">
  <img src="https://github.com/melihcolak0/OganiECommerce/blob/08f36fe3dae5060d23bc6e1395bbe9102e6af9ef/ss/screencapture-localhost-7292-Recipe-Index-2025-10-25-15_48_34.png" alt="image alt">
</div>
<div align="center">
  <img src="https://github.com/melihcolak0/OganiECommerce/blob/08f36fe3dae5060d23bc6e1395bbe9102e6af9ef/ss/screencapture-localhost-7292-Forecast-Index-2025-10-25-15_45_31.png" alt="image alt">
</div>
<div align="center">
  <img src="https://github.com/melihcolak0/OganiECommerce/blob/08f36fe3dae5060d23bc6e1395bbe9102e6af9ef/ss2/screencapture-localhost-7292-Log-Index-2025-10-25-20_19_00.png" alt="image alt">
</div>
