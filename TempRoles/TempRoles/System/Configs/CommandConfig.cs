using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempRoles.System.Configs
{
    public class CommandConfig
    {
        public string CommandName { get; set; } = "setrole";

        public string[] CommandAliases { get; set; } = new string[] { "srole" };

        public string CommandDescription { get; set; } = "Da un rol con tiempo de expiracion, tambien puedes añadir tiempo usando el addtime como argumento.";

        
    }
}
