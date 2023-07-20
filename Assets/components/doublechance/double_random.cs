using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class double_random : MonoBehaviour
{
    public int betsplaceable = 0;
    List<GameObject> allbuttons;
    public List<int> generatednumbers;
   
    private void Start()
    {
        generatednumbers = new List<int>();
        allbuttons = new List<GameObject>();
        allbuttons = GameObject.FindGameObjectsWithTag("double_double_button").ToList();
    }
    public void placerandombets()
    {

        generatednumbers.Clear();
        for (int i = 0; i < betsplaceable; i++)
        {
            allbuttons[getrandombutton()].GetComponent<doublechance_button>().onBetbuttonclicked();
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

