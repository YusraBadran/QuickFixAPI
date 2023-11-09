using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuickFix.Dbcontexts;
using QuickFix.Identity.Shared.Models;
using QuickFix.Identity.Shared.Models.Security.Jwt;
using QuickFix.Middlewares;

var builder = WebApplication.CreateBuilder(args);
string MyAllowSpecificOrigins = "_MaAllowSpecificOrigins";

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors(options =>
options.AddPolicy(name: MyAllowSpecificOrigins,
    builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    })
);
builder.Services.AddIdentity<ApplicationUser,ApplicationRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    PasswordOptions passwordOptions = new PasswordOptions
    {
        RequireDigit = false,
        RequiredLength = 6,
        RequireLowercase = false,
        RequireNonAlphanumeric = false,
        RequireUppercase = false
    };
    options.Password = passwordOptions;
}).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
/// <summary>
/// Add MediatR
/// </summary>
builder.Services.AddMediatR(cfg=> cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(key: nameof(JwtOptions)));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(jwt =>
{
    var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection(key: "JwtConfig:Secrety").Value);
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey  = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime = false,
        ValidateLifetime = true
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IJwtService,JwtService>();

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
