using HumanResourceManagement.Api;
using HumanResourceManagement.Api.Extensions;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text.Json.Serialization;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    Log.Information("Starting web application");
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddKeyVaultIfConfigured(builder.Configuration);

    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices(builder.Configuration);
    builder.Services.AddWebServices();

    // Register the Swagger generator
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "HR Management API", Version = "v1" });

        // Optional: Set the comments path for the Swagger JSON and UI.
        // This enables the Swagger UI to display XML comments from controllers.
        ////var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        ////var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        ////c.IncludeXmlComments(xmlPath);
    });

    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

    var app = builder.Build();

    app.ConfigureExceptionHandler();
    
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        ////await app.InitialiseDatabaseAsync();
    }
    else
    {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHealthChecks("/health");
    app.UseHttpsRedirection();
    app.UseStaticFiles();


    app.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=index}/{id?}");

    app.MapRazorPages();

    app.MapFallbackToFile("index.html");

    //app.UseExceptionHandler(options => 
    //{    
    //});

    #if (UseApiOnly)
    app.Map("/", () => Results.Redirect("/api"));
    #endif


    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();

    // Enable middleware to serve Swagger UI (HTML, JS, CSS, etc.),
    // specifying the Swagger JSON endpoint.
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HR Management API");
    });

    app.MapEndpoints();

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program { }