using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BetplaceSameType_S2W : MonoBehaviour
{
    [SerializeField] S2Pbutton[] betbuttons;
    [SerializeField] AudioSource asa;
    public void OnButtonClick()
    {
        asa.Play();
        foreach(S2Pbutton btns in betbuttons)
        {
            btns.onclick();//
        }
    }
}
