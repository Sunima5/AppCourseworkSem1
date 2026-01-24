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

            // ✅ REGISTER SERVICES
            builder.Services.AddSingleton<JournalService>();
            builder.Services.AddSingleton<TagService>();
            builder.Services.AddSingleton<MoodService>();
            builder.Services.AddSingleton<AppState>();


#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            // 🔥 SAFE BACKGROUND SEEDING (NON-BLOCKING)
            Task.Run(async () =>
            {
                await SeedDatabaseAsync(app);
            });

            return app;
        }

        private static async Task SeedDatabaseAsync(MauiApp app)
        {
            using var scope = app.Services.CreateScope();

            var tagService = scope.ServiceProvider.GetRequiredService<TagService>();
            await tagService.SeedTagsAsync();

            var moodService = scope.ServiceProvider.GetRequiredService<MoodService>();
            await moodService.SeedMoodsAsync();
        }
    }
}
