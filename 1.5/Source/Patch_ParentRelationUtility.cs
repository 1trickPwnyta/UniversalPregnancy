using HarmonyLib;
using RimWorld;
using Verse;

namespace UniversalPregnancy
{
    [HarmonyPatch(typeof(ParentRelationUtility))]
    [HarmonyPatch("GetParent")]
    public static class Patch_ParentRelationUtility_GetParent
    {
        public static void Postfix(Pawn pawn, Gender parentGender, ref Pawn __result)
        {
            Pawn previousResult = __result;
            __result = null;
            if (pawn.relations is Pawn_RelationsTracker)
            {
                Pawn_RelationsTracker relations = (Pawn_RelationsTracker)pawn.relations;
                if (parentGender == Gender.Female)
                {
                    __result = relations.mother;
                }
                else if (parentGender == Gender.Male)
                {
                    __result = relations.father;
                }
            }
            if (__result == null)
            {
                __result = previousResult;
            }
        }
    }
}
