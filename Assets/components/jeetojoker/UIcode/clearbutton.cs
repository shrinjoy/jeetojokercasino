using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearbutton : MonoBehaviour
{
    public void clearbets()
    {
        foreach(Betbuttons bt in GameObject.FindObjectsOfType<Betbuttons>())
        {
            bt.resetBetbutton();
        }
    }
}
