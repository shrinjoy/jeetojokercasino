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
    int mode = 0;

    public void setResult(string result,string timestring,int mode=0)
    {
        if (mode == 0)
        {
            datetime.text = timestring;
            if (result == "NR00")
            {
                icon.sprite = sprites[0];
                textresult.text = "JH";
            }
            else if (result == "NR01")
            {
                icon.sprite = sprites[1];
                textresult.text = "JS";
            }
            else if (result == "NR02")
            {
                icon.sprite = sprites[2];
                textresult.text = "JD";
            }
            //
            else if (result == "NR03")
            {
                icon.sprite = sprites[3];
                textresult.text = "JC";
            }
            //
            else if (result == "NR04")
            {
                icon.sprite = sprites[0];
                textresult.text = "QH";
            }
            else if (result == "NR05")
            {
                icon.sprite = sprites[1];
                textresult.text = "QS";
            }
            else if (result == "NR06")
            {
                icon.sprite = sprites[2];
                textresult.text = "QD";
            }
            else if (result == "NR07")
            {
                icon.sprite = sprites[3];
                textresult.text = "QC";
            }
            //
            else if (result == "NR08")
            {
                icon.sprite = sprites[0];
                textresult.text = "KH";
            }
            else if (result == "NR09")
            {
                icon.sprite = sprites[1];
                textresult.text = "KS";
            }
            else if (result == "NR10")
            {
                icon.sprite = sprites[2];
                textresult.text = "KD";
            }
            else if (result == "NR11")
            {
                icon.sprite = sprites[3];
                textresult.text = "KC";
            }
        }
        if (mode == 1)
        {
            datetime.text = timestring;
            if (result == "NR00")
            {
                icon.sprite = sprites[0];
                textresult.text = "AH";
            }
            else if (result == "NR01")
            {
                icon.sprite = sprites[1];
                textresult.text = "AS";
            }
            else if (result == "NR02")
            {
                icon.sprite = sprites[2];
                textresult.text = "AD";
            }
            //
            else if (result == "NR03")
            {
                icon.sprite = sprites[3];
                textresult.text = "AC";
            }
            ////////////////////////////////
            else if (result == "NR04")
            {
                icon.sprite = sprites[0];
                textresult.text = "KH";
            }
            else if (result == "NR05")
            {
                icon.sprite = sprites[1];
                textresult.text = "KS";
            }
            else if (result == "NR06")
            {
                icon.sprite = sprites[2];
                textresult.text = "KD";
            }
            else if (result == "NR07")
            {
                icon.sprite = sprites[3];
                textresult.text = "KC";
            }
            ///////////////////////////////
            else if (result == "NR08")
            {
                icon.sprite = sprites[0];
                textresult.text = "QH";
            }
            else if (result == "NR09")
            {
                icon.sprite = sprites[1];
                textresult.text = "QS";
            }
            else if (result == "NR10")
            {
                icon.sprite = sprites[2];
                textresult.text = "QD";
            }
            else if (result == "NR11")
            {
                icon.sprite = sprites[3];
                textresult.text = "QC";
            }
            /////////////////////////////
            else if (result == "NR12")
            {
                icon.sprite = sprites[0];
                textresult.text = "JH";
            }
            else if (result == "NR13")
            {
                icon.sprite = sprites[1];
                textresult.text = "JS";
            }
            else if (result == "NR14")
            {
                icon.sprite = sprites[2];
                textresult.text = "JD";
            }
            else if (result == "NR15")
            {
                icon.sprite = sprites[3];
                textresult.text = "JC";
            }

        }
        if(mode==2)
        {
            datetime.text = timestring;
            icon.sprite = null;
            if (result == "NR00")
            {     
                textresult.text = "0";
            }
            else if (result == "NR01")
            {
             textresult.text = "1";
            }
            else if (result == "NR02")
            {
                textresult.text = "2";
            }
            else if (result == "NR03")
            {
                textresult.text = "3";
            }
            else if (result == "NR04")
            {
                textresult.text = "4";
            }
            else if (result == "NR05")
            {
                textresult.text = "5";
            }
            else if (result == "NR06")
            {
                textresult.text = "6";
            }
            else if (result == "NR07")
            {
                textresult.text = "7";
            }
            else if (result == "NR08")
            {
                textresult.text = "8";
            }
            else if (result == "NR09")
            {
                textresult.text = "9";
            }
        }
    }
  
}

