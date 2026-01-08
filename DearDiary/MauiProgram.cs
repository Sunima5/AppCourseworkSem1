using Microsoft.Extensions.Logging;
using DearDiary.Services;

namespace DearDiary
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
                });

            builder.Services.AddMauiBlazorWebView();

            // ✅ ADD THIS LINE
            builder.Services.AddSingleton<JournalService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();

            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
