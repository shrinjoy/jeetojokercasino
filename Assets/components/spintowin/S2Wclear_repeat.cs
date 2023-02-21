using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class S2Wclear_repeat : MonoBehaviour
{
   
   public List<BetbuttonData> betbuttons2 = new List<BetbuttonData>();
    [SerializeField]public TMP_Text clearbuttontext;

   public bool allowrepeat = false;
    public void clear_clicked()
    {
        GetComponentInParent<AudioSource>().Play();
        if (betbuttons2.Count>0&&allowrepeat==true)
        {
            repeat();
        }
      else if(betbuttons2.Count>0 && allowrepeat ==false) {

            clear();

        }
      else if(betbuttons2.Count<1 && allowrepeat==false)
        {
            clear();
        }
    }

    public void repeat()
    {
        
        foreach (BetbuttonData btd in betbuttons2)
        {
            if (btd.betamount > 0)
            {
                btd.betbutton.betamount = btd.betamount;
                btd.betbutton.clickcount = btd.clicks;
                btd.betbutton.updateUI();
            }
        }
        clearbuttontext.text = "Clear";
        allowrepeat = false;
    }

    public void clear()
    {
        foreach (S2Pbutton bt in GameObject.FindObjectsOfType<S2Pbutton>())
        {
            bt.resetbet();
            // bt.updateUI();
        }
        if(betbuttons2.Count>0)
        {
            clearbuttontext.text = "Repeat";
            allowrepeat = true;
        }    
    }
}

[System.Serializable]
public struct BetbuttonData
{
    public int clicks;
    public int betamount;
    public S2Pbutton betbutton;
}
