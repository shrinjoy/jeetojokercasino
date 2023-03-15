using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doublebutton : MonoBehaviour
{
    [SerializeField]AudioSource asa;
    [SerializeField] int mode = 0;
    public void doublebets()
    {
        asa.Play(); 
        foreach (Betbuttons bt in GameObject.FindObjectsOfType<Betbuttons>())
        {
            if (mode == 0)
            {
                if ((bt.betamount * 2) < GameObject.FindObjectOfType<jeetoJoker_GAMEMANAGER>().totalbalance)
                {
                    bt.betamount *= 2;
                    bt.updateBetButtonData();
                }
            }
            else if(mode==1)
            {
                if ((bt.betamount * 2) < GameObject.FindObjectOfType<bihari16>().totalbalance)
                {
                    bt.betamount *= 2;
                    bt.updateBetButtonData();//
                }
            }
        }
    }
}
