using CommandSystem;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempRoles.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    class MainCommand : ICommand
    {
        public string Command => System.Plugin.Instance.Config.CommandConfig.CommandName;

        public string[] Aliases => System.Plugin.Instance.Config.CommandConfig.CommandAliases;

        public string Description => System.Plugin.Instance.Config.CommandConfig.CommandDescription;

        string addtimehelp = $"\nModo de uso:\n```cs\n {System.Plugin.Instance.Config.CommandConfig.CommandName} [UserID]";
       private string commandhelp = "```cs\n[ Help ]\n``````md\n# Sintaxis\nLo que esta entre [] es obligatorio\nLo que esta entre {} es opcional\n"
            + "\n## Tiempo y su sintaxis\n"
            + "m = mes\nd = dias\n h = horas\n s = segundos"
            + "\n## Comando\n"
            + $"{System.Plugin.Instance.Config.CommandConfig.CommandName} [UserID] [Nombre del rol] [Duración] {{Nombre del jugador}}\n"
            + "\n## Explicacion de cada requerimiento\n"
            + "[UserID]\n"
            + "Hace referencia a la ID del jugador que contiene @discord @steam @northwood.\n\n"
            + "[Nombre del rol]\n"
            + "Es exactamente lo que dice, el nombre del rol que le vas a dar al jugador. No importa si esta en mayuscula o miniscula o mezclado con eso, el plugin automaticamente pasa todo a minuscula.\n\n"
            + "[Duración]\n"
            + "Es la duracion que tendra el rol, leer ## Tiempo y su sintaxis para poder entender.\n\n"
            + "{Nombre del jugador}\n"
            + "Es el nombre del jugador que tendra como referencia en la base de datos, este dato se actualiza automaticamente con el nombre del jugador al entrar a la partida, asi que no es obligatorio."
            + "\n\n"
            + "## Ejemplos\n"
            + $"1. {System.Plugin.Instance.Config.CommandConfig.CommandName} 111111@steam donador 30d\n2. {System.Plugin.Instance.Config.CommandConfig.CommandName} 111111@steam donador 1m\n3. {System.Plugin.Instance.Config.CommandConfig.CommandName} 111111@discord donador 30d1h12s\nEl ultimo ejemplo le dara el rol por 30 dias, 1 hora y 12 segundos.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender != null)
            {
                switch (arguments.At(0))
                {
                    case "help":
                        {
                            response = commandhelp;
                            return true;
                        }
                    case "addtime":
                        {
                            if (arguments.Count() < 3)
                            {
                                response = $"Error de Sintaxis: Hay menos de 3 argumentos, por favor usa \"{Command} help\"";
                                return false;
                            }
                            else if (arguments.Count > 3)
                            {
                                response = $"Error de Sintaxis: Hay mas de 3 argumentos, por favor usa \"{Command} help\"";
                                return false;
                            }
                            response = null;
                            return true;
                        }
                    default:
                        if (arguments.Count() < 3)
                        {
                            response = $"Error de Sintaxis: Hay menos de 3 argumentos, por favor usa \"{Command} help\"";
                            return false;
                        }
                        else if(arguments.Count > 3)
                        {
                            response = $"Error de Sintaxis: Hay mas de 3 argumentos, por favor usa \"{Command} help\"";
                            return false;
                        }
                        response = null;
                        return false;
                }
            }
            else
            {
                response = "Sender is null";
                Log.Error(this.GetType().Name + " Sender is Null");
                return false;
            }
        }
    }
}
