using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Betbuttons : MonoBehaviour
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
    [SerializeField] FakeBetbuttons fakebetbutton;


    [SerializeField] UnityEngine.UI.Image coinicon;
    [SerializeField] UnityEngine.UI.Image image;
    [SerializeField] TMPro.TMP_Text betamounttext;
    public int betamount;
    [SerializeField] int mode = 0;
   
    // Start is called before the first frame update
    public void Start()
    {
      
    }
    public void onBetButtonClick()
    {
        GameObject.FindObjectOfType<clearbutton>().allowrepeat = false;
        GameObject.FindObjectOfType<clearbutton>().clearbuttontext.text = "Clear";
        GetComponentInParent<AudioSource>().Play();
        if (mode == 0)
        {
            GameObject.FindObjectOfType<jeetoJoker_GAMEMANAGER>().win0.text = "0";
            GameObject.FindObjectOfType<jeetoJoker_GAMEMANAGER>().win1.text = "0";
        }
        if (mode == 1)
        {

            GameObject.FindObjectOfType<bihari16>().win0.text = "0";
            GameObject.FindObjectOfType<bihari16>().win1.text = "0";
        }
      

        if (GameObject.FindObjectOfType<RemoveButton>().removebets == false)
        {
            if (mode == 0)
            {
                if ((betamount + GameObject.FindObjectOfType<timeManager>().selectedcoinamount) < GameObject.FindObjectOfType<jeetoJoker_GAMEMANAGER>().fakebalance)
                {
                    betamount += GameObject.FindObjectOfType<timeManager>().selectedcoinamount;
                    updateBetButtonData();


                }
            }
            if (mode == 1)
            {
                if ((betamount + GameObject.FindObjectOfType<timeManager>().selectedcoinamount) < GameObject.FindObjectOfType<bihari16>().fakebalance)
                {
                    betamount += GameObject.FindObjectOfType<timeManager>().selectedcoinamount;
                    updateBetButtonData();
                  
                  
                }
            }

        }
       else  if (GameObject.FindObjectOfType<RemoveButton>().removebets == true)
        {
            print("1 remove bet");
            betamount -= 1;
            updateBetButtonData();
          //  GameObject.FindObjectOfType<RemoveButton>().onclickremove();
        }
    }
    // Update is called once per frame
    public void updateBetButtonData()
    {
        fakebetbutton.updateBetButtonData(betamount);
        betamounttext.text = betamount.ToString();
        coinicon.color = new Color(Color.white.r, Color.white.g, Color.white.b, 255);
        if (betamount <= 0)
        {
            betamounttext.text = "Play";
            coinicon.color = new Color(0, 0, 0, 0);
            image.sprite = betbuttonnormal;
        }

        if(betamount > 0 && mode ==1) {
            image.sprite = betbuttonupto10;
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


        if (mode == 0)
        {
            GameObject.FindObjectOfType<jeetoJoker_GAMEMANAGER>().FakeUpdateBalance();
        }
        if (mode == 1)
        {
            GameObject.FindObjectOfType<bihari16>().FakeUpdateBalance();
        }

    }

    public void resetBetbutton()
    {
        betamount = 0;
        updateBetButtonData();
    }

}
