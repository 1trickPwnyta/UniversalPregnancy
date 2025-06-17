using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace UniversalPregnancy
{
    [HarmonyPatch(typeof(ITab_Pawn_Feeding))]
    [HarmonyPatch("DrawRow")]
    public static class Patch_ITab_Pawn_Feeding
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Ldfld && instruction.operand is FieldInfo info && (info == typeof(PawnRelationDef).Field(nameof(PawnRelationDef.labelFemale)) || info == typeof(Def).Field(nameof(Def.label))))
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_2);
                    yield return new CodeInstruction(OpCodes.Call, typeof(PawnRelationDef).Method(nameof(PawnRelationDef.GetGenderSpecificLabelCap)));
                    continue;
                }

                yield return instruction;
            }
        }
    }
}
