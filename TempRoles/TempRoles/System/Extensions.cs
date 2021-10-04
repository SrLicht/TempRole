using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempRoles.System
{
    public static class Extensions
    {
        internal static Dictionary<string, string> TempUserRoles = new Dictionary<string, string>();

        public static bool HaveReservedSlotRole(string userId)
        {
            return Plugin.Instance.Config.ReservedSlotRoles.Contains(GetRole(userId));
        }

        public static string GetRole(string userId)
        {
            return TempUserRoles.TryGetValue(userId, out var ret) ? ret : null;
        }
    }
}
