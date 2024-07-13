using Microsoft.EntityFrameworkCore;
using QLNS.Data;
using QLNS.Helper;
using QLNS.Interfaces;
using QLNS.Models;
using QLNS.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<ICatalog, CatalogRepository>()
				.AddTransient<IBoardnew, BoardnewRepository>()
				.AddTransient<IReview, ReviewRepository>()
				.AddTransient<IProduct, ProductRepository>()
				.AddTransient<ICart, CartRepository>()
                .AddTransient<IUser, UserRepository>()
                .AddTransient<IAdmin, AdminRepository>()
				.AddTransient<ISlide, SlideRepository>();
				
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
	// Configure session options here
	options.Cookie.Name = "YourSessionCookieName";
	options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
	// Add other session options as needed
});
// List Sesstion

//username
//length_order
//sumprice
//cart_local
//errorMsg

// End list Sesstion
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
