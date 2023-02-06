using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class forwardanimationloop : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] Image images;
    int counter = 0;
    public float waittime = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Playaniamtion());
    }
    IEnumerator Playaniamtion()
    {
        while (true)
        {
            counter = counter + 1;
            if (counter > sprites.Length - 1)
            {

                counter = 0;
            }
            images.sprite = sprites[counter];
            yield return new WaitForSecondsRealtime(waittime);  
        }
    }
    
}
