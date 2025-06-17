using UnityEngine;
using Verse;

namespace UniversalPregnancy
{
    public class UniversalPregnancyMod : Mod
    {
        public const string PACKAGE_ID = "universalpregnancy.1trickPwnyta";
        public const string PACKAGE_NAME = "Universal Pregnancy";

        public static UniversalPregnancySettings Settings;

        public UniversalPregnancyMod(ModContentPack content) : base(content)
        {
            Settings = GetSettings<UniversalPregnancySettings>();

            Log.Message($"[{PACKAGE_NAME}] Loaded.");
        }

        public override string SettingsCategory() => PACKAGE_NAME;

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            UniversalPregnancySettings.DoSettingsWindowContents(inRect);
        }
    }
}
