using SQLite;
using DearDiary.Data;
using DearDiary.Models;

namespace DearDiary.Services;

public class TagService
{
    private readonly SQLiteAsyncConnection _db;

    public TagService()
    {
        _db = AppDatabase.Database;
    }

    /* ============================= */
    /* SEED TAGS (RUN ONCE)          */
    /* ============================= */

    public async Task SeedTagsAsync()
    {
        var count = await _db.Table<Tag>().CountAsync();
        if (count > 0)
            return;

        var tags = new[]
        {
            new Tag { Name = "Work" },
            new Tag { Name = "Career" },
            new Tag { Name = "Studies" },
            new Tag { Name = "Family" },
            new Tag { Name = "Friends" },
            new Tag { Name = "Relationships" },
            new Tag { Name = "Health" },
            new Tag { Name = "Fitness" },
            new Tag { Name = "Personal Growth" },
            new Tag { Name = "Self-care" },
            new Tag { Name = "Hobbies" },
            new Tag { Name = "Travel" },
            new Tag { Name = "Nature" },
            new Tag { Name = "Finance" },
            new Tag { Name = "Spirituality" },
            new Tag { Name = "Birthday" },
            new Tag { Name = "Holiday" },
            new Tag { Name = "Vacation" },
            new Tag { Name = "Celebration" },
            new Tag { Name = "Exercise" },
            new Tag { Name = "Reading" },
            new Tag { Name = "Writing" },
            new Tag { Name = "Cooking" },
            new Tag { Name = "Meditation" },
            new Tag { Name = "Yoga" },
            new Tag { Name = "Music" },
            new Tag { Name = "Shopping" },
            new Tag { Name = "Parenting" },
            new Tag { Name = "Projects" },
            new Tag { Name = "Planning" },
            new Tag { Name = "Reflection" }
        };

        await _db.InsertAllAsync(tags);
    }

    /* ============================= */
    /* READ TAGS                     */
    /* ============================= */

    // Used by TagPage / dropdowns
    public async Task<List<Tag>> GetAllAsync()
    {
        return await _db.Table<Tag>()
            .OrderBy(t => t.Name)
            .ToListAsync();
    }

    /* ============================= */
    /* TAG → JOURNAL RELATION        */
    /* ============================= */

    // 🔥 REQUIRED by TagPage.razor
    public async Task<List<Journal>> GetByTagAsync(string tag)
    {
        return await _db.Table<Journal>()
            .Where(j => j.Tags != null && j.Tags.Contains(tag))
            .OrderByDescending(j => j.EntryDate)
            .ToListAsync();
    }

    // 🔥 REQUIRED by Home.razor
    public async Task<Dictionary<string, int>> GetTagCountsAsync()
    {
        var journals = await _db.Table<Journal>().ToListAsync();

        return journals
            .Where(j => !string.IsNullOrWhiteSpace(j.Tags))
            .SelectMany(j => j.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries))
            .Select(t => t.Trim())
            .GroupBy(t => t)
            .ToDictionary(g => g.Key, g => g.Count());
    }
}
