using HarmonyLib;
using RimWorld;
using Verse;

namespace UniversalPregnancy
{
    [HarmonyPatch(typeof(StatPart_FertilityByGenderAge))]
    [HarmonyPatch("AgeFactor")]
    public static class Patch_StatPart_FertilityByGenderAge
    {
        public static void Postfix(SimpleCurve ___femaleFertilityAgeFactor, SimpleCurve ___maleFertilityAgeFactor, Pawn pawn, ref float __result)
        {
            SimpleCurve curve;
            if (pawn.gender == Gender.Female)
            {
                if (UniversalPregnancySettings.UseFemaleFertilityForFemales)
                {
                    curve = ___femaleFertilityAgeFactor;
                }
                else
                {
                    curve = ___maleFertilityAgeFactor;
                }
            }
            else
            {
                if (UniversalPregnancySettings.UseFemaleFertilityForMales)
                {
                    curve = ___femaleFertilityAgeFactor;
                }
                else
                {
                    curve = ___maleFertilityAgeFactor;
                }
            }
            __result = curve.Evaluate(pawn.ageTracker.AgeBiologicalYearsFloat);

#if DEBUG
            __result = 1f;
#endif
        }
    }
}
