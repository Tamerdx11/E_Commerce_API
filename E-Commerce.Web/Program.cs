using E_Commerce.Domain.Contracts;
using E_Commerce.Service.DependencyInjection;
using E_Commerce.Service.Services;
using E_Commerce.ServiceAbstraction;
using E_Commerce.Web.Handlers;
using E_Commerce.Web.Middlewares;
using ECommerce.Persistance.Context;
using ECommerce.Persistance.DependencyInjection;
using ECommerce.Persistance.Repositories;
using Microsoft.AspNetCore.Mvc;
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
        builder.Services.AddExceptionHandler<ExceptionHandler>();
        builder.Services.AddProblemDetails();

        builder.Services.Configure<ApiBehaviorOptions>(opt => 
        {
            opt.InvalidModelStateResponseFactory = actionContext =>
            {
                var error = actionContext.ModelState.Where(x => x.Value!.Errors.Any())
                .ToDictionary(x => x.Key, 
                y => y.Value!.Errors.Select(e => e.ErrorMessage).ToArray());

                var problem = new ProblemDetails
                {
                    Title = "VAlidation Error!",
                    Detail = "One or more validation error occurs!",
                    Status = StatusCodes.Status400BadRequest,
                    Extensions = { { "errors",error} }
                };

                return new BadRequestObjectResult(problem);
            };
        });

        var app = builder.Build();

        using var scope = app.Services.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        await initializer.InitializeAsync();

        ///using (var scope = app.Services.CreateScope())
        ///{
        ///    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        ///    await db.Database.MigrateAsync();
        ///}

        ///app.Use(async (context, next) =>
        ///{
        ///    try
        ///    {
        ///        await next.Invoke(context);
        ///    }
        ///    catch (Exception ex) 
        ///    {
        ///        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        ///        await context.Response.WriteAsJsonAsync(new
        ///        {
        ///            StatusCode = StatusCodes.Status500InternalServerError,
        ///            ex.Message
        ///        });
        ///    }
        ///});

        //app.UseMiddleware<GlobalExceptionHandler>();

        //app.UseCustomExceptionHandler();

        app.UseExceptionHandler();

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