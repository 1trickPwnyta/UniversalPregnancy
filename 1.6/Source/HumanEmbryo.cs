using Verse;

namespace UniversalPregnancy
{
    public class HumanEmbryo : RimWorld.HumanEmbryo
    {
        public Pawn mother;
        public Pawn father;

        public new void ExposeData()
        {
            Scribe_References.Look<Pawn>(ref mother, "mother");
            Scribe_References.Look<Pawn>(ref father, "father");
        }
    }
}
