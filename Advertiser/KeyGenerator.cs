namespace Advertiser;

public static class AppPreferencesService
{
    private const string UniqueDeviceIdKey = "UniqueId";

    public static string GetUniqueDeviceId()
    {
        if (!Preferences.ContainsKey(UniqueDeviceIdKey))
        {
            var newId = Guid.NewGuid().ToString();
            Preferences.Set(UniqueDeviceIdKey, newId);
        }

        return Preferences.Get(UniqueDeviceIdKey, string.Empty);
    }
}