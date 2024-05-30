using ApiTajamarAWS.Data;
using ApiTajamarAWS.Helpers;
using ApiTajamarAWS.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
namespace ApiTajamarAWS;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        string connectionString = Configuration.GetConnectionString("MySql");
        services.AddDbContext<TajamarContext>
           (options => options.UseMySql(connectionString,
           ServerVersion.AutoDetect(connectionString)));

        services.AddTransient<RepositoryUsuarios>();
        services.AddTransient<RepositoryEmpresa>();
        services.AddTransient<RepositoryEntrevista>();

        HelperActionServicesOAuth helper = new HelperActionServicesOAuth(Configuration);
        services.AddSingleton<HelperActionServicesOAuth>(helper);
        services.AddAuthentication(helper.GetAuthenticateSchema()).AddJwtBearer(helper.GetJwtBearerOptions());

        services.AddDbContext<TajamarContext>
            (options => options.UseMySql(connectionString,
            ServerVersion.AutoDetect(connectionString)));

        services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin", x => x.AllowAnyOrigin());
        });
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Api Tajamar AWS",
                Version = "v1"
            });
        });

        services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

      app.UseCors(options => options.AllowAnyOrigin());
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint(url: "swagger/v1/swagger.json",
                "ApiTajamarAWs");
            options.RoutePrefix = "";
        });
        app.UseHttpsRedirection();
 
        app.UseRouting();
 
        app.UseAuthorization();


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}