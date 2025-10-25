# 🛒 ASP.NET Core 9.0 ve PostgreSQL ile Ogani E-Ticaret Sitesi
Bu repository, M&Y Yazılım Akademi bünyesinde yaptığım on altıncı proje olan ASP.NET Core 9.0 ve PostgreSQL ile Ogani E-Ticaret Sitesi projesini içermektedir. Bu eğitimde bana yol gösteren Murat Yücedağ'a çok teşekkür ederim.

Bu proje, ASP.NET Core 9.0 ve PostgreSQL kullanılarak geliştirilmiş, temel e-ticaret işlevlerini barındıran modern bir web uygulamasıdır. Proje tek katmanlı bir yapıda, folder structure prensiplerine uygun olarak tasarlanmış ve gereksiz sınıflar kullanılmadan sade, okunabilir bir mimari anlayışıyla geliştirilmiştir. Geliştirme sürecinde performans, sürdürülebilirlik ve kod okunabilirliği ön planda tutulmuştur.

Sistem, sipariş istatistikleri, ürün listeleme, satış tahmini ve veri analitiği gibi modülleri bir araya getirir. Ayrıca, ML.NET ile satış tahmini gibi yapay zekâ destekli özellikleriyle, klasik stok yönetimi uygulamalarının ötesinde bir kullanıcı deneyimi sunar.

Veri tabanı olarak tamamen ücretsiz olan PostgreSQL üzerinde ilişkisel tablolar tasarlanmış ve Ürünler, Kategoriler, Siparişler, Müşteriler gibi temel entity’ler için dinamik veri yapıları oluşturulmuştur. Bu sayede proje sadece bir demo değil, gerçek bir sektörel uygulamaya dönüştürülebilecek nitelikte güçlü bir temel kazanmıştır. Projede eksiklikler muhakkak vardır. Bu bir eğitim projesidir.

---

### ⚙️ Proje Özellikleri
- 🧩 Veritabanı Yapısı: Category, Product, Customer ve Order tabloları PostgreSQL üzerinde yapılandırıldı.
- 🐘 PostgreSQL Entegrasyonu: Tüm veriler PostgreSQL üzerinde saklanmakta, güvenli CRUD işlemleri yapılmaktadır.
🍎 Ürün Verisi (Product Case): ChatGPT yardımıyla oluşturulan 250 adet yiyecek ürünü (meyve, sebze, içecek, tatlı, tuzlu vb.) veritabanına insert sorgusu olarak eklendi.
👤 Müşteri Verisi (Customer Case): ChatGPT kullanılarak hazırlanan 500 adet Türkçe müşteri verisi PostgreSQL’e aktarıldı.
📦 Sipariş Verisi (Order Case): 100.000 adet sipariş verisi CSV dosyası olarak hazırlanıp sisteme yüklendi.
🧠 Admin Paneli (Admin Case): CRUD işlemlerinin yapılabildiği modern bir yönetim paneli oluşturuldu.
📊 Dashboard & Analitik: Admin panelinde widget’lar, istatistik kartları, tablo ve grafikler içeren bir dashboard yer almakta.
🍳 Yemek Öneri Özelliği (AI Integration): Kullanıcı elindeki malzemeleri girerek Google Gemini üzerinden yapay zekâ destekli yemek önerileri alabiliyor.
🚫 Sepet Özelliği: Projede alışveriş sepeti bulunmamaktadır; sistem sadece veri analizi ve yönetim odaklı tasarlanmıştır.
💬 WhatsApp Entegrasyonu: Sağ üstteki telefon ikonuna tıklandığında WhatsApp Web üzerinden iletişim başlatılabiliyor.
📈 Sipariş Tahmin Analizi (ML.NET Case): ML.NET kullanılarak 2025 verilerine göre 2026’nın ilk 3 ayına ait şehir bazlı sipariş tahmini yapılmakta.
🥇 Müşteri Segmentasyonu:
Gold Customers: Ayda 5+ sipariş
Silver Customers: Ayda 2–4 sipariş
Bronze Customers: Ayda 1 veya daha az sipariş
Bu segment dağılımı dashboard üzerinde pie chart olarak gösterilmektedir.
🗺️ Harita Entegrasyonu (Map Case): Leaflet / Chart.js Map kullanılarak Türkiye haritası üzerinde şehir bazlı sipariş yoğunluğu heatmap olarak gösterilmektedir.
Şehir seçildiğinde toplam sipariş sayısı, ortalama sipariş tutarı ve en çok tercih edilen kategori bilgileri görüntülenir.
🧾 Loglama Sistemi: Admin panelindeki her CRUD işlemi (ekleme, silme, güncelleme) Log tablosuna kaydedilir.
Log tablosunda şu sütunlar bulunur: LogId, UserName, ActionType, Entity, Date.
Son işlemler dashboard’da “Son Aktiviteler” bölümünde listelenir.

---

## 🚀 Kullandığım Teknolojiler

- 💻 ASP.NET Core 9.0 (MVC) - Modern .NET altyapısı ve güçlü backend yapısı
- 🐘 PostgreSQL - 	İlişkisel veritabanı yönetimi
- ⚙️ Entity Framework Core - ORM aracı ile veritabanı işlemleri
- 🔄 AutoMapper - Entity–DTO dönüşümleri için
- 🤖 ML.NET - Satış tahmini algoritmaları için
- ⚡ SignalR - Gerçek zamanlı chatbot iletişimi
- 🌐 RapidAPI + ChatGPT - AI destekli sohbet entegrasyonu
- 🧱 Tek Katmanlı Mimari - Temiz, modüler ve ölçeklenebilir yapı
- 🧩 ViewComponent - Tekrarlayan UI bileşenlerinin yönetimi
- 🎨 HTML5, CSS3, Bootstrap, JavaScript - Modern ve responsive UI tasarımı

---

## 🧭 Proje Bölümleri

### 👨‍🍳 Ana Sayfa

Kullanıcılar burada:
- Bloom Bakery firmasının hakkımızda, hizmetlerimiz, şeflerimiz ve referanslarımız gibi bölümlerini inceleyebilir.
- Ürün kategorilerini ve fiyat aralıklarını filtreleyebilir ve ürünleri inceleyebilir.

### 🧮 Admin Paneli
- Ürün, kategori ve hizmetler gibi entity'lerin CRUD işlemleri yapılabilir.
- Sipariş yönetimi ve tahminleme yapabilir.
- ChatBot ile işlerini hızlandırabilir.
- Ürün ve Hakkımda bölümü oluştururken yapa zeka ile tek tıkta işlemlerini gerçekleştirebilir.
- Girilen malzemelerle hangi yemeklerin yapılabileceği görülebilir.

---

## 💡 Genel Değerlendirme
Bloom Bakery, klasik bir ürün yönetimi projesinin ötesinde; AI destekli tahmin, gerçek zamanlı etkileşim ve modern katmanlı mimarisi ile sektörel düzeyde bir altyapı sunar.
Proje eğitim amaçlı olarak geliştirilmiştir, ancak mevcut mimarisi ile gerçek bir işletmede uygulanabilir düzeydedir.

---

## 🖼️ Projeden Ekran Görüntüleri

### ➡️ Ana Sayfa
<div align="center">
  <img src="https://github.com/melihcolak0/BloomBakery/blob/4080ef4bb6c74d9e691b770ee85f71a672736fc7/ss/screencapture-localhost-7186-Default-Index-2025-10-19-13_15_50.png" alt="image alt">
</div>
