using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class last10resultobjectsetter : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text singletext;
    [SerializeField] TMPro.TMP_Text doubletext;
    [SerializeField] TMPro.TMP_Text time;
    [SerializeField]TMPro.TMP_Text  multi;
    public void setdata(string result,string multitext = "N",string rtime="")
    {
        if(time!=null)
        {
            time.text = DateTime.Parse(rtime).ToString("HH:mm");
        }
        singletext.text = result.Substring(2, 1);
        doubletext.text =  result.Substring(3, 1);
        multi.text = multitext;
        if (multitext != "N")
        {
            InvokeRepeating(nameof(shrinkunshrink), 0, 2);
        }
    }
   public void shrinkunshrink()
    {
        StartCoroutine(ShrinkUnshrinkAnimation(singletext.gameObject, doubletext.gameObject, multi.gameObject, 1.0f));

    }
    private IEnumerator ShrinkUnshrinkAnimation(GameObject singletexts,GameObject doubltexts,GameObject multipliertext,float animationDuration)
    {
        // Get the initial scale of both texts.
        Vector3 initialScaleShrink = singletext.transform.localScale;
      

        Vector3 initialScaleUnshrink = multipliertext.transform.localScale;

        // Calculate the target scale for both texts (fully shrunken and fully unshrunk).
        Vector3 targetScaleShrink = Vector3.zero;
        Vector3 targetScaleUnshrink = Vector3.one;

        float timer = 0f;

        while (timer < animationDuration)
        {
            timer += Time.deltaTime;

            // Calculate the current scale for both texts based on the interpolation between initial and target scales.
            float t = Mathf.Clamp01(timer / animationDuration);
            singletext.transform.localScale = Vector3.Lerp(initialScaleShrink, targetScaleShrink, t);
            doubletext.transform.localScale= Vector3.Lerp(initialScaleShrink, targetScaleShrink, t);
            multipliertext.transform.localScale = Vector3.Lerp(initialScaleUnshrink, targetScaleUnshrink, t);

            yield return null;
        }

        // Ensure that the texts are in their final states at the end of the animation.
        singletext.transform.localScale = targetScaleShrink;
        multipliertext.transform.localScale = targetScaleUnshrink;
         timer = 0f;

        while (timer < animationDuration)
        {
            timer += Time.deltaTime;

            // Calculate the current scale for both texts based on the interpolation between initial and target scales.
            float t = Mathf.Clamp01(timer / animationDuration);
            singletext.transform.localScale = Vector3.Lerp(initialScaleUnshrink, targetScaleUnshrink, t);
            doubletext.transform.localScale = Vector3.Lerp(initialScaleUnshrink, targetScaleUnshrink, t);
            multipliertext.transform.localScale = Vector3.Lerp(initialScaleShrink, targetScaleShrink, t);

            yield return null;
        }
    }
}
