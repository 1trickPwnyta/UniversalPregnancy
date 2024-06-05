namespace UniversalPregnancy
{
    public static class Debug
    {
        public static void Log(string message)
        {
#if DEBUG
            Verse.Log.Message($"[{UniversalPregnancyMod.PACKAGE_NAME}] {message}");
#endif
        }
    }
}
