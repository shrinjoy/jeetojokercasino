using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infobutton : MonoBehaviour
{
    [SerializeField] GameObject gb;
    public void info_btn()
    {
        gb.SetActive(true);
    }
}
