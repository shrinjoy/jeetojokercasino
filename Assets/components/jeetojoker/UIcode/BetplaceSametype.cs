using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetplaceSametype : MonoBehaviour
{
    [SerializeField] GameObject[] gbs;
    [SerializeField] Sprite[] sprites;
    [SerializeField] Image img;
    [SerializeField] TMPro.TMP_Text betplaytext;
    Color initialcolor;
    private void Start()
    {
        initialcolor = betplaytext.color;
    }

    public void reset()
    {
        betplaytext.text = "Play";
        img.sprite = sprites[0];
        betplaytext.color = initialcolor;
    }
    public void onclickBPST()
    {
        foreach(GameObject gb in gbs)
        {
            gb.GetComponent<Betbuttons>().onBetButtonClick();
        }
        int bam = 0;
        foreach (GameObject bt in gbs)
        {
            bam += bt.GetComponent<Betbuttons>().betamount;
        }
        if (bam < 1)
        {
            betplaytext.text = "Play";
            
            
            img.sprite = sprites[0];

            betplaytext.color = initialcolor;
        }
        if (bam > 0)
        {
            betplaytext.text = GameObject.FindObjectOfType<timeManager>().selectedcoinamount.ToString();
            betplaytext.color = Color.black;

            if (GameObject.FindObjectOfType<timeManager>().selectedcoinamount ==1)
            {
                img.sprite = sprites[1];
            }
            if (GameObject.FindObjectOfType<timeManager>().selectedcoinamount == 2)
            {
                img.sprite = sprites[2];
            }
            if (GameObject.FindObjectOfType<timeManager>().selectedcoinamount == 5)
            {
                img.sprite = sprites[3];
            }
            if(GameObject.FindObjectOfType<timeManager>().selectedcoinamount==10)
            {
                img.sprite = sprites[4];
            }
            if (GameObject.FindObjectOfType<timeManager>().selectedcoinamount==50)
            {
                img.sprite = sprites[5];
            }
            if (GameObject.FindObjectOfType<timeManager>().selectedcoinamount==100)
            {
                img.sprite = sprites[6];
            }
            if (GameObject.FindObjectOfType<timeManager>().selectedcoinamount==500)
            {
                img.sprite = sprites[7];
            }
        }
    }
  
}
