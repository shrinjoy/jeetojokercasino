using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class betchips : MonoBehaviour
{
    // Start is called before the first frame update
    bool isselected = true;
    [SerializeField] RectTransform coin_image;
    [SerializeField] GameObject backgroundcoinimage;
    Coroutine cr;
    Vector3 initialSize;
    Vector3 finalSize;
    [SerializeField] int coin_value;
    [SerializeField]bool enabledonstart=false;
    private void Awake()
    {
        initialSize= transform.localScale;
        finalSize = initialSize * 1.02f;
      
    }
    private void Start()
    {
        if (enabledonstart == true)
        {
            onSelected();//
        }
    }
    public void onSelected()
    {
        print("btselectd:" + coin_value);
        GetComponentInParent<AudioSource>().Play();
        foreach (betchips chip in GameObject.FindObjectsOfType<betchips>())
        {
            chip.resetChip();
        }
        if (GameObject.FindObjectOfType<timeManager>() != null)
        {
            GameObject.FindObjectOfType<timeManager>().selectedcoinamount = coin_value;
        }
        isselected = true;
        cr =  StartCoroutine(playchipanimation());
        backgroundcoinimage.SetActive(true);
       
        coin_image.localScale=finalSize;    
    }
    public void resetChip()
    {
        isselected = false;
        backgroundcoinimage.SetActive(false);
        coin_image.localScale = initialSize;
      
        
    }
    IEnumerator playchipanimation()
    {
        while(isselected==true)
        {
            coin_image.eulerAngles += new Vector3(0,0,1f);
            yield return new WaitForEndOfFrame();
        }
    }    
   
}
