using Verse;

namespace UniversalPregnancy
{
    public class UniversalPregnancyMod : Mod
    {
        public const string PACKAGE_ID = "universalpregnancy.1trickPwnyta";
        public const string PACKAGE_NAME = "UniversalPregnancy";

        public UniversalPregnancyMod(ModContentPack content) : base(content)
        {
            Log.Message($"[{PACKAGE_NAME}] Loaded.");
        }
    }
}
