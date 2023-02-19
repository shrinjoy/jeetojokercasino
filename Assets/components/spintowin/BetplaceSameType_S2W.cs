using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BetplaceSameType_S2W : MonoBehaviour
{
    [SerializeField] S2Pbutton[] betbuttons;
    public void OnButtonClick()
    {
        foreach(S2Pbutton btns in betbuttons)
        {
            btns.onclick();//


        }
    }
}
