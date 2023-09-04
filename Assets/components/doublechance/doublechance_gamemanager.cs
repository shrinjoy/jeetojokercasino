using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;
using TMPro;

public class doublechance_gamemanager : timeManager
{
    bool sequenceended = true;
    string result;
    [SerializeField] TMPro.TMP_Text timer;
    [SerializeField] TMPro.TMP_Text datetimetext;
    [SerializeField] GameObject ResultPanel_content;
    [SerializeField] GameObject last10resultprefab;
    [SerializeField] FortuneWheelManager singles_wheel;
    [SerializeField] TMPro.TMP_Text status;
    [SerializeField] FortuneWheelManager doubles_wheel;
    [SerializeField] GameObject multiplieranimationobject;
    [SerializeField] marquee multiplierscrollanimationobject;
    [SerializeField] TMPro.TMP_Text resultstring;
    public TMPro.TMP_Text balance;
    public int totalbalance;
    public int totalbetplaced;
    public TMPro.TMP_Text totalplay;
    public GameObject singlebutton_panel;
    public GameObject doublebutton_panel;
    public Dictionary<string, doublechance_button> btns_dict = new Dictionary<string, doublechance_button>();

    [SerializeField] public List<doublechance_button> single_buttons_list = new List<doublechance_button>();


    [SerializeField] public List<doublechance_button> double_buttons_list = new List<doublechance_button>();

    [SerializeField] public List<betdata> buttons_list_rebet = new List<betdata>();
    [SerializeField] GameObject single_hightlight, double_highlight;
    [SerializeField] TMP_Text single_text_highlight;
    [SerializeField] TMP_Text double_text_highlight;
    [SerializeField] TMP_Text singlebets;
    [SerializeField] TMP_Text doublebets;

    SQL_manager sqlmanager;
    userManager usermanager;
    betManager betmanager;
    Coroutine statuscr;
    [SerializeField] GameObject noinputpanel;

    bool nomorebets = false;
    [Header("win settings")]
    public GameObject winamount_panel;
    public TMPro.TMP_Text winamount_panel_wintext;
    public GameObject uwinanimation;
    public GameObject coinflipobject;
    public GameObject coindanceobject;
    public TMPro.TMP_Text win0;
    public TMPro.TMP_Text singletext;
    public TMPro.TMP_Text doubletext;

    public TMPro.TMP_Text win1;
    public TMPro.TMP_Text win2_single;
    public TMPro.TMP_Text win3_double;

    bool resultsetupdone = false;
    float swc;
    float dwc;
    [SerializeField] AudioClip nomorebetsplease;
    [SerializeField] AudioClip placeyourbets;
    [SerializeField] AudioClip winsound;
    [SerializeField] AudioClip wheelspin;

    AudioSource audiosource;

    public void updateplayamount()
    {

        int s = 0;
        int d = 0;
        foreach (doublechance_button sb in single_buttons_list)
        {
            s += sb.betamount;

        }
        foreach (doublechance_button db in double_buttons_list)
        {
            d += db.betamount;


        }
        singlebets.text = s.ToString();
        doublebets.text = d.ToString();

    }

    public struct betdata
    {
        public doublechance_button betbtn;
        public int btnvalue;
    }

