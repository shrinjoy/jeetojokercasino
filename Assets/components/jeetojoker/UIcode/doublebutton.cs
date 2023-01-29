using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doublebutton : MonoBehaviour
{
    public void doublebets()
    {
        foreach (Betbuttons bt in GameObject.FindObjectsOfType<Betbuttons>())
        {
            bt.betamount *= 2;
            bt.updateBetButtonData();   
        }
    }
}
