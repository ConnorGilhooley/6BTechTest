using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SixBeeHealthTech.Components;
using SixBeeHealthTech.Components.DBContext;
using SixBeeHealthTech.Services;
using SixBeeHealthTech.Components.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllers();
builder.Services.AddServerSideBlazor();


builder.Services.AddScoped(sp => new HttpClient() { BaseAddress = new Uri(builder.Configuration["BaseAddress"] ?? "https://localhost:5043") });
builder.Services.AddScoped<AppointmentService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.MapControllers();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
