using HarmonyLib;
using Verse;

namespace UniversalPregnancy
{
    [HarmonyPatch(typeof(RimWorld.PregnancyUtility))]
    [HarmonyPatch(nameof(RimWorld.PregnancyUtility.ApplyBirthOutcome_NewTemp))]
    public static class Patch_PregnancyUtility_ApplyBirthOutcome_NewTemp
    {
        public static void Postfix(Pawn geneticMother, Pawn father, Thing __result)
        {
            if (__result is Pawn)
            {
                Pawn baby = (Pawn)__result;
                if (baby.relations is Pawn_RelationsTracker)
                {
                    Pawn_RelationsTracker relations = (Pawn_RelationsTracker)baby.relations;
                    relations.mother = geneticMother;
                    relations.father = father;
                }
            }
        }
    }
}
