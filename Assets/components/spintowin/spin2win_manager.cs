using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class spin2win_manager : timeManager
{
    [SerializeField] TMPro.TMP_Text gameIDtext;
    [SerializeField] TMPro.TMP_Text betplacedtext;
    [SerializeField] TMPro.TMP_Text balance;

    [SerializeField]public TMPro.TMP_Text wintext;
    [SerializeField] TMPro.TMP_Text timetext;
    [SerializeField] Image Timerprogressbar;
    [SerializeField] FortuneWheelManager fortunewheelobject;
    [SerializeField] GameObject marker;
    [SerializeField] GameObject ResultPanel_content;
    [SerializeField] GameObject ResultsPanel_content_object;
    [SerializeField] S2Pbutton[] bet_buttons;
    bool sequenceended = true;
    bool ResetData = false;
    string result;
    public int totalbetplaced;
    int totalbalance;
    public int fakebalance;
    bool betplaced=false;
    [SerializeField] GameObject statusobject;
    [SerializeField] TMP_Text datetimetext;
    [SerializeField] GameObject winpanel;
    [SerializeField] TMP_Text wintextonwinpanel;
    [SerializeField] GameObject coinflipanimation;
    [SerializeField] TMP_Text resulttext;
    [SerializeField] GameObject noinput;
    [SerializeField] GameObject uwinanimation;
    [SerializeField] AudioClip placeyourbets;
    [SerializeField] AudioClip nomorebets;
    [SerializeField] AudioClip lastchance;
    [SerializeField] AudioClip wheelspinningsound;
    [SerializeField] AudioClip winsound;
    [SerializeField] AudioClip nowinsound;
    bool lastbettextshown=false;
    bool betplacedshown = false;
    bool nomorebetsshown = false;
    private void Start()
    {
       base.Start();
       StartCoroutine(addlastgameresults());
       StartCoroutine(UpdateBalanceAndInfo());
        GameObject.FindObjectOfType<AudioSource>().clip = placeyourbets;
        GameObject.FindObjectOfType<AudioSource>().Play();
        showstatus("Place your bets");
    }
    private void Update()
    {
        datetimetext.text = DateTime.Now.AddSeconds(30).ToString("yyyy-MM-dd hh:mm:ss tt"); 
        if (ResetData==true)
        {
            StartCoroutine(addlastgameresults());
            uwinanimation.SetActive(false);
            StartCoroutine(UpdateBalanceAndInfo());
            GameObject.FindObjectOfType<S2Wclear_repeat>().clear();
            showstatus("Place your bets");
            ResetData = false;
            sequenceended = true;
            betplacedtext.text = "0";
            GameObject.FindObjectOfType<AudioSource>().clip = placeyourbets;
            GameObject.FindObjectOfType<AudioSource>().Play();
            betplaced = false;
            lastbettextshown= false;
            betplacedshown = false;
            nomorebetsshown = false;
        }
        if(realtime<10)
        {
           noinput.SetActive(true);
        }
        if (realtime > 10)
        {
            noinput.SetActive(false);
        }
            realtime = Mathf.Clamp((float)realtime, 0, 999);
        int minutes = Mathf.FloorToInt((float)realtime / 60F);
        int seconds = Mathf.FloorToInt((float)realtime - minutes * 60);
       
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        
        Timerprogressbar.fillAmount =1.0f- (float)(realtime /120.0);
        if(realtime<=15 && lastbettextshown==false)
        {
            lastbettextshown= true;
            GameObject.FindObjectOfType<AudioSource>().clip = lastchance;
            GameObject.FindObjectOfType<AudioSource>().Play();
            showstatus("Last Chance");
        }
        if(realtime <10 && totalbetplaced >0 && betplacedshown==false)
        {
            betplacedshown = true;
            showstatus("Your bets have been accepted");
        }
        if(realtime <10 && totalbetplaced <1 && nomorebetsshown==false)
        {
            nomorebetsshown= true;
            GameObject.FindObjectOfType<AudioSource>().clip = nomorebets;
            GameObject.FindObjectOfType<AudioSource>().Play();
            showstatus("No more bet please");
        }
        if (realtime < 8 && totalbetplaced > 0 && nomorebetsshown==false)
        {
            nomorebetsshown = true;

            GameObject.FindObjectOfType<AudioSource>().clip = nomorebets;
            GameObject.FindObjectOfType<AudioSource>().Play();
            showstatus("No more bet please");
        }
        timetext.text = niceTime;
        if(realtime<11 && betplaced==false)
        {
            betplaced = true;
           StartCoroutine( sendResult());
            
        }
       

    }
     public void showstatus(string st)
    {
     StartCoroutine(statusanim(st));
    }
    IEnumerator statusanim(string text)
    {
        statusobject.SetActive(true);
        statusobject.GetComponentInChildren<TMP_Text>().text = text;
        yield return new WaitForSecondsRealtime(2.0f);
        statusobject.SetActive(false);
    }
    IEnumerator UpdateBalanceAndInfo()
    {

        totalbalance = GameObject.FindObjectOfType<SQL_manager>().balance(GameObject.FindObjectOfType<userManager>().getUserData().id);
        balance.text = totalbalance.ToString();
        gameIDtext.text = GameObject.FindObjectOfType<betManager>().gameResultId.ToString();
        fakebalance = totalbalance;

        resetTimer();
        yield return null;

    }
    public IEnumerator addlastgameresults()
    {

        foreach(Transform gb in ResultPanel_content.GetComponentsInChildren<Transform>())
        {
            if(gb!= ResultPanel_content.transform)
            {
                Destroy(gb.gameObject);
            }
        }
        string endtime = GameObject.FindObjectOfType<betManager>().gameResultTime;


        string starttime = DateTime.Parse(endtime).AddMinutes(-30).ToString("hh:mm:ss tt");
        int i = 0;

        SqlCommand sqlCmnd = new SqlCommand();
        //
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = " SELECT top(10) * FROM [taas].[dbo].[results] order by [taas].[dbo].[results].id desc";
        print(sqlCmnd.CommandText);
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        while (sqlData.Read())
        {
            GameObject gb = GameObject.Instantiate(ResultsPanel_content_object, ResultPanel_content.transform, false);
            gb.GetComponent<ResultPanel_resultObject_s2w>().SetResult(sqlData["result"].ToString() + sqlData["status"].ToString());

            
        }
        sqlData.Close();
        sqlData.DisposeAsync();

        yield return null;
    }
    public override void GameSequence()
    {
        try
        {
            if (sequenceended == true)
            {
                result = GameObject.FindObjectOfType<SQL_manager>().GetComponent<betManager>().getResult("spin2win");
                if (result != null && sequenceended == true)
                {
                    print("game sequnce started");

                    sequenceended = false;
                    StartCoroutine(Spin2Win());
                }
            }
        }
        catch (Exception ex)
        {
            print("failed to get result");
            this.GameSequence();
        }

    }
    public void FakeUpdateBalance()
    {
        totalbetplaced = 0;
        foreach (S2Pbutton bt in GameObject.FindObjectsOfType<S2Pbutton>())
        {
            totalbetplaced += bt.betamount;
        }
        betplacedtext.text = totalbetplaced.ToString();
       


        int bet = (totalbalance - totalbetplaced);
        fakebalance = bet;
        bet = Mathf.Clamp(bet, 0, 999999999);
      //  balance.text = bet.ToString();
      
    }
    IEnumerator Spin2Win()
    {
        marker.SetActive(false);
        resulttext.text = "";
        int sector = 0;
        string xresult = result.Substring(0, 4);
        #region RESULT_CONVERSION
        if (xresult=="NR00")
        {
            //0
            sector = 6;
        }
        if (xresult == "NR01")
        {
            //1
            sector = 5;
        }
        if (xresult == "NR02")
        {
            //2
            sector = 4;
        }
        if (xresult == "NR03")
        {
            //3
            sector=3;
        }
        if (xresult == "NR04")
        {
            //4
            sector=2;
        }
        if (xresult == "NR05")
        {
            //5
            sector=1;
        }
        if (xresult == "NR06")
        {
            //6
            sector=0;
        }
        if (xresult == "NR07")
        {
            //7
            sector = 9;
        }
        if (xresult == "NR08")
        {
            //8
            sector= 8;
        }
        if (xresult == "NR09")
        {
            //9
            sector= 7;
        }
#endregion
        print(sector);
        GameObject.FindObjectOfType<AudioSource>().clip = wheelspinningsound;
        GameObject.FindObjectOfType<AudioSource>().Play();
        fortunewheelobject.TurnWheel(sector);
        GameObject.FindObjectOfType<MultiplierAnimation_single>().PlayMultiplierAnimation(result.Substring(4));
        while (fortunewheelobject.isspinning == true)
        {
            yield return new WaitForEndOfFrame();
        }
        
        resulttext.text = ResultConverters.S2w_ResultConverter(xresult);
        resulttext.enabled = true;
        GameObject.FindObjectOfType<AudioSource>().clip = nowinsound;
        GameObject.FindObjectOfType<AudioSource>().Play();
        getwinamount();
       
        yield return new WaitForSeconds(1);
        coinflipanimation.SetActive(false);
        yield return new WaitForSeconds(3);
       
       

        marker.SetActive(true);
        
        
       
        
        
        
        winpanel.SetActive(false);
        yield return new WaitForSeconds(4.0f);
        ResetData = true;
        yield return null;
    }
    IEnumerator sendResult()
    {

       DateTime currenttime = GameObject.FindObjectOfType<SQL_manager>().get_time();
        if (GameObject.FindObjectOfType<SQL_manager>().canLogin(GameObject.FindObjectOfType<userManager>().getUserData().id, GameObject.FindObjectOfType<userManager>().getUserData().password, GameObject.FindObjectOfType<userManager>().getUserData().macid))
        {
            print("called sebt result");
            if (totalbalance > (totalbalance - totalbetplaced) && totalbetplaced > 0)
            {
                string status = "Print";
                string gm = "gm";
                string barcode = generatebarcode();
                string command = "INSERT INTO [taas].[dbo].[tengp] (a00,a01,a02,a03,a04,a05,a06,a07,a08,a09," +
                    "tot,qty," +
                    "g_date,status,ter_id,g_id,g_time,p_time,bar,gm,flag) values ("
                    + bet_buttons[0].betamount + "," + bet_buttons[1].betamount + "," + bet_buttons[2].betamount + "," + bet_buttons[3].betamount + "," + bet_buttons[4].betamount + "," + bet_buttons[5].betamount + "," + bet_buttons[6].betamount + "," + bet_buttons[7].betamount + "," + bet_buttons[8].betamount + "," + bet_buttons[9].betamount
                    + "," + totalbetplaced + "," + totalbetplaced + ","
                    + "'" + DateTime.Today.ToString("yyyy-MM-dd 00:00:00.000") + "'" + "," + "'" + status + "'" + ",'" + GameObject.FindObjectOfType<userManager>().getUserData().id + "'," + GameObject.FindObjectOfType<betManager>().gameResultId + "," + "'" + GameObject.FindObjectOfType<betManager>().gameResultTime + "'" + "," + "'" + DateTime.Today.ToString("yyyy-MM-dd") + " " + currenttime.ToString("HH:mm:ss.000") + "'" + "," + "'" + barcode + "'" + "," + "'" + gm + "'" + "," + 1 + ")";
                print(command);
                SqlCommand sqlCmnd = new SqlCommand();
                SqlDataReader sqldata = null;
                sqlCmnd.CommandTimeout = 60;
                sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
                sqlCmnd.CommandType = CommandType.Text;
                sqlCmnd.CommandText = command;
                sqldata = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);

                sqldata.Close();
                sqldata.DisposeAsync();
                GameObject.FindObjectOfType<S2Wclear_repeat>().betbuttons2.Clear();
                foreach (S2Pbutton btns in bet_buttons)
                {

                    
                    BetbuttonData data = new BetbuttonData();
                    data.betbutton = btns;
                    data.betamount = btns.betamount;
                    data.clicks = btns.clickcount;
                    GameObject.FindObjectOfType<S2Wclear_repeat>().betbuttons2.Add(data);
                }
                print(totalbetplaced);
                GameObject.FindObjectOfType<SQL_manager>().updatebalanceindatabase(GameObject.FindObjectOfType<userManager>().getUserData().id, totalbetplaced);
                StartCoroutine(UpdateBalanceAndInfo());
              

            }
        }

        else
        {
            SceneManager.LoadScene(0);
        }

        yield return null;
    }
    public string generatebarcode()
    {
        string output = null;
        string[] alphabets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        output = alphabets[UnityEngine.Random.Range(0, alphabets.Length)] + DateTime.Now.ToString("ss") + alphabets[UnityEngine.Random.Range(0, alphabets.Length)] + UnityEngine.Random.Range(0, 9999) + alphabets[UnityEngine.Random.Range(0, alphabets.Length)] + alphabets[UnityEngine.Random.Range(0, alphabets.Length)] + alphabets[UnityEngine.Random.Range(0, alphabets.Length)];
        print(output);
        return output;
    }
    void getwinamount()
    {

        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = "SELECT [clm] FROM [taas].[dbo].[tengp] where g_id=" + GameObject.FindObjectOfType<betManager>().gameResultId + " and ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and status='Prize' and g_time='" + GameObject.FindObjectOfType<betManager>().gameResultTime.ToString() + "' and g_date='" + GameObject.FindObjectOfType<SQL_manager>().server_day.ToString("yyyy-MMM-dd") + "'";//this is the sql command we use to get data about user
        print(sqlCmnd.CommandText);
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        int intwinamount = 0;

        while (sqlData.Read())
        {
            if (sqlData["clm"] != null || sqlData["clm"] != "Null")
            {
                intwinamount += Convert.ToInt32(sqlData["clm"].ToString());
               // winamount_panel.SetActive(true);
                //winamount_panel_wintext.text = intwinamount.ToString();
            }


        }
        sqlData.Close();
        sqlData.DisposeAsync();

        if (intwinamount > 0)
        {

            // GetComponent<AudioSource>().clip = winaudio;
            //GetComponent<AudioSource>().Play();
            winpanel.SetActive(true);
            uwinanimation.SetActive(true);
            coinflipanimation.SetActive(true);

            GameObject.FindObjectOfType<AudioSource>().clip = winsound;
            GameObject.FindObjectOfType<AudioSource>().Play();


            print("winamount:" + intwinamount);
            GameObject.FindObjectOfType<SQL_manager>().addubalanceindatabase(GameObject.FindObjectOfType<userManager>().getUserData().id, intwinamount);
          
            wintext.text = intwinamount.ToString();
            wintextonwinpanel.text = intwinamount.ToString();   
            winpanel.SetActive(true);   
        }
        if (intwinamount <= 0)
        {
            print("no win amount");
            wintext.text = " "; 
        }

        removestat();

    }
    void removestat()
    {
        string command = "UPDATE [taas].[dbo].[tengp] set status='Claimed'  WHERE  ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and status = 'Prize'";
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
        print(command);
        sqlData.Close();
        sqlData.DisposeAsync();
    }
}
