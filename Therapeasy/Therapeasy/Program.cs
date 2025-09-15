using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Therapeasy.Client.Pages;
using Therapeasy.Components;
using Therapeasy.Components.Account;
using Therapeasy.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddInteractiveServerComponents()
    .AddAuthenticationStateSerialization();
    
builder.Services.AddHttpClient();
// builder.Services.AddControllers()
//     .ConfigureApiBehaviorOptions(options =>
//     {
//         options.SuppressConsumesConstraintForFormFileParameters = true;
//         options.SuppressInferBindingSourcesForParameters = true;
//         options.SuppressModelStateInvalidFilter = true;
//         options.SuppressMapClientErrors = true;
//         options.ClientErrorMapping[StatusCodes.Status404NotFound].Link =
//             "https://httpstatuses.com/404";
//     });

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddSqlite<PlanDbContext>("Data Source=plan.db");
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllers();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(Therapeasy.Client._Imports).Assembly);

// Initialize the database
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    // PlanDbContext seeding (already present)
    var planDb = scope.ServiceProvider.GetRequiredService<PlanDbContext>();
    if (planDb.Database.EnsureCreated())
    {
        SeedData.Initialize(planDb);
    }

    // ApplicationDbContext role seeding
    var appDb = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = new[] { "Admin", "Therapist", "Patient" };
    foreach (var role in roles)
    {
        if (!roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
        {
            roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
        }
    }
}

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
