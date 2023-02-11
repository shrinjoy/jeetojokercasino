using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class bihari16 : timeManager
{
    [Header("Balance and Info")]
    [SerializeField] GameObject noinputpanel;
    [SerializeField] TMPro.TMP_Text balance;
    [SerializeField] TMPro.TMP_Text balance2;
    [SerializeField] TMPro.TMP_Text gameid;
    [SerializeField] TMPro.TMP_Text timer;
    [SerializeField] GameObject targetofresult;
    [SerializeField] GameObject resultpanel;
    Vector3 result_starting_pos;
    [SerializeField] FortuneWheelManager outtercirlcle;
    [SerializeField] FortuneWheelManager innercircle;
    [SerializeField] marquee marqueeanim;
    [SerializeField] GameObject resultobject;
    [SerializeField] TMPro.TMP_Text datetimetext;
    int totalbetplaced = 0;
    int totalbalance = 0;
    public int fakebalance = 0;
    [SerializeField] TMPro.TMP_Text playamount;
    [SerializeField] TMPro.TMP_Text playamount2;
    [SerializeField] Betbuttons[] bet_buttons;
    [SerializeField] Sprite brightmarker;
    [SerializeField] Sprite darkmarker;
    [SerializeField] Image markerimage;
    [SerializeField] public TMPro.TMP_Text win0;
    [SerializeField] public TMPro.TMP_Text win1;
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
    [SerializeField] GameObject coinflipobject;
    [SerializeField] GameObject coindanceobject;
    [SerializeField] Image timerprogress;
    bool lastchance = false;
    // Start is called before the first frame update

    bool resultsentdone;
    string result;
    string xresult;
    [SerializeField] TMPro.TMP_Text betinfotext;
    public bool resetData = false;
    bool updatedata = true;
    bool betplaced = false;
    private void Start()
    {
        base.Start();
        result_starting_pos = resultpanel.transform.position;
        GetComponent<AudioSource>().clip = gamestartaudiosource;
        GetComponent<AudioSource>().Play();

        StartCoroutine(UpdateBalanceAndInfo());
        StartCoroutine(addlast9gameresults());

    }
    // Update is called once per frame
    void Update()
    {
       
        timer.text = Mathf.Clamp((int)realtime, 0, 999).ToString();
        datetimetext.text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");
        timerprogress.fillAmount = Convert.ToInt32(timer.text) / 120.0f;
       
        if (updatedata == true)
        {
            StartCoroutine(UpdateBalanceAndInfo());

            StartCoroutine(addlast9gameresults());
            resetTimer();
            updatedata = false;
        }


        if (realtime >= 15)
        {
            betinfotext.text = "Place your chips";
            lastchance = true;
        }

        if (realtime <= 15 && realtime > 14)
        {
            GetComponent<AudioSource>().clip = last15sec;
            GetComponent<AudioSource>().Play();
            betinfotext.text = "Last Chance";
            lastchance = true;
        }
        //

        if (realtime < 11 && resultsentdone == false)
        {
            GetComponent<AudioSource>().clip = nomoreplay;
            GetComponent<AudioSource>().Play();
           infopanel.SetActive(false);
            noinputpanel.SetActive(true);
           sendResult();
            resultsentdone = true;
        }
        if (realtime < 10 && betplaced)
        {
            betinfotext.text = "your bets have been accepted";
        }
        if (realtime < 10 && betplaced==false)
        {
            betinfotext.text = "no more bet";
        }
        if (realtime < 8 &&  betplaced==true )
        {
            betinfotext.text = "no more bet";
        }

    }
    public void sendResult()
    {
        betinfotext.text = "Your bets have been accepted";
        DateTime currenttime = GameObject.FindObjectOfType<SQL_manager>().get_time();
        if (GameObject.FindObjectOfType<SQL_manager>().canLogin(GameObject.FindObjectOfType<userManager>().getUserData().id, GameObject.FindObjectOfType<userManager>().getUserData().password, GameObject.FindObjectOfType<userManager>().getUserData().macid))
        {
            if (totalbalance > (totalbalance - totalbetplaced) && totalbetplaced > 0)
            {
                string status = "Print";
                string gm = "gm";
                string barcode = generatebarcode();
                string command = "INSERT INTO [taas].[dbo].[bet16] (a00,a01,a02,a03,a04,a05,a06,a07,a08,a09,a10,a11,a12,a13,a14,a15," +
                    "tot,qty," +
                    "g_date,status,ter_id,g_id,g_time,p_time,bar,gm,flag) values ("
                    + bet_buttons[0].betamount + "," + bet_buttons[1].betamount + "," + bet_buttons[2].betamount + "," + bet_buttons[3].betamount + "," + bet_buttons[4].betamount + "," + bet_buttons[5].betamount + "," + bet_buttons[6].betamount + "," + bet_buttons[7].betamount + "," + bet_buttons[8].betamount + "," + bet_buttons[9].betamount + "," + bet_buttons[10].betamount + "," + bet_buttons[11].betamount + "," + bet_buttons[12].betamount + "," + bet_buttons[13].betamount + "," + bet_buttons[14].betamount + "," + bet_buttons[15].betamount
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
                print(totalbetplaced);
                GameObject.FindObjectOfType<SQL_manager>().updatebalanceindatabase(GameObject.FindObjectOfType<userManager>().getUserData().id, totalbetplaced);
                StartCoroutine(UpdateBalanceAndInfo());

            }
            betplaced = true;
        }

        else
        {
            SceneManager.LoadScene(0);
        }


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
        foreach (Betbuttons bt in GameObject.FindObjectsOfType<Betbuttons>())
        {
            totalbetplaced += bt.betamount;
        }
        playamount.text = totalbetplaced.ToString();
        playamount2.text = totalbetplaced.ToString();


        int bet = (totalbalance - totalbetplaced);
        fakebalance = bet;
        bet = Mathf.Clamp(bet, 0, 999999999);
        balance.text = bet.ToString();
        balance2.text = bet.ToString();
    }
    public override void GameSequence()
    {
        try
        {
            if (sequenceended == true)
            {
                result = GameObject.FindObjectOfType<SQL_manager>().GetComponent<betManager>().getResult("bihari16");
                if (result != null && sequenceended == true)
                {
                    print("game sequnce started");

                    sequenceended = false;
                    StartCoroutine(bihari6sequence());
                }
            }
        }
        catch (Exception ex)
        {
            print("failed to get result");
            this.GameSequence();
        }

    }
    public IEnumerator addlast9gameresults()
    {
        string endtime = GameObject.FindObjectOfType<betManager>().gameResultTime;
        string endtime2 = DateTime.Parse(endtime).AddMinutes(-4).ToString("hh:mm:ss tt");
        string starttime = DateTime.Parse(endtime).AddMinutes(-20).ToString("hh:mm:ss tt");
        int i = 0;
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = " SELECT TOP (9) id, * FROM [taas].[dbo].[results16] order by [taas].[dbo].[results16] .id desc";
        print(sqlCmnd.CommandText);
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        while (sqlData.Read())
        {

            // gb.transform.position = content.transform.position;
            // gb.transform.rotation = Quaternion.identity;
            if (i < resultsetter.Length)
            {
                resultsetter[i].setResult(sqlData["result"].ToString());
                resultsetter[i].GetComponent<multiplier_resultpanel>().ShowMultiplier(sqlData["status"].ToString());

            }
            i = i + 1;
        }
        sqlData.Close();
        sqlData.DisposeAsync();
        yield return null;
    }
    IEnumerator bihari6sequence()
    {
        print(result);
        markerimage.enabled = false;

        resultobject.SetActive(false);
        GameObject.FindObjectOfType<MultiplierAnimation>().resetstate();

        while (Vector3.Distance(targetofresult.transform.position, resultpanel.transform.position) > 0.1f)
        {
            resultpanel.transform.position = Vector3.Lerp(resultpanel.transform.position, targetofresult.transform.position, Time.deltaTime * 4.0f);
            yield return new WaitForEndOfFrame();

        }
        xresult = result.Substring(0, 4);
        int sector = 0;
        if (xresult == "NR00" || xresult == "NR01" || xresult == "NR02" || xresult == "NR03")
        {
            //  A
            sector = 1;
        }
        else if (xresult == "NR04" || xresult == "NR05" || xresult == "NR06" || xresult == "NR07")
        {
            //  k
            sector = 0;
        }
        else if (xresult == "NR08" || xresult == "NR09" || xresult == "NR10" || xresult == "NR11")
        {
            //  q
            sector = 3;
        }
        else if (xresult == "NR12" || xresult == "NR13" || xresult == "NR14" || xresult == "NR15")
        {
            //  j
            sector = 2;
        }

        outtercirlcle.TurnWheel(sector);

        if (xresult == "NR00" || xresult == "NR04" || xresult == "NR08" || xresult == "NR12")
        {
            //spade
            sector = 1;
        }
        else if (xresult == "NR01" || xresult == "NR05" || xresult == "NR09" || xresult == "NR13")
        {
            //heart
            sector = 2;
        }
        else if (xresult == "NR02" || xresult == "NR06" || xresult == "NR10" || xresult == "NR14")
        {
            //DIAMON
            sector = 4;
        }
        else if (xresult == "NR03" || xresult == "NR07" || xresult == "NR11" || xresult == "NR15")
        {
            //CLOVER
            sector = 0;
        }
        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().clip = wheelspinning;
        GetComponent<AudioSource>().Play();
        panelresult.setResult(xresult);

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
        resultobject.GetComponent<ResultSetter>().setResult(xresult);
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
        while (Vector3.Distance(result_starting_pos, resultpanel.transform.position) > 0.1f)
        {
            resultpanel.transform.position = Vector3.Lerp(resultpanel.transform.position, result_starting_pos, Time.deltaTime * 4.0f);
            yield return new WaitForEndOfFrame();
        }


        resultsentdone = false;

        GameObject.FindObjectOfType<clearbutton>().clearbets();
        noinputpanel.SetActive(false);


        sequenceended = true;
        GetComponent<AudioSource>().clip = gamestartaudiosource;
        GetComponent<AudioSource>().Play();
        updatedata = true;
        yield return null;
    }
    IEnumerator UpdateBalanceAndInfo()
    {

        totalbalance = GameObject.FindObjectOfType<SQL_manager>().balance(GameObject.FindObjectOfType<userManager>().getUserData().id);
        balance.text = totalbalance.ToString();
        balance2.text = totalbalance.ToString();
        fakebalance = totalbalance;
        gameid.text = GameObject.FindObjectOfType<betManager>().gameResultId.ToString();


        yield return null;

    }
    void getwinamount()
    {

        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = "SELECT [clm] FROM [taas].[dbo].[bet16] where g_id=" + GameObject.FindObjectOfType<betManager>().gameResultId + " and ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and status='Prize' and g_time='" + GameObject.FindObjectOfType<betManager>().gameResultTime.ToString() + "' and g_date='" + GameObject.FindObjectOfType<SQL_manager>().server_day.ToString("yyyy-MMM-dd") + "'";//this is the sql command we use to get data about user
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

            if (intwinamount > 900)
            {
                coinflipobject.SetActive(true);
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



    }
}
