namespace RentAMovie.Services
{
    //преди реална база, имахме временно решение за моментна база

    /*public class InMemoryUserStore
    {
        private static readonly Dictionary<string, string> Users = new();

        public static bool Exists(string username) => Users.ContainsKey(username);

        public static bool Register(string username, string password)
        {
            if (Users.ContainsKey(username)) return false;
            Users[username] = password;
            return true;
        }

        public static bool Validate(string username, string password)
        {
            return Users.TryGetValue(username, out var stored) && stored == password;
        }
    }*/
}
