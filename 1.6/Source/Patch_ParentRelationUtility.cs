using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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

    [HarmonyPatch(typeof(ParentRelationUtility))]
    [HarmonyPatch(nameof(ParentRelationUtility.SetFather))]
    public static class Patch_ParentRelationUtility_SetFather
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Bne_Un_S)
                {
                    yield return new CodeInstruction(OpCodes.Pop);
                    yield return new CodeInstruction(OpCodes.Pop);
                    instruction.opcode = OpCodes.Br;
                }

                yield return instruction;
            }
        }
    }

    [HarmonyPatch(typeof(ParentRelationUtility))]
    [HarmonyPatch(nameof(ParentRelationUtility.SetMother))]
    public static class Patch_ParentRelationUtility_SetMother
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            bool finished = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (!finished && instruction.opcode == OpCodes.Beq_S)
                {
                    yield return new CodeInstruction(OpCodes.Pop);
                    yield return new CodeInstruction(OpCodes.Pop);
                    instruction.opcode = OpCodes.Br;
                    finished = true;
                }

                yield return instruction;
            }
        }
    }
}
