using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class FortuneWheelManager : MonoBehaviour
{
    private bool _isStarted;
    private float[] _sectorsAngles;
    private float _finalAngle;
    private float _startAngle = 0;
    private float _currentLerpRotationTime;
    float time;
    public AnimationCurve curve;
    public bool isspinning=false;
    public float[] sectors;
    public GameObject Circle;
   
    
    // Rotatable Object with rewards
    public void TurnWheel (int stoppinganglesector)
    {
    	// Player has enough money to turn the wheel    
    	    _currentLerpRotationTime = 0f;	
    	    // Fill the necessary angles (for example if you want to have 12 sectors you need to fill the angles with 30 degrees step)
    	    _sectorsAngles = sectors;  	
    	    int fullCircles = 6;
    	    float randomFinalAngle = _sectorsAngles[stoppinganglesector];
        // Here we set up how many circles our wheel should rotate before stop
        _finalAngle = -(fullCircles * 360 + randomFinalAngle);
        isspinning = true;

    }
    private void Start()
    {
        
    }
    void FixedUpdate ()
    {
        
        float maxLerpRotationTime = 9.0f*curve.Evaluate(time);
        time += Time.deltaTime;
        _currentLerpRotationTime += Time.fixedDeltaTime;
        if (_currentLerpRotationTime > maxLerpRotationTime || Circle.GetComponent<RectTransform>().eulerAngles.z == _finalAngle)
        {
            _currentLerpRotationTime = maxLerpRotationTime;
            _isStarted = false;
            _startAngle = _finalAngle % 360;
            isspinning = false;
        }
        else
        {
            isspinning = true;
        }
    	float t = _currentLerpRotationTime / maxLerpRotationTime;
    	t = t * t * t * (t * (6f * t - 15f) + 10f);  
    	float angle = Mathf.Lerp (_startAngle, _finalAngle, t);
    	Circle.GetComponent<RectTransform>().eulerAngles = new Vector3 (0, 0, angle);
    }  


}
