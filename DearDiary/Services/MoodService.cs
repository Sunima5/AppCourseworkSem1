using DearDiary.Data;
using DearDiary.Models;
using SQLite;

namespace DearDiary.Services;

public class MoodService
{
    private readonly SQLiteAsyncConnection _db;

    public MoodService()
    {
        _db = AppDatabase.Database;
    }

    /* ============================= */
    /* SEED MOODS (RUN ONCE)         */
    /* ============================= */

    public async Task SeedMoodsAsync()
    {
        var count = await _db.Table<Mood>().CountAsync();
        if (count > 0) return;

        var moods = new[]
        {
            // POSITIVE
            new Mood { Category = "Positive", Name = "Happy" },
            new Mood { Category = "Positive", Name = "Excited" },
            new Mood { Category = "Positive", Name = "Relaxed" },
            new Mood { Category = "Positive", Name = "Grateful" },
            new Mood { Category = "Positive", Name = "Confident" },

            // NEUTRAL
            new Mood { Category = "Neutral", Name = "Calm" },
            new Mood { Category = "Neutral", Name = "Thoughtful" },
            new Mood { Category = "Neutral", Name = "Curious" },
            new Mood { Category = "Neutral", Name = "Nostalgic" },
            new Mood { Category = "Neutral", Name = "Bored" },

            // NEGATIVE
            new Mood { Category = "Negative", Name = "Sad" },
            new Mood { Category = "Negative", Name = "Angry" },
            new Mood { Category = "Negative", Name = "Stressed" },
            new Mood { Category = "Negative", Name = "Lonely" },
            new Mood { Category = "Negative", Name = "Anxious" }
        };

        await _db.InsertAllAsync(moods);
    }

    /* ============================= */
    /* READ FOR UI                  */
    /* ============================= */

  
      public async Task<List<string>> GetPrimaryMoodsAsync()
    {
        var moods = await _db.Table<Mood>().ToListAsync();

        return moods
            .Select(m => m.Category)
            .Distinct()
            .ToList();
    }


    public async Task<List<Mood>> GetSecondaryMoodsAsync(string category)
    {
        return await _db.Table<Mood>()
            .Where(m => m.Category == category)
            .OrderBy(m => m.Name)
            .ToListAsync();
    }
}
