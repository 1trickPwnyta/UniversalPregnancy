using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace UniversalPregnancy
{
    [HarmonyPatch(typeof(JobDriver_Lovin))]
    [HarmonyPatch("<MakeNewToils>b__12_4")]
    public static class Patch_JobDriver_Lovin_MakeNewToils
    {
        public static float TestPregnancyChance = 100.0f;

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            Debug.Log("Entered JobDriver_Lovin transpiler");

            bool foundBiotechActive = false;
            bool foundPawns = false;

            foreach (CodeInstruction instruction in instructions)
            {
                // Find instruction checking if Biotech is active
                if (!foundBiotechActive && instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == UniversalPregnancyRefs.m_ModsConfig_BiotechActive)
                {
                    Debug.Log("Found BiotechActive");
                    foundBiotechActive = true;
                }

                // Find instruction where we set variable pawn and set it to this.pawn instead
                if (foundBiotechActive && !foundPawns && instruction.opcode == OpCodes.Stloc_3)
                {
                    Debug.Log("Set pawn to this.pawn and pawn2 to this.Partner");
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldfld, UniversalPregnancyRefs.f_JobDriver_pawn);
                    yield return new CodeInstruction(OpCodes.Stloc_2);
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call, UniversalPregnancyRefs.m_JobDriver_Lovin_Partner);
                    yield return new CodeInstruction(OpCodes.Stloc_3);
                    foundPawns = true;
                    continue;
                }

#if DEBUG
                // Replace vanilla base pregnancy chance with test value
                if (instruction.opcode == OpCodes.Ldsfld && (FieldInfo)instruction.operand == UniversalPregnancyRefs.f_JobDriver_Lovin_PregnancyChance)
                {
                    Debug.Log("Set base pregnancy chance to test value");
                    instruction.operand = UniversalPregnancyRefs.f_Patch_JobDriver_Lovin_MakeNewToils_TestPregnancyChance;
                }
#endif

                // Return the instruction
                yield return instruction;
            }
        }
    }
}
