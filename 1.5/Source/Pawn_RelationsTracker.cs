using Verse;

namespace UniversalPregnancy
{
    public class Pawn_RelationsTracker : RimWorld.Pawn_RelationsTracker
    {
        public Pawn mother;
        public Pawn father;

        public Pawn_RelationsTracker(Pawn pawn) : base(pawn)
        {

        }

        public new void ExposeData()
        {
            Scribe_References.Look<Pawn>(ref mother, "mother");
            Scribe_References.Look<Pawn>(ref father, "father");
        }
    }
}
