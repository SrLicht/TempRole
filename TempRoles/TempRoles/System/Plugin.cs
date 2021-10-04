using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempRoles.System.DB;

namespace TempRoles.System
{
    public class Plugin : Plugin<Config>
    {

        #region Variables and shit
        /// <summary>
        /// Gets the only existing instance of this plugin.
        /// </summary>
        public static Plugin Instance;

        /// <summary>
        /// Database.
        /// </summary>
        public RoleDatabase roleDB;

        /// <inheritdoc/>
        public override string Name => "TempRoles";

        /// <summary>
        /// Lista de Handlers.
        /// </summary>
        private List<Base.Handler> handlers;

        /// <inheritdoc/>
        public override string Prefix => "temp_roles";

        /// <inheritdoc/>
        public override string Author => "Cerberus Team: Uriel, Licht, Beryl";

        /// <inheritdoc/>
        public override Version Version => new Version(3, 0, 5);

        /// <inheritdoc/>
        public override Version RequiredExiledVersion => new Version(3, 0, 0);
        #endregion


        public override void OnEnabled()
        {
            Instance = this;
            handlers = new List<Base.Handler>() { new Handlers.MainHandler() };

            foreach (var item in handlers)
            {
                item.Start();
            }
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            foreach (var item in handlers)
            {
                item.Stop();
            }
            Instance = null;

            base.OnDisabled();
        }
    }
}
