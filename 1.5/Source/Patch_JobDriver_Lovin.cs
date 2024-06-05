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
            bool foundBiotechActive = false;
            bool foundPawns = false;

            foreach (CodeInstruction instruction in instructions)
            {
                // Find instruction checking if Biotech is active
                if (!foundBiotechActive && instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == UniversalPregnancyRefs.m_ModsConfig_BiotechActive)
                {
                    foundBiotechActive = true;
                }

                // Find instruction where we set variables pawn and pawn2 and set them randomly to this.pawn and this.Partner
                if (foundBiotechActive && !foundPawns && instruction.opcode == OpCodes.Stloc_3)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldfld, UniversalPregnancyRefs.f_JobDriver_pawn);
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call, UniversalPregnancyRefs.m_JobDriver_Lovin_Partner);
                    yield return new CodeInstruction(OpCodes.Ldloca_S, 2);
                    yield return new CodeInstruction(OpCodes.Ldloca_S, 3);
                    yield return new CodeInstruction(OpCodes.Call, UniversalPregnancyRefs.m_Utility_ShuffleTwo);
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