    public void setstatus(string s)
    {
        if (statuscr != null)
        {
            StopCoroutine(statuscr);
        }

        statuscr = StartCoroutine(Isetstatus(s));
    }
    public IEnumerator Isetstatus(string s)
    {
        status.text = s;
        yield return new WaitForSecondsRealtime(2.0f);
        if (realtime < 10)
        {
            status.text = "no more bets please";
            status.color = Color.red;
        }
        if (realtime > 10)
        {
            status.text = "place you chip";
            status.color = Color.white;

        }
    }
    // Start is called before the first frame update
    public void gamesetup()
    {


        foreach (doublechance_button btn in singlebutton_panel.GetComponentsInChildren<doublechance_button>())
        {
            single_buttons_list.Add(btn);

        }
        foreach (doublechance_button btn in doublebutton_panel.GetComponentsInChildren<doublechance_button>())
        {

            double_buttons_list.Add(btn);

        }
        foreach (doublechance_button btn in single_buttons_list)
        {
            btns_dict.Add(btn.name, btn);
        }
        //print("doublebtns lenght:" + double_buttons_list.Count);
        foreach (doublechance_button btn in double_buttons_list)
        {

            btns_dict.Add("d" + btn.name, btn);
            //print("added  a btn double");
        }

        // Array.Sort(double_buttons);
        //print("setup is done");
    }
    void Start()
    {
         audiosource = gameObject.AddComponent<AudioSource>();
         swc = singles_wheel.duration;
         dwc = doubles_wheel.duration;
        base.Start();
        sqlmanager = GameObject.FindObjectOfType<SQL_manager>();
        usermanager = GameObject.FindObjectOfType<userManager>();
        betmanager = GameObject.FindObjectOfType<betManager>();
        claimbets();
        gamesetup();
        resultstring.enabled = true;

       

        StartCoroutine(addlastgameresults());
        multiplierscrollanimationobject.enabled = false;
        StartCoroutine(UpdateBalanceAndInfo());

        sendresult();
        setstatus("place your chip");
        audiosource.clip = placeyourbets;
        audiosource.Play();
        // placebetonall();
        InvokeRepeating(nameof(autoupdatebalance), 0, 3);

    }
    Coroutine crx = null;
    public void autoupdatebalance()
    {
        if (crx != null)
        {
            StopCoroutine(crx);
        }
        crx = StartCoroutine(UpdateBalanceAndInfo());
    }
    public void sendresult()
    {

        if (totalbetplaced > 0)
        {
            if (totalbetplaced <= totalbalance)
            {
                DateTime currenttime = sqlmanager.get_time();
                string bar = generatebarcode();
                string betbuttondata = null;
                string key = null;
                int internalkey = -1;
                bool resetinternalkey = false;
                for (int i = 0; i < btns_dict.Count; i++)
                {
                    if (i < 9)
                    {
                        internalkey += 1;
                        key = internalkey.ToString();
                    }


                    if (i > 9 && i < 20)
                    {
                        if (resetinternalkey == false)
                        {
                            internalkey = -1;
                            resetinternalkey = true;
                        }
                        internalkey += 1;
                        key = "d0" + internalkey.ToString();

                    }
                    if (i > 19)
                    {
                        internalkey += 1;
                        key = "d" + internalkey.ToString();
                    }
                    //print(key);
                    betbuttondata += btns_dict[key].betamount.ToString();
                    betbuttondata += ",";

                }
                //print(betbuttondata);
                string sqlquerytosend = "INSERT INTO [taas].[dbo].[doup]  ([a0],[a1],[a2],[a3],[a4],[a5],[a6],[a7],[a8],[a9],[a00],[a01],[a02],[a03],[a04],[a05],[a06],[a07],[a08],[a09],[a10],[a11],[a12],[a13],[a14],[a15],[a16],[a17],[a18],[a19],[a20],[a21],[a22],[a23],[a24],[a25],[a26],[a27],[a28],[a29],[a30],[a31],[a32],[a33],[a34],[a35],[a36],[a37],[a38],[a39],[a40],[a41] ,[a42],[a43],[a44],[a45],[a46],[a47],[a48],[a49],[a50],[a51],[a52],[a53],[a54],[a55],[a56],[a57],[a58],[a59] ,[a60] ,[a61],[a62],[a63],[a64],[a65],[a66],[a67],[a68],[a69],[a70],[a71],[a72],[a73],[a74],[a75],[a76],[a77],[a78],[a79],[a80],[a81],[a82] ,[a83] ,[a84],[a85],[a86],[a87],[a88],[a89],[a90],[a91],[a92],[a93],[a94],[a95],[a96],[a97],[a98],[a99],[tot],[qty],[g_date],[status],[ter_id],[g_id],[g_time],[p_time],[bar],[gm],[flag],[st_point])" +
                    "VALUES(" + betbuttondata + totalbetplaced + "," + totalbetplaced + ",'" + DateTime.Today.ToString("yyyy-MM-dd 00:00:00.000") + "'" + ",'Print'," + "'" + usermanager.getUserData().id + "'," + betmanager.gameResultId + "," + "'" + betmanager.gameResultTime + "'" + "," + "'" + DateTime.Today.ToString("yyyy-MM-dd") + " " + currenttime.ToString("HH:mm:ss.000") + "'" + "," + "'" + bar + "'" + "," + "' gm '" + "," + 1 + "," + totalbalance + ")";
                //print(sqlquerytosend);
                SqlCommand sqlCmnd = new SqlCommand();
                SqlDataReader sqldata = null;
                sqlCmnd.CommandTimeout = 60;
                sqlCmnd.Connection = sqlmanager.SQLconn;
                sqlCmnd.CommandType = CommandType.Text;
                sqlCmnd.CommandText = sqlquerytosend;
                sqldata = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);

                sqldata.Close();
                sqldata.DisposeAsync();

                //print(totalbetplaced);

                int totalbet = totalbetplaced;

                buttons_list_rebet.Clear();
                foreach (doublechance_button btn in btns_dict.Values)
                {
                    if (btn.betamount > 0)
                    {
                        betdata btd = new betdata();
                        btd.betbtn = btn;
                        btd.btnvalue = btn.betamount;
                        buttons_list_rebet.Add(btd);
                    }

                   // btn.ResetBetButton();
                }
                setstatus("your bet have been accepted " + bar);

                sqlmanager.updatebalanceindatabase(usermanager.getUserData().id, totalbet);

            }
            else
            {
                setstatus("not enough balance");

                //print("not enough balance");
            }
        }
        else
        {
            setstatus("no chip placed");
        }
        StartCoroutine(UpdateBalanceAndInfo());
        resetTimer();
        
    }
    public string generatebarcode()
    {
        string output = null;
        string[] alphabets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        output = alphabets[UnityEngine.Random.Range(0, alphabets.Length)] + DateTime.Now.ToString("ss") + alphabets[UnityEngine.Random.Range(0, alphabets.Length)] + UnityEngine.Random.Range(0, 9999) + alphabets[UnityEngine.Random.Range(0, alphabets.Length)] + alphabets[UnityEngine.Random.Range(0, alphabets.Length)] + alphabets[UnityEngine.Random.Range(0, alphabets.Length)];
        //print(output);
        return output + "M";
    }

    public void rebet()
    {
        foreach (betdata btn in buttons_list_rebet)
        {
            btn.betbtn.Updatebetdata(btn.btnvalue);
        }
        updateplayamount();
    }

    // Update is called once per frame
   public IEnumerator UpdateBalanceAndInfo()
    {

        totalbalance = sqlmanager.balance(usermanager.getUserData().id);
        if (totalbalance < 0)
        {
            totalbalance = 0;
        }

        balance.text = totalbalance.ToString();



        yield return null;

    }

    void Update()
    {
        if(realtime>10)
        {
            timer.color = Color.white;
        }
        else if(realtime<10)
        {
            timer.color = Color.red;
        }

        timer.text = Mathf.Clamp((int)realtime, 0, 999).ToString();
        
        datetimetext.text = DateTime.Now.AddSeconds(40).ToString("yyyy-MM-dd hh:mm:ss tt");
        totalplay.text = totalbetplaced.ToString();
        totalbetplaced = Mathf.Clamp(totalbetplaced, 0, 9999999);
        if (realtime < 10 && nomorebets == false)
        {
            noinputpanel.SetActive(true);
            audiosource.clip = nomorebetsplease;
            audiosource.Play(); 
           // setstatus("no more bets please");
            nomorebets = true;
            sendresult();
            foreach (doublechance_button btn in single_buttons_list)
            {
               // btn.ResetBetButton();
            }
            foreach (doublechance_button btn in double_buttons_list)
            {
                ///               btn.ResetBetButton();
            }
            foreach (buttonpanelanimaion btnanim in GameObject.FindObjectsOfType<buttonpanelanimaion>())
            {
                if (btnanim.isactive == true)
                {
                    btnanim.setpanelstate();
                }
            }
        }

    }
    public override void bloackbet()
    {
        noinputpanel.SetActive(true);
        setstatus("game will resume in 2 mins");
    }
    public IEnumerator addlastgameresults()
    {

        foreach (Transform gb in ResultPanel_content.GetComponentsInChildren<Transform>())
        {
            if (gb != ResultPanel_content.transform)
            {
                Destroy(gb.gameObject);
            }

        }






        SqlCommand sqlCmnd = new SqlCommand();

        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = sqlmanager.SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = " SELECT top(10) * FROM [taas].[dbo].[resultsDou] order by [taas].[dbo].[resultsDou].id desc";

        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        string lastresult = null;
        
        while (sqlData.Read())
        {
            GameObject gb = GameObject.Instantiate(last10resultprefab, ResultPanel_content.transform, false);
            gb.GetComponent<last10resultobjectsetter>().setdata(sqlData["result"].ToString() + sqlData["status"].ToString(), sqlData["status"].ToString(), sqlData["g_time"].ToString());
            if (resultsetupdone == false)
            {
                lastresult = sqlData["result"].ToString() + sqlData["status"].ToString();
                Vector2 sector = resulttosectorconvert(lastresult);
                multiplierscrollanimationobject.enabled = true;
                resultstring.enabled = true;

                resultstring.text = sqlData["result"].ToString().Substring(2);
                StartCoroutine(multiplieranimationobject.GetComponent<MultiplierAnimation>().multiplieranimation(lastresult.Substring(4)));

                singles_wheel.duration = 0.1f;
                doubles_wheel.duration = 0.1f;
                singles_wheel.TurnWheel((int)sector.y);
                doubles_wheel.TurnWheel((int)sector.x);
                while (singles_wheel.isspinning == true && singles_wheel.isspinning == true)
                {
                    yield return null;
                }

                resultsetupdone = true;
            }
            
            yield return null;

        }

       
        sqlData.Close();
        sqlData.DisposeAsync();

        yield return null;
    }
    public override void GameSequence()
    {

        //print("game sequence");
        try
        {
            if (sequenceended == true)
            {
                result = GameObject.FindObjectOfType<SQL_manager>().GetComponent<betManager>().getResult("doubletrouble");
                if (result != null && sequenceended == true)
                {
                    //print("game sequnce started");

                    sequenceended = false;
                    StartCoroutine(doublechancesequence());
                }
            }
        }
        catch (Exception ex)
        {
            print("failed to get result");
            // this.GameSequence();
        }

    }
    IEnumerator doublechancesequence()
    {
        uwinanimation.SetActive(false);
        singles_wheel.duration = swc;
         doubles_wheel.duration = dwc;


        //print(result);
        multiplieranimationobject.SetActive(true);
        multiplieranimationobject.GetComponent<MultiplierAnimation>().resetstate();
        resultstring.enabled = false;
        single_hightlight.SetActive(false);
        double_highlight.SetActive(false);
        Vector2 sector = resulttosectorconvert(result);
        singles_wheel.TurnWheel((int)sector.y);
        doubles_wheel.TurnWheel((int)sector.x);
        audiosource.clip = wheelspin;
        audiosource.Play();

        multiplierscrollanimationobject.enabled = true;
        while (singles_wheel.isspinning && doubles_wheel.isspinning)
        {
            yield return new WaitForSeconds(0.001f);
        }
        multiplierscrollanimationobject.enabled = false;

        StartCoroutine(multiplieranimationobject.GetComponent<MultiplierAnimation>().multiplieranimation(result.Substring(4)));
        yield return new WaitForSeconds(0.3f);
        //print(result.Substring(2, 2));
        resultstring.enabled = true;

        resultstring.text = result.Substring(2, 2);
        single_text_highlight.text = Convert.ToInt32(result.Substring(3, 1)).ToString();
        double_text_highlight.text = Convert.ToInt32(result.Substring(2, 1)).ToString();

        double_highlight.SetActive(true);       
        yield return new WaitForSeconds(0.3f);
        single_hightlight.SetActive(true);
         // int singles = Convert.ToInt32(result.Substring(3, 1));
        // int doubles = Convert.ToInt32(result.Substring(2, 1));


        getwinamount();
        yield return new WaitForSeconds(1.0f);
        coinflipobject.SetActive(false);
        yield return new WaitForSeconds(4.0f);
        winamount_panel.SetActive(false);
        uwinanimation.SetActive(false);

        //GetComponent<AudioSource>().Stop();


        sequenceended = true;
        resetTimer();
        StartCoroutine(addlastgameresults());
        noinputpanel.SetActive(false);
        nomorebets = false;
        audiosource.clip = placeyourbets;
        audiosource.Play();
        status.color = Color.white;
        setstatus("place your chip");
        win0.text = "";
        win1.text = "";
        win2_single.text = "";
        win3_double.text = "";
        foreach (doublechance_button btn in single_buttons_list)
        {
             btn.ResetBetButton();
        }
        foreach (doublechance_button btn in double_buttons_list)
        {
          btn.ResetBetButton();
        }
        StartCoroutine(UpdateBalanceAndInfo());

    }
    public Vector2 resulttosectorconvert(string result)
    {
        //8=0 3=1 7=2 4=3 6=4 0=5 5=6 1=7 9=8 2=9

        Vector2 xy = Vector2.zero; ;
        int singles = Convert.ToInt32(result.Substring(3, 1));
        int doubles = Convert.ToInt32(result.Substring(2, 1));
        //cause fuck you i am not getting paid enough 
        if (singles == 8)
        {
            xy.y = 0;
        }
        else if (singles == 3)
        {
            xy.y = 1;
        }
        else if (singles == 7)
        {
            xy.y = 2;
        }
        else if (singles == 4)
        {
            xy.y = 3;
        }
        else if (singles == 6)
        {
            xy.y = 4;
        }
        else if (singles == 0)
        {
            xy.y = 5;
        }
        else if (singles == 5)
        {
            xy.y = 6;
        }
        else if (singles == 1)
        {
            xy.y = 7;
        }
        else if (singles == 9)
        {
            xy.y = 8;
        }
        else if (singles == 2)
        {
            xy.y = 9;
        }
        if (doubles == 8)
        {
            xy.x = 0;
        }
        else if (doubles == 3)
        {
            xy.x = 1;
        }
        else if (doubles == 7)
        {
            xy.x = 2;
        }
        else if (doubles == 4)
        {
            xy.x = 3;
        }
        else if (doubles == 6)
        {
            xy.x = 4;
        }
        else if (doubles == 0)
        {
            xy.x = 5;
        }
        if (doubles == 5)
        {
            xy.x = 6;
        }
        else if (doubles == 1)
        {
            xy.x = 7;
        }
        else if (doubles == 9)
        {
            xy.x = 8;
        }
        else if (doubles == 2)
        {
            xy.x = 9;
        }
        return xy;

    }



    public void placebetonall()
    {
        foreach (doublechance_button btn in FindObjectsOfType<doublechance_button>())
        {
            btn.onBetbuttonclicked();
        }
    }
    void getwinamount()
    {

        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = sqlmanager.SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = "select r.result,'a' + SUBSTRING(r.result, 3, 2)  result,sum(isnull(d.clm,0)) + sum(isnull(d.sclm,0)) as total,sum(isnull(d.clm,0)) as doublecol,sum(isnull(d.sclm,0)) as single from doup d, resultsDou r WHERE r.g_date=d.g_date and r.g_time=d.g_time and d.g_date='" + GameObject.FindObjectOfType<SQL_manager>().server_day.ToString("yyyy-MMM-dd") + "' and d.g_time='" + betmanager.gameResultTime.ToString() + "'   AND ter_id='" + usermanager.getUserData().id + "' GROUP BY r.result";
        
            print("get win amount:"+sqlCmnd.CommandText);
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        int intwinamount = 0;
        int singlewinamount = 0;
        int doublewinamount = 0;

        if (sqlData.Read())
        {
            if (sqlData["total"] != null || sqlData["total"].ToString() != "Null" || sqlData["total"].ToString().Trim() != string.Empty)
            {
                try
                {
                    print("win amount:" + sqlData["total"].ToString());
                    intwinamount = Convert.ToInt32(sqlData["total"].ToString());
                    print("win amount is " + intwinamount); 

                    if (sqlData["single"] != null || sqlData["single"].ToString() != "Null")
                    {
                        singlewinamount = Convert.ToInt32(sqlData["single"].ToString());
                    }
                    if (sqlData["doublecol"] != null || sqlData["doublecol"].ToString() != "Null")
                    {
                        doublewinamount = Convert.ToInt32(sqlData["doublecol"].ToString());
                    }

                   
                }
                catch (Exception ex)
                {

                }
            }


        }
        sqlData.Close();
        sqlData.DisposeAsync();

        if (intwinamount > 0)
        {
            winamount_panel.SetActive(true);
            winamount_panel_wintext.text = intwinamount.ToString();
            audiosource.clip = winsound;
            audiosource.Play();
            uwinanimation.SetActive(true);
            coinflipobject.SetActive(true);
            if (intwinamount > 900)
            {

                //  coindanceobject.SetActive(true);

            }



            //print("winamount:" + intwinamount);
            // sqlmanager.addubalanceindatabase(usermanager.getUserData().id, intwinamount);
            claimbets();
            win0.text = intwinamount.ToString();
            win1.text = intwinamount.ToString();
            UpdateBalanceAndInfo();
            singletext.text = singlewinamount.ToString();
            doubletext.text = doublewinamount.ToString();
            win2_single.text = singlewinamount.ToString();
            win3_double.text = doublewinamount.ToString();
        }
        if (intwinamount <= 0)
        {
            //print("no win amount");
            win0.text = "";
            win1.text = "";
        }
        removestat();

        //
    }
    void removestat()
    {
        //
        DateTime currenttime = sqlmanager.get_time();
        string command = "UPDATE [taas].[dbo].[doup] set status='Claimed',clm_tm='" + DateTime.Today.ToString("yyyy-MM-dd") + " " + currenttime.ToString("HH:mm:ss.000") + "'   WHERE  ter_id='" + usermanager.getUserData().id + "' and status = 'Prize'";

        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = sqlmanager.SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = command;//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        //print(command);
        if (sqlData.Read())
        {

        }

        sqlData.Close();
        sqlData.DisposeAsync();
    }
    public void claimbets()
    {

        string command = "SELECT SUM(clm) as totalclaim  FROM [taas].[dbo].[doup]  WHERE  ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and status = 'Prize'";

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
        sqlmanager.addubalanceindatabase(usermanager.getUserData().id, betamountwon);

        removestat();

    }
}
