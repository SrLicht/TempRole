using Exiled.API.Features;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TempRoles.System.DB
{
    public class RoleDatabase : Base.Database
    {
        public RoleDatabase(string url) : base(url) { }

        protected static string CREATETABLE = "create table if not exists tempRole (id varchar(64) primary key, username varchar(64), role varchar(64) not null, duration datetime not null)";
        override public void CreateDatabase()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(url);
                conn.Open();
                var cmd = new MySqlCommand(CREATETABLE, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                Log.Error("Error on create Database: " + e.ToString());
            }
        }

        public void CleanupDatabase()
        {
            var a = "DELETE from tempRole WHERE duration < CURDATE()";
            MySqlConnection conn = new MySqlConnection(url);
            conn.Open();
            var cmd = new MySqlCommand(a, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private static string HASPLAYER = "select role from tempRole where id = @val1 and duration > now()";
        async public Task<string> HasRole(string player)
        {
            MySqlConnection conn = new MySqlConnection(url);
            await conn.OpenAsync();
            MySqlCommand cmd = new MySqlCommand(HASPLAYER, conn);
            cmd.Parameters.AddWithValue("val1", player);
            await cmd.PrepareAsync();
            MySqlDataReader rdr = await cmd.ExecuteReaderAsync();
            string ret = null;
            if (await rdr.ReadAsync())
            {
                ret = rdr.GetString(0);
            }
            rdr.Close();
            await conn.CloseAsync();
            return ret;
        }

        private static string HASPLAYERS = "select id,role from tempRole where duration > now()";
        async public Task<Dictionary<string, string>> GetPlayersRoles()
        {
            MySqlConnection conn = new MySqlConnection(url);
            await conn.OpenAsync();
            MySqlCommand cmd = new MySqlCommand(HASPLAYERS, conn);
            MySqlDataReader rdr = await cmd.ExecuteReaderAsync();
            Dictionary<string, string> players = new Dictionary<string, string>();
            while (await rdr.ReadAsync())
            {
                var userId = rdr.GetString(0);
                var role = rdr.GetString(1);
                players.Add(userId, role);
            }
            rdr.Close();
            await conn.CloseAsync();
            return players;
        }

        public Dictionary<string, string> GetPlayersRolesSync()
        {
            MySqlConnection conn = new MySqlConnection(url);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(HASPLAYERS, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            Dictionary<string, string> players = new Dictionary<string, string>();
            while (rdr.Read())
            {
                var userId = rdr.GetString(0);
                var role = rdr.GetString(1);
                players.Add(userId, role);
            }
            rdr.Close();
            conn.Close();
            return players;
        }

        private static string UPDATEPLAYERTIME = "update tempRole set duration = addtime((select duration from tempRole where id = @val1), @val2), username = @val3 where id = @val1";
        /// <summary>
        /// Agrega tiempo a un jugador ya existente en la DB.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="username"></param>
        /// <param name="span"></param>
        /// <returns></returns>
        async public Task<int> AddPlayerRoleTime(string player, string username, TimeSpan span)
        {
            if (await HasRole(player) == null) return -1;
            MySqlConnection conn = new MySqlConnection(url);
            await conn.OpenAsync();
            MySqlCommand cmd = new MySqlCommand(UPDATEPLAYERTIME, conn);
            cmd.Parameters.AddWithValue("val1", player);
            cmd.Parameters.AddWithValue("val2", span.ToString(@"d\ hh\:mm\:ss"));
            cmd.Parameters.AddWithValue("val3", username);
            Log.Error(cmd.Parameters["val2"].Value);
            await cmd.PrepareAsync();
            int ret = await cmd.ExecuteNonQueryAsync();
            await conn.CloseAsync();
            return ret;

        }

        private static string UPDATEPLAYER = "insert into tempRole values (@val1, @val3, @val4, @val2) on duplicate key update duration = @val2, username = @val3, role = @val4";

        /// <summary>
        /// Agrega a un jugador a la DB con X rol Y tiempo y Z nombre
        /// </summary>
        /// <returns></returns>
        async public Task<int> SetPlayerRole(string player, string username, string role, DateTime date)
        {
            MySqlConnection conn = new MySqlConnection(url);
            await conn.OpenAsync();
            MySqlCommand cmd = new MySqlCommand(UPDATEPLAYER, conn);
            cmd.Parameters.AddWithValue("val1", player);
            cmd.Parameters.AddWithValue("val2", date.ToString("yyyy-MM-dd hh:mm:ss"));
            cmd.Parameters.AddWithValue("val3", username);
            cmd.Parameters.AddWithValue("val4", role);
            await cmd.PrepareAsync();
            int ret = await cmd.ExecuteNonQueryAsync();
            await conn.CloseAsync();
            return ret;
        }

        private static string UPDATEPLAYERNAME = "update tempRole set username = @val1 where id = @val2";
        /// <summary>
        /// Establece el nombre de un jugador ya agregado a la DB.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        async public Task<int> SetPlayerName(string player, string username)
        {
            MySqlConnection conn = new MySqlConnection(url);
            await conn.OpenAsync();
            MySqlCommand cmd = new MySqlCommand(UPDATEPLAYERNAME, conn);
            cmd.Parameters.AddWithValue("val1", username);
            cmd.Parameters.AddWithValue("val2", player);
            await cmd.PrepareAsync();
            int ret = await cmd.ExecuteNonQueryAsync();
            await conn.CloseAsync();
            return ret;

        }
    }
}
