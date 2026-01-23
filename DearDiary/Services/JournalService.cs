using SQLite;
using DearDiary.Data;
using DearDiary.Models;

namespace DearDiary.Services;

public class JournalService
{
    private readonly SQLiteAsyncConnection _db;

    public JournalService()
    {
        _db = AppDatabase.Database;
    }

    /* ========================= */
    /* JOURNAL CRUD              */
    /* ========================= */

    // Save or Update
    public async Task<int> SaveAsync(Journal journal)
    {
        journal.UpdatedAt = DateTime.Now;

        if (journal.Id != 0)
        {
            await _db.UpdateAsync(journal);
            return journal.Id;
        }

        journal.CreatedAt = DateTime.Now;
        await _db.InsertAsync(journal);
        return journal.Id;
    }

    // Get all journals
    public async Task<List<Journal>> GetAllAsync()
    {
        return await _db.Table<Journal>()
            .OrderByDescending(j => j.EntryDate)
            .ToListAsync();
    }

    // 🔥 IMPORTANT: Get journal by DATE (not ID)
    public async Task<Journal?> GetByDateAsync(DateTime date)
    {
        var normalizedDate = date.Date;

        return await _db.Table<Journal>()
            .Where(j => j.EntryDate == normalizedDate)
            .FirstOrDefaultAsync();
    }

    // Get by ID (still useful)
    public async Task<Journal?> GetByIdAsync(int id)
    {
        return await _db.FindAsync<Journal>(id);
    }

    // Delete by ID
    public async Task DeleteAsync(int id)
    {
        await _db.DeleteAsync<Journal>(id);
    }

    // 🔒 Ensure only ONE journal per day
    public async Task<bool> ExistsForDateAsync(DateTime date)
    {
        return await GetByDateAsync(date) != null;
    }
}
