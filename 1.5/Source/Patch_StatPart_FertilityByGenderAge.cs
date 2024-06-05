using HarmonyLib;
using RimWorld;
using Verse;

namespace UniversalPregnancy
{
    [HarmonyPatch(typeof(StatPart_FertilityByGenderAge))]
    [HarmonyPatch("AgeFactor")]
    public static class Patch_StatPart_FertilityByGenderAge
    {
        public static void Postfix(SimpleCurve ___femaleFertilityAgeFactor, Pawn pawn, ref float __result)
        {
            __result = ___femaleFertilityAgeFactor.Evaluate(pawn.ageTracker.AgeBiologicalYearsFloat);

#if DEBUG
            __result = 1f;
#endif
        }
    }
}
