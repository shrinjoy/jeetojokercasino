using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class historyInfoSetter : MonoBehaviour
{
    public TMPro.TMP_Text serialnumber;
    public TMPro.TMP_Text gameid;
    public TMPro.TMP_Text playtext;
    public TMPro.TMP_Text wintext;
    public TMPro.TMP_Text resulttext;
    
    public void setdata(string sno,string barcode,string play,string win,string result)
    {
        serialnumber.text = sno;    
        gameid.text = barcode;
        playtext.text = play;
        wintext.text = win;
        resulttext.text = result;
        
    }
    public void onclickhistory()
    {
        GameObject.FindObjectOfType<historypanel>().barcode = serialnumber.text;
    }


}