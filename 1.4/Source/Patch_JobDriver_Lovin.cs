using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace UniversalPregnancy
{
    [HarmonyPatch(typeof(JobDriver_Lovin))]
    [HarmonyPatch("MakeNewToils")]
    public static class Patch_JobDriver_Lovin_MakeNewToils
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            bool foundBiotechActive = false;

            foreach (CodeInstruction instruction in instructions)
            {
                // Find instruction checking if Biotech is active
                if (!foundBiotechActive && (MethodInfo)instruction.operand == UniversalPregnancyRefs.m_ModsConfig_BiotechActive)
                {
                    foundBiotechActive = true;
                }

                // Find instruction where we set variable pawn and set it to this.pawn instead
                if (foundBiotechActive && instruction.opcode == OpCodes.Stloc_2)
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldfld, UniversalPregnancyRefs.f_JobDriver_pawn);
                }

                // Find instruction where we set variable pawn2 and set it to this.Partner instead
                if (foundBiotechActive && instruction.opcode == OpCodes.Stloc_3)
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call, UniversalPregnancyRefs.m_JobDriver_Lovin_Partner);
                }

                // Return the instruction
                yield return instruction;
            }
        }
    }
}
