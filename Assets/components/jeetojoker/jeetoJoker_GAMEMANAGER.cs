using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jeetoJoker_GAMEMANAGER :timeManager
{
    [Header("Balance and Info")]
    [SerializeField]   TMPro.TMP_Text balance;
    [SerializeField]   TMPro.TMP_Text gameid;
    [SerializeField]   TMPro.TMP_Text timer;
    [SerializeField] GameObject targetofresult;
    [SerializeField] GameObject resultpanel;
    Vector3 result_starting_pos;
    // Start is called before the first frame update
    bool startedsequence=false;
    private void Start()
    {
        result_starting_pos = resultpanel.transform.position;
        base.Start();   
        UpdateBalanceAndInfo(); 
    }
    // Update is called once per frame
    void Update()
    {
       timer.text= realtime.ToString();
    }
    public override void GameSequence()
    {
       if(startedsequence== false)
        {
           
            startedsequence = true;
            StartCoroutine(jeetojokersequence());
        }
    }
    IEnumerator jeetojokersequence()
    {
        print("started panel extension");
        while(Vector3.Distance(targetofresult.transform.position,resultpanel.transform.position)>0.1f)
        {
            resultpanel.transform.position = Vector3.Lerp(resultpanel.transform.position, targetofresult.transform.position,Time.deltaTime*0.5f);
        }
        yield return new WaitForSeconds(4);
        print("started panel dextension");
        while (Vector3.Distance(result_starting_pos, resultpanel.transform.position) > 0.1f)
        {
            resultpanel.transform.position = Vector3.Lerp(resultpanel.transform.position,result_starting_pos, Time.deltaTime * 0.5f);
        }
        startedsequence = false;
        UpdateBalanceAndInfo();
        resetTimer();
        yield return null;
    }
    public void UpdateBalanceAndInfo()
    {
        
        balance.text=GameObject.FindObjectOfType<SQL_manager>().balance(GameObject.FindObjectOfType<userManager>().getUserData().id).ToString();
        gameid.text= GameObject.FindObjectOfType<betManager>().gameResultId.ToString();
    }
}
