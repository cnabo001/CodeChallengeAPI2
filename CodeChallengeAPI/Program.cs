using CodeChallengeAPI.Interfaces;
using CodeChallengeAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .WithOrigins("http://127.0.0.1:4200") 
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()); 
});

builder.Services.AddScoped<ICRUDService, CRUDService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    var configuration = app.Configuration;
    var appSettings = configuration.GetSection("AppSettings");
    var useHttps = appSettings.GetValue<bool>("UseHttps");

    if (!useHttps)
    {
        app.Use((context, next) =>
        {
            context.Request.Scheme = "http";
            return next();
        });
    }
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
