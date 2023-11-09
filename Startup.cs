using GeoBuyerParser.DB;
using GeoBuyerParser2.Repositories;
using GeoBuyerParser2.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

public class Startup
{
    private IWebHostEnvironment _env;

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        _env = env;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        Directory.CreateDirectory($"{_env.ContentRootPath}/db_file/");
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite($"Data Source={_env.ContentRootPath}/db_file/app.db");
        }, ServiceLifetime.Singleton);

        services.AddSingleton<Repository>();

        services.AddScoped<OSMService>();

        // Add necessary services for the web API
        services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
