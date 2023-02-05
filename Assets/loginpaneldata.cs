using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loginpaneldata : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TMPro.TMP_Text balance;
    [SerializeField] TMPro.TMP_Text id;
    [SerializeField] AudioSource asa;
    
    private void Start()
    {
        balance.text = GameObject.FindObjectOfType<SQL_manager>().balance(GameObject.FindObjectOfType<userManager>().getUserData().id).ToString();
        id.text =GameObject.FindObjectOfType<userManager>().getUserData().id;

    }
    public void exitGame()
    {
        asa.Play();
        Application.Quit();
    }
}
