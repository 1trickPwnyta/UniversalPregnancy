using HarmonyLib;

namespace UniversalPregnancy
{
    [HarmonyPatch(typeof(RimWorld.Pawn_RelationsTracker))]
    [HarmonyPatch(nameof(RimWorld.Pawn_RelationsTracker.ExposeData))]
    public static class Patch_Pawn_RelationsTracker_ExposeData
    {
        public static void Postfix(RimWorld.Pawn_RelationsTracker __instance)
        {
            if (__instance is Pawn_RelationsTracker)
            {
                ((Pawn_RelationsTracker)__instance).ExposeData();
            }
        }
    }
}
