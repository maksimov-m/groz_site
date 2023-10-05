using Geo_Back.DBs;
using Geo_Back.Models;
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
            builder.Services.AddDbContext<UsersDB>(options =>
            {
                options.UseNpgsql(dataSourceUsers, builder => builder.EnableRetryOnFailure());
            });


            builder.Services.AddMemoryCache();

            builder.Services.AddMvc();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Сервис по индетификации пользователей",
                    Description = "Сервис управляет личными данными пользователей и их доступом к другим сервисам"
                });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            builder.Services.AddTransient<UserService>();
            builder.Services.AddTransient<IdentityService>();

            var app = builder.Build();
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

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var user_service = services.GetRequiredService<UserService>();
                var db_user = services.GetRequiredService<UsersDB>();
                {
                    int count = 0;
                    while (!db_user.Database.CanConnect())
                    {
                        Console.WriteLine($"Can't connect to DB. {connectionStringUsers}. Wait 5 sec.");
                        Task.Delay(500);
                        if (count > 100)
                            throw new Exception("Не возможно подключиться к БД");
                    }
                    try
                    {
                        db_user.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message, "An error occurred while seeding the database.");
                    }
                    db_user.SaveChanges();
                    user_service.AddUser(new StudentModel(){
                        FirstName = "Равиль",
                        LastName = "Хазиев",
                        Patronymic = "Русланович",
                        Login = "A1",
                        Password = "A1"
                    }).Wait();
                    user_service.AddUser(new StudentModel()
                    {
                        FirstName = "Максимов",
                        LastName = "Максим",
                        Patronymic = "Сергеевич",
                        Login = "A2",
                        Password = "A2"
                    }).Wait();

                    user_service.AddUser(new TeacherModel()
                    {
                        FirstName = "Оляс",
                        LastName = "Кударя",
                        Patronymic = "Ыич",
                        Login = "A3",
                        Password = "A3"
                    }).Wait();
                }

                app.Run();
            }
        }
    }
}