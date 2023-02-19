using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S2Pbutton : MonoBehaviour
{
    [SerializeField] Sprite[] cointsprite;
    [SerializeField] Image coinimage;
    [SerializeField] TMP_Text betamounttext;
    public int betamount;
    public int clickcount;
    public void onclick()
    {
        
        if (GameObject.FindObjectOfType<RemoveButton>().removebets == false && (betamount+GameObject.FindObjectOfType<timeManager>().selectedcoinamount)<=GameObject.FindObjectOfType<spin2win_manager>().fakebalance)
        {
            betamount += GameObject.FindObjectOfType<timeManager>().selectedcoinamount;
            clickcount += 1;
            clickcount = Mathf.Clamp(clickcount, 0, cointsprite.Length - 1);
            updateUI();
        }
        if (GameObject.FindObjectOfType<RemoveButton>().removebets == true)
        {
            if(betamount>0)
            {
                betamount -= GameObject.FindObjectOfType<timeManager>().selectedcoinamount;
                updateUI();
                if (betamount < 1)
                {
                    resetbet();
                }

            }
            
         
            GameObject.FindObjectOfType<RemoveButton>().onclickremove();
           

        }
        GameObject.FindObjectOfType<spin2win_manager>().FakeUpdateBalance();
        GameObject.FindObjectOfType<spin2win_manager>().wintext.text = "";
    }
    public void updateUI()
    {
        coinimage.sprite = cointsprite[clickcount];
        betamounttext.text = betamount.ToString();

    }
    public void resetbet()
    {
        clickcount = 0;
        betamount = 0;
        betamounttext.text = " ";
        coinimage.sprite = cointsprite[clickcount];
    }
}
