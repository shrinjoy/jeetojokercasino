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
        
            id.text =GameObject.FindObjectOfType<userManager>().getUserData().id;
        InvokeRepeating(nameof(updatedata),0,1);
    }
    public async void updatedata()
    {
        PlayerdataResponse response=await GameObject.FindObjectOfType<CasinoAPI>().getuserdata(GameObject.FindObjectOfType<userManager>().getUserData().id, GameObject.FindObjectOfType<userManager>().getUserData().password);
        int bal = response.balance;
            if (bal < 0)

        {
            bal = 0;
        }
        balance.text = bal.ToString();
    }
    public void exitGame()
    {
        asa.Play();
        Application.Quit();
    }
}
