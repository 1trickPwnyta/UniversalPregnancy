using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace UniversalPregnancy
{
    [HarmonyPatch(typeof(PawnRelationWorker_Sibling))]
    [HarmonyPatch(nameof(PawnRelationWorker_Sibling.InRelation))]
    public static class Patch_PawnRelationWorker_Sibling
    {
        public static bool Prefix(Pawn me, Pawn other, ref bool __result)
        {
            IEnumerable<Pawn> myParents = new[] { ((Pawn_RelationsTracker)me.relations).mother, ((Pawn_RelationsTracker)me.relations).father };
            IEnumerable<Pawn> otherParents = new[] { ((Pawn_RelationsTracker)other.relations).mother, ((Pawn_RelationsTracker)other.relations).father };
            __result = me != other && myParents.Intersect(otherParents).Count() == myParents.Count();
            return false;
        }
    }
}
