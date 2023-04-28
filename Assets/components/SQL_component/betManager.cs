using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class betManager : MonoBehaviour
{
    //
 [SerializeField]  public string gameResultTime;
  [SerializeField] public int gameResultId;
  
    SQL_manager sqm;
   
    private void Start()
    {
        sqm = GameObject.FindObjectOfType<SQL_manager>();

    }
    public void setResultData(string gameresulttime,int gameid)
    {
        gameResultTime= gameresulttime;
        gameResultId= gameid;
    }
    public string getResult(string gamemode)
    {
        try
        {
            string gameResult = sqm.betResult(gameResultTime, gameResultId, gamemode);

            return gameResult;
        }
        catch
        {
            return null;
        }
    }
}
