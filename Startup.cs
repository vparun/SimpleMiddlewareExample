public class Startup
{
    public IConfiguration configRoot
    {
        get;
    }

    public Startup(IConfiguration configuration)
    {
        configRoot = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors();
        app.UseAuthorization();

        app.MapControllers();

        app.Use(async (context, next) =>
        {
            Console.WriteLine($"Logic before executing the next delegate in the Use method");
            await next.Invoke();
            Console.WriteLine($"Logic after executing the next delegate in the Use method");
        });

        app.Run(async context =>
        {
            Console.WriteLine($"Writing the response to the client in the Run method");
            await context.Response.WriteAsync("Hello from the middleware component.");
        });

        app.Run();
    }
}