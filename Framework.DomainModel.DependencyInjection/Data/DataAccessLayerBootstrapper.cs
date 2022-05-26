using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Sharpee.Framework.DomainModel.DependencyInjection.Data;

public static class DataAccessLayerBootstrapper
{
    /// <summary>
    /// This method ensures database has been created and is always updated.
    /// </summary>
    /// <typeparam name="T">T is type of DbContext of the project.</typeparam>
    /// <param name="app">app is ApplicationBuilder of the project.</param>
    public static void UseDatabase<T>(this IApplicationBuilder app)
        where T : DbContext
    {
        try
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetService<T>();
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}