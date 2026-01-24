using DearDiary.Models;
using SQLite;

namespace DearDiary.Data;

public class AppDatabase
{
    private static SQLiteAsyncConnection _database;

    public static SQLiteAsyncConnection Database
    {
        get
        {
            if (_database == null)
            {
                var path = Path.Combine(
                    FileSystem.AppDataDirectory,
                    "deardiary.db"
                );

                _database = new SQLiteAsyncConnection(path);

                // CREATE TABLES (ONLY ONCE)
                _database.CreateTableAsync<Journal>().Wait();
                _database.CreateTableAsync<Mood>().Wait();
                _database.CreateTableAsync<Tag>().Wait();
                _database.CreateTableAsync<JournalMood>().Wait();
                _database.CreateTableAsync<JournalTag>().Wait();
            
            }

            return _database;
        }
    }
}
