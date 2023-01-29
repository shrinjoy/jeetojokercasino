using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetplaceSametype : MonoBehaviour
{
    [SerializeField] GameObject[] gbs;
   public void onclickBPST()
    {
        foreach(GameObject gb in gbs)
        {
            gb.GetComponent<Betbuttons>().onBetButtonClick();
        }
    }
}
