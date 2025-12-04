using System.Text.Json;

namespace Library.Structure
{
    public static class MyExtentionMethod
    {
        public static void SetObject(this ISession session,string Key,object Value)
        {
            session.SetString(Key, JsonSerializer.Serialize(Value));
        }
        public static T GetObject<T>(this ISession session,string Key)
        {
            var value = session.GetString(Key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
