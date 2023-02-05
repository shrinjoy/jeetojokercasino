using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearbutton : MonoBehaviour
{
    [SerializeField]AudioSource asa;
    public void clearbets()
    {
        asa.Play();
        foreach(Betbuttons bt in GameObject.FindObjectsOfType<Betbuttons>())
        {
            bt.resetBetbutton();
        }
    }
}
