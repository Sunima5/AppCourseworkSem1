using DearDiary.Models;
using SQLite;

namespace DearDiary.Services;

public class UserService
{
    private readonly SQLiteAsyncConnection _db;

    public UserService()
    {
        var dbPath = Path.Combine(
            FileSystem.AppDataDirectory,
            "deardiary.db3"
        );

        _db = new SQLiteAsyncConnection(dbPath);
    }

    public async Task<User> GetOrCreateUserAsync()
    {
        await _db.CreateTableAsync<User>();

        var user = await _db.Table<User>().FirstOrDefaultAsync();

        if (user == null)
        {
            user = new User
            {
                Username = "Sunima",
                Pin = "1234"
            };

            await _db.InsertAsync(user);
        }

        return user;
    }
}
