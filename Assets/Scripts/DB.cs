using UnityEngine;

using System;
using System.IO;
using SQLiter;
using System.Data;
using Mono.Data.SqliteClient;



public class DB
{
    //const string connectionString = "URI=file:C:\\Users\\user\\Desktop\\Hedgehog2.db";
    const string connectionString = "URI=file:Assets/Scripts/Hedgehog3.db";
    public SqliteConnection Connection = new (connectionString);

    
    public void openConnection()
    {
        //Connection = new SqliteConnection(@"Data Source = C:\Users\user\Downloads\Telegram Desktop\cliccccc\cliccccc\Assets\Scripts\Hedgehog.db; Version=3;");
        Connection = new(connectionString);

        Console.WriteLine(connectionString);
        Connection.Open();
    }

    public void closeConnection()
    {
        if (Connection.State == System.Data.ConnectionState.Open){ 
            Connection.Close(); }
            
    }

    public SqliteConnection getConnection()
    {
        return Connection;

        
    }

}
