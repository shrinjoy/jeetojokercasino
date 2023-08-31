using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel_resultObject_s2w : MonoBehaviour
{
    [SerializeField] GameObject normalobject;
    [SerializeField] GameObject multiplierobject;
    [SerializeField] TMPro.TMP_Text NormalResulttext;
    [SerializeField] TMPro.TMP_Text multiplieResultText;
    [SerializeField] TMPro.TMP_Text multiplierText;
    [SerializeField] Color[] color;
    [SerializeField] TMP_Text timertext;
    public void SetResult(string Result,string rtimer="")
    {
        if (timertext != null)
        {
            timertext.text = DateTime.Parse(rtimer).ToString("HH:mm"); ;
        }
        this.GetComponent<Image>().color = color[Convert.ToInt32(ResultConverters.S2w_ResultConverter(Result.Substring(0, 3)))];

        if(Result.Substring(4)=="N")
        {
            //normal mode
            normalobject.SetActive(true);
            NormalResulttext.text =  ResultConverters.S2w_ResultConverter(Result.Substring(0,3));
        }
        if (Result.Substring(4) != "N")
        {
            //normal mode
            normalobject.SetActive(false);
            multiplierobject.SetActive(true);
            multiplieResultText.text = ResultConverters.S2w_ResultConverter(Result.Substring(0,3));
            multiplierText.text = Result.Substring(4);
        }

    }

}
