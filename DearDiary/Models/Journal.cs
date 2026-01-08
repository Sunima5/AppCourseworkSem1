using SQLite;

namespace DearDiary.Models;

public class Journal
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Indexed(Unique = true)]
    public DateTime EntryDate { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

    public string PrimaryMood { get; set; } = string.Empty;
    public string SecondaryMood { get; set; } = string.Empty;

    // TAGS (comma separated)
    public string Tags { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
