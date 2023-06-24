using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doublechance_clear_button : MonoBehaviour
{
    public void clearbets()
    {
        foreach(doublechance_button btns in GameObject.FindObjectsOfType<doublechance_button>())
        {
            btns.ResetBetButton();
        }
    }
}
