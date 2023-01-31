using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSetter : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Sprite[] sprites;
    [SerializeField] TMPro.TMP_Text textresult;
    public bool nonresult = false;
    public void setResult(string result)
    {
        if(result=="NR00")
        {
            icon.sprite = sprites[0];
            textresult.text = "JC";
        }
        else if (result == "NR01")
        {
            icon.sprite = sprites[1];
            textresult.text = "JD";
        }
        else if (result == "NR02")
        {
            icon.sprite = sprites[2];
            textresult.text = "JS";
        }
        //
        else if (result == "NR03")
        {
            icon.sprite = sprites[3];
            textresult.text = "JH";
        }
        //
        else if (result == "NR04")
        {
            icon.sprite = sprites[0];
            textresult.text = "QC";
        }
        else if (result == "NR05")
        {
            icon.sprite = sprites[1];
            textresult.text = "QD";
        }
        else if (result == "NR06")
        {
            icon.sprite = sprites[2];
            textresult.text = "QS";
        }
        else if (result == "NR07")
        {
            icon.sprite = sprites[3];
            textresult.text = "QH";
        }
        //
        else if (result == "NR08")
        {
            icon.sprite = sprites[0];
            textresult.text = "KC";
        }
        else if (result == "NR09")
        {
            icon.sprite = sprites[1];
            textresult.text = "KD";
        }
        else if (result == "NR10")
        {
            icon.sprite = sprites[2];
            textresult.text = "KS";
        }
        else if (result == "NR11")
        {
            icon.sprite = sprites[3];
            textresult.text = "KH";
        }
        if(nonresult==true)
        {
            textresult.text = textresult.text.Substring(0, 1);
        }
    }
}
/*
 SELECT '12 Card 2' as scheme,g_date+' ' 
+g_time as dattim,CASE 
WHEN r.result='NR00' THEN 'JC' 
WHEN r.result='NR01' THEN 'JD' 
WHEN r.result='NR02' THEN 'JS'  
WHEN r.result='NR03' THEN 'JH'
WHEN r.result='NR04' THEN 'QC' 
WHEN r.result='NR05' THEN 'QD'
WHEN r.result='NR06' THEN 'QS' 
WHEN r.result='NR07' THEN 'QH'  
WHEN r.result='NR08' THEN 'KC' 
WHEN r.result='NR09' THEN 'KD' 
WHEN r.result='NR10' THEN 'KS'   
WHEN r.result='NR11' THEN 'KH'    END as result FROM resultsTaa r  where g_date = '" & Format(dtpdt.Value, "dd-MMM-yyyy") & "'
 
 */