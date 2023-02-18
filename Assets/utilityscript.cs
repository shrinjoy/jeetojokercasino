using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class utilityscript : MonoBehaviour
{
   public void exitapp()
    {
        Application.Quit();
    }
    
}
public static class ResultConverters
    {
  public static string S2w_ResultConverter(string s)
    {
        string r = null;
        if (s == "NR00")
        {
            r = "0";
        }
        if (s == "NR01")
        {
            r = "1";
        }
        if (s == "NR02")
        {
            r = "2";
        }
        if (s == "NR03")
        {
            r = "3";
        }
        if (s == "NR04")
        {
            r = "4";
        }
        if (s == "NR05")
        {
            r = "5";
        }
        if (s == "NR06")
        {
            r = "6";
        }
        if (s == "NR07")
        {
            r = "7";
        }
        if (s == "NR08")
        {
            r = "8";
        }
        if (s == "NR09")
        {
            r = "9";
        }

        return r;
    }
}
//