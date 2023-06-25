using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CalendarDateItem : MonoBehaviour {

    public void OnDateItemClick()
    {
        GetComponentInParent<CalendarController>().OnDateItemClick(gameObject.GetComponentInChildren<Text>().text);
        //print(gameObject.GetComponentInChildren<Text>().text);
    }
}
