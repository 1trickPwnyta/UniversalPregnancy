using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using Verse.AI;

namespace UniversalPregnancy
{
    public static class UniversalPregnancyRefs
    {
        public static readonly MethodInfo m_Utility_ShuffleTwo = AccessTools.Method(typeof(Utility), nameof(Utility.ShuffleTwo));
        public static readonly MethodInfo m_ModsConfig_BiotechActive = AccessTools.Method(typeof(ModsConfig), "get_BiotechActive");
        public static readonly MethodInfo m_JobDriver_Lovin_Partner = AccessTools.Method(typeof(JobDriver_Lovin), "get_Partner");
        public static readonly MethodInfo m_Pawn_RelationsTracker_AddDirectRelation = AccessTools.Method(typeof(RimWorld.Pawn_RelationsTracker), nameof(RimWorld.Pawn_RelationsTracker.AddDirectRelation));
        public static readonly MethodInfo m_Utility_SetParents = AccessTools.Method(typeof(Utility), nameof(Utility.SetParents));
        public static readonly MethodInfo m_HumanEmbryo_TryPopulateGenes = AccessTools.Method(typeof(RimWorld.HumanEmbryo), nameof(RimWorld.HumanEmbryo.TryPopulateGenes));

        public static readonly FieldInfo f_JobDriver_pawn = AccessTools.Field(typeof(JobDriver), nameof(JobDriver.pawn));
        public static readonly FieldInfo f_JobDriver_Lovin_PregnancyChance = AccessTools.Field(typeof(JobDriver_Lovin), "PregnancyChance");
        public static readonly FieldInfo f_Patch_JobDriver_Lovin_MakeNewToils_TweakPregnancyChance = AccessTools.Field(typeof(Patch_JobDriver_Lovin), nameof(Patch_JobDriver_Lovin.TweakPregnancyChance));
        public static readonly FieldInfo f_Pawn_relations = AccessTools.Field(typeof(Pawn), nameof(Pawn.relations));
        public static readonly FieldInfo f_Pawn_gender = AccessTools.Field(typeof(Pawn), nameof(Pawn.gender));
    }
}
