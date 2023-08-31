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
   public bool sequenceended = true;
    bool ResetData = false;
    string result;
    public int totalbetplaced;
    public int totalbalance;
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
    bool anybetsplaced = false;
    bool lastbettextshown=false;
    bool betplacedshown = false;
    bool nomorebetsshown = false;
    private void Start()
    {
        claimbets();
       base.Start();
       addlastgameresults();
        GameObject.FindObjectOfType<betManager>().setResultData();
      UpdateBalanceAndInfo();
        GameObject.FindObjectOfType<AudioSource>().clip = placeyourbets;
        GameObject.FindObjectOfType<AudioSource>().Play();
        showstatus("Place your bets");
        InvokeRepeating(nameof(autoupdatebalance), 0, 3);
    }

    Coroutine crx = null;
    public void autoupdatebalance()
    {
       UpdateBalanceAndInfo();
    }
    private void Update()
    {
        datetimetext.text = DateTime.Now.AddSeconds(40).ToString("yyyy-MM-dd hh:mm:ss tt"); 
        if (ResetData==true)
        {
            GameObject.FindObjectOfType<betManager>().setResultData();
            resetTimer();
            addlastgameresults();
            uwinanimation.SetActive(false);
            UpdateBalanceAndInfo();
            GameObject.FindObjectOfType<S2Wclear_repeat>().clear();
            showstatus("Place your bets");
            ResetData = false;
            
            betplacedtext.text = "0";
            GameObject.FindObjectOfType<AudioSource>().clip = placeyourbets;
            GameObject.FindObjectOfType<AudioSource>().Play();
            betplaced = false;
            lastbettextshown= false;
            betplacedshown = false;
            nomorebetsshown = false;
            anybetsplaced = false;
        }
        if(realtime<10)
        {
           noinput.SetActive(true);
        }
        if (realtime > 10)
        {
            sequenceended = true;
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
        if(realtime <10 && totalbetplaced >0 && betplacedshown==false && anybetsplaced==true)
        {
            betplacedshown = true;
          
            // showstatus("Your bets have been accepted");
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
            foreach (S2Pbutton btns in bet_buttons)
            {

                btns.resetbet();
            }
            FakeUpdateBalance();

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
   public async void UpdateBalanceAndInfo()
    {
     PlayerdataResponse pdr=   await GameObject.FindObjectOfType<CasinoAPI>().getuserdata(GameObject.FindObjectOfType<userManager>().getUserData().id, GameObject.FindObjectOfType<userManager>().getUserData().password);

        totalbalance = pdr.balance;


        if (totalbalance < 0)
        {
            totalbalance = 0;
        }
        balance.text = totalbalance.ToString();
       

        gameIDtext.text = GameObject.FindObjectOfType<betManager>().gameResultId.ToString();
        fakebalance = totalbalance;

      
        

    }
    public async void addlastgameresults()
    {

        foreach(Transform xgb in ResultPanel_content.GetComponentsInChildren<Transform>())
        {
            if(xgb!= ResultPanel_content.transform)
            {
                Destroy(xgb.gameObject);
            }
        }
        last10result res = await GameObject.FindObjectOfType<CasinoAPI>().getlast10result("http://191.101.3.139:3000/s2w/getlastresults/");

        foreach (last10ResultItem item in res.Results)
        {


            GameObject gb = GameObject.Instantiate(ResultsPanel_content_object, ResultPanel_content.transform, false);
            print(item.Result);
            gb.GetComponent<ResultPanel_resultObject_s2w>().SetResult(item.Result+"N", item.Time);

        } 

        
    }
    public override async void GameSequence()
    {
        try
        {
            if (sequenceended == true)
            {
                result =await GameObject.FindObjectOfType<betManager>().getResult("spin2win");
              
                if (result != null && sequenceended == true)
                {
                 
                    result += "N";
                    sequenceended = false;
                    StartCoroutine(Spin2Win());
                }
                else
                {
                  

                }
            }
        }
        catch (Exception ex)
        {
            print(ex);
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
        string xresult = result.Substring(0, 3);
        #region RESULT_CONVERSION
        print(xresult);
        if (xresult=="NR0")
        {
            //0
            sector = 6;
        }
        if (xresult == "NR1")
        {
            //1
            sector = 5;
        }
        if (xresult == "NR2")
        {
            //2
            sector = 4;
        }
        if (xresult == "NR3")
        {
            //3
            sector=3;
        }
        if (xresult == "NR4")
        {
            //4
            sector=2;
        }
        if (xresult == "NR5")
        {
            //5
            sector=1;
        }
        if (xresult == "NR6")
        {
            //6
            sector=0;
        }
        if (xresult == "NR7")
        {
            //7
            sector = 9;
        }
        if (xresult == "NR8")
        {
            //8
            sector= 8;
        }
        if (xresult == "NR9")
        {
            //9
            sector= 7;
        }
#endregion
        //print(sector);
        GameObject.FindObjectOfType<AudioSource>().clip = wheelspinningsound;
        GameObject.FindObjectOfType<AudioSource>().Play();
        fortunewheelobject.TurnWheel(sector);
        GameObject.FindObjectOfType<MultiplierAnimation_single>().PlayMultiplierAnimation(result.Substring(3));
        while (fortunewheelobject.isspinning == true)
        {
            yield return new WaitForEndOfFrame();
        }
        
        resulttext.text = ResultConverters.S2w_ResultConverter(xresult);
        resulttext.enabled = true;
        GameObject.FindObjectOfType<AudioSource>().clip = nowinsound;
        GameObject.FindObjectOfType<AudioSource>().Play();
        yield return new WaitForSeconds(1.5f);
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
    public void sendbetdata()
    {
        StartCoroutine(sendResult());
    }
    IEnumerator sendResult()
    {
       DateTime currenttime = GameObject.FindObjectOfType<SQL_manager>().get_time();
        if (GameObject.FindObjectOfType<SQL_manager>().canLogin(GameObject.FindObjectOfType<userManager>().getUserData().id, GameObject.FindObjectOfType<userManager>().getUserData().password, GameObject.FindObjectOfType<userManager>().getUserData().macid))
        {
            //print("called sebt result");
            if ((totalbalance -totalbetplaced)>=0 && totalbalance>0&&  totalbetplaced > 0)
            {
                anybetsplaced = true;
                string status = "Print";
                string gm = "gm";
                string barcode = generatebarcode();
                string command = "INSERT INTO [taas].[dbo].[tengp] (a00,a01,a02,a03,a04,a05,a06,a07,a08,a09," +
                    "tot,qty," +
                    "g_date,status,ter_id,g_id,g_time,p_time,bar,gm,flag,st_point) values ("
                    + bet_buttons[0].betamount + "," + bet_buttons[1].betamount + "," + bet_buttons[2].betamount + "," + bet_buttons[3].betamount + "," + bet_buttons[4].betamount + "," + bet_buttons[5].betamount + "," + bet_buttons[6].betamount + "," + bet_buttons[7].betamount + "," + bet_buttons[8].betamount + "," + bet_buttons[9].betamount
                    + "," + totalbetplaced + "," + totalbetplaced + ","
                    + "'" + DateTime.Today.ToString("yyyy-MM-dd 00:00:00.000") + "'" + "," + "'" + status + "'" + ",'" + GameObject.FindObjectOfType<userManager>().getUserData().id + "'," + GameObject.FindObjectOfType<betManager>().gameResultId + "," + "'" + GameObject.FindObjectOfType<betManager>().gameResultTime + "'" + "," + "'" + DateTime.Today.ToString("yyyy-MM-dd") + " " + currenttime.ToString("HH:mm:ss.000") + "'" + "," + "'" + barcode + "'" + "," + "'" + gm + "'" + "," + 1 +","+totalbalance+ ")";
                //print(command);
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
                    btns.resetbet();
                }
                //print(totalbetplaced);
                GameObject.FindObjectOfType<SQL_manager>().updatebalanceindatabase(GameObject.FindObjectOfType<userManager>().getUserData().id, totalbetplaced);
                UpdateBalanceAndInfo();
                showstatus("your bet has been accepted ID:"+barcode);
               
            }
        }

        else
        {
            SceneManager.LoadScene(0);
        }
        betplacedtext.text = "0";
        resetTimer();
       yield return null;
    }
    public string generatebarcode()
    {
        string output = null;
        string[] alphabets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        output = alphabets[UnityEngine.Random.Range(0, alphabets.Length)] + DateTime.Now.ToString("ss") + alphabets[UnityEngine.Random.Range(0, alphabets.Length)] + UnityEngine.Random.Range(0, 9999) + alphabets[UnityEngine.Random.Range(0, alphabets.Length)] + alphabets[UnityEngine.Random.Range(0, alphabets.Length)] + alphabets[UnityEngine.Random.Range(0, alphabets.Length)];
        //print(output);
        return output + "M";
    }
   async void getwinamount()
    {

      
        int intwinamount = await GameObject.FindObjectOfType<CasinoAPI>().getwinamount(GameObject.FindObjectOfType<userManager>().getUserData().id,GameObject.FindObjectOfType<betManager>().gameResultId);

       
       

        if (intwinamount > 0)
        {

            // GetComponent<AudioSource>().clip = winaudio;
            //GetComponent<AudioSource>().Play();
            winpanel.SetActive(true);
            uwinanimation.SetActive(true);
            coinflipanimation.SetActive(true);

            GameObject.FindObjectOfType<AudioSource>().clip = winsound;
            GameObject.FindObjectOfType<AudioSource>().Play();


            //print("winamount:" + intwinamount);
            GameObject.FindObjectOfType<SQL_manager>().addubalanceindatabase(GameObject.FindObjectOfType<userManager>().getUserData().id, intwinamount);
          
            wintext.text = intwinamount.ToString();
            wintextonwinpanel.text = intwinamount.ToString();   
            winpanel.SetActive(true);   
        }
        if (intwinamount <= 0)
        {
            //print("no win amount");
            wintext.text = " "; 
        }

        

    }
    void removestat()
    {

     DateTime   currenttime = GameObject.FindObjectOfType<SQL_manager>().get_time();


        string command = "UPDATE [taas].[dbo].[tengp] set status='Claimed',clm_tm='" + DateTime.Today.ToString("yyyy-MM-dd") + " " + currenttime.ToString("HH:mm:ss.000") + "'   WHERE  ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and status = 'Prize'";
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
        //print(command);
        sqlData.Close();
        sqlData.DisposeAsync();
    }
    public void claimbets()
    {

       
        string command = "SELECT SUM(clm) as totalclaim  FROM [taas].[dbo].[tengp]  WHERE  ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and status = 'Prize'";
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
        sqlData.DisposeAsync();
        GameObject.FindObjectOfType<SQL_manager>().addubalanceindatabase(GameObject.FindObjectOfType<userManager>().getUserData().id, betamountwon);
      
        removestat();

    }
   
}
