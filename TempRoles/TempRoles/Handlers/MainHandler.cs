using Exiled.API.Features;
using Exiled.Events.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempRoles.System;

namespace TempRoles.Handlers
{
    public class MainHandler : Base.Handler
    {
        public override void Start()
        {
            Exiled.Events.Handlers.Player.Verified += OnPlayerJoined;
            Exiled.Events.Handlers.Server.RoundEnded += OnRoundEnd;

            Extensions.TempUserRoles = Plugin.roleDB.GetPlayersRolesSync();
        }

        public override void Stop()
        {
            Exiled.Events.Handlers.Player.Verified -= OnPlayerJoined;
            Exiled.Events.Handlers.Server.RoundEnded -= OnRoundEnd;
            Extensions.TempUserRoles.Clear();
        }

        /// <summary>
        /// Actualiza la base de datos, al final de la ronda.
        /// </summary>
        public async void OnRoundEnd(RoundEndedEventArgs _)
        {
            Extensions.TempUserRoles = await Plugin.roleDB.GetPlayersRoles();
        }

        
        /// <summary>
        /// Si el jugador tiene un rol en la base de datos, esto se lo dara.
        /// </summary>
        private async void OnPlayerJoined(VerifiedEventArgs ev)
        {
            if (ev.Player is null)
                return;

            CheckForVip(ev.Player);

            if (Extensions.GetRole(ev.Player.UserId) != null)
            {
                // Esto es para actualizar el nombre en la base de datos.
                await Plugin.roleDB.SetPlayerName(ev.Player.UserId, ev.Player.Nickname);
            }
        }


        /// <summary>
        /// Checkea si el Jugador tiene VIP o algun rol dado por la DataBase.
        /// </summary>
        /// <param name="player"></param>
        private void CheckForVip(Player player)
        {
            var role = Extensions.GetRole(player.UserId);
            if (role != null)
            {
                if (ServerStatic.GetPermissionsHandler()._groups.ContainsKey(role))
                {
                    player.Group = ServerStatic.GetPermissionsHandler()._groups[role];
                }
                else
                {
                    Log.Error($"El grupo {role} no existe.");
                }
            }
        }


        

    }
}
