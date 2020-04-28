using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using System.IO;
using UnityEngine.UI;

public class db : MonoBehaviour
{
    public CategoryList categoryList;
    private string conn, sqlQuery;
    IDbConnection dbconn;
    IDbCommand dbcmd;

    string DatabaseName = "QuizAppDB.s3db";
    void Start()
    {
        string[] filePaths = Directory.GetFiles(Application.persistentDataPath + "/");
        string filepath = Application.persistentDataPath + "/" + DatabaseName;
        if (!File.Exists(filepath))
        {
            Debug.LogWarning("File \"" + filepath + "\" does not exist. Attempting to create from \"" +
                             Application.dataPath + "!/assets/QuizAppDB");
        }

        conn = "URI=file:" + filepath;
    }
    public void PlayButton()
    {
        CategorySearch();
    }

    private void CategorySearch()
    {
        using (dbconn = new SqliteConnection(conn))
        {
            int len = 0;
            List<string> lista = new List<string>();
            string NazwaKategorii;
            dbconn.Open(); //Open connection to the database.
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT NazwaKategorii " + "FROM Kategorie";
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                len++;
                NazwaKategorii = reader.GetString(0);

                Debug.Log(" Kategoria =" + NazwaKategorii);
                lista.Add(NazwaKategorii);

            }
            categoryList.CreateList(len, lista);
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();


        }

    }
}