using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class randombets : MonoBehaviour
{
    public int betsplaceable = 0;
    List<Betbuttons> allbuttons;
    public List<int> generatednumbers;
    private void Start()
    {
        generatednumbers = new List<int>();
        allbuttons = new List<Betbuttons>();
        allbuttons = GameObject.FindObjectsOfType<Betbuttons>().ToList();

    }
    public void placerandombets()
    {
       
        generatednumbers.Clear();
        for (int i = 0; i < betsplaceable; i++)
        {
            allbuttons[getrandombutton()].onBetButtonClick();
        }

    }

    int getrandombutton()
    {


        int randomnumber = Random.Range(0, allbuttons.Count);

        if (generatednumbers.Contains(randomnumber) == false)
        {
            generatednumbers.Add(randomnumber);
            return randomnumber;
        }

        return getrandombutton();

    }
}
