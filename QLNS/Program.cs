using Microsoft.EntityFrameworkCore;
using QLNS.Helper;
using QLNS.Interfaces;
using QLNS.Models;
using QLNS.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7156") });
builder.Services.AddTransient<ICatalog, CatalogRepository>()
				.AddTransient<IBoardnew, BoardnewRepository>()
				.AddTransient<IReview, ReviewRepository>()
				.AddTransient<IProduct, ProductRepository>()
				.AddTransient<ICart, CartRepository>()
                .AddTransient<IUser, UserRepository>()
                .AddTransient<IAdmin, AdminRepository>()
                .AddTransient<IProducer, ProducerRepository>()
                .AddTransient<ISupplyList, SupplyListRepository>()
                .AddTransient<ISupplyInvoice, SupplyInvoiceRepository>()
				.AddTransient<IImportDetail, ImportDetailRepository>()
                .AddTransient<IOrder, OrderRepository>()
                .AddTransient<IOrdered, OrderedRepository>()
                .AddTransient<ISlide, SlideRepository>()
                .AddTransient<IAccount, AccountRepository>();
				
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
	// Configure session options here
	options.Cookie.Name = "YourSessionCookieName";
	options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
	// Add other session options as needed
});
// List Sesstion

//Username ---- Tên đăng nhập
//length_order ----- số sản phẩm được thêm vào giỏ hàng
//Fullname ---- Tên người dùng
//sumprice ---- Tổng giá
//cart_local ----- list sản phẩm-số lượng trong giỏ hàng
//errorMsg ---- thông báo lỗi
//Role
//id_user ---- mã khách/nvien
// End list Sesstion
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
//builder.Services.AddDbContext<DataContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//});
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
