using IPTV_Website.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IMockTvDataService, MockTvDataService>();
//builder.Services.AddHttpClient<ApiTvDataService>(client =>
//{
//    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]);
//    client.DefaultRequestHeaders.Add("Accept", "application/json");
//});

//builder.Services.AddScoped<IMockTvDataService, ApiTvDataService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{data?}");

app.Run();
