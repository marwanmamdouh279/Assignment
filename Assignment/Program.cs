using Assignment.Mapping;
using Assignment_BussinesLogicLayer.Interfaces;
using Assignment_BussinesLogicLayer.Reposatories;
using Assignment_DataAccesslayer.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AssignmentContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));  // best for any update on db any time (can be changed on sserver for long time )
            });                                                      //  allow DI for AssignmentContext
            builder.Services.AddScoped<IDepartmentReposatory,DepartmentReposatory>();      // Alloww DI for DepartmentReposatory
            builder.Services.AddScoped<IEmployeeReposatory, EmployeeReposatory>();         //Allow DI for EmployeeReposatory
            //builder.Services.AddAutoMapper(typeof(EmployeeProfile);
            builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts(); 
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
