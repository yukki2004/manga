
using Manhwa.Application;
using Manhwa.Infrastructure;
using Microsoft.Extensions.FileProviders;

namespace Manhwa.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplicationServices();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            var storagePath = builder.Configuration["FileStorage:RootPath"] ?? "D:\\mangafire_storage";
            var requestPath = "/files"; 

            if (!Directory.Exists(storagePath))
            {
                Directory.CreateDirectory(storagePath);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(storagePath),
                RequestPath = requestPath
            });
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
