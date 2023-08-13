using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
public class timeManager : MonoBehaviour
{
    // Start is called before the first frame update
    bool betversion = false;
    DateTime timetillnextgame;
    DateTime servertime;
    [HideInInspector]
    public int gameTime;
    [HideInInspector]
    protected double realtime = 0;
    bool isGameSequenceRunning = false;
    public int selectedcoinamount=2;
   [SerializeField] int mode = 0;
    bool nonbetversion = false;
    public void Start()
    {
        timetillnextgame = GameObject.FindObjectOfType<SQL_manager>().timeForNextGame(mode);//this.GetComponent<SQL_manager>().timeTillNextGame().Subtract(DateTime.Now);

        servertime = GameObject.FindObjectOfType<SQL_manager>().get_time().AddSeconds(5);
        print("time till next game:" + timetillnextgame);
        print("server time:" + servertime);//time patch 
        if (timetillnextgame.ToString("hh:mm:ss tt") == "12:00:00 AM" || timetillnextgame.ToString("hh:mm:ss tt") == "01:00:00 AM")
        {
            //patch; 
        timetillnextgame=    timetillnextgame.AddDays(1);
        }
        double ts =timetillnextgame.Subtract(servertime).TotalSeconds;
        realtime = ts;
        print(realtime);
        InvokeRepeating(nameof(timeloop), 1, 1);

    }
    void OnApplicationFocus(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SceneManager.LoadScene(0);
        }
    }
    // Update is called once per frame
    public void timeloop()
    {

       
            if (realtime <= 0.0f )
            {

                try
                {
                    GameSequence();

                }
                catch
                {
                    print("lost internet");
                }
            }
            else
            {
                resetTimer();
                realtime -= 1;


            }
          
        

    }
    public void resetTimer()
    {
        timetillnextgame = GameObject.FindObjectOfType<SQL_manager>().timeForNextGame(mode);//this.GetComponent<SQL_manager>().timeTillNextGame().Subtract(DateTime.Now);

        servertime = GameObject.FindObjectOfType<SQL_manager>().get_time().AddSeconds(5);
      
        if (timetillnextgame.ToString("hh:mm:ss tt") == "12:00:00 AM" || timetillnextgame.ToString("hh:mm:ss tt") == "01:00:00 AM")
        {
            //patch; 
            timetillnextgame = timetillnextgame.AddDays(1);
        }
        double ts = timetillnextgame.Subtract(servertime).TotalSeconds;

        realtime = ts;
        
    }

    public virtual void GameSequence() { }



   public void removestat()
    {

        DateTime currenttime = GameObject.FindObjectOfType<SQL_manager>().get_time();


        string command = "UPDATE [taas].[dbo].[tengp] set status='Claimed',clm_tm='" + DateTime.Today.ToString("yyyy-MM-dd") + " " + currenttime.ToString("HH:mm:ss.000") + "'   WHERE  ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and status = 'Prize'";
        string command1 = "UPDATE [taas].[dbo].[tasp] set status='Claimed',clm_tm='" + DateTime.Today.ToString("yyyy-MM-dd") + " " + currenttime.ToString("HH:mm:ss.000") + "'   WHERE  ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and status = 'Prize'";
        string command2 = "UPDATE [taas].[dbo].[bet16] set status='Claimed',clm_tm='" + DateTime.Today.ToString("yyyy-MM-dd") + " " + currenttime.ToString("HH:mm:ss.000") + "'   WHERE  ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and status = 'Prize'";
        string command3 = "UPDATE [taas].[dbo].[doup] set status='Claimed',clm_tm='" + DateTime.Today.ToString("yyyy-MM-dd") + " " + currenttime.ToString("HH:mm:ss.000") + "'   WHERE  ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and status = 'Prize'";

        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = command;//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        if (sqlData.Read())
        {

        }
        sqlData.Close();

        //
        sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = command1;//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        if (sqlData.Read())
        {

        }
        sqlData.Close();

        //
        sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = command2;//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        if (sqlData.Read())
        {

        }
        sqlData.Close();

        //
        sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = command2;//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        if (sqlData.Read())
        {

        }
        //print(command);
        sqlData.Close();
        sqlData.DisposeAsync();
    }
    public void claimbets()
    {

        string command = "SELECT ISNULL(SUM(clm),0) as totalclaim  FROM [taas].[dbo].[bet16]  WHERE  ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and status = 'Prize'";

        string command1 = "SELECT ISNULL(SUM(clm),0) as totalclaim  FROM [taas].[dbo].[tasp]  WHERE  ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and status = 'Prize'";

        string command2 = "SELECT ISNULL(SUM(clm),0) as totalclaim  FROM [taas].[dbo].[tengp]  WHERE  ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and status = 'Prize'";

        string command3 = "SELECT ISNULL(SUM(clm)+SUM(sclm),0) as totalclaim  FROM [taas].[dbo].[doup]  WHERE  ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and status = 'Prize'";

        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = command;//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        int betamountwon = 0;
        if (sqlData.Read())
        {
            try
            {

                betamountwon = Convert.ToInt32(sqlData["totalclaim"].ToString());
            }
            catch
            {
                //print("no amount claimed");
            }

        }
        sqlData.Close();

        GameObject.FindObjectOfType<SQL_manager>().addubalanceindatabase(GameObject.FindObjectOfType<userManager>().getUserData().id, betamountwon);
        //
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = command1;//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        betamountwon = 0;
        if (sqlData.Read())
        {
            try
            {

                betamountwon = Convert.ToInt32(sqlData["totalclaim"].ToString());
            }
            catch
            {
                //print("no amount claimed");
            }

        }
        sqlData.Close();

        GameObject.FindObjectOfType<SQL_manager>().addubalanceindatabase(GameObject.FindObjectOfType<userManager>().getUserData().id, betamountwon);
        //
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = command2;//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        betamountwon = 0;
        if (sqlData.Read())
        {
            try
            {

                betamountwon = Convert.ToInt32(sqlData["totalclaim"].ToString());
            }
            catch
            {
                //print("no amount claimed");
            }

        }
        sqlData.Close();

        GameObject.FindObjectOfType<SQL_manager>().addubalanceindatabase(GameObject.FindObjectOfType<userManager>().getUserData().id, betamountwon);
        //
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = command3;//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        betamountwon = 0;
        if (sqlData.Read())
        {
            try
            {

                betamountwon = Convert.ToInt32(sqlData["totalclaim"].ToString());
            }
            catch
            {
                //print("no amount claimed");
            }

        }
        sqlData.Close();
        sqlData.DisposeAsync();
        GameObject.FindObjectOfType<SQL_manager>().addubalanceindatabase(GameObject.FindObjectOfType<userManager>().getUserData().id, betamountwon);

        removestat();

    }


}
