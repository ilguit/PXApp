namespace PXApp.Core.Db;

internal static class DbDefaultValues
{
    internal const string GeneratedId = "gen_random_uuid()";
    internal const string CurrentUtcDateTime = "timezone('utc'::text, now())";
}