using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class clearbutton : MonoBehaviour
{
    [SerializeField]AudioSource asa;
    [SerializeField] TMP_Text canceltext;

    public List<betbuttondata> btd=  new List<betbuttondata>();
    bool cancel = true;
    public void clearbets()
    {
        if (btd.Count < 1)
        {
            canceltext.text = "Clear";
            cancel = true;
        }
        else if (btd.Count > 0)
        {
            canceltext.text = "Repeat";

            cancel = false;
        }

        asa.Play();
       
            foreach (Betbuttons bt in GameObject.FindObjectsOfType<Betbuttons>())
            {
                bt.resetBetbutton();
            }
        
       
        
    }
    public void repeat()
    {
        if (cancel == false)
        {
            foreach (betbuttondata d in btd)
            {
                d.betbutton.betamount = d.betamount;
                d.betbutton.updateBetButtonData();
            }
            btd.Clear();
            canceltext.text = "Clear";
        }
    }
    public void addtolist(Betbuttons btnd)
    {
        betbuttondata data = new betbuttondata();
        data.betbutton = btnd;
        data.betamount = btnd.betamount;
        btd.Add(data);  
    }
    
    [System.Serializable]
    public struct betbuttondata
    {
        public int betamount;
        public Betbuttons betbutton;
    }
}
