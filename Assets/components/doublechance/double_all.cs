using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class double_all : MonoBehaviour
{
    [SerializeField]string[] buttonnames;
    public void onclickall()
    {
        print("select all clicked");
        for(int i =0;i<buttonnames.Length;i++)
        {
            GameObject.Find(buttonnames[i]).GetComponent<doublechance_button>().onBetbuttonclicked();

        }
    }
}
