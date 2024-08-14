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
        public static void Postfix(Pawn pawn, Gender parentGender, ref Pawn __result)
        {
            if (pawn.relations != null)
            {
                List<Pawn> parents = pawn.relations.DirectRelations.Where(r => r.def == PawnRelationDefOf.Parent).Select(r => r.otherPawn).ToList();
                if (parents.Count > 1 && parents[0].gender == parents[1].gender)
                {
                    if (parentGender == Gender.Male)
                    {
                        __result = parents.MinBy(p => p.thingIDNumber);
                    }
                    else
                    {
                        __result = parents.MaxBy(p => p.thingIDNumber);
                    }
                }
            }
        }
    }
}
