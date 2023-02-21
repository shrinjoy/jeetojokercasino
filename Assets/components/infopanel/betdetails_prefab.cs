using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class betdetails_prefab : MonoBehaviour
{
    [SerializeField] TMP_Text betpositiontext;
    [SerializeField] TMP_Text playamount;
    [SerializeField] TMP_Text wontext;
    public void SetData(string bp,string pa,string w)
    {
        betpositiontext.text = bp;
        playamount.text = pa;   
        wontext.text = w;
    }


}
