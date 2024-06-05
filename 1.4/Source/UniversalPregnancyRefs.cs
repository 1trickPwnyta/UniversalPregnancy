using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using Verse.AI;

namespace UniversalPregnancy
{
    public static class UniversalPregnancyRefs
    {
        public static readonly MethodInfo m_ModsConfig_BiotechActive = AccessTools.Method(typeof(ModsConfig), nameof(ModsConfig.BiotechActive));
        public static readonly MethodInfo m_JobDriver_Lovin_Partner = AccessTools.Method(typeof(JobDriver_Lovin), "get_Partner");

        public static readonly FieldInfo f_JobDriver_pawn = AccessTools.Field(typeof(JobDriver), nameof(JobDriver.pawn));
    }
}
