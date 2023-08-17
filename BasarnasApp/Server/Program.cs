using BasarnasApp.Server;
using BasarnasApp.Server.Data;
using BasarnasApp.Server.Models;
using BasarnasApp.Server.Services;
using BasarnasApp.Server.Services.ServiceContracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using OcphAspCoreApiAuth.Server;

var builder = WebApplication.CreateBuilder(args);
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
if (builder.Environment.IsProduction())
{
    builder.WebHost.UseKestrel(serverOptions =>
    {
        serverOptions.ListenLocalhost(5020);
    });
  
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
//options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
       .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddIdentityServer()
//    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

builder.Services.AddOcphAuthServer(builder.Configuration);


//app

builder.Services.AddScoped<IInstansiService, InstansiService>();
builder.Services.AddScoped<IDistrictService, DistrictService>();
builder.Services.AddScoped<IJenisKejadianService, JenisKejadianervice>();
builder.Services.AddScoped<IPihakTerkaitService, PihakTerkaitService>();
builder.Services.AddScoped<IPelaporService, PelaporService>();
builder.Services.AddScoped<IKejadianService, KejadianService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
       new[] { "application/octet-stream" });
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {

        var context = services.GetRequiredService<ApplicationDbContext>();

        if (context.Database.EnsureCreated())
            context.Database.Migrate();
        if (!context.Roles.Any())
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("Instansi"));
            await roleManager.CreateAsync(new IdentityRole("Pelapor"));
        }
        if (!context.Users.Any())
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            var user = new ApplicationUser() { Email = "admin@gmail.com", UserName = "admin@gmail.com", EmailConfirmed = true };
            var userCreated = await userManager.CreateAsync(user, "Password@123");
            if (userCreated.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}



app.UseResponseCompression();

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.MapHub<BasarnasHub>("/apphub");
app.Run();
