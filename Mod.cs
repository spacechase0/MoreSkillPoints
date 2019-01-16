using Harmony;
using spacechase0.MiniModLoader.Api.Mods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MoreSkillPoints
{
    public class Mod : IMod
    {
        public override void AfterModsLoaded()
        {
            var harmony = HarmonyInstance.Create("spacechase0.MoreSkillPoints");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
