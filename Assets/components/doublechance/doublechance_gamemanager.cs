using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;

public class doublechance_gamemanager : timeManager
{
    bool sequenceended=true;
    string result;
    [SerializeField] TMPro.TMP_Text timer;
    [SerializeField] TMPro.TMP_Text datetimetext;
    [SerializeField] GameObject ResultPanel_content;
    [SerializeField] GameObject last10resultprefab;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        StartCoroutine(addlastgameresults());
    }

    // Update is called once per frame
    void Update()
    {
        timer.text = Mathf.Clamp((int)realtime, 0, 999).ToString();
        datetimetext.text = DateTime.Now.AddSeconds(40).ToString("yyyy-MM-dd hh:mm:ss tt");

       

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
        yield return new WaitForFixedUpdate();
        resetTimer();
        sequenceended = true;
        StartCoroutine(addlastgameresults());

    }
}
