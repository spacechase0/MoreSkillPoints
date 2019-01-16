using Harmony;
using Pathea;
using Pathea.FeatureNs;
using Pathea.ModuleNs;
using Pathea.PlayerAbilityNs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MoreSkillPoints
{
    [HarmonyPatch(typeof(Player))]
    [HarmonyPatch("Deserialize")]
    public static class PlayerDeserializeHook
    {
        public static void Postfix( Player __instance )
        {
            var totalPoints = (int) AccessTools.Field(typeof(PlayerAbilityModule), "totalPoint").GetValue(Module<PlayerAbilityModule>.Self);
            if (totalPoints == __instance.ActorLevel - 1)
            {
                Module<PlayerAbilityModule>.Self.SetTotalPointForFixing((__instance.ActorLevel - 1) * 2);
            }
        }
    }

    [HarmonyPatch(typeof(Player))]
    [HarmonyPatch("AddExp")]
    public static class PlayerAddExpHook
    {
        public static bool Prefix(Player __instance, int exp, bool showTips)
        {
            if (__instance.actor == null)
            {
                return false;
            }
            int actorLevel = __instance.ActorLevel;
            exp += Mathf.RoundToInt(Module<FeatureModule>.Self.ModifyFloat(FeatureType.ExpGain, new object[]
            {
                (float)exp
            }));
            __instance.actor.AddExp(exp);
            if (actorLevel != __instance.ActorLevel)
            {
                Module<PlayerAbilityModule>.Self.GainPoint((__instance.ActorLevel - actorLevel) * 2);
            }
            if (showTips)
            {
            }
            return false;
        }
    }
}
