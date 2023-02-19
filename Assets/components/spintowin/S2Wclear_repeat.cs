using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class S2Wclear_repeat : MonoBehaviour
{
    List<BetbuttonData> betbuttons= new List<BetbuttonData>();
    [SerializeField] TMP_Text clearbuttontext;
    public void clear_clicked()
    {
        if (betbuttons.Count < 1)
        {
            foreach (S2Pbutton sp in GameObject.FindObjectsOfType<S2Pbutton>())
            {
                if (sp.betamount > 0)
                {
                    BetbuttonData data = new BetbuttonData();
                    data.clicks = sp.clickcount;

                    data.betamount = sp.betamount;
                    data.betbutton = sp;
                    betbuttons.Add(data);
                }
                sp.resetbet();
            }
            clearbuttontext.text = "Repeat";
        
        }
        else if(betbuttons.Count>1)
        {
            foreach(BetbuttonData btd in betbuttons)
            {
                btd.betbutton.clickcount = btd.clicks;
                btd.betbutton.betamount = btd.betamount;
                btd.betbutton.updateUI();

            }
            betbuttons.Clear();
            clearbuttontext.text="Clear";
        }

    }
}
struct BetbuttonData
{
    public int clicks;
    public int betamount;
    public S2Pbutton betbutton;
}
