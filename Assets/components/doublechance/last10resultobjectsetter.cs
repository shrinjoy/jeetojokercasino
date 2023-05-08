using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class last10resultobjectsetter : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text singletext;
    [SerializeField] TMPro.TMP_Text doubletext;

    public void setdata(string result)
    {
        singletext.text = result.Substring(3, 1);
        doubletext.text = result.Substring(2,1);

    }
}
