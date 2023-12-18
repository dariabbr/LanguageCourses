using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CoursesAPI.Helpers;
using CoursesAPI.Interfaces;
using CoursesAPI.Mapper;
using CoursesAPI.Models;
using CoursesAPI.Services;

var builder = WebApplication.CreateBuilder(args);
    
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<ICourseService, CourseService>();

builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("DefaultPolicy", p =>
    {
        p.AllowAnyHeader();
        p.AllowAnyOrigin();
        p.AllowAnyMethod();
    });
});

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<SyncActionFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("DefaultPolicy");
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();