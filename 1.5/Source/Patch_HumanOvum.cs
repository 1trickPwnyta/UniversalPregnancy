using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
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
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Callvirt && (MethodInfo)instruction.operand == UniversalPregnancyRefs.m_HumanEmbryo_TryPopulateGenes)
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    yield return new CodeInstruction(OpCodes.Call, UniversalPregnancyRefs.m_Utility_SetParents);
                    yield return new CodeInstruction(OpCodes.Ldloc_0);
                }
                yield return instruction;
            }
        }
    }
}
