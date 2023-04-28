using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeManager : MonoBehaviour
{
    // Start is called before the first frame update
    DateTime timetillnextgame;
    DateTime servertime;
    [HideInInspector]
    public int gameTime;
    [HideInInspector]
    protected double realtime = 0;
    bool isGameSequenceRunning = false;
    public int selectedcoinamount=2;
   [SerializeField] int mode = 0;
    public void Start()
    {
        timetillnextgame = GameObject.FindObjectOfType<SQL_manager>().timeForNextGame(mode);//this.GetComponent<SQL_manager>().timeTillNextGame().Subtract(DateTime.Now);

        servertime = GameObject.FindObjectOfType<SQL_manager>().get_time().AddSeconds(14);
        //("time till next game:" + timetillnextgame);
        //("server time:" + servertime);//time patch 
        if (timetillnextgame.ToString("hh:mm:ss tt") == "12:00:00 AM" || timetillnextgame.ToString("hh:mm:ss tt") == "01:00:00 AM")
        {
            //patch; 
        timetillnextgame=    timetillnextgame.AddDays(1);
        }
        double ts =timetillnextgame.Subtract(servertime).TotalSeconds;
        realtime = ts;
        //(realtime);
        StartCoroutine(timeloop());

    }

    // Update is called once per frame
    public IEnumerator timeloop()
    {

        while (true)
        {
            if (realtime <= 0.0f)
            {


                GameSequence();

            }
            else
            {

                realtime -= 1;


            }
            yield return new WaitForSeconds(1.0f);
        }

    }
    public void resetTimer()
    {
        timetillnextgame = GameObject.FindObjectOfType<SQL_manager>().timeForNextGame(mode);//this.GetComponent<SQL_manager>().timeTillNextGame().Subtract(DateTime.Now);

        servertime = GameObject.FindObjectOfType<SQL_manager>().get_time().AddSeconds(7);
        //("time till next game:" + timetillnextgame);
        //("server time:" + servertime);
        if (timetillnextgame.ToString("hh:mm:ss tt") == "12:00:00 AM" || timetillnextgame.ToString("hh:mm:ss tt") == "01:00:00 AM")
        {
            //patch; 
            timetillnextgame = timetillnextgame.AddDays(1);
        }
        double ts = timetillnextgame.Subtract(servertime).TotalSeconds;

        realtime = ts;
        //(realtime);
    }

    public virtual void GameSequence() { }


}
