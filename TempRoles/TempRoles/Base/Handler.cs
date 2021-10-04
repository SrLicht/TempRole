﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempRoles.System;

namespace TempRoles.Base
{
    /// <summary>
    /// Base API for CerberusTweaks allow create handlers in a "easily" way.
    /// </summary>
    public abstract class Handler
    {
        /// <summary>
        /// Plugin Instance
        /// </summary>
        protected Plugin Plugin => Plugin.Instance;

        /// <summary>
        /// Triggered when plugin is loaded
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// Triggered when plugin is stopped or server restart.
        /// </summary>
        public abstract void Stop();
    }
}
