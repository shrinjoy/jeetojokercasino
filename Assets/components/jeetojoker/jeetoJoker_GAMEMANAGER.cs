using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
    [SerializeField] FortuneWheelManager outtercirlcle;
    [SerializeField] FortuneWheelManager innercircle;
   
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
        GameObject.FindObjectOfType<MultiplierAnimation>().resetstate();
        print("started panel extension");
        while(Vector3.Distance(targetofresult.transform.position,resultpanel.transform.position)>0.1f)
        {
            resultpanel.transform.position = Vector3.Lerp(resultpanel.transform.position, targetofresult.transform.position,Time.deltaTime*4.0f);
            yield return new WaitForEndOfFrame();
                
        }
        outtercirlcle.TurnWheel(0);
        while(outtercirlcle.isspinning==true)
        {
            yield return new WaitForEndOfFrame();
        }
        innercircle.TurnWheel(0);
        StartCoroutine(GameObject.FindObjectOfType<MultiplierAnimation>().multiplieranimation("N"));
        while (innercircle.isspinning == true)
        {
            yield return new WaitForEndOfFrame();
        }
        
        yield return new WaitForSeconds(4);
        print("started panel dextension");
        while (Vector3.Distance(result_starting_pos, resultpanel.transform.position) > 0.1f)
        {
            resultpanel.transform.position = Vector3.Lerp(resultpanel.transform.position,result_starting_pos, Time.deltaTime * 4.0f);
            yield return new WaitForEndOfFrame();
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
