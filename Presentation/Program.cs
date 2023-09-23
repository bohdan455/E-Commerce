using Presentation.ApiRequests;
using Presentation.ApiRequests.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("Api", client =>
{
    var url = builder.Configuration["ApiUrl"] ?? throw new ArgumentException("Provide Api url in app config");
    client.BaseAddress = new Uri(url);
});

builder.Services.AddScoped<IProductApiRequests, ProductApiRequests>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
