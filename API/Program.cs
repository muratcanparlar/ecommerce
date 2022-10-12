using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebApiDatabase"));
});

var app = builder.Build();

//Configure(app);
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

void Configure(WebApplication host)
{
    using var scope = host.Services.CreateScope();
    var services = scope.ServiceProvider;
    var logger= services.GetRequiredService<ILogger<Program>>();

    try
    {
        var dbContext = services.GetRequiredService<StoreContext>();

        if (dbContext.Database.IsSqlServer())
        {
            dbContext.Database.Migrate();
            DbInitializer.Initialize(dbContext);
        }

       
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Problem migrating data");
    }
}