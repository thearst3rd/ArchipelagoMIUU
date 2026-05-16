using HarmonyLib;
using UnityEngine;

namespace ArchipelagoMIUU.Patches
{
    //Handle medal time icons for Treasureboxsanity.
    [HarmonyPatch(typeof(MedalsDisplay), "Setup")]
    class MedalsDisplay_Setup_Patch
    {
        public static bool Prefix(MedalsDisplay __instance, float silver, float gold)
        {
            if (!ConnectHandler.Authenticated)
            {
                return true;
            }
            bool flag = LocationHandler.isLocationChecked(LevelSelect.instance.level.id + "-tb");
            string text = flag ? "<space=0.5em> <sprite=9>" : "<space=0.5em> <sprite=8>";
            __instance.MedalTimes.text = "<sprite=1> " + SegmentedTime.SPTimeText(silver) + "<space=0.5em> <sprite=2> " + SegmentedTime.SPTimeText(gold) + text;
            return false;
        }
    }

}