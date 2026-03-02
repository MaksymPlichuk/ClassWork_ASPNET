using System.Text.Json;

namespace MVC_Shop.Services
{
    public static class SessionService
    {
        public static string Key { get; } = "CartKey";

        public static void Set<T>(this ISession session, T value)
        {
            session.SetString(Key, JsonSerializer.Serialize(value));
        }
        public static T? Get<T>(this ISession session) { 
            var value = session.GetString(Key);

            if (value == null) { return default; }

            T? res = JsonSerializer.Deserialize<T>(value);
            return res;
        }
    }
}
