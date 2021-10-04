using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.API.Interfaces;

namespace TempRoles.System
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        [Description("Rellenar con la informacion de la base de datos de Mysql")]
        public string DatabaseURL { get; set; } = "server=localhost;user=root;database=world;port=3306;password=******;";

        [Description("Cualquier rol que pongas aqui, tendra slot reservado.")]
        public List<string> ReservedSlotRoles { get; set; } = new List<string>() { "owner" };
    }
}