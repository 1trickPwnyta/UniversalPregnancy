using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace UniversalPregnancy
{
    [HarmonyPatch(typeof(PawnComponentsUtility))]
    [HarmonyPatch(nameof(PawnComponentsUtility.CreateInitialComponents))]
    public static class Patch_PawnComponentsUtility_CreateInitialComponents
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            Debug.Log("Entered CreateInitialComponents transpiler");
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Newobj && (ConstructorInfo)instruction.operand == UniversalPregnancyRefs.m_RimWorld_Pawn_RelationsTracker_ctor)
                {
                    Debug.Log("Using our own Pawn_RelationsTracker");
                    instruction.operand = UniversalPregnancyRefs.m_Pawn_RelationsTracker_ctor;
                }
                yield return instruction;
            }
        }
    }

    [HarmonyPatch(typeof(PawnComponentsUtility))]
    [HarmonyPatch(nameof(PawnComponentsUtility.AddAndRemoveDynamicComponents))]
    public static class Patch_PawnComponentsUtility_AddAndRemoveDynamicComponents
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            Debug.Log("Entered AddAndRemoveDynamicComponents transpiler");
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Newobj && (ConstructorInfo)instruction.operand == UniversalPregnancyRefs.m_RimWorld_Pawn_RelationsTracker_ctor)
                {
                    Debug.Log("Using our own Pawn_RelationsTracker");
                    instruction.operand = UniversalPregnancyRefs.m_Pawn_RelationsTracker_ctor;
                }
                yield return instruction;
            }
        }
    }
}
