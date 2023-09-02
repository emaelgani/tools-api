namespace Tools.Data.Entities
{
    public class User
    {
        public int IdUser { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
