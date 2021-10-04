using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempRoles.System.Patches
{
    [HarmonyPatch(typeof(ReservedSlot), nameof(ReservedSlot.HasReservedSlot))]
    public class ReservedSlotRolePatch
    {
        public static void Postfix(string userId, ref bool __result)
        {
            if (!__result)
            {
                __result = Extensions.HaveReservedSlotRole(userId);
            }
        }
    }
}
