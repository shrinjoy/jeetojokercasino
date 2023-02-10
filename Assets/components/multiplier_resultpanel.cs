using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class multiplier_resultpanel : MonoBehaviour
{
    [SerializeField] TMP_Text multipliertext;
   public void ShowMultiplier(string n)
    {
        if (n != "N")
        {
            multipliertext.text = n;
        }
        else
        {
            multipliertext.text = "";
        }

    }
}
