using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace UniversalPregnancy
{
    public class UniversalPregnancySettings : ModSettings
    {
        public static bool UseFemaleFertilityForMales = false;
        public static bool UseFemaleFertilityForFemales = true;

        private static string GetFertilityLabel(bool female)
        {
            return female ? "UniversalPregnancy_FemaleFertility".Translate() : "UniversalPregnancy_MaleFertility".Translate();
        }

        public static void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();

            listingStandard.Begin(inRect);

            if (listingStandard.ButtonTextLabeled("UniversalPregnancy_FemaleFertilitySetting".Translate(), GetFertilityLabel(UseFemaleFertilityForFemales)))
            {
                FloatMenuOption femaleOption = new FloatMenuOption(GetFertilityLabel(true), delegate
                {
                    UseFemaleFertilityForFemales = true;
                });
                FloatMenuOption maleOption = new FloatMenuOption(GetFertilityLabel(false), delegate
                {
                    UseFemaleFertilityForFemales = false;
                });
                Find.WindowStack.Add(new FloatMenu(new List<FloatMenuOption>() { femaleOption, maleOption }));
            }

            if (listingStandard.ButtonTextLabeled("UniversalPregnancy_MaleFertilitySetting".Translate(), GetFertilityLabel(UseFemaleFertilityForMales)))
            {
                FloatMenuOption femaleOption = new FloatMenuOption(GetFertilityLabel(true), delegate
                {
                    UseFemaleFertilityForMales = true;
                });
                FloatMenuOption maleOption = new FloatMenuOption(GetFertilityLabel(false), delegate
                {
                    UseFemaleFertilityForMales = false;
                });
                Find.WindowStack.Add(new FloatMenu(new List<FloatMenuOption>() { femaleOption, maleOption }));
            }

            listingStandard.End();
        }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref UseFemaleFertilityForMales, "UseFemaleFertilityForMales", false);
            Scribe_Values.Look(ref UseFemaleFertilityForFemales, "UseFemaleFertilityForFemales", true);
        }
    }
}