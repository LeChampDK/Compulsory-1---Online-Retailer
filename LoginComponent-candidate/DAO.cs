using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace LoginComponent
{
    public class DAO
    {
        private SqliteConnection _connection;
        public DAO()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.Mode = SqliteOpenMode.ReadWriteCreate;
            connectionStringBuilder.DataSource = "../../../database/users.db";

            _connection = new SqliteConnection(connectionStringBuilder.ConnectionString);
            _connection.Open();

            Execute("DROP TABLE IF EXISTS LoginCredentials");
            Execute("CREATE TABLE LoginCredentials(email VARCHAR(255), hashedPassword VARCHAR(255), salt VARCHAR(255))");
        }

        private void Execute(string sql)
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }

        public bool Login(string email, string password)
        {
            var selectCmd = _connection.CreateCommand();
            selectCmd.CommandText = "SELECT * FROM LoginCredentials WHERE email = @email AND hashedPassword = @hashedPassword";
            selectCmd.Parameters.AddWithValue("@email", email);
            selectCmd.Parameters.AddWithValue("@hashedPassword", password);

            using (var reader = selectCmd.ExecuteReader())
            {
                return reader.Read();
            }
        }

        public bool CreateLogin(string email, HashAndSalt hashAndSalt)
        {
            var insertCmd = new SqliteCommand("INSERT INTO LoginCredentials(email, hashedPassword, salt) VALUES(@email, @hashedPassword, @salt)");
            insertCmd.Connection = _connection;
            insertCmd.Parameters.AddWithValue("email", email);
            insertCmd.Parameters.AddWithValue("hashedPassword", hashAndSalt.Hash);
            insertCmd.Parameters.AddWithValue("salt", hashAndSalt.Salt);
            if (insertCmd.ExecuteNonQuery() > 0)
            {
                return true;
            }

            return false;
        }

        public bool UpdateLogin(string email, string password)
        {
            var insertCmd = new SqliteCommand("UPDATE LoginCredentials SET hashedPassword = @newHashedPassword WHERE email = @email");
            insertCmd.Connection = _connection;
            insertCmd.Parameters.AddWithValue("email", email);
            insertCmd.Parameters.AddWithValue("newHashedPassword", password);
            if (insertCmd.ExecuteNonQuery() > 0)
            {
                return true;
            }

            return false;
        }

        public string GetLoginPasswordSalt(string email)
        {
            var selectCmd = _connection.CreateCommand();
            selectCmd.CommandText = "SELECT salt FROM LoginCredentials WHERE email = @email";
            selectCmd.Parameters.AddWithValue("@email", email);

            string salt;

            using (var reader = selectCmd.ExecuteReader())
            {                
                if(!reader.Read())
                    throw new KeyNotFoundException();

                salt = reader.GetString(0);
            }

            return salt;
        }
    }
}