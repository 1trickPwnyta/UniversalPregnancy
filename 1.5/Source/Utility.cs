using RimWorld;
using Verse;

namespace UniversalPregnancy
{
    public static class Utility
    {
        public static void SetParents(HumanEmbryo embryo, HumanOvum ovum, Pawn father)
        {
            if (embryo is HumanEmbryo)
            {
                embryo.mother = ovum.GetComp<CompHasPawnSources>().pawnSources.FirstOrFallback();
                embryo.father = father;
            }
        }

        public static void ShuffleTwo(object a, object b, ref object c, ref object d)
        {
            if (Rand.Chance(0.5f))
            {
                c = a;
                d = b;
            }
            else
            {
                c = b;
                d = a;
            }
        }
    }
}
