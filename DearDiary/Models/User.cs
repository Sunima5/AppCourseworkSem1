using SQLite;

namespace DearDiary.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int UserId { get; set; }

        [Unique]
        public string Username { get; set; } = "";

        public string Pin { get; set; } = "";
    }
}
