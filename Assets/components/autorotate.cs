using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class autorotate : MonoBehaviour
{
    [SerializeField] float speed;  
    
    void Update()
    {
        this.GetComponent<RectTransform>().eulerAngles += new Vector3(0, 0, 1)*Time.deltaTime*speed;  
    }
}
