using System;
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
    public TMPro.TMP_Text start_point_txt;
    public TMPro.TMP_Text end_point_txt;
    //
    public void setdata(string sno,string barcode,string play,string win,string result,string status,string stpoint)
    {
        serialnumber.text = sno;    
        gameid.text = barcode;
        playtext.text = play;
        wintext.text = win;
        resulttext.text = result;
        if(status.ToLower() == "//" )
        {
            status = "N/W";
        }
        statustxt.text = status;

        int start = Convert.ToInt32(stpoint);
        int totalplayed = (Convert.ToInt32(play));
        int end = start-totalplayed;

        start_point_txt.text = stpoint;
        end_point_txt.text =end.ToString();

    }
    public void onclickhistory()
    {
        GameObject.FindObjectOfType<historypanel>().barcode =gameid.text;
    }


}
