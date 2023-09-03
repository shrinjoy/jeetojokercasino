using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SqlClient;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class SQL_manager : MonoBehaviour
{

    public SqlConnection SQLconn;
    [SerializeField] GameObject badmacid;
    [SerializeField] TMPro.TMP_Text warningtext;
    public DateTime server_day;
    bool setupdone = false;
    private void Start()
    {

        try
        {
            ConnecttoServer();
            setupdone = true;
        }
        catch (Exception ex)
        {
            //print("failed to connect..... retrying");
            ConnecttoServer();
        }

    }
    public void ConnecttoServer()
    {

        SQLconn = new SqlConnection();
        DontDestroyOnLoad(this);
        SQLconn = initSQL();

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //print("connecting to server...");

    }
    public void Update()
    {
        if (setupdone == true)
        {

            if (SQLconn.State == ConnectionState.Closed)
            {
                try
                {
                    ConnecttoServer();
                }
                catch (Exception ex)
                {
                    //print("failed to reconnect in update loop trying to reconnect.....");
                }
            }
            if (SceneManager.GetActiveScene().buildIndex == 0 && (warningtext == null || badmacid == null))
            {
                badmacid = GameObject.FindGameObjectWithTag("WN");
                warningtext = GameObject.FindGameObjectWithTag("WT").GetComponent<TMPro.TMP_Text>();
            }


        }
    }
    public DateTime get_time()
    {
        DateTime dt = new DateTime();
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = "SELECT GETDATE() as CurrentTime";//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        if (sqlData.Read())
        {
            dt = DateTime.Parse(sqlData["CurrentTime"].ToString());
        }
        sqlData.Close();
        sqlData.DisposeAsync();
        server_day = DateTime.Parse(dt.ToString("dd-MMM-yyyy"));
        dt = DateTime.Parse(dt.ToString("hh:mm:ss tt"));
        return dt;
    }
    //initSQL() inits the sql connection and opens it for other methods dependend on sql to run 
    public SqlConnection initSQL()
    {
        string sqlConnectionString = @"Data Source=103.76.228.21,1433;User ID = sa; Password=cron@123#;Initial Catalog = taas;MultipleActiveResultSets=True";
        SqlConnection sqlConnection = new SqlConnection(sqlConnectionString);
        sqlConnection.Open();
        return sqlConnection;
    }
    //canLogin() checks if user with certain id and pass is present in data base if not then it will return false other wise it will return true
    public bool canLogin(string id, string pass, string macid)
    {
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        //print(SQLconn.State);
        sqlCmnd.CommandText = "SELECT * FROM[taas].[dbo].[g_master] WHERE term_id ='" + id + "' and pass ='" + pass + "'";//this is the sql command we use to get data about user

        //print(sqlCmnd.CommandText);
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        if (sqlData.Read())
        {
            if (sqlData["pass"].ToString() == pass && sqlData["term_id"].ToString() == id)
            {
                //print("pass found with id");
                if (sqlData["macid"].ToString() == macid && Convert.ToInt32(sqlData["flag"].ToString()) != 5)
                {
                    //print("mac id found");
                    if (this.GetComponent<userManager>())
                    {
                        this.GetComponent<userManager>().setUserData(sqlData["term_id"].ToString(), sqlData["term_name"].ToString(), sqlData["pass"].ToString(), sqlData["macid"].ToString(), sqlData["comm"].ToString());
                        sqlData.Close();
                        sqlData.DisposeAsync();

                        return true;
                    }
                    return false;
                }
                else if (sqlData["macid"].ToString() != macid || Convert.ToInt32(sqlData["flag"].ToString()) == 5)
                {
                    //print("invalid mac id");
                    if (SceneManager.GetActiveScene().buildIndex == 0)
                    {
                        StartCoroutine(showmacnogowarning());
                    }
                    else
                    {
                        SceneManager.LoadScene(0);
                        StartCoroutine(showmacnogowarning());

                    }
                    warningtext.text = "Please Contact Office";
                    sqlData.Close();
                    sqlData.DisposeAsync();
                    addmacid(macid, id);
                    return false;
                }
                return false;

            }
            if (sqlData["pass"].ToString() != pass || sqlData["term_id"].ToString() != id || sqlData["pass"].ToString() == null || sqlData["term_id"].ToString() == null)
            {


                sqlData.Close();
                sqlData.DisposeAsync();
                return false;
            }
            return false;

        }
        else
        {

            warningtext.text = "wrong username or password";
            showmacnogowarning();
            return false;
        }
        sqlData.Close();
        sqlData.DisposeAsync();
        return false;
    }
    IEnumerator showmacnogowarning()
    {
        badmacid = GameObject.FindWithTag("WN");
        if (badmacid != null)
        {
            badmacid.GetComponent<Image>().enabled = true;
            yield return new WaitForSecondsRealtime(2.0f);
            badmacid.GetComponent<Image>().enabled = false;
        }

    }
    public void addmacid(string macid, string termid)
    {
        //
        string command = "UPDATE [taas].[dbo].[g_master] set newmacid='" + macid + "',flag=3 WHERE term_id='" + termid + "'";
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = command;//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        //print(sqlCmnd.CommandText);
        if (sqlData.Read())
        {
        }
        sqlData.Close();
        sqlData.DisposeAsync();
    }
    public int balance(string termid)
    {
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = "SELECT lim from [taas].[dbo].[g_master] where term_id='" + termid + "'";//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        if (sqlData.Read())
        {
            int bal = Convert.ToInt32(sqlData["lim"].ToString());
            sqlData.Close();
            sqlData.DisposeAsync();
            return bal;
        }
        sqlData.Close();
        sqlData.DisposeAsync();
        return 0;
    }
    public void updatebalanceindatabase(string termid, int totalbetplaced)
    {
        int mainbal = balance(termid);
        int updatedbal = mainbal - totalbetplaced;
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = SQLconn;
        sqlCmnd.CommandType = CommandType.Text;

        sqlCmnd.CommandText = "UPDATE [taas].[dbo].[g_master]  SET lim=" + updatedbal + " WHERE term_id='" + termid + "'";//this is the sql command we use to get data about user

        //print("new balance is:" + updatedbal);
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        sqlData.Read();
        sqlData.Close();
        sqlData.DisposeAsync();
    }
    public void addubalanceindatabase(string termid, int claim)
    {
        int mainbal = balance(termid);
        int updatedbal = mainbal + claim;
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = "UPDATE [taas].[dbo].[g_master]  SET lim=" + updatedbal + " WHERE term_id='" + termid + "'";//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        sqlData.Read();
        sqlData.Close();
        sqlData.DisposeAsync();
    }
    public string betResult(string time, int id, string gamename)
    {
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        //print("bet time:" + time);
        if (DateTime.Parse(time).ToString("hh:mm:ss tt") == "12:00:00 AM" || DateTime.Parse(time).ToString("hh:mm:ss tt") == "01:00:00 AM")
        {
            server_day = server_day.AddDays(1);
        }
        if (gamename == "joker")
        {
            sqlCmnd.CommandText = "SELECT top(1) [taas].[dbo].[resultsTaa].g_date,[taas].[dbo].[resultsTaa].g_time,[taas].[dbo].[resultsTaa].result,[taas].[dbo].[resultsTaa].status FROM [taas].[dbo].[resultsTaa] WHERE g_time='" + time + "'  and g_date='" + server_day.ToString("dd-MMM-yyyy") + "' ORDER by id desc";//this is the sql command we use to get data about user
        }
        if (gamename == "bihari16")
        {

            sqlCmnd.CommandText = "SELECT top(1) [taas].[dbo].[results16].result,[taas].[dbo].[results16].status FROM [taas].[dbo].[results16] WHERE g_time='" + time + "'and g_date='" + server_day.ToString("dd-MMM-yyyy") + "' ORDER by id desc";//this is the sql command we use to get data about user

        }
        if (gamename == "spin2win")
        {

            sqlCmnd.CommandText = "SELECT top(1) [taas].[dbo].[results].result,[taas].[dbo].[results].status FROM [taas].[dbo].[results] WHERE g_time='" + time + "'and g_date='" + server_day.ToString("dd-MMM-yyyy") + "' ORDER by id desc";//this is the sql command we use to get data about user

        }
        if (gamename == "doubletrouble")
        {

            sqlCmnd.CommandText = "SELECT top(1) [taas].[dbo].[resultsdou].result,[taas].[dbo].[resultsdou].status FROM [taas].[dbo].[resultsdou] WHERE g_time='" + time + "'and g_date='" + server_day.ToString("dd-MMM-yyyy") + "' ORDER by id desc";//this is the sql command we use to get data about user

        }
        //print(sqlCmnd.CommandText);//
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        if (sqlData.Read())
        {
            //idk why i did this back then but pretty sure there was a reason so just follow the ritual
            string result = "";
            if (gamename == "joker")
            {
                //print(sqlData["g_date"].ToString() + sqlData["g_time"].ToString());
                result = sqlData["result"].ToString() + sqlData["status"].ToString();

                //print("results:" + result);
                sqlData.Close();
                sqlData.DisposeAsync();
            }
            if (gamename == "bihari16")
            {
                result = sqlData["result"].ToString() + sqlData["status"].ToString();

                //print("results:" + result);
                sqlData.Close();
                sqlData.DisposeAsync();//
            }
            if (gamename == "spin2win")
            {
                result = sqlData["result"].ToString() + sqlData["status"].ToString();

                //print("results:" + result);
                sqlData.Close();
                sqlData.DisposeAsync();//
            }
            if (gamename == "doubletrouble")
            {
                result = sqlData["result"].ToString() + sqlData["status"].ToString();

                //print("results:" + result);
                sqlData.Close();
                sqlData.DisposeAsync();//
            }
            sqlData.Close();
            sqlData.DisposeAsync();
            return result;
        }
        sqlData.Close();
        sqlData.DisposeAsync();
        return null;
    }
    public DateTime timeForNextGame(int mode = 0)
    {
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        if (mode == 0)
        {
            sqlCmnd.CommandText = "SELECT * FROM [taas].[dbo].[g_rule12] WHERE tag=1";//this is the sql command we use to get data about time
        }
        if (mode == 1)
        {
            sqlCmnd.CommandText = "SELECT * FROM [taas].[dbo].[g_rule16] WHERE tag=1";

        }
        if (mode == 2)
        {
            sqlCmnd.CommandText = "SELECT * FROM [taas].[dbo].[g_rule10] WHERE tag=1";

        }
        if (mode == 3)
        {
            sqlCmnd.CommandText = "SELECT * FROM [taas].[dbo].[g_rule99] WHERE tag=1";

        }
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        if (sqlData.Read())
        {
            if (sqlData["g_time"] != null)
            {
                string time = sqlData["g_time"].ToString();
                DateTime date = DateTime.Parse(DateTime.Parse(time).ToString("hh:mm:ss tt"));

                if (this.GetComponent<betManager>() != null)
                {
                    this.GetComponent<betManager>().setResultData(time, Convert.ToInt32(sqlData["id"].ToString()));
                }
                sqlData.Close();
                sqlData.DisposeAsync();
                return (date);
            }
            else
            {
                //print("no tag 1");
                sqlData.Close();
                sqlData.DisposeAsync();
                return get_time();
            }

        }
        sqlData.Close();
        sqlData.DisposeAsync();
        return get_time();
    }

}
