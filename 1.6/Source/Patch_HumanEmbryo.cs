using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;

namespace UniversalPregnancy
{
    [HarmonyPatch(typeof(RimWorld.HumanEmbryo))]
    [HarmonyPatch("CanImplantReport")]
    public static class Patch_HumanEmbryo_CanImplantReport
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
    }

    [HarmonyPatch(typeof(RimWorld.HumanEmbryo))]
    [HarmonyPatch("get_Mother")]
    public static class Patch_HumanEmbryo_get_Mother
    {
        public static void Postfix(RimWorld.HumanEmbryo __instance, ref Pawn __result)
        {
            if (__instance is HumanEmbryo)
            {
                HumanEmbryo embryo = (HumanEmbryo)__instance;
                if (embryo.mother != null)
                {
                    __result = embryo.mother;
                }
            }
        }
    }

    [HarmonyPatch(typeof(RimWorld.HumanEmbryo))]
    [HarmonyPatch("get_Father")]
    public static class Patch_HumanEmbryo_get_Father
    {
        public static void Postfix(RimWorld.HumanEmbryo __instance, ref Pawn __result)
        {
            if (__instance is HumanEmbryo)
            {
                HumanEmbryo embryo = (HumanEmbryo)__instance;
                if (embryo.father != null)
                {
                    __result = embryo.father;
                }
            }
        }
    }

    [HarmonyPatch(typeof(RimWorld.HumanEmbryo))]
    [HarmonyPatch(nameof(RimWorld.HumanEmbryo.ExposeData))]
    public static class Patch_HumanEmbryo_ExposeData
    {
        public static void Prefix(RimWorld.HumanEmbryo __instance)
        {
            if (__instance is HumanEmbryo)
            {
                HumanEmbryo embryo = (HumanEmbryo)__instance;
                embryo.ExposeData();
            }
        }
    }
}
