using System;
using System.Data.SQLite;

namespace schiff
{
    class Program
    {
        static void Main(string[] args)
        {
            //DeleteTables();
            InitializeApp();

            // Login
            Login();


        }

        public static void InitializeApp()
        {
            // Initialize application
            Initialize init = new Initialize();
            // Check if Tables exist
            if (init.DBExists())
            {
                Console.WriteLine("Tables exist");
            }
            else
            {
                // Create Tables
                init.CreateTables();
                Console.WriteLine("Tables created");
            }
        }

        public static void DeleteTables()
        {
            // To reset the database
            string cs = @"URI=file:C:\Users\midistle\sqlite_test\test2.db";
            var con = new SQLiteConnection(cs);

            con.Open();

            var cmd = new SQLiteCommand(con);
            cmd.CommandText = "DROP TABLE user";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "DROP TABLE ships";
            cmd.ExecuteNonQuery();

            con.Close();
        }

        public static void Login()
        {
            Login loginObject = new Login();
            loginObject.userLogin();
        }
    }
}
