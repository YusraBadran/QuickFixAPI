using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuickFix.Dbcontexts;
using QuickFix.Middlewares;

var builder = WebApplication.CreateBuilder(args);
string MyAllowSpecificOrigins = "_MaAllowSpecificOrigins";

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddCors(options =>
options.AddPolicy(name: MyAllowSpecificOrigins,
    builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    })
);

/// <summary>
/// Add MediatR
/// </summary>
builder.Services.AddMediatR(cfg=> cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<HandlerMiddlewareErrors>(app.Environment);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
