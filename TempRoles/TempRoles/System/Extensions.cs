using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        /// <summary>
        /// Convierte una <see cref="string"/> a <see cref="DateTime"/>. Acepta <see cref="RegexOptions"/>
        /// </summary>
        /// <param name="duration"></param>
        /// <returns><see cref="DateTime"/></returns>
        public static DateTime ConvertToDateTime(string duration)
        {
            var durationpattern = @"(?:(\d+)y)?(?:(\d+)d)?(?:(\d+)h)?(?:(\d+)m)?(?:(\d+)s)?";
            var dateTime = DateTime.UtcNow;
            var match = Regex.Match(duration, durationpattern, RegexOptions.IgnoreCase);
            if (!match.Success)
                throw new ArgumentException();
            if (match.Groups[1].Captures.Count > 0) dateTime = dateTime.AddYears((int)double.Parse(match.Groups[1].Captures[0].Value));
            if (match.Groups[2].Captures.Count > 0) dateTime = dateTime.AddDays(double.Parse(match.Groups[2].Captures[0].Value));
            if (match.Groups[3].Captures.Count > 0) dateTime = dateTime.AddHours(double.Parse(match.Groups[3].Captures[0].Value));
            if (match.Groups[4].Captures.Count > 0) dateTime = dateTime.AddMinutes(double.Parse(match.Groups[4].Captures[0].Value));
            if (match.Groups[5].Captures.Count > 0) dateTime = dateTime.AddSeconds(double.Parse(match.Groups[5].Captures[0].Value));
            return dateTime;
        }
    }
}
