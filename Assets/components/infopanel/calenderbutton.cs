using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class calenderbutton : MonoBehaviour
{
    public GameObject calenderob;
    public CalendarController cc;
    public TMPro.TMP_Text calendertext;
    public bool fetchdata=false;
    string localdatetimeyear;
    public void oncalenderbuttonclicked()
    {
        calenderob.SetActive(true);
     
    }
   
}
