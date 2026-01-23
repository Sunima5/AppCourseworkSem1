using SQLite;
using DearDiary.Data;
using DearDiary.Models;

namespace DearDiary.Services;

public class MoodService
{
    private readonly SQLiteAsyncConnection _db;

    public MoodService()
    {
        _db = AppDatabase.Database;
    }

    /* ============================= */
    /* SEED DATA (RUNS ONCE)         */
    /* ============================= */

    public async Task SeedMoodsAsync()
    {
        var count = await _db.Table<Mood>().CountAsync();
        if (count > 0) return; // already seeded

        // PRIMARY MOODS
        var primaryMoods = new[]
        {
            new Mood { Name = "Happy", Type = "Primary" },
            new Mood { Name = "Sad", Type = "Primary" },
            new Mood { Name = "Angry", Type = "Primary" },
            new Mood { Name = "Fear", Type = "Primary" },
            new Mood { Name = "Surprise", Type = "Primary" },
            new Mood { Name = "Peaceful", Type = "Primary" }
        };

        await _db.InsertAllAsync(primaryMoods);

        // Fetch inserted primaries (to get IDs)
        var primaries = await _db.Table<Mood>()
            .Where(m => m.Type == "Primary")
            .ToListAsync();

        int Id(string name) => primaries.First(m => m.Name == name).Id;

        // SECONDARY MOODS
        var secondaryMoods = new[]
        {
            new Mood { Name = "Cheerful", Type = "Secondary", ParentMoodId = Id("Happy") },
            new Mood { Name = "Proud", Type = "Secondary", ParentMoodId = Id("Happy") },

            new Mood { Name = "Lonely", Type = "Secondary", ParentMoodId = Id("Sad") },
            new Mood { Name = "Tired", Type = "Secondary", ParentMoodId = Id("Sad") },

            new Mood { Name = "Frustrated", Type = "Secondary", ParentMoodId = Id("Angry") },
            new Mood { Name = "Irritated", Type = "Secondary", ParentMoodId = Id("Angry") },

            new Mood { Name = "Anxious", Type = "Secondary", ParentMoodId = Id("Fear") },
            new Mood { Name = "Worried", Type = "Secondary", ParentMoodId = Id("Fear") },

            new Mood { Name = "Shocked", Type = "Secondary", ParentMoodId = Id("Surprise") },
            new Mood { Name = "Confused", Type = "Secondary", ParentMoodId = Id("Surprise") },

            new Mood { Name = "Calm", Type = "Secondary", ParentMoodId = Id("Peaceful") },
            new Mood { Name = "Grateful", Type = "Secondary", ParentMoodId = Id("Peaceful") }
        };

        await _db.InsertAllAsync(secondaryMoods);
    }

    /* ============================= */
    /* READ METHODS (FOR UI LATER)   */
    /* ============================= */

    public async Task<List<Mood>> GetPrimaryMoodsAsync()
    {
        return await _db.Table<Mood>()
            .Where(m => m.Type == "Primary")
            .OrderBy(m => m.Name)
            .ToListAsync();
    }

    public async Task<List<Mood>> GetSecondaryMoodsAsync(int primaryMoodId)
    {
        return await _db.Table<Mood>()
            .Where(m => m.ParentMoodId == primaryMoodId)
            .OrderBy(m => m.Name)
            .ToListAsync();
    }
}
