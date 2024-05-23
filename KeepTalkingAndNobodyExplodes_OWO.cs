﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }
}
