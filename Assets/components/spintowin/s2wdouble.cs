using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s2wdouble : MonoBehaviour
{
   public void onclick()
    {
        foreach (S2Pbutton bt in GameObject.FindObjectsOfType<S2Pbutton>())
        {
            GetComponentInParent<AudioSource>().Play();

            if ( bt.betamount>0&& (bt.betamount * 2) < GameObject.FindObjectOfType<spin2win_manager>().fakebalance)
            {
                bt.betamount += bt.betamount;
                bt.clickcount += 1;
                bt.clickcount = Math.Clamp(bt.clickcount, 0,5);
                bt.updateUI();
            }


        }
    }
}
