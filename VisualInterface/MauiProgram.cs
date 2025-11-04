using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using BackendLibrary;
using VisualInterface;

namespace VisualInterface
{
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

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // Registrera backend och UI-sidor i DI
            builder.Services.AddSingleton<WorkerRegistry>();
            builder.Services.AddSingleton<TimeManagement>();
            builder.Services.AddSingleton<MainPage>();

            return builder.Build();
        }
    }
}
