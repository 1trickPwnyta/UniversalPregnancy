using Verse;

namespace UniversalPregnancy
{
    public static class Utility
    {
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
