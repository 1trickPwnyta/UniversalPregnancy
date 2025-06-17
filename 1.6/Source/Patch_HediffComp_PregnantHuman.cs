using HarmonyLib;
using Verse;

namespace UniversalPregnancy
{
    [HarmonyPatch(typeof(HediffComp_PregnantHuman))]
    [HarmonyPatch("get_CompTipStringExtra")]
    public static class Patch_HediffComp_PregnantHuman
    {
        public static void Postfix(HediffComp_PregnantHuman __instance, ref string __result)
        {
            Hediff_Pregnant hediff_Pregnant = (Hediff_Pregnant)__instance.parent;
            TaggedString t = "";
            if (hediff_Pregnant.Father != null && hediff_Pregnant.Father != __instance.parent.pawn)
            {
                t += "\n" + "FatherTip".Translate() + ": " + hediff_Pregnant.Father.LabelShort.CapitalizeFirst().Colorize(ColoredText.NameColor);
            }
            if (hediff_Pregnant.Mother != null && hediff_Pregnant.Mother != __instance.parent.pawn)
            {
                t += "\n" + "MotherTip".Translate() + ": " + hediff_Pregnant.Mother.LabelShort.CapitalizeFirst().Colorize(ColoredText.NameColor);
            }
            __result = t.Resolve();
        }
    }
}
