using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace UniversalPregnancy
{
    [HarmonyPatch(typeof(PawnGenerator))]
    [HarmonyPatch("GenerateInitialHediffs")]
    public static class Patch_PawnGenerator
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionsList = instructions.ToList();
            for (int i = 0; i < instructionsList.Count; i++)
            {
                CodeInstruction instruction = instructionsList[i];
                if (instruction.opcode == OpCodes.Ldfld && (FieldInfo)instruction.operand == UniversalPregnancyRefs.f_Pawn_gender)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Dup);
                    i++;
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
