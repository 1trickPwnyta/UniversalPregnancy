using RimWorld;
using Verse;

namespace UniversalPregnancy
{
    public class PregnancyUtility
    {
        public static AcceptanceReport CanEverProduceChild(Pawn first, Pawn second)
        {
            if (first.Dead)
            {
                return "PawnIsDead".Translate(first.Named("PAWN"));
            }
            if (second.Dead)
            {
                return "PawnIsDead".Translate(second.Named("PAWN"));
            }
            Pawn pawn = first;
            Pawn pawn2 = second;
            bool flag = pawn.GetStatValue(StatDefOf.Fertility, true, -1) <= 0f;
            bool flag2 = pawn2.GetStatValue(StatDefOf.Fertility, true, -1) <= 0f;
            if (flag && flag2)
            {
                return "PawnsAreInfertile".Translate(pawn.Named("PAWN1"), pawn2.Named("PAWN2")).Resolve();
            }
            if (flag != flag2)
            {
                return "PawnIsInfertile".Translate((flag ? pawn : pawn2).Named("PAWN")).Resolve();
            }
            bool flag3 = !pawn.ageTracker.CurLifeStage.reproductive;
            bool flag4 = !pawn2.ageTracker.CurLifeStage.reproductive;
            if (flag3 && flag4)
            {
                return "PawnsAreTooYoung".Translate(pawn.Named("PAWN1"), pawn2.Named("PAWN2")).Resolve();
            }
            if (flag3 != flag4)
            {
                return "PawnIsTooYoung".Translate((flag3 ? pawn : pawn2).Named("PAWN")).Resolve();
            }
            bool flag5 = pawn2.Sterile() && RimWorld.PregnancyUtility.GetPregnancyHediff(pawn2) == null;
            bool flag6 = pawn.Sterile();
            if (flag6 && flag5)
            {
                return "PawnsAreSterile".Translate(pawn.Named("PAWN1"), pawn2.Named("PAWN2")).Resolve();
            }
            if (flag6 != flag5)
            {
                return "PawnIsSterile".Translate((flag6 ? pawn : pawn2).Named("PAWN")).Resolve();
            }
            return true;
        }
    }
}
