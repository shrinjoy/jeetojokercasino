using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.InteropServices;
using UnityEngine;
using ZXing.QrCode.Internal;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class jeetoJoker_GAMEMANAGER :timeManager
{
    [Header("Balance and Info")]
    [SerializeField] GameObject noinputpanel;
    [SerializeField]   TMPro.TMP_Text balance;
    [SerializeField] TMPro.TMP_Text balance2;
    [SerializeField]   TMPro.TMP_Text gameid;
    [SerializeField]   TMPro.TMP_Text timer;
    [SerializeField] GameObject targetofresult;
    [SerializeField] GameObject resultpanel;
    Vector3 result_starting_pos;
    [SerializeField] FortuneWheelManager outtercirlcle;
    [SerializeField] FortuneWheelManager innercircle;
    [SerializeField]marquee marqueeanim;
    [SerializeField] GameObject resultobject;
    [SerializeField]TMPro.TMP_Text datetimetext;
     int totalbetplaced=0;
    public int totalbalance=0;
    public int fakebalance=0;
    [SerializeField] TMPro.TMP_Text playamount;
    [SerializeField] TMPro.TMP_Text playamount2;
    [SerializeField] Betbuttons[] bet_buttons;
    [SerializeField] Sprite brightmarker;
    [SerializeField] Sprite darkmarker;
    [SerializeField] Image markerimage;
    [SerializeField]public TMPro.TMP_Text win0;
    [SerializeField]public TMPro.TMP_Text win1;
    [SerializeField] ResultSetter[] resultsetter;
    [SerializeField] ResultSetter panelresult;
    [SerializeField] GameObject infopanel;
    [SerializeField] TMPro.TMP_Text winamount_panel_wintext;
    [SerializeField] GameObject winamount_panel;
    [SerializeField] AudioClip gamestartaudiosource;
    [SerializeField] AudioClip wheelspinning;
    [SerializeField] AudioClip last15sec;
    [SerializeField] AudioClip nomoreplay;
    [SerializeField] AudioClip winaudio;
    bool sequenceended = true;
    [SerializeField]GameObject coinflipobject;
    [SerializeField] GameObject coindanceobject;
    bool lastchance = false;
    // Start is called before the first frame update
    bool betplaced=false;
    bool resultsentdone;
    string result;
    string xresult;
    [SerializeField] TMPro.TMP_Text betinfotext;
    public bool resetData = false;
    bool updatedata = true;
   [SerializeField] bool firstrun = true;
    DateTime currenttime;
    public GameObject uwinanimationcircle;
    private void Start()
    {
        claimbets();
        base.Start();
        result_starting_pos = resultpanel.transform.position;
        GetComponent<AudioSource>().clip = gamestartaudiosource;
        GetComponent<AudioSource>().Play();
       
        StartCoroutine(UpdateBalanceAndInfo());
        currenttime = GameObject.FindObjectOfType<SQL_manager>().get_time();
        //  StartCoroutine(addlast9gameresults());

    }
    // Update is called once per frame
    bool placeyourbetsshow = false;
    void Update()
    {
        timer.text=Mathf.Clamp((int)realtime,0,999).ToString();
        datetimetext.text = DateTime.Now.AddSeconds(40).ToString("yyyy-MM-dd hh:mm:ss tt");
       
        if(updatedata ==true)
        {
            StartCoroutine(UpdateBalanceAndInfo());

            StartCoroutine(addlast9gameresults());
            resetTimer();
            uwinanimationcircle.SetActive(false);
            updatedata = false;
            placeyourbetsshow = false;
        }


        if(realtime >= 15 &&placeyourbetsshow==false)
        {
            
            showstat("Place your chips");
            placeyourbetsshow = true;
            lastchance = true;
        }

        if(realtime<=15 && realtime >14)
        {
            GetComponent<AudioSource>().clip = last15sec;
            GetComponent<AudioSource>().Play();
            showstat("Last Chance");
            lastchance = true;
        }
        //

        if(realtime<11 && resultsentdone==false)
        {
            GetComponent<AudioSource>().clip =  nomoreplay;
            GetComponent<AudioSource>().Play();
            infopanel.SetActive(false);
            noinputpanel.SetActive(true);
            foreach (Betbuttons btns in bet_buttons)
            {


                btns.resetBetbutton();
            }
            resultsentdone =true;
        }
        if (realtime < 10 && betplaced)
        {
            showstat("No more bets");
            
        }
        if (realtime < 10 && betplaced == false)
        { 
             showstat("No more bets");
           
        }
        if (realtime < 8 && betplaced == true)
        {

            //showstat("Your bets have been accepted") ;
        }
        
    }
    Coroutine cr;
    public void showstat(string stat)
    {
       
        if(cr !=null)
        {
          StopCoroutine(cr);
        }
        cr =  StartCoroutine(Ishowstat(stat));
    }
    public IEnumerator Ishowstat(string txt)
    {
        betinfotext.text = txt;
        yield return new WaitForSeconds(2.0f);
        if(realtime>15)
        betinfotext.text = "Place your chips";
    }
    public void sendResult()
    {
      
        currenttime = GameObject.FindObjectOfType<SQL_manager>().get_time();
        if (GameObject.FindObjectOfType<SQL_manager>().canLogin(GameObject.FindObjectOfType<userManager>().getUserData().id, GameObject.FindObjectOfType<userManager>().getUserData().password, GameObject.FindObjectOfType<userManager>().getUserData().macid))
            {
                

            if ( (totalbalance - totalbetplaced)>=0 && totalbetplaced > 0)
            {
                string status = "Print";
                string gm = "gm";
                string barcode = generatebarcode();
                string command = "INSERT INTO [taas].[dbo].[tasp] (a00,a01,a02,a03,a04,a05,a06,a07,a08,a09,a10,a11," +
                    "tot,qty," +
                    "g_date,status,ter_id,g_id,g_time,p_time,bar,gm,flag) values ("
                    + bet_buttons[0].betamount + "," + bet_buttons[1].betamount + "," + bet_buttons[2].betamount + "," + bet_buttons[3].betamount + "," + bet_buttons[4].betamount + "," + bet_buttons[5].betamount + "," + bet_buttons[6].betamount + "," + bet_buttons[7].betamount + "," + bet_buttons[8].betamount + "," + bet_buttons[9].betamount + "," + bet_buttons[10].betamount + "," + bet_buttons[11].betamount
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
                GameObject.FindObjectOfType<clearbutton>().betbuttons2.Clear();
                print(totalbetplaced);
                foreach (Betbuttons btns in bet_buttons)
                {


                    BetbuttonData2 data = new BetbuttonData2();
                    data.betbutton = btns;
                    data.betamount = btns.betamount;

                    GameObject.FindObjectOfType<clearbutton>().betbuttons2.Add(data);
                }
               
                GameObject.FindObjectOfType<SQL_manager>().updatebalanceindatabase(GameObject.FindObjectOfType<userManager>().getUserData().id, totalbetplaced);
                StartCoroutine(UpdateBalanceAndInfo());
                betplaced = true;
                playamount.text = "0";
                playamount2.text = "0";
                showstat("Your bets have been accepted ID:"+barcode);
                GameObject.FindObjectOfType<clearbutton>().clear();
            }
        }
        
        else
        {
            SceneManager.LoadScene(0);
        }
        GameObject.FindObjectOfType<clearbutton>().clear();

    }
        public string generatebarcode()
        {
            string output = null;
            string[] alphabets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            output = alphabets[UnityEngine.Random.Range(0, alphabets.Length)] + DateTime.Now.ToString("ss") + alphabets[UnityEngine.Random.Range(0, alphabets.Length)] + UnityEngine.Random.Range(0, 9999) + alphabets[UnityEngine.Random.Range(0, alphabets.Length)] + alphabets[UnityEngine.Random.Range(0, alphabets.Length)] + alphabets[UnityEngine.Random.Range(0, alphabets.Length)];
            print(output);
            return output;
        }
        public void FakeUpdateBalance()
    {
        totalbetplaced = 0;
        foreach(Betbuttons bt in GameObject.FindObjectsOfType<Betbuttons>())
        {
            totalbetplaced += bt.betamount;
        }
        playamount.text = totalbetplaced.ToString();
        playamount2.text = totalbetplaced.ToString();

        
        int bet = (totalbalance - totalbetplaced);
        fakebalance = bet;
        bet = Mathf.Clamp(bet, 0, 999999999);
       // balance.text = bet.ToString();
       // balance2.text = bet.ToString();
    }
    public override void GameSequence()
    {
        try
        {
            if (sequenceended == true)
            {
                result = GameObject.FindObjectOfType<SQL_manager>().GetComponent<betManager>().getResult("joker");
                if (result != null && sequenceended == true)
                {
                    print("game sequnce started");

                    sequenceended = false;
                    StartCoroutine(jeetojokersequence());
                }
            }
        }
        catch (Exception ex)
        {
            print("failed to get result");
            this.GameSequence();
        }

    }
    //
    public IEnumerator addlast9gameresults()
    {
        string endtime = GameObject.FindObjectOfType<betManager>().gameResultTime;


        string starttime = DateTime.Parse(endtime).AddMinutes(-20).ToString("hh:mm:ss tt");
        int i = 0;

        SqlCommand sqlCmnd = new SqlCommand();
        //
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = " SELECT top(10) * FROM [taas].[dbo].[resultsTaa]   order by [taas].[dbo].[resultsTaa].id desc";
        print(sqlCmnd.CommandText);
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        while (sqlData.Read())
        {

            if (i < resultsetter.Length)
            {
                print(sqlData["id"].ToString());

                resultsetter[i].setResult(sqlData["result"].ToString());
                resultsetter[i].GetComponent<multiplier_resultpanel>().ShowMultiplier(sqlData["status"].ToString());



            }

            i = i + 1;
        }
        sqlData.Close();
        sqlData.DisposeAsync();
        
        yield return null;
    }
    IEnumerator jeetojokersequence()
    {
        markerimage.enabled = false;
        
        resultobject.SetActive(false);
        GameObject.FindObjectOfType<MultiplierAnimation>().resetstate();
       
        while(Vector3.Distance(targetofresult.transform.position,resultpanel.transform.position)>0.1f)
        {
            resultpanel.transform.position = Vector3.Lerp(resultpanel.transform.position, targetofresult.transform.position,Time.deltaTime*4.0f);
            yield return new WaitForEndOfFrame();
                
        }
        xresult = result.Substring(0, 4);
        int sector=0;   
        if(xresult=="NR00"|| xresult == "NR01" || xresult == "NR02" || xresult == "NR03")
        {
            sector = 0;
        }
        else if (xresult == "NR04" || xresult == "NR05" || xresult == "NR06" || xresult == "NR07")
        {
            sector = 1;
        }
        else if (xresult == "NR08" || xresult == "NR09" || xresult == "NR10" || xresult == "NR11")
        {
            sector = 2;
        }
        
        outtercirlcle.TurnWheel(sector);
      
        if (xresult == "NR00" || xresult == "NR04" || xresult == "NR08")
        {//heart
            sector = 3;
        }
        else if (xresult == "NR01" || xresult == "NR05" || xresult == "NR09")
        {//spade
            sector = 2;
        }
        else if (xresult == "NR02" || xresult == "NR06" || xresult == "NR10")
        {
            //diamond
            sector = 5;
        }
        else if (xresult == "NR03" || xresult == "NR07" || xresult == "NR11")
        {//clover
            sector = 0;
        }
        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().clip = wheelspinning;
        GetComponent<AudioSource>().Play();
        panelresult.setResult(xresult);
        panelresult.GetComponent<multiplier_resultpanel>().ShowMultiplier(result.Substring(4));
        innercircle.TurnWheel(sector);
        marqueeanim.enabled = true;
      
        
        while (innercircle.isspinning == true)
        {
            yield return new WaitForSecondsRealtime(0.01f);
        }
        GetComponent<AudioSource>().loop = false;
        marqueeanim.enabled = false;

        StartCoroutine(GameObject.FindObjectOfType<MultiplierAnimation>().multiplieranimation(result.Substring(4)));
        yield return new WaitForSeconds(1.0f);
        resultobject.GetComponent<ResultSetter>().setResult(result.Substring(0, 4));
        resultobject.SetActive(true);
        markerimage.enabled = true;
      
        
        markerimage.sprite = brightmarker;
        yield return new WaitForSecondsRealtime(0.1f);
        markerimage.sprite = darkmarker;
        yield return new WaitForSecondsRealtime(0.1f);

        markerimage.sprite = brightmarker;
        yield return new WaitForSecondsRealtime(0.1f);

        markerimage.sprite = darkmarker;
        yield return new WaitForSecondsRealtime(0.1f);
       
        markerimage.sprite = brightmarker;
        yield return new WaitForSecondsRealtime(0.1f);

        markerimage.sprite = darkmarker;
        yield return new WaitForSecondsRealtime(0.1f);

        markerimage.sprite = brightmarker;


        getwinamount();
        yield return new WaitForSeconds(1.0f);
        coinflipobject.SetActive(false);
        yield return new WaitForSeconds(4.0f);
        winamount_panel.SetActive(false);
        GetComponent<AudioSource>().Stop();
        coindanceobject.SetActive(false);
        while (Vector3.Distance(result_starting_pos, resultpanel.transform.position) > 0.1f )
        {
            resultpanel.transform.position = Vector3.Lerp(resultpanel.transform.position,result_starting_pos, Time.deltaTime * 4.0f);
            yield return new WaitForEndOfFrame();
        }
        
       
        resultsentdone= false;

        GameObject.FindObjectOfType<clearbutton>().clear();
       
        noinputpanel.SetActive(false);
       
        
        sequenceended = true;
        GetComponent<AudioSource>().clip = gamestartaudiosource;
        GetComponent<AudioSource>().Play();
        updatedata = true;
        betplaced= false;
        yield return null;
    }
    IEnumerator  UpdateBalanceAndInfo()
    {
       
        totalbalance = GameObject.FindObjectOfType<SQL_manager>().balance(GameObject.FindObjectOfType<userManager>().getUserData().id);
        balance.text=totalbalance.ToString();
        balance2.text =totalbalance.ToString();
        fakebalance = totalbalance;
        gameid.text= GameObject.FindObjectOfType<betManager>().gameResultId.ToString();
       
        
        yield return null;
       
    }
    void getwinamount()
    {

        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = "SELECT [clm] FROM [taas].[dbo].[tasp] where g_id=" + GameObject.FindObjectOfType<betManager>().gameResultId + " and ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and status='Prize' and g_time='" + GameObject.FindObjectOfType<betManager>().gameResultTime.ToString() + "' and g_date='" + GameObject.FindObjectOfType<SQL_manager>().server_day.ToString("yyyy-MMM-dd") + "'";//this is the sql command we use to get data about user
        print(sqlCmnd.CommandText);
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        int intwinamount = 0;
     
        while (sqlData.Read())
        {
            if (sqlData["clm"] != null || sqlData["clm"] != "Null")
            {
                intwinamount += Convert.ToInt32(sqlData["clm"].ToString());
                winamount_panel.SetActive(true);
                winamount_panel_wintext.text = intwinamount.ToString();
            }


        }
        sqlData.Close();
        sqlData.DisposeAsync();
        
        if (intwinamount > 0)
        {
            
            GetComponent<AudioSource>().clip = winaudio;
            GetComponent<AudioSource>().Play();
            uwinanimationcircle.SetActive(true);
            coinflipobject.SetActive(true);
            if (intwinamount>900)
            {
               
                coindanceobject.SetActive(true);

            }


           
            print("winamount:" + intwinamount);
            GameObject.FindObjectOfType<SQL_manager>().addubalanceindatabase(GameObject.FindObjectOfType<userManager>().getUserData().id, intwinamount);
            win0.text = intwinamount.ToString();
            win1.text = intwinamount.ToString();
        }
        if (intwinamount <= 0)
        {
            print("no win amount");
            win0.text = "";
            win1.text = "";
        }

        removestat();
       
    }
    void removestat()
    {
        string command = "UPDATE [taas].[dbo].[tasp] set status='Claimed'  WHERE  ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id +"' and status = 'Prize'";
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
    public void claimbets()
    {

       
        string command = "SELECT SUM(clm) as totalclaim  FROM [taas].[dbo].[tasp]  WHERE  ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and status = 'Prize'";
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
                print("no amount claimed");
            }

        }
        sqlData.Close();
        sqlData.DisposeAsync();
        GameObject.FindObjectOfType<SQL_manager>().addubalanceindatabase(GameObject.FindObjectOfType<userManager>().getUserData().id, betamountwon);

        removestat2();

    }
    void removestat2()
    {
        string command = "UPDATE [taas].[dbo].[tasp]set status='Claimed' WHERE  ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and status = 'Prize'";
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = command;//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        while (sqlData.Read()) { }

        sqlData.Close();
        sqlData.DisposeAsync();
    }
}
