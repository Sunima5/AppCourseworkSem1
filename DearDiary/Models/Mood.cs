using SQLite;

public class Mood
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Category { get; set; } = "";
    public string Name { get; set; } = "";
}
