using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infobutton : MonoBehaviour
{
    [SerializeField] GameObject gb;
    [SerializeField] AudioSource asa;
    public void info_btn()
    {
        asa.Play();
        gb.SetActive(true);
    }
}
