using projekt.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddHttpContextAccessor();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddScoped<IDataMealsService, DataMealsService>(); //nie dzia�� service do pobierania listy dan, zeby nie ladowa�o sie ta dlugo przy wejsciu na stronie z daniamia
//builder.Services.Add(DataProductService);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                // Specify where to redirect un-authenticated users
                options.LoginPath = "/Account/Login";

                // Specify the name of the auth cookie.
                // ASP.NET picks a dumb name by default.
                options.Cookie.Name = "my_app_auth_cookie";
            });


// Build the application.
var app = builder.Build();

// Configure the HTTP request pipeline.
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();