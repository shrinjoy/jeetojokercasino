using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeBetbuttons : MonoBehaviour
{
    [SerializeField] Sprite betbuttonnormal;
    [SerializeField] Sprite betbuttonupto10;
    [SerializeField] Sprite betbuttonupto50;
    [SerializeField] Sprite betbuttonupto100;
    [SerializeField] Sprite coin1;
    [SerializeField] Sprite coin2;
    [SerializeField] Sprite coin5;
    [SerializeField] Sprite coin10;
    [SerializeField] Sprite coin50;
    [SerializeField] Sprite coin100;
    [SerializeField] Sprite coin500;



    [SerializeField] UnityEngine.UI.Image coinicon;
    [SerializeField] UnityEngine.UI.Image image;
    [SerializeField] TMPro.TMP_Text betamounttext;
    public int betamount;
    int mode = 0;
 
    // Update is called once per frame
    public void updateBetButtonData(int betamount)
    {
        betamounttext.text = betamount.ToString();
        if(mode ==1 && betamount >0)
        {
            image.sprite = betbuttonupto10;
        }
        coinicon.color = new Color(Color.white.r, Color.white.g, Color.white.b, 255);
        if (betamount <= 0)
        {
            betamounttext.text = "Play";
            coinicon.color = new Color(0, 0, 0, 0);
            image.sprite = betbuttonnormal;
        }
        if (betamount >= 10)
        {

            image.sprite = betbuttonupto10;
        }
        if (betamount >= 50)
        {
            image.sprite = betbuttonupto50;
        }
        else if (betamount >= 100)
        {
            image.sprite = betbuttonupto100;
        }
        if (betamount >= 1)
        {
            coinicon.sprite = coin1;
        }
        if (betamount >= 2)
        {
            coinicon.sprite = coin2;
        }
        if (betamount >= 5)
        {
            coinicon.sprite = coin5;
        }
        if (betamount >= 10)
        {
            coinicon.sprite = coin10;
        }
        if (betamount >= 50)
        {
            coinicon.sprite = coin50;
        }
        if (betamount >= 100)
        {
            coinicon.sprite = coin100;
        }
        if (betamount >= 500)
        {
            coinicon.sprite = coin500;
        }



       

    }

    public void resetBetbutton()
    {
     
        updateBetButtonData(0);
    }
}
