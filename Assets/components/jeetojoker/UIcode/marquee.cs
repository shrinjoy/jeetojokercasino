using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marquee : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 startingpos;
    Vector3 finalpos;

    private void OnEnable()
    {
       
        startingpos = this.GetComponent<RectTransform>().position;
        finalpos = startingpos + new Vector3(1800, 0, 0);
        this.GetComponent<TMPro.TMP_Text>().enabled = true;
    }
    private void OnDisable()
    {

       this.GetComponent<TMPro.TMP_Text>().enabled= false;
    }
    // Update is called once per frame
    void Update()
    {
        if (isActiveAndEnabled)
        {
            this.GetComponent<RectTransform>().position += new Vector3(50.0f, 0, 0);
            if (this.GetComponent<RectTransform>().position.x > finalpos.x)
            {
                this.GetComponent<RectTransform>().position = startingpos;
            }
        }
    }
}
