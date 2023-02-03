using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelChanger : MonoBehaviour
{
    public GameObject Targetpanel;
    public GameObject[] target_panels;
    public void changepanel()
    {
        foreach(GameObject gb in target_panels)
        {
            gb.SetActive(false);    
        }
        Targetpanel.SetActive(true);
    }
    
}
