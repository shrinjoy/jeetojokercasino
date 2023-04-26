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
    public TMPro.TMP_Text statustxt;
    public void setdata(string sno,string barcode,string play,string win,string result,string status)
    {
        serialnumber.text = sno;    
        gameid.text = barcode;
        playtext.text = play;
        wintext.text = win;
        resulttext.text = result;
        if(status.ToLower() == "print" )
        {
            status = "N/W";
        }
        statustxt.text = status;
    }
    public void onclickhistory()
    {
        GameObject.FindObjectOfType<historypanel>().barcode =gameid.text;
    }


}
