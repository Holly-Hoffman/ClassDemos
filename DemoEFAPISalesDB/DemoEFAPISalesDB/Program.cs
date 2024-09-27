using DemoEFAPISalesDB.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoEFAPISalesDB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<SALESDBContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("SALESDBConnectionString"))
                );
            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
