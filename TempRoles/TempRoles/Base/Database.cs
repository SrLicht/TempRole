using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempRoles.Base
{
    public abstract class Database
    {
        /// <summary>
        /// URL donde
        /// </summary>
        protected string url;

        /// <summary>
        /// Instancia de MysqlConnector
        /// </summary>
        /// <param name="url"></param>

        public Database(string url)
        {
            this.url = url;
        }

        /// <summary>
        /// Crea una base de datos.
        /// </summary>
        public abstract void CreateDatabase();
    }
}
