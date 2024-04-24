using ExpressVoitures.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ExpressVoitures.Models;
using Microsoft.Extensions.DependencyInjection;
using static System.Formats.Asn1.AsnWriter;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ExpressVoituresContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ExpressVoituresContext") ?? throw new InvalidOperationException("Connection string 'ExpressVoituresContext' not found.")));

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//UPD08 UserIdentity mngt, old : builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>() //UserIdentity mngt
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    // Initialize ExpressVoitures datas test records 
    SeedData.Initialize(services);

    //UserIdentity mngt
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    var roleExist = await roleManager.RoleExistsAsync("Admin");
    if (!roleExist)
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    var adminUser = await userManager.FindByEmailAsync("jacques@expressvoitures.fr");
    if (adminUser == null)
    {
        adminUser = new IdentityUser
        {
            UserName = "jacques@expressvoitures.fr",
            Email = "jacques@expressvoitures.fr",
        };
        Console.WriteLine("admin user creation...");

        var admin = await userManager.CreateAsync(adminUser, "Admin@123");
        if (admin.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
            Console.WriteLine("Admin user created {0}", adminUser.UserName);
        }
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// UPD06 ControllerRoute for Home to Vehicules Controller
app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Home}/{action=Index}/{id?}");
    pattern: "{controller=Vehicules}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
