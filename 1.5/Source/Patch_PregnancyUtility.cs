using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace UniversalPregnancy
{
    [HarmonyPatch(typeof(PregnancyUtility))]
    [HarmonyPatch(nameof(PregnancyUtility.CanEverProduceChild))]
    public static class Patch_PregnancyUtility_CanEverProduceChild
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            bool foundGender = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (!foundGender && instruction.opcode == OpCodes.Bne_Un_S)
                {
                    yield return new CodeInstruction(OpCodes.Pop);
                    yield return new CodeInstruction(OpCodes.Pop);
                    instruction.opcode = OpCodes.Br_S;
                    foundGender = true;
                }

                if (instruction.opcode == OpCodes.Stloc_0)
                {
                    CodeInstruction popInstruction = new CodeInstruction(OpCodes.Pop);
                    popInstruction.labels.AddRange(instruction.labels);
                    yield return popInstruction;
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    instruction.labels.Clear();
                }

                if (instruction.opcode == OpCodes.Stloc_1)
                {
                    CodeInstruction popInstruction = new CodeInstruction(OpCodes.Pop);
                    popInstruction.labels.AddRange(instruction.labels);
                    yield return popInstruction;
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    instruction.labels.Clear();
                }

                yield return instruction;
            }
        }
    }

    [HarmonyPatch(typeof(PregnancyUtility))]
    [HarmonyPatch(nameof(PregnancyUtility.ApplyBirthOutcome_NewTemp))]
    public static class Patch_PregnancyUtility_ApplyBirthOutcome_NewTemp
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            bool foundDirectRelations = false;
            bool finished = false;

            foreach (CodeInstruction instruction in instructions)
            {
                if (!foundDirectRelations && instruction.opcode == OpCodes.Callvirt && (MethodInfo)instruction.operand == UniversalPregnancyRefs.m_Pawn_RelationsTracker_AddDirectRelation)
                {
                    foundDirectRelations = true;
                }

                if (foundDirectRelations && !finished && instruction.opcode == OpCodes.Beq_S)
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldarg_S, 5);
                    yield return new CodeInstruction(OpCodes.Ldarg_S, 6);
                    yield return new CodeInstruction(OpCodes.Beq_S, instruction.operand);
                    finished = true;
                    continue;
                }

                yield return instruction;
            }
        }

        public static void Postfix(Pawn geneticMother, Pawn father, Thing __result)
        {
            if (__result is Pawn)
            {
                Pawn baby = (Pawn)__result;
                if (baby.relations is Pawn_RelationsTracker)
                {
                    Pawn_RelationsTracker relations = (Pawn_RelationsTracker)baby.relations;
                    relations.mother = geneticMother;
                    relations.father = father;
                }
            }
        }
    }
}
