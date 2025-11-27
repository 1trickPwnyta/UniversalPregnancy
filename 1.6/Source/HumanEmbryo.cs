using Verse;

namespace UniversalPregnancy
{
    public class HumanEmbryo : RimWorld.HumanEmbryo
    {
        public Pawn mother;
        public Pawn father;

        public new void ExposeData()
        {
            Scribe_References.Look(ref mother, "mother");
            Scribe_References.Look(ref father, "father");
        }
    }
}
