using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doublechance_button : MonoBehaviour
{

    [SerializeField] GameObject backgroundimage;
   
    [SerializeField] TMPro.TMP_Text betbuttonID;
    [SerializeField] TMPro.TMP_Text buttonbetamounttext;
    
    public int betamount;
    public int previousbetamount = 0;

    public void ResetBetButton()
    {
        backgroundimage.SetActive(false);
        buttonbetamounttext.enabled = false;
        GameObject.FindObjectOfType<doublechance_gamemanager>().totalbetplaced -= betamount;
    }
    public void Start()
    {
        betbuttonID.text = this.transform.name;
       
      
    }


    public void onBetbuttonclicked()
    {
        if ((betamount + GameObject.FindObjectOfType<timeManager>().selectedcoinamount + GameObject.FindObjectOfType<doublechance_gamemanager>().totalbetplaced) < GameObject.FindObjectOfType<doublechance_gamemanager>().totalbalance)
        {
            betamount += GameObject.FindObjectOfType<timeManager>().selectedcoinamount;
            backgroundimage.SetActive(true);
            buttonbetamounttext.enabled = true;
            buttonbetamounttext.text = betamount.ToString();
            GameObject.FindObjectOfType<doublechance_gamemanager>().totalbetplaced += betamount - previousbetamount;
            previousbetamount = betamount;
        }
        else
        {
            print("not enough balance");
        }

    }
}
