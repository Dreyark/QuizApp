using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using System.IO;
using UnityEngine.UI;
using UnityEditor;
using System.Text;
using System.Collections.Specialized;

public class db : MonoBehaviour
{
    public GameObject Login, Password, menu, LogInScreen, wiadomosc;
    public menu men;
    public CategoryList categoryList;
    public Quiz quiz;
    public List<string> lista = new List<string>();
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

            // UNITY_ANDROID
            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/QuizAppDB.s3db");
            while (!loadDB.isDone) { }
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDB.bytes);




        }

        conn = "URI=file:" + filepath;

    }
    public void PlayButton()
    {
        CategorySearch();
    }

    [Obsolete]
    public void LoggingIn()
    {
        string nazwa = Login.GetComponent<Text>().text;
        string haslo = Password.GetComponent<Text>().text;
        string uzytkownik = SignIn(nazwa, haslo);
        if (uzytkownik != "")
        {
            menu.GetComponent<menu>().LoggedIn(uzytkownik);
            LogInScreen.SetActive(false);
        }
        else
        {
            wiadomosc.SetActive(true);
            wiadomosc.GetComponent<Text>().text = "Zła nazwa użytkownika lub hasło";
        }
    }

    private void CategorySearch()
    {
        using (dbconn = new SqliteConnection(conn))
        {
            //Debug.Log(conn);
            int len = 0;
            string NazwaKategorii;
            lista = new List<string>();
            dbconn.Open(); //Open connection to the database.
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT NazwaKategorii " + "FROM Kategorie";
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                len++;
                NazwaKategorii = reader.GetString(0);

                //Debug.Log(" Kategoria =" + NazwaKategorii);
                lista.Add(NazwaKategorii);

            }
            //categoryList.removeList();
            categoryList.CreateList(len, lista);
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();


        }

    }

    public void CategoryList(string category)
    {
        using (dbconn = new SqliteConnection(conn))
        {
            string Pytanie, OdpA, OdpB, OdpC, OdpD, URL;
            int pop;
            dbconn.Open(); //Open connection to the database.
            IDbCommand dbcmd = dbconn.CreateCommand();
            //string sqlQuery = "SELECT Pytanie, OdpowiedzA, OdpowiedzB, OdpowiedzC, OdpowiedzD, Poprawna, ObrazekPytania.URL FROM Kategorie INNER JOIN Pytania on Kategorie.Id = Pytania.IdKategorii left join ObrazekPytania on ObrazekPytania.IdPytania = Pytania.Id where Kategorie.NazwaKategorii = '" + category + "'";// table name
            string sqlQuery = "SELECT Pytanie, OdpowiedzA, OdpowiedzB, OdpowiedzC, OdpowiedzD, Poprawna, IFNULL(ObrazekPytania.URL,'NULL')  FROM Kategorie INNER JOIN Pytania on Kategorie.Id = Pytania.IdKategorii LEFT JOIN ObrazekPytania on Pytania.Id = ObrazekPytania.IdPytania where Kategorie.NazwaKategorii = " + "'" + category + "'";// table name
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                //  string id = reader.GetString(0);
                Pytanie = reader.GetString(0);
                OdpA = reader.GetString(1);
                OdpB = reader.GetString(2);
                OdpC = reader.GetString(3);
                OdpD = reader.GetString(4);
                pop = reader.GetInt16(5);
                URL = reader.GetString(6);
                quiz.SetQuiz(OdpA, OdpB, OdpC, OdpD, Pytanie, pop, URL);
                quiz.kategoria = category;
            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();


        }

    }

    private string SignIn(string User, string Password)
    {
        string Nazwa = "";
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open();
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT Nazwa " + "FROM Uzytkownicy WHERE Nazwa = '" + User + "' AND Haslo = '" + Password + "'";
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                Nazwa = reader.GetString(0);
            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();

        }
        return Nazwa;
    }

    public void Scoreboard(string category)
    {
        using (dbconn = new SqliteConnection(conn))
        {
            quiz.Scoreboard = " Punkty    Gracz   \n \n";
            int len = 1;
            string gracz;
            int punkty;
            dbconn.Open();
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "Select Uzytkownicy.Nazwa, Wyniki.Punkty from Wyniki inner join Uzytkownicy on Uzytkownicy.id = Wyniki.IdKonta inner join Kategorie on Kategorie.Id = Wyniki.IdKategorii where Kategorie.NazwaKategorii= '" + category + "' order by Punkty desc;";
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                if (len == 10)
                {
                    break;
                }
                len++;
                gracz = reader.GetString(0);
                punkty = reader.GetInt16(1);
                quiz.Scoreboard += string.Format(("\t {0,3} \t \t \t {1,-16}" + Environment.NewLine), punkty.ToString(), gracz);
                //quiz.Scoreboard += punkty.ToString() + "\t \t \t" + gracz + "\n";
            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
        }

    }

    public void UpdateScoreboard(string category, string userName, string punkty)
    {
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open();
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "INSERT INTO Wyniki(IdKategorii, IdKonta, Punkty) SELECT Kategorie.Id, Uzytkownicy.id, " + punkty + " from Uzytkownicy, Kategorie where Kategorie.NazwaKategorii = '" + category + "' and Uzytkownicy.Nazwa = '" + userName + "'";
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }

    }

    public void AddCategory(string category)
    {
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open();
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "Insert into Kategorie (NazwaKategorii) Values('"+category+"')";
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }

    }

    public void AddQuestion(string A, string B, string C, string D, string Pyt, int pop, int cat)
    {
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open();
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "INSERT INTO Pytania (Pytanie, OdpowiedzA, OdpowiedzB, OdpowiedzC, OdpowiedzD, Poprawna, IdKategorii) VALUES('"+Pyt+"', '"+A+"', '"+B+"', '"+C+"', '"+D+"', "+pop+", "+cat+"); ";
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }

    }

    public void Register()
    {
        wiadomosc.SetActive(true);
        string nazwa = Login.GetComponent<Text>().text;
        string haslo = Password.GetComponent<Text>().text;
        if (nazwa.Contains(" ") || haslo.Contains(" "))
        {
            wiadomosc.GetComponent<Text>().text = "Nazwa i hasło nie mogą zawierać spacji";
            wiadomosc.SetActive(true);
        }
        else
        {
            using (dbconn = new SqliteConnection(conn))
            {
                dbconn.Open();
                IDbCommand dbcmd = dbconn.CreateCommand();
                try
                {
                    string sqlQuery = "INSERT INTO Uzytkownicy(Nazwa, Haslo) VALUES('" + nazwa + "','" + haslo + "');";

                    dbcmd.CommandText = sqlQuery;
                    dbcmd.ExecuteScalar();
                    dbconn.Close();
                    men.Start();
                    wiadomosc.SetActive(false);
                }
                catch (SqliteException ex)
                {
                    Debug.Log(ex.Data.Keys);
                    wiadomosc.GetComponent<Text>().text = "Podana nazwa użytkownika jest już zajęta";
                    wiadomosc.SetActive(true);
                }
            }
        }

    }
}