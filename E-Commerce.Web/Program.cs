using E_Commerce.Domain.Contracts;
using ECommerce.Persistance.Context;
using ECommerce.Persistance.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;


namespace E_Commerce.Web
{
    public class Program
    {
        public static async void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddPersistanceServices(builder.Configuration);
            

            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await initializer.InitializeAsync();

            //using (var scope = app.Services.CreateScope())
            //{
            //    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            //    await db.Database.MigrateAsync();
            //}

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
