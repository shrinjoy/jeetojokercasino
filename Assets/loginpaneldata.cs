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
        int bal = GameObject.FindObjectOfType<SQL_manager>().balance(GameObject.FindObjectOfType<userManager>().getUserData().id);
        if(bal < 0)

        {
            bal = 0;
        }
        balance.text = bal.ToString();
            id.text =GameObject.FindObjectOfType<userManager>().getUserData().id;

    }
    public void exitGame()
    {
        asa.Play();
        Application.Quit();
    }
}
