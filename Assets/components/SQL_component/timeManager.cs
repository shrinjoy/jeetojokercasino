using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class timeManager : MonoBehaviour
{
    // Start is called before the first frame update
    bool betversion = false;
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
    public async void Start()
    {
        int d = await GameObject.FindObjectOfType<CasinoAPI>().gettimeleft("http://191.101.3.139:3000/s2w/gettimeleft/");

        realtime = (double)d;
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
            if (realtime > 10.0f)
            {
                resetTimer();
            }
            else
            {
                realtime -= 1;
            }

            }
          
        

    }
    public async void resetTimer()
    {
        int d = await GameObject.FindObjectOfType<CasinoAPI>().gettimeleft("http://191.101.3.139:3000/s2w/gettimeleft/");

        realtime = (double)d;

    }

    public virtual async void GameSequence() { }


}
