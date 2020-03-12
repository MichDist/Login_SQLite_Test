using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;


namespace schiff
{
    class Initialize
    {
        // Check if Table user exists and return result (true/false)
        public bool DBExists()
        {
            string cs = @"URI=file:C:\Users\midistle\sqlite_test\test2.db";
            string stm = "SELECT name FROM sqlite_master WHERE type='table' AND name='user'";

            var con = new SQLiteConnection(cs);
            con.Open();

            var cmd = new SQLiteCommand(stm, con);
            SQLiteDataReader rdr = cmd.ExecuteReader();
            bool tableExists = rdr.Read();
            con.Close();

            return tableExists;
        }

        public void CreateTables()
        {
            string cs = @"URI=file:C:\Users\midistle\sqlite_test\test2.db";
            var con = new SQLiteConnection(cs);

            con.Open();
            // Create user table
            var cmd = new SQLiteCommand(con);
            cmd.CommandText = "CREATE TABLE user(user_id INTEGER PRIMARY KEY, user_name TEXT, password TEXT)";
            cmd.ExecuteNonQuery();

            // Create ships table
            cmd.CommandText = "CREATE TABLE ships(ship_id INTEGER PRIMARY KEY, ship_name TEXT, user_id INTEGER, ship_class INTEGER, engine TEXT, sail TEXT)";
            cmd.ExecuteNonQuery();

            con.Close();
        }
    }
}
