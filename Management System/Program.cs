

using Management_System.Infrustructure.Contexts;
using Management_System.Initializer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddEasyDi();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSingleton<IPuterContext>(new PuterContext());

builder.Services.AddDbContextPool<MainContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SysManagmentDB")).UseLazyLoadingProxies();
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<MainContext>()
    .AddDefaultTokenProviders();

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("Admin_Access",
//        policy => policy.RequireClaim("AdminAccess"));
//});

builder.Services.AddScoped<IDbinitializer, DbInitializer>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.MapControllerRoute(
//        name: "default",
//        pattern: "{controller=Account}/{action=Login}");

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");


using (var scop = app.Services.CreateScope())
{
    var dbinitializer = scop.ServiceProvider.GetRequiredService<IDbinitializer>();
    await dbinitializer.Initialize();
}

app.Run();
