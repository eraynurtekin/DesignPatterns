using BaseProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Composite.Models;

namespace BaseProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();

            var identityDbContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();

            var userManager = scope.ServiceProvider.GetRequiredService <UserManager<AppUser>>();

            identityDbContext.Database.Migrate(); //Uygulama ayaða kalktýðýnda update-database dememize gerek kalmadý. Uygulanmayan mig oluþturur yoksa oluþturur uygular.

            if (!userManager.Users.Any())
            {
                var newUser = new AppUser() { UserName = "user1", Email = "user1@gmail.com" };
                userManager.CreateAsync(newUser,"Password12*").Wait();
                userManager.CreateAsync(new AppUser() { UserName = "user2", Email = "user2@gmail.com"},"Password12*").Wait();
                userManager.CreateAsync(new AppUser() { UserName = "user3", Email = "user3@gmail.com"},"Password12*").Wait();
                userManager.CreateAsync(new AppUser() { UserName = "user4", Email = "user4@gmail.com"},"Password12*").Wait();
                userManager.CreateAsync(new AppUser() { UserName = "user5", Email = "user5@gmail.com"},"Password12*").Wait();



                var newCategory1 = new Category { Name = "Suç Romanlarý", ReferenceId = 0, UserId = newUser.Id };
                var newCategory2 = new Category { Name = "Cinayet Romanlarý", ReferenceId = 0, UserId = newUser.Id };
                var newCategory3 = new Category { Name = "Polisiye Romanlarý", ReferenceId = 0, UserId = newUser.Id };

                identityDbContext.Categories.AddRange(newCategory1,newCategory2,newCategory3);

                identityDbContext.SaveChanges();

                var subCategory1 = new Category { Name = "Suç Romanlarý 1", ReferenceId = newCategory1.Id, UserId = newUser.Id };
                var subCategory2 = new Category { Name = "Cinayet Romanlarý 1", ReferenceId = newCategory2.Id, UserId = newUser.Id };
                var subCategory3 = new Category { Name = "Polisiye Romanlarý 1", ReferenceId = newCategory3.Id, UserId = newUser.Id };


                identityDbContext.Categories.AddRange(subCategory1 ,subCategory2,subCategory3);
                identityDbContext.SaveChanges();

                var subCategory4 = new Category { Name = "Cinayet Romanlarý 1.1", ReferenceId = subCategory2.Id, UserId = newUser.Id };
               
                identityDbContext.Categories.Add(subCategory4 );
                identityDbContext.SaveChanges();
            
            }




            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
