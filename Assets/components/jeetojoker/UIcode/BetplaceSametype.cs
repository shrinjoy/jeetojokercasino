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
    int clickcount = 0;
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
        clickcount= 0;
    }
    public void onclickBPST()
    {
        clickcount += 1;
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
            betplaytext.text = clickcount.ToString();
            betplaytext.color = Color.black;

            if (clickcount >=1)
            {
                img.sprite = sprites[1];
            }
            if (clickcount>= 2)
            {
                img.sprite = sprites[2];
            }
            if (clickcount >= 5)
            {
                img.sprite = sprites[3];
            }
            if(clickcount>=10)
            {
                img.sprite = sprites[4];
            }
            if (clickcount>=50)
            {
                img.sprite = sprites[5];
            }
            if (clickcount>=100)
            {
                img.sprite = sprites[6];
            }
            if (clickcount>=500)
            {
                img.sprite = sprites[7];
            }
        }
    }
    private void Update()
    {
        int bam = 0;
        foreach (GameObject bt in gbs)
        {
            bam += bt.GetComponent<Betbuttons>().betamount;
        }
        if(bam <1)
        {
            reset();
        }
    }

}
