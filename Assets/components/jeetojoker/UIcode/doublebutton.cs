using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doublebutton : MonoBehaviour
{
    public void doublebets()
    {
        foreach (Betbuttons bt in GameObject.FindObjectsOfType<Betbuttons>())
        {
            if ((bt.betamount * 2) < GameObject.FindObjectOfType<jeetoJoker_GAMEMANAGER>().fakebalance)
            {
                bt.betamount *= 2;
                bt.updateBetButtonData();
            }
        }
    }
}
