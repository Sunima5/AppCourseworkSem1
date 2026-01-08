using SQLite;
using DearDiary.Models;

namespace DearDiary.Services;

public class JournalService
{
    private readonly SQLiteAsyncConnection _db;

    public JournalService()
    {
        var path = Path.Combine(FileSystem.AppDataDirectory, "deardiary.db");
        _db = new SQLiteAsyncConnection(path);
        _db.CreateTableAsync<Journal>().Wait();
    }

    public async Task<List<Journal>> GetAllAsync()
    {
        return await _db.Table<Journal>()
            .OrderByDescending(j => j.EntryDate)
            .ToListAsync();
    }

    public async Task<Journal?> GetByDateAsync(DateTime date)
    {
        return await _db.Table<Journal>()
            .Where(j => j.EntryDate == date.Date)
            .FirstOrDefaultAsync();
    }

    public async Task SaveAsync(Journal journal)
    {
        journal.EntryDate = journal.EntryDate.Date;

        var existing = await GetByDateAsync(journal.EntryDate);

        if (existing == null)
        {
            journal.CreatedAt = DateTime.Now;
            journal.UpdatedAt = DateTime.Now;
            await _db.InsertAsync(journal);
        }
        else
        {
            existing.Title = journal.Title;
            existing.Content = journal.Content;
            existing.PrimaryMood = journal.PrimaryMood;
            existing.SecondaryMood = journal.SecondaryMood;
            existing.Tags = journal.Tags;
            existing.UpdatedAt = DateTime.Now;

            await _db.UpdateAsync(existing);
        }
    }

    public async Task DeleteAsync(DateTime date)
    {
        var existing = await GetByDateAsync(date);
        if (existing != null)
            await _db.DeleteAsync(existing);
    }

    // TAG FILTER
    public async Task<List<Journal>> GetByTagAsync(string tag)
    {
        return await _db.Table<Journal>()
            .Where(j => j.Tags.Contains(tag))
            .OrderByDescending(j => j.EntryDate)
            .ToListAsync();
    }

    // TAG COUNTS
    public async Task<Dictionary<string, int>> GetTagCountsAsync()
    {
        var journals = await GetAllAsync();
        var dict = new Dictionary<string, int>();

        foreach (var j in journals)
        {
            if (string.IsNullOrWhiteSpace(j.Tags))
                continue;

            foreach (var tag in j.Tags.Split(','))
            {
                var t = tag.Trim();
                if (dict.ContainsKey(t))
                    dict[t]++;
                else
                    dict[t] = 1;
            }
        }

        return dict;
    }
}
