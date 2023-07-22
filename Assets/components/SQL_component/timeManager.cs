using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    bool nonbetversion = false;
    public void Start()
    {
        timetillnextgame = GameObject.FindObjectOfType<SQL_manager>().timeForNextGame(mode);//this.GetComponent<SQL_manager>().timeTillNextGame().Subtract(DateTime.Now);

        servertime = GameObject.FindObjectOfType<SQL_manager>().get_time().AddSeconds(5);
        print("time till next game:" + timetillnextgame);
        print("server time:" + servertime);//time patch 
        if (timetillnextgame.ToString("hh:mm:ss tt") == "12:00:00 AM" || timetillnextgame.ToString("hh:mm:ss tt") == "01:00:00 AM")
        {
            //patch; 
        timetillnextgame=    timetillnextgame.AddDays(1);
        }
        double ts =timetillnextgame.Subtract(servertime).TotalSeconds;
        realtime = ts;
        print(realtime);
        InvokeRepeating(nameof(timeloop), 1, 1);

    }
    void OnApplicationFocus(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SceneManager.LoadScene(0);
        }
    }
    // Update is called once per frame
    public void timeloop()
    {

       
            if (realtime <= 0.0f )
            {

                try
                {
                    GameSequence();

                }
                catch
                {
                    print("lost internet");
                }
            }
            else
            {
                resetTimer();
                realtime -= 1;


            }
          
        

    }
    public void resetTimer()
    {
        timetillnextgame = GameObject.FindObjectOfType<SQL_manager>().timeForNextGame(mode);//this.GetComponent<SQL_manager>().timeTillNextGame().Subtract(DateTime.Now);

        servertime = GameObject.FindObjectOfType<SQL_manager>().get_time().AddSeconds(5);
      
        if (timetillnextgame.ToString("hh:mm:ss tt") == "12:00:00 AM" || timetillnextgame.ToString("hh:mm:ss tt") == "01:00:00 AM")
        {
            //patch; 
            timetillnextgame = timetillnextgame.AddDays(1);
        }
        double ts = timetillnextgame.Subtract(servertime).TotalSeconds;

        realtime = ts;
        
    }

    public virtual void GameSequence() { }


}
