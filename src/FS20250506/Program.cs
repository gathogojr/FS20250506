using FS20250506.Data;
using FS20250506.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// Build the Edm model
var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntitySet<Customer>("Customers");
modelBuilder.EntitySet<Order>("Orders");

// Configure the OData service
builder.Services.AddControllers().AddOData(
    options =>
    {
        var model = modelBuilder.GetEdmModel();

        options.EnableQueryFeatures();
        options.AddRouteComponents(
            model: model);
        options.AddRouteComponents(
            routePrefix: "v2",
            model: model);
    });

// Configure swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure the DbContext
builder.Services.AddDbContext<FsDbContext>(
    options => options.UseInMemoryDatabase("FsDb"));

var app = builder.Build();

app.UseSwagger(); // Generates the Swagger JSON
app.UseSwaggerUI(); // Serves the Swagger UI
app.UseRouting();
app.MapControllers();

// Add seed data to the database
using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var db = serviceScope.ServiceProvider.GetRequiredService<FsDbContext>();

    FsDbHelper.SeedDb(db);
}

app.Run();
