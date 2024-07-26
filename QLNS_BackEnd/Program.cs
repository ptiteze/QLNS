using Microsoft.EntityFrameworkCore;
using QLNS_BackEnd.Data;
using QLNS_BackEnd.Helper;
using QLNS_BackEnd.Interfaces;
using QLNS_BackEnd.Repositories;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<ICatalog, CatalogRepository>()
				.AddTransient<IBoardnew, BoardnewRepository>()
				.AddTransient<IReview, ReviewRepository>()
				.AddTransient<IProduct, ProductRepository>()
				.AddTransient<ICart, CartRepository>()
				.AddTransient<IUser, UserRepository>()
				.AddTransient<IAdmin, AdminRepository>()
				.AddTransient<ISlide, SlideRepository>();
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
builder.Services.AddDbContext<DataContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//var app = builder.Build();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();