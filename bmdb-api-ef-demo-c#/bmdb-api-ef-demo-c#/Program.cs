using bmdb_api_ef_demo_c_.Models;
using Microsoft.EntityFrameworkCore;

namespace bmdb_api_ef_demo_c_
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<BMDBContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("BMDBConnectionString"))
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
