using InventoryManagement.Data;
using InventoryManagement.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// **1️⃣ Konfigurasi koneksi ke MySQL**
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<InventoryContext>(options =>
    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(8, 0, 26)) // Sesuaikan dengan versi MySQL/MariaDB
    ));

// **2️⃣ Tambahkan layanan MVC + JSON case-insensitive**
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

// **3️⃣ (Opsional) Aktifkan CORS**
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

// **4️⃣ Middleware Error Handling**
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

// **5️⃣ Middleware umum**
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthorization();

// **6️⃣ Tambahkan Route untuk Dashboard, Sumur, dan Areas**
app.MapControllerRoute(
    name: "dashboard_sumur",
    pattern: "HomeSumur/{action=Index}/{id?}",
    defaults: new { controller = "HomeSumur" });

app.MapControllerRoute(
    name: "areas",
    pattern: "Areas/{action=Index}/{id?}",
    defaults: new { controller = "Areas" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// **7️⃣ Pastikan Database diupdate saat aplikasi mulai**
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<InventoryContext>();
    context.Database.Migrate(); // ✅ Pastikan migrasi berjalan otomatis saat aplikasi pertama kali dijalankan
}

// **8️⃣ Jalankan aplikasi**
app.Run();
