using System;
using MelonLoader;
using HarmonyLib;
using MyOWOVest;
using Assets.Scripts.Records;

[assembly: MelonInfo(typeof(KeepTalkingAndNobodyExplodes_OWO.KeepTalkingAndNobodyExplodes_OWO), "KeepTalkingAndNobodyExplodes_OWO", "1.0.0", "Florian Fahrenberger")]
[assembly: MelonGame("Steel Crate Games", "Keep Talking and Nobody Explodes")]


namespace KeepTalkingAndNobodyExplodes_OWO
{
    public class KeepTalkingAndNobodyExplodes_OWO : MelonMod
    {
        public static TactsuitVR tactsuitVr;

        public override void OnInitializeMelon()
        {
            tactsuitVr = new TactsuitVR();
        }

        [HarmonyPatch(typeof(Bomb), "Detonate", new Type[] { })]
        public class OWO_BombExploded
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.PlayBackFeedback("BombExplode");
            }
        }

        [HarmonyPatch(typeof(Bomb), "BombSolved", new Type[] { })]
        public class OWO_BombSolved
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.PlayBackFeedback("BombSolved");
            }
        }

        [HarmonyPatch(typeof(Bomb), "OnStrike", new Type[] { typeof(BombComponent) })]
        public class OWO_Mistake
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.PlayBackFeedback("ComponentStrike");
            }
        }

        [HarmonyPatch(typeof(Bomb), "OnPass", new Type[] { typeof(BombComponent) })]
        public class OWO_CorrectEntry
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.PlayBackFeedback("ComponentPass");
            }
        }
        
        [HarmonyPatch(typeof(BombComponent), "HandleStrike", new Type[] { typeof(InteractionTypeEnum) })]
        public class OWO_ComponentStrike
        {
            [HarmonyPostfix]
            public static void Postfix(BombComponent __instance, InteractionTypeEnum interactionType)
            {
                if (interactionType == InteractionTypeEnum.CutWire)
                    tactsuitVr.PlayBackFeedback("ElectricShock");
            }
        }

        [HarmonyPatch(typeof(TimerComponent), "Update", new Type[] { })]
        public class bhaptics_UpdateTimer
        {
            [HarmonyPostfix]
            public static void Postfix(TimerComponent __instance)
            {
                if ((__instance.TimeRemaining < 61f) && (__instance.TimeRemaining > 59f)) tactsuitVr.PlayBackFeedback("ThreeHeartBeats");
                if ((__instance.TimeRemaining < 31f) && (__instance.TimeRemaining > 29f)) tactsuitVr.PlayBackFeedback("ThreeHeartBeats");
                if ((__instance.TimeRemaining < 11f) && (__instance.TimeRemaining > 9f)) tactsuitVr.PlayBackFeedback("ThreeHeartBeats");
            }
        }

    }
}
