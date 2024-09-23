var builder = WebApplication.CreateBuilder(args);
IronPdf.License.LicenseKey = "IRONSUITE.MARHEUSERIC.GMAIL.COM.8464-499882260E-GXKFZBL2Y6B262-THULSFDZHFY2-IDD3CHCALELG-K4V65LF77LYU-7HA2FLNGUSZ3-RZ5W2DYUA6XB-4YOKNQ-TEUBCM5IMO2NUA-DEPLOYMENT.TRIAL-VNCAXE.TRIAL.EXPIRES.23.OCT.2024"; // Adicione a chave aqui

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
