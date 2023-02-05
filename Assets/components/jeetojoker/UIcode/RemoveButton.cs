using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveButton : MonoBehaviour
{
    public bool removebets;
    public AudioSource asa;
    //
   public void onclickremove()
    {
        asa.Play();
        if (removebets == true) { removebets=false; GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, 1.0f); }
        else if (removebets == false) { removebets= true; GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b,0.5f); }

    }
}
