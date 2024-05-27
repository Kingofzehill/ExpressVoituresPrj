using ExpressVoitures.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ExpressVoitures.Models;
using ExpressVoitures.Services;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using ExpressVoitures.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddDbContext<ExpressVoituresContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ExpressVoituresContext") ?? throw new InvalidOperationException("Connection string 'ExpressVoituresContext' not found.")));

    // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    
    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
        .AddRoles<IdentityRole>() //UserIdentity mngt
        .AddEntityFrameworkStores<ApplicationDbContext>();
    builder.Services.AddControllersWithViews();

    // Dependency injections for PathService / ImageService / VehiculeService.
    //      https://stackoverflow.com/questions/51691211/asp-net-adding-apicontroller-as-service-for-dependency-injection
    builder.Services.AddSingleton<PathService>();
    builder.Services.AddSingleton<ImageService>();
    builder.Services.AddSingleton<VehiculeService>();
    // FIX12 adding controller services for use in services classes ==> circular dependencies ==> Refactoring code needed.    

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
            var admin = await userManager.CreateAsync(adminUser, "Admin@123");
            if (admin.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");                
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

    // FIX08 add locolization fr-FR (for managing comma separator in Prices)
    //      ==> globalize your app for perfect culture management with jquery (client side)
    //      https://github.com/dotnet/AspNetCore.Docs/issues/4076
    //      https://stackoverflow.com/questions/48066208/mvc-jquery-validation-does-not-accept-comma-as-decimal-separator
    // var defaultCulture = new CultureInfo("en-US"); 
    var defaultCulture = new CultureInfo("fr-FR"); 
    defaultCulture.NumberFormat.CurrencySymbol = "€";
    //defaultCulture.NumberFormat.CurrencyDecimalSeparator = ".";
    defaultCulture.NumberFormat.CurrencyDecimalSeparator = ",";
    //defaultCulture.NumberFormat.NumberDecimalSeparator = ".";
    defaultCulture.NumberFormat.NumberDecimalSeparator = ",";
    var dateformat = new DateTimeFormatInfo();
    dateformat.ShortDatePattern = "dd/MM/yyyy"; //FullDateTimePattern = "{0:dd/MM/yyyy}";
    defaultCulture.DateTimeFormat = dateformat;
    var localizationOptions = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(defaultCulture),
            SupportedCultures = new List<CultureInfo> { defaultCulture },
            SupportedUICultures = new List<CultureInfo> { defaultCulture },
        };
        CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
        CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;
        //app.UseRequestLocalization("en-US");
        app.UseRequestLocalization("fr-FR");

    app.Run();
