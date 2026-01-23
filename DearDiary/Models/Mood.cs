using SQLite;

namespace DearDiary.Models;

public class Mood
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name { get; set; }          // Happy, Sad, etc.
    public string Type { get; set; }          // "Primary" or "Secondary"
    public int? ParentMoodId { get; set; }    // NULL for primary moods
}
