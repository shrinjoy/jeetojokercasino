using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MultiplierAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public TMPro.TMP_Text multiplier;
    public GameObject multiplier_point;
    public RectTransform multiplierlisttextobject;
    float fontsizefinal;
    float fontsizeinitial;
    Vector3 startingpos;
   
    private void Start()
    {
        fontsizeinitial = multiplier.fontSize;
        fontsizefinal = multiplier.fontSize - 5;
        startingpos = multiplier.transform.gameObject.transform.position;


    }
    public void resetstate()
    {
        multiplier.text = " ";
        multiplier.transform.position = startingpos;
        multiplier.fontSize = fontsizeinitial;
    }
    public IEnumerator multiplieranimation(string multipliertext)
    {
       
        multiplier.text = multipliertext;
         
            while(Vector3.Distance(multiplier.transform.position, multiplier_point.transform.position) > 0.1f)
            {
            multiplier.transform.position = Vector3.Lerp(multiplier.transform.position, multiplier_point.transform.position, Time.deltaTime * 5.0f);
            multiplier.fontSize = Mathf.Lerp(multiplier.fontSize, fontsizefinal, Time.deltaTime * 5.0f);
            yield return new WaitForEndOfFrame();

            }

        
       
        
       
    }
}
