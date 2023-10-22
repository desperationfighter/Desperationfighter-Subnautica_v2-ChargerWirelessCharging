using ChargerWirelessCharging.Mono;
using HarmonyLib;

namespace ChargerWirelessCharging.Patches
{
    [HarmonyPatch(typeof(Charger))]
    [HarmonyPatch(nameof(Charger.Start))]
    public static class Charger_Start_Patch
    {
        [HarmonyPostfix]
        private static void PostFix(Charger __instance)
        {
            __instance.gameObject.AddComponent<WirelessChargerMonoObject>();
        }
    }
}