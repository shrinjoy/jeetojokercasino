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
       
        if (s == "NR0")
        {
            r = "0";
        }
        if (s == "NR1")
        {
            r = "1";
        }
        if (s == "NR2")
        {
            r = "2";
        }
        if (s == "NR3")
        {
            r = "3";
        }
        if (s == "NR4")
        {
            r = "4";
        }
        if (s == "NR5")
        {
            r = "5";
        }
        if (s == "NR6")
        {
            r = "6";
        }
        if (s == "NR7")
        {
            r = "7";
        }
        if (s == "NR8")
        {
            r = "8";
        }
        if (s == "NR9")
        {
            r = "9";
        }

        return r;
    }
}
//