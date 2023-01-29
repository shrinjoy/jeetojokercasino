using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveButton : MonoBehaviour
{
    public bool removebets;
    //
   public void onclickremove()
    {
        if (removebets == true) { removebets=false;}
        else if (removebets == false) { removebets= true; }

    }
}
