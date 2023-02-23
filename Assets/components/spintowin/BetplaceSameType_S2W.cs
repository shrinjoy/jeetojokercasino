using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BetplaceSameType_S2W : MonoBehaviour
{
    [SerializeField] S2Pbutton[] betbuttons;
    public void OnButtonClick()
    {
        GetComponentInParent<AudioSource>().Stop();
        GetComponentInParent<AudioSource>().Play();
        foreach(S2Pbutton btns in betbuttons)
        {
            btns.onclick();//
        }
    }
}
