using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace StokTakip.Config
{
    internal class DbConfig
    {
        // Veritabanı bağlantı adresi
        // NOT: Pwd= kısmına MySQL kurarken belirlediğin şifreyi yazmalısın!
        private static string connectionString = "Server=localhost;Database=StokTakipDB;Uid=root;Pwd=;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
