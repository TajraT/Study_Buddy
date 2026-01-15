using System.Text.Json;
using Study_Buddy.Models;

namespace Study_Buddy.Services;

public static class FocusStatsService
{
    private const string Key = "focus_sessions";

    public static List<FocusSession> GetSessions()
    {
        var json = Preferences.Get(Key, string.Empty);

        if (string.IsNullOrEmpty(json))
            return new List<FocusSession>();

        return JsonSerializer.Deserialize<List<FocusSession>>(json)
               ?? new List<FocusSession>();
    }

    public static void SaveSession(FocusSession session)
    {
        var sessions = GetSessions();
        sessions.Add(session);

        var json = JsonSerializer.Serialize(sessions);
        Preferences.Set(Key, json);
    }
}
