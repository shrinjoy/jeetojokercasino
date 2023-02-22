using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class betdetails_prefab : MonoBehaviour
{
    [SerializeField] TMP_Text betpositiontext;
    [SerializeField] TMP_Text playamount;
    [SerializeField] TMP_Text wontext;
    [SerializeField] Sprite[] sprites;
    [SerializeField] Image iconpng;
    int mode = 0;
    public void SetData(string bp,string pa,string w,int m)
    {
        mode = m;
        if(mode==0)
        {

            betpositiontext.text = bp;
            playamount.text = pa;
            wontext.text = w;
            if(bp.Substring(1)=="H")
            {
                iconpng.sprite = sprites[0];//heart
            }
            else if (bp.Substring(1) == "S")
            {
                iconpng.sprite = sprites[1];

            }
            else if (bp.Substring(1) == "D")
            {
                iconpng.sprite = sprites[2];

            }
            else if (bp.Substring(1) == "C")
            {
                iconpng.sprite = sprites[3];

            }

        }
    }


}
