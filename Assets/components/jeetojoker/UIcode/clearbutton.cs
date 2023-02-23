using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class clearbutton : MonoBehaviour
{
    public List<BetbuttonData2> betbuttons2 = new List<BetbuttonData2>();
    [SerializeField]public TMP_Text clearbuttontext;
    public bool allowrepeat = false;
    public void clear_clicked()
    {
        foreach(BetplaceSametype bts in GameObject.FindObjectsOfType<BetplaceSametype>())
        {
            bts.reset();
        }


        if (betbuttons2.Count > 0 && allowrepeat == true)
        {
            repeat();
        }
        else if (betbuttons2.Count > 0 && allowrepeat == false)
        {

            clear();

        }
        else if (betbuttons2.Count < 1 && allowrepeat == false)
        {
            clear();
        }
    }

    public void repeat()
    {

        foreach (BetbuttonData2 btd in betbuttons2)
        {
            if (btd.betamount > 0)
            {
                btd.betbutton.betamount = btd.betamount;
                btd.betbutton.updateBetButtonData();
            } 
        }
        clearbuttontext.text = "Clear";
        allowrepeat = false;
    }

    public void clear()
    {
        foreach (BetplaceSametype bts in GameObject.FindObjectsOfType<BetplaceSametype>())
        {
            bts.reset();
        }

        foreach (Betbuttons bt in GameObject.FindObjectsOfType<Betbuttons>())
        {
            bt.resetBetbutton();
            // bt.updateUI();
        }
        if (betbuttons2.Count > 0)
        {
            clearbuttontext.text = "Repeat";
            allowrepeat = true;
        }
    }
}

[System.Serializable]
public struct BetbuttonData2
{
    public int clicks;
    public int betamount;
    public Betbuttons betbutton;
}
