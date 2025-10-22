using E_Commerce.Domain.Contracts;
using E_Commerce.Service.DependencyInjection;
using E_Commerce.Service.Services;
using E_Commerce.ServiceAbstraction;
using ECommerce.Persistance.Context;
using ECommerce.Persistance.DependencyInjection;
using ECommerce.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddPersistanceServices(builder.Configuration)
            .AddApplicationServices();


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

        app.UseStaticFiles();

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();
        //app.UseResponseCaching();

        app.Run();
    }
}