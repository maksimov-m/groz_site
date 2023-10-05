using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Npgsql;
using System.Reflection;

namespace Geo_Back
{
    
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionStringUsers = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            NpgsqlDataSourceBuilder npgBuilderUser = new NpgsqlDataSourceBuilder(connectionStringUsers);
            using var dataSourceUsers = npgBuilderUser.Build();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(dataSourceUsers, builder => builder.EnableRetryOnFailure());
            });

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddMemoryCache();

            builder.Services.AddMvc();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "—ервис по индетификации пользователей",
                    Description = "—ервис управл€ет личными данными пользователей и их доступом к другим сервисам"
                });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            builder.Services.AddTransient<UserService>();



            var app = builder.Build();

            app.UseMigrationsEndPoint();
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "api/IdentityServer/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/api/IdentityServer/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}