using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doublechance_button : MonoBehaviour
{

    [SerializeField] GameObject backgroundimage;

    [SerializeField] TMPro.TMP_Text betbuttonID;
    [SerializeField] TMPro.TMP_Text buttonbetamounttext;
    AudioSource ASA;
    public int betamount;
    public int previousbetamount = 0;
    doublechance_gamemanager dbgm;
    double_chance_removebutton dbrb;
    timeManager timeManager;
    [SerializeField] AudioClip betplaceaudio;
    [SerializeField] AudioClip betremoveaudio;
    public void ResetBetButton()
    {
        backgroundimage.SetActive(false);
        buttonbetamounttext.enabled = false;
       dbgm.totalbetplaced -= betamount;
        betamount = 0;
        previousbetamount = 0;

        dbgm.updateplayamount();


    }
    public void Start()
    {
        betbuttonID.text = this.transform.name;
        dbgm = GameObject.FindObjectOfType<doublechance_gamemanager>();
        dbrb = GameObject.FindObjectOfType<double_chance_removebutton>();
        timeManager = GameObject.FindObjectOfType<timeManager>();
       ASA= gameObject.AddComponent<AudioSource>();
    }
    public void Updatebetdata(int betvalue)
    {
        betamount = betvalue;
        backgroundimage.SetActive(true);
        buttonbetamounttext.enabled = true;
        buttonbetamounttext.text = betamount.ToString();
        dbgm.totalbetplaced += betamount - previousbetamount;
        previousbetamount = betamount;
        ASA.playOnAwake = false;
    }

    public void onBetbuttonclicked()
    {
        if (dbrb.removebet == false)
        {
            ASA.clip = betplaceaudio;
            if ((betamount + timeManager.selectedcoinamount + dbgm.totalbetplaced) < dbgm.totalbalance)
            {
                betamount += timeManager.selectedcoinamount;
                backgroundimage.SetActive(true);
                buttonbetamounttext.enabled = true;
                buttonbetamounttext.text = betamount.ToString();
                dbgm.totalbetplaced += betamount - previousbetamount;
                previousbetamount = betamount;
            }
            else
            {
                //print("not enough balance");
                dbgm.setstatus("not enough balance");
            }
        }
        else if (dbrb.removebet == true)
        {
            ASA.clip = betremoveaudio;
            if (betamount > 0)
            {
                betamount -= timeManager.selectedcoinamount;
            } 

           dbgm.totalbetplaced -= previousbetamount-betamount;
            previousbetamount = betamount;

            buttonbetamounttext.text = betamount.ToString();
            if(betamount<1)
            {
                backgroundimage.SetActive(false);
                buttonbetamounttext.enabled = false;
            }
        }
        ASA.Play();
        dbgm.updateplayamount();
    }
}
