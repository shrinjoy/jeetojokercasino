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



    [SerializeField] UnityEngine.UI.Image coinicon;
    [SerializeField] UnityEngine.UI.Image image;
    [SerializeField] TMPro.TMP_Text betamounttext;
    public int betamount;

    // Start is called before the first frame update
    public void onBetButtonClick()
    {
        if (GameObject.FindObjectOfType<RemoveButton>().removebets == false)
        {
            betamount += GameObject.FindObjectOfType<timeManager>().selectedcoinamount;
            updateBetButtonData();
        }
        else if (GameObject.FindObjectOfType<RemoveButton>().removebets == true)
        {
            resetBetbutton();
        }
    }
        // Update is called once per frame
        public void updateBetButtonData()
        {
            betamounttext.text = betamount.ToString();
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



            GameObject.FindObjectOfType<jeetoJoker_GAMEMANAGER>().FakeUpdateBalance();

        }
    
        public void resetBetbutton()
        {
            betamount = 0;
            updateBetButtonData();
        }
    
}
