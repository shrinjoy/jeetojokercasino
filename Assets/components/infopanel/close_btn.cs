using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class close_btn : MonoBehaviour
{
   [SerializeField] GameObject infopanel;
    public void closepanel()
    {
        infopanel.SetActive(false);
    }
}
