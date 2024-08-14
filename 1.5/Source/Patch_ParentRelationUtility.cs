using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace UniversalPregnancy
{
    [HarmonyPatch(typeof(ParentRelationUtility))]
    [HarmonyPatch("GetParent")]
    public static class Patch_ParentRelationUtility_GetParent
    {
        public static bool Prefix(Pawn pawn, Gender parentGender, ref Pawn __result)
        {
            if (!pawn.RaceProps.IsFlesh)
            {
                __result = null;
                return false;
            }
            if (pawn.relations == null)
            {
                __result = null;
                return false;
            }

            List<Pawn> parents = pawn.relations.DirectRelations.Where(r => r.def == PawnRelationDefOf.Parent).Select(r => r.otherPawn).ToList();
            if (parents.Count == 0)
            {
                __result = null;
                return false;
            }
            if (parents.Count == 1)
            {
                __result = parentGender == Gender.Male ? parents[0] : null;
                return false;
            }

            if (parentGender == Gender.Male)
            {
                __result = parents.MinBy(p => p.thingIDNumber);
            }
            else
            {
                __result = parents.MaxBy(p => p.thingIDNumber);
            }
            return false;
        }
    }
}
