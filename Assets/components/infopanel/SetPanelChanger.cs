using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPanelChanger : MonoBehaviour
{
    public PanelChanger changer;
    public GameObject targetPanel;
   public void setPanelData()
    {
        GetComponentInParent<AudioSource>().Play();
        changer.Targetpanel = targetPanel;
        changer.changepanel();
    }
}
