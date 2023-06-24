using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class double_chance_removebutton : MonoBehaviour
{
    public bool removebet = false;
    public void Toggleremovebet()
    {
        if (removebet == false)
        {
            removebet = true;
        }
        else if (removebet == true)
        { 
            removebet = false; 
        }
    }
}
