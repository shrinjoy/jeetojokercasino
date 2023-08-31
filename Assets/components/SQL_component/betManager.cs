using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public async void setResultData(string gameresulttime="",int gameid=0)
    {
        gamedata data = await GameObject.FindObjectOfType<CasinoAPI>().getgamedata("http://191.101.3.139:3000/s2w/getgameinfo/");
        print("updated game data");
        gameResultTime= data.currentgamedrawtime;
        gameResultId= data.gameid;
    }
    public async Task<string> getResult(string gamemode)
    {
        string  gameResult =await GameObject.FindObjectOfType<CasinoAPI>().getresultbyid(GameObject.FindObjectOfType<betManager>().gameResultId);
        print("result" + gameResult);
       return gameResult;
    }
}
