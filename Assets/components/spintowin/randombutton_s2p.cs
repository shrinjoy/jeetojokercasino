using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class randombutton_s2p : MonoBehaviour
{
    public int betsplaceable = 0;
    List<S2Pbutton> allbuttons;
    public List<int> generatednumbers;
    private void Start()
    {
        generatednumbers = new List<int>();
        allbuttons = new List<S2Pbutton>();
        allbuttons = GameObject.FindObjectsOfType<S2Pbutton>().ToList();

    }
    public void placerandombets()
    {

        generatednumbers.Clear();
        for (int i = 0; i < betsplaceable; i++)
        {
            allbuttons[getrandombutton()].onclick();
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

