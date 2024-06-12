
using Microsoft.EntityFrameworkCore;
using MTG_Tavern_MVP.Data;
using MTG_Tavern_MVP.Services;

namespace MTG_Tavern_MVP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
            builder.Services.AddScoped<PasswordHashingService, PasswordHashingService>();

            builder.Services.AddCors((setup =>
            {
                setup.AddPolicy("default", (options =>
                {
                    options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                }));
            }));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("default");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
