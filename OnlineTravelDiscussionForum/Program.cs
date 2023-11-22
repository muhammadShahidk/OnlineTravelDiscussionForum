using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineTravelDiscussionForum.Modals;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//auth 
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ForumDataContext>(options =>
{

    options.UseSqlServer(builder.Configuration.GetConnectionString("DiscussionFormDb"));
});

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