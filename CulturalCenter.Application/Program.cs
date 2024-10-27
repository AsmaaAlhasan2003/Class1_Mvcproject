using ApplicationData;
using ApplicationData.Repositories;
using ApplicationData.Repository;
using CulturalCenter.Application.Areas.Identity.Data;
using CulturalCenter.Application.Data;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
     

        // الحصول على سلسلة الاتصال لقاعدة البيانات CulturalCenterApplicationContext
        var connectionStringCulturalCenter = builder.Configuration.GetConnectionString("CulturalCenterApplicationContextConnection")
            ?? throw new InvalidOperationException("Connection string 'CulturalCenterApplicationContextConnection' not found.");

        // الحصول على سلسلة الاتصال لقاعدة البيانات MnagementBdContext
        var connectionStringMnagementBd = builder.Configuration.GetConnectionString("CulturalCnterDB")
            ?? throw new InvalidOperationException("Connection string 'CulturalCnterDB' not found.");

        // تسجيل DbContext لـ CulturalCenterApplicationContext
        builder.Services.AddDbContext<CulturalCenterApplicationContext>(options =>
            options.UseSqlServer(connectionStringCulturalCenter));

        // تسجيل DbContext لـ MnagementBdContext
        builder.Services.AddDbContext<MnagementBdContext>(options =>
            options.UseSqlServer(connectionStringMnagementBd));

        // تسجيل الهوية مع المستخدم المخصص CulturalCenterApplicationUser
        builder.Services.AddDefaultIdentity<CulturalCenterApplicationUser>(options =>
            options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<CulturalCenterApplicationContext>();

        // تسجيل الريبوزيتوري
        builder.Services.AddTransient<IRepository<Book>, BookRepository>();
        builder.Services.AddTransient<IRepository<Visitor>, VisitorRepository>();
        builder.Services.AddTransient<IRepository<Exhibition>,ExhibitionRepository>();
        builder.Services.AddTransient<IRepository<Author>, AuthorRepository>();
        builder.Services.AddTransient<IRepository<Loan>,LoanRepository>();

        builder.Services.AddControllersWithViews();
        builder.Services.AddControllers();
        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();
        app.MapControllers();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        // إضافة الأدوار (Roles)
        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new[] { "Admin", "Manager", "Member" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        // إضافة مستخدم Admin
        using (var scope = app.Services.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<CulturalCenterApplicationUser>>();

            string adminEmail = "admin@admin.com";
            string adminPassword = "Admin123@gmail.com";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new CulturalCenterApplicationUser
                {
                    Email = adminEmail,
                    UserName = adminEmail
                };

                await userManager.CreateAsync(adminUser, adminPassword);
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            // إضافة مستخدم Member
            string memberEmail = "member@member.com";
            string memberPassword = "Member123@gmail.com";

            if (await userManager.FindByEmailAsync(memberEmail) == null)
            {
                var memberUser = new CulturalCenterApplicationUser
                {
                    Email = memberEmail,
                    UserName = memberEmail
                };

                await userManager.CreateAsync(memberUser, memberPassword);
                await userManager.AddToRoleAsync(memberUser, "Member");
            }
        }

        app.Run();
    }
}
