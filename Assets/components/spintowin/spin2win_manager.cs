using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class spin2win_manager : timeManager
{
    [SerializeField] TMPro.TMP_Text gameIDtext;
    [SerializeField] TMPro.TMP_Text betplacedtext;
    [SerializeField] TMPro.TMP_Text wintext;
    [SerializeField] TMPro.TMP_Text timetext;
    [SerializeField] Image Timerprogressbar;
    [SerializeField] FortuneWheelManager fortunewheelobject;
    [SerializeField] GameObject marker;
    [SerializeField] GameObject ResultPanel_content;
    [SerializeField] GameObject ResultsPanel_content_object;
    bool sequenceended = true;
    bool ResetData = false;
    string result;
    private void Start()
    {
       base.Start();
        StartCoroutine(addlastgameresults());

    }
    private void Update()
    {
        if(ResetData==true)
        {
            StartCoroutine(addlastgameresults());
            ResetData = false;
        }


        realtime = Mathf.Clamp((float)realtime, 0, 999);
        int minutes = Mathf.FloorToInt((float)realtime / 60F);
        int seconds = Mathf.FloorToInt((float)realtime - minutes * 60);
       
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        
        Timerprogressbar.fillAmount =1.0f- (float)(realtime /120.0);
        timetext.text = niceTime;
       

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
    IEnumerator Spin2Win()
    {
        marker.SetActive(false);

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
        fortunewheelobject.TurnWheel(sector);
        GameObject.FindObjectOfType<MultiplierAnimation_single>().PlayMultiplierAnimation(result.Substring(4));
        while (fortunewheelobject.isspinning == true)
        {
            yield return new WaitForEndOfFrame();
        }

        ResetData = true;
        resetTimer();
        marker.SetActive(true);
        sequenceended= true;
      
        yield return null;
    }

}
