using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DataAccess;
using MathGame.Maui.Views;

namespace MathGame.Maui;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        var config = new ConfigurationBuilder()
            .AddUserSecrets<App>()
            .Build();

        var conn = config.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<DataContext>(options =>
        {
            var conn = config.GetConnectionString("DefaultConnection");
            options.UseMySql(conn, ServerVersion.AutoDetect(conn));
        });

        builder.Services.AddTransient<Views.Game>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
