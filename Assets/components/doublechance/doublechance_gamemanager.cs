using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;

public class doublechance_gamemanager : timeManager
{
    bool sequenceended = true;
    string result;
    [SerializeField] TMPro.TMP_Text timer;
    [SerializeField] TMPro.TMP_Text datetimetext;
    [SerializeField] GameObject ResultPanel_content;
    [SerializeField] GameObject last10resultprefab;
    [SerializeField] FortuneWheelManager singles_wheel;
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

    [SerializeField] public doublechance_button[] single_buttons = new doublechance_button[10];
    

    [SerializeField] public doublechance_button[] double_buttons = new doublechance_button[100];

    // Start is called before the first frame update
    public void gamesetup()
    {
        int i = 0;
        print("setting single buttons");
        foreach (doublechance_button btn in singlebutton_panel.GetComponentsInChildren<doublechance_button>())
        {
            single_buttons[i] = btn;
            i += 1;
        }
    
        i = 0;
        print("setting double buttons");

        foreach (doublechance_button btn in doublebutton_panel.GetComponentsInChildren<doublechance_button>())
        {
            print("found double");
            double_buttons[i] = btn;
            i += 1;
        }
       // Array.Sort(double_buttons);
        print("setup is done");
    }
    void Start()
    {
        gamesetup();
        resultstring.enabled = true;
        base.Start();
        multiplieranimationobject.SetActive(false);

        StartCoroutine(addlastgameresults());
        StartCoroutine(UpdateBalanceAndInfo());
        multiplierscrollanimationobject.enabled = false;
        resultstring.text = " ";
    }

    // Update is called once per frame
    IEnumerator UpdateBalanceAndInfo()
    {

        totalbalance = GameObject.FindObjectOfType<SQL_manager>().balance(GameObject.FindObjectOfType<userManager>().getUserData().id);
        if (totalbalance < 0)
        {
            totalbalance = 0;
        }

        balance.text = totalbalance.ToString();



        yield return null;

    }

    void Update()
    {
        timer.text = Mathf.Clamp((int)realtime, 0, 999).ToString();
        datetimetext.text = DateTime.Now.AddSeconds(40).ToString("yyyy-MM-dd hh:mm:ss tt");
        totalplay.text = totalbetplaced.ToString();


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
        string endtime = GameObject.FindObjectOfType<betManager>().gameResultTime;


        string starttime = DateTime.Parse(endtime).AddMinutes(-30).ToString("hh:mm:ss tt");
        int i = 0;

        SqlCommand sqlCmnd = new SqlCommand();
        //
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = " SELECT top(10) * FROM [taas].[dbo].[resultsDou] order by [taas].[dbo].[resultsDou].id desc";
        print(sqlCmnd.CommandText);
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);

        while (sqlData.Read())
        {
            GameObject gb = GameObject.Instantiate(last10resultprefab, ResultPanel_content.transform, false);

            gb.GetComponent<last10resultobjectsetter>().setdata(sqlData["result"].ToString() + sqlData["status"].ToString());

        }
        sqlData.Close();
        sqlData.DisposeAsync();

        yield return null;
    }
    public override void GameSequence()
    {
        print("game sequence");
        try
        {
            if (sequenceended == true)
            {
                result = GameObject.FindObjectOfType<SQL_manager>().GetComponent<betManager>().getResult("doubletrouble");
                if (result != null && sequenceended == true)
                {
                    print("game sequnce started");

                    sequenceended = false;
                    StartCoroutine(doublechancesequence());
                }
            }
        }
        catch (Exception ex)
        {
            print("failed to get result");
            this.GameSequence();
        }

    }
    IEnumerator doublechancesequence()
    {
        print(result);
        multiplieranimationobject.SetActive(true);
        multiplieranimationobject.GetComponent<MultiplierAnimation>().resetstate();
        resultstring.enabled = true;

        Vector2 sector = resulttosectorconvert(result);
        singles_wheel.TurnWheel((int)sector.y);
        doubles_wheel.TurnWheel((int)sector.x);
        multiplierscrollanimationobject.enabled = true;
        while (singles_wheel.isspinning && doubles_wheel.isspinning)
        {
            yield return new WaitForSeconds(0.001f);
        }
        multiplierscrollanimationobject.enabled = false;

        StartCoroutine(multiplieranimationobject.GetComponent<MultiplierAnimation>().multiplieranimation(result.Substring(4)));
        yield return new WaitForSeconds(0.3f);
        print(result.Substring(2, 2));
        resultstring.text = result.Substring(2, 2);

        yield return new WaitForFixedUpdate();
        resetTimer();
        sequenceended = true;
        StartCoroutine(addlastgameresults());

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
}
