using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detailsbutton : MonoBehaviour
{
    public GameObject detailspanel;
    public void showdetailspanel()
    {
        if(GameObject.FindObjectOfType<claimmanager>().gameid  != null)
        {
            
            this.GetComponent<AudioSource>().Play();
            detailspanel.SetActive(true);
        }
    }
}
