using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultiplierAnimation_single : MonoBehaviour
{
    string[] multiplier_text_objects = { "10x", "9x", "8x", "7x", "6x", "5x", "4x", "3x", "2x", "1x", "N" };
    [SerializeField] TMP_Text multipliertext=null;
    int counter = 0;
    [SerializeField] Transform multipliertarget;
    Vector3 startpointl;
    float startsize;

    private void Start()
    {
        multipliertext.enabled = false;
       startpointl= transform.position;
        startsize = multipliertext.fontSize;
    }

    public void PlayMultiplierAnimation(string multiplierstring)
    {
        this.transform.position = startpointl;
        multipliertext.fontSize= startsize; 
        multipliertext.enabled = true;
        counter = 0;
        StartCoroutine(IMultiplierAnimation(multiplierstring));
    }
    IEnumerator IMultiplierAnimation(string N)
    {

        while(counter<6)
        {
            for(int i=0;i<multiplier_text_objects.Length;i++)
            {
                multipliertext.text = multiplier_text_objects[i];
                yield return new WaitForSeconds(0.1f);
            }
            counter++;
            yield return new WaitForEndOfFrame();
        }
        while(Vector3.Distance(this.transform.position,multipliertarget.transform.position)>0.1f)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, multipliertarget.transform.position,Time.fixedDeltaTime*5.0f);
            multipliertext.fontSize = Mathf.Lerp(startsize,startsize*0.050f,Time.deltaTime*3.0f);
            yield return new WaitForEndOfFrame();
        }
        multipliertext.text = N;
        
        yield return null;
    }
}
