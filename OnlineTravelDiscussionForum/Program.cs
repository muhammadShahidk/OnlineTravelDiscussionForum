using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using OnlineTravelDiscussionForum;
using OnlineTravelDiscussionForum.Data;
using OnlineTravelDiscussionForum.Dtos;
using OnlineTravelDiscussionForum.Interfaces;
using OnlineTravelDiscussionForum.Modals;
using OnlineTravelDiscussionForum.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);


//Add services to the container.
//auth 

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddDbContext<ForumDbContext>(options =>
{

    options.UseSqlServer(builder.Configuration.GetConnectionString("DiscussionFormDb"));
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEMailService, EmailService>();
builder.Services.AddScoped<IProfileService, ProfileService>();


// Add Identity
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ForumDbContext>()
    .AddDefaultTokenProviders();


// Config Identity
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 3;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedEmail = false;
//setup email configuration email correction
    options.User.RequireUniqueEmail = true;
    //options.Tokens.
});



// Add Authentication and JwtBearer 
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    });
    //.AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

builder.Services.AddControllers();
//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(builder =>
//    {
//        builder
//            .WithOrigins("http://localhost:4200", "http://192.168.10.21:8081", "http://localhost:8081")
//            //.AllowAnyOrigin()
//            .AllowAnyMethod()
//            .AllowAnyHeader();
//    });
//});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

//app.UseCors("AllowAll");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inject app Dependencies (Dependency Injection)





// pipeline
var app = builder.Build();





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ApiResponseMiddleware>();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
