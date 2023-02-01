using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.InteropServices;
using UnityEngine;
using ZXing.QrCode.Internal;
using UnityEngine.UI;

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
     int totalbalance=0;
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
    // Start is called before the first frame update
    bool startedsequence =false;
    bool resultsentdone;
    string result;
    string xresult;
    [SerializeField] TMPro.TMP_Text betinfotext;
    public bool resetData = false;
    private void Start()
    {
        result_starting_pos = resultpanel.transform.position;
       
        base.Start();   
        StartCoroutine(UpdateBalanceAndInfo());
        StartCoroutine(addlast9gameresults());
    }
    // Update is called once per frame
    void Update()
    {
        timer.text=Mathf.Clamp( realtime,0,999).ToString();
        datetimetext.text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");
        if(resetData==true)
        {
            try
            {
                StartCoroutine(UpdateBalanceAndInfo());
                StartCoroutine(addlast9gameresults());
                StartCoroutine(getwinamount());
                resetData = true;
            }
            catch { 
              resetData= false;
            }
       
        }
        
        if(realtime >= 15)
        {
            betinfotext.text = "Place your chips";
        }

        if(realtime<=15 && realtime >11)
        {
            betinfotext.text = "Last Chance";

        }
        //

        if(realtime<10 && resultsentdone==false)
        {
            noinputpanel.SetActive(true);
            StartCoroutine(sendResult());
            resultsentdone=true;
        }
        if(realtime<11 && resultsentdone)
        {
            betinfotext.text = "no more bets please";
        }
    }
    public IEnumerator sendResult()
    {
        betinfotext.text = "Your bets have been accepted";
        if (totalbalance > (totalbalance-totalbetplaced) && totalbetplaced > 0)
        {
            string status = "Print";
            string gm = "gm";
            string barcode = generatebarcode();
            string command = "INSERT INTO [taas].[dbo].[tasp] (a00,a01,a02,a03,a04,a05,a06,a07,a08,a09,a10,a11," +
                "tot,qty," +
                "g_date,status,ter_id,g_id,g_time,p_time,bar,gm,flag) values ("
                + bet_buttons[0].betamount + "," + bet_buttons[1].betamount + "," + bet_buttons[2].betamount + "," + bet_buttons[3].betamount + "," + bet_buttons[4].betamount + "," + bet_buttons[5].betamount + "," + bet_buttons[6].betamount + "," + bet_buttons[7].betamount + "," + bet_buttons[8].betamount + "," + bet_buttons[9].betamount + "," + bet_buttons[10].betamount + "," + bet_buttons[11].betamount
                + "," + totalbetplaced + "," + totalbetplaced+ ","
                + "'" + DateTime.Today.ToString("yyyy-MM-dd 00:00:00.000") + "'" + "," + "'" + status + "'" + ",'" + GameObject.FindObjectOfType<userManager>().getUserData().id + "'," + GameObject.FindObjectOfType<betManager>().gameResultId + "," + "'" + GameObject.FindObjectOfType<betManager>().gameResultTime + "'" + "," + "'" + DateTime.Today.ToString("yyyy-MM-dd")+" "+DateTime.Now.ToString("HH:mm:ss.000") + "'" + "," + "'" + barcode + "'" + "," + "'" + gm + "'" + "," + 1 + ")";
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
            GameObject.FindObjectOfType<SQL_manager>().updatebalanceindatabase(GameObject.FindObjectOfType<userManager>().getUserData().id,totalbetplaced);
            UpdateBalanceAndInfo();
          
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
        balance.text = bet.ToString();
        balance2.text = bet.ToString();
    }
    public override void GameSequence()
    {
       if(startedsequence== false)
        {
            startedsequence = true;
            StartCoroutine(jeetojokersequence());
            result = GameObject.FindObjectOfType<betManager>().getResult("joker");
            xresult = result.Substring(0, 4);
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
        sqlCmnd.CommandText = "  SELECT  * FROM [taas].[dbo].[resultsTaa] where  g_time between '" + starttime + "' and '" + endtime2 + "' and g_date='" + DateTime.Today.ToString("MM/dd/yyyy") + " " + "00:00:00.000'";
        print(sqlCmnd.CommandText);
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        while (sqlData.Read())
        {
           
            // gb.transform.position = content.transform.position;
            // gb.transform.rotation = Quaternion.identity;
            if (i <= resultsetter.Length - 1)
            {
                resultsetter[i].setResult(sqlData["result"].ToString());
               
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
        {
            sector = 0;
        }
        else if (xresult == "NR01" || xresult == "NR05" || xresult == "NR09")
        {
            sector = 1;
        }
        else if (xresult == "NR02" || xresult == "NR06" || xresult == "NR10")
        {
            sector = 2;
        }
        else if (xresult == "NR03" || xresult == "NR07" || xresult == "NR11")
        {
            sector = 3;
        }
        panelresult.setResult(xresult);

        innercircle.TurnWheel(sector);
        marqueeanim.enabled = true;
      
        
        while (innercircle.isspinning == true)
        {
            yield return new WaitForSecondsRealtime(0.01f);
        }
        marqueeanim.enabled = false;
        StartCoroutine(GameObject.FindObjectOfType<MultiplierAnimation>().multiplieranimation(result.Substring(4)));
        yield return new WaitForSeconds(1.0f);
        resultobject.GetComponent<ResultSetter>().setResult(xresult);//result.Substring(0, 4));
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

        
        
        yield return new WaitForSeconds(1.0f);
        while (Vector3.Distance(result_starting_pos, resultpanel.transform.position) > 0.1f)
        {
            resultpanel.transform.position = Vector3.Lerp(resultpanel.transform.position,result_starting_pos, Time.deltaTime * 4.0f);
            yield return new WaitForEndOfFrame();
        }
        startedsequence = false;
       
        resultsentdone= false;
        
        GameObject.FindObjectOfType<clearbutton>().clearbets();
        noinputpanel.SetActive(false);
        resetData = true;
        yield return null;
    }
    IEnumerator  UpdateBalanceAndInfo()
    {
       
        totalbalance = GameObject.FindObjectOfType<SQL_manager>().balance(GameObject.FindObjectOfType<userManager>().getUserData().id);
        balance.text=totalbalance.ToString();
        balance2.text =totalbalance.ToString();
        fakebalance = totalbalance;
        gameid.text= GameObject.FindObjectOfType<betManager>().gameResultId.ToString();
        resetTimer();
        
        yield return null;
       
    }
    IEnumerator getwinamount()
    {
       
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = "SELECT [clm] FROM [taas].[dbo].[tasp] where g_id=" + GameObject.FindObjectOfType<betManager>().gameResultId + " and ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "'and status='Prize'";//this is the sql command we use to get data about user
        print(sqlCmnd.CommandText);
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        int intwinamount = 0;
        while (sqlData.Read())
        {

            intwinamount += Convert.ToInt32(sqlData["clm"].ToString());
        }
        sqlData.Close();
        sqlData.DisposeAsync();
        print("winamount:" + intwinamount);
        win0.text =intwinamount.ToString();
        win1.text = intwinamount.ToString();
        yield return null;

    }
}
