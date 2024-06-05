using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace UniversalPregnancy
{
    [HarmonyPatch(typeof(SocialCardUtility))]
    [HarmonyPatch("DrawPregnancyApproach")]
    public static class Patch_SocialCardUtility_DrawPregnancyApproach
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                // Look for call to CanEverProduceChild and replace it with a call to our own function
                if (instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == UniversalPregnancyRefs.m_RimWorld_PregnancyUtility_CanEverProduceChild)
                {
                    Debug.Log("Found CanEverProduceChild");
                    instruction.operand = UniversalPregnancyRefs.m_PregnancyUtility_CanEverProduceChild;
                }
                yield return instruction;
            }
        }
    }
}
