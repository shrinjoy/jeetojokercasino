using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Results_object_info : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Sprite[] sprites;
    [SerializeField] TMPro.TMP_Text textresult;
    [SerializeField] TMPro.TMP_Text datetime;
    public void setResult(string result,string timestring)
    {
        datetime.text = timestring;
        if (result == "NR00")
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
    }
  
}

