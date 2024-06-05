using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;

namespace UniversalPregnancy
{
    [HarmonyPatch(typeof(HumanOvum))]
    [HarmonyPatch("CanFertilizeReport")]
    public static class Patch_HumanOvum_CanFertilizeReport
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            bool foundGender = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (!foundGender && instruction.opcode == OpCodes.Beq_S)
                {
                    yield return new CodeInstruction(OpCodes.Pop);
                    yield return new CodeInstruction(OpCodes.Pop);
                    instruction.opcode = OpCodes.Br_S;
                    foundGender = true;
                }

                yield return instruction;
            }
        }

        public static void Postfix(HumanOvum __instance, Pawn pawn, ref AcceptanceReport __result)
        {
            if (__result.Accepted)
            {
                List<Pawn> pawnSources = __instance.GetComp<CompHasPawnSources>().pawnSources;
                if (pawnSources.Contains(pawn))
                {
                    __result = "UniversalPregnancy_CannotFertilizeOwnOvum".Translate();
                }
            }
        }
    }

    [HarmonyPatch(typeof(HumanOvum))]
    [HarmonyPatch(nameof(HumanOvum.ProduceEmbryo))]
    public static class Patch_HumanOvum_ProduceEmbryo
    {
        public static void Postfix(HumanOvum __instance, Pawn father, Thing __result)
        {
            HumanEmbryo embryo = (HumanEmbryo)__result;
            embryo.mother = __instance.GetComp<CompHasPawnSources>().pawnSources.FirstOrFallback();
            embryo.father = father;
        }
    }
}
