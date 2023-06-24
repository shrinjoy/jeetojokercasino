using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonpanelanimaion : MonoBehaviour
{
    // Start is called before the first frame update
    bool isactive=false;
    public GameObject target;
    public GameObject panel;
    public Vector3 startpos;
    public buttonpanelanimaion otherbtnanim;
    public void Awake()
    {
        startpos=panel.transform.position+new Vector3(-200,0,0);
    }
    public void setpanelstate()
    {

        if (otherbtnanim.isactive == true)
        {
            otherbtnanim.setpanelstate();
        }
        if(isactive==false)
        {
        LeanTween.move(panel,target.transform.position,0.5f);
            this.transform.localScale = new Vector3(-1,1,1);
        isactive=true;
        }
        else if(isactive==true)
        {
        LeanTween.move(panel,startpos,0.5f);
            this.transform.localScale = new Vector3(1, 1, 1);

            isactive = false;
        }
    }
}
