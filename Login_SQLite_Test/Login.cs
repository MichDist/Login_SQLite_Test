using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace schiff
{
    class Login
    {
        public void userLogin()
        {
            int iLogin = 0;
            while (iLogin != 1)
            {
                Console.WriteLine("Create a new user (n) or use an existing user (e)?");
                string loginDecision = Console.ReadLine();
                if (loginDecision == "n")
                {
                    // Create new user
                    Console.Write("Username: ");
                    string userName = Console.ReadLine();
                    Console.Write("Password: ");
                    string password = Console.ReadLine();

                    string cs = @"URI=file:C:\Users\midistle\sqlite_test\test2.db";
                    var con = new SQLiteConnection(cs);

                    con.Open();

                    var cmd = new SQLiteCommand(con);
                    cmd.CommandText = "INSERT INTO user(user_name, password) VALUES(@user_name, @password)";
                    cmd.Parameters.AddWithValue("@user_name", userName);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();

                    con.Close();
                    // Check if user was inserted?
                    Console.WriteLine("User created");
                }
                else
                {
                    // Use existing user
                    Console.Write("Username: ");
                    string userName = Console.ReadLine();
                    Console.Write("Password: ");
                    string password = Console.ReadLine();

                    // Check if user exists and password is correct
                    string cs = @"URI=file:C:\Users\midistle\sqlite_test\test2.db";
                    var con = new SQLiteConnection(cs);
                    //string stm = $"SELECT user_name, password FROM user where user_name = {userName}";

                    con.Open();

                    var cmd = new SQLiteCommand(con);
                    cmd.CommandText = "SELECT EXISTS(SELECT 1 FROM user WHERE user_name = @user_name)";
                    cmd.Parameters.AddWithValue("@user_name", userName);
                    SQLiteDataReader rdr = cmd.ExecuteReader();

                    bool bUserExists = false;
                    while (rdr.Read())
                    {
                        bUserExists = rdr.GetBoolean(0);

                    }
                    Console.WriteLine(Convert.ToString(bUserExists));
                    con.Close();

                    if (bUserExists)
                    {
                        // Check pw, .ExecuteScalar() could also be used here
                        con.Open();
                        var cmdPW = new SQLiteCommand(con);
                        cmdPW.CommandText = "SELECT password FROM user WHERE user_name = @user_name";
                        cmdPW.Parameters.AddWithValue("@user_name", userName);
                        SQLiteDataReader rdrPW = cmdPW.ExecuteReader();
                        string passwordDB = "";
                        while (rdrPW.Read())
                        {
                            passwordDB = rdrPW.GetString(0);
                        }
                        if (password == passwordDB)
                        {
                            Console.WriteLine("Login Sucessfull.");
                            iLogin = 1;
                        }
                        else
                        {
                            Console.WriteLine("Wrong password.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("User does not exist!");
                    }

                }
            }
        }
    }
}
