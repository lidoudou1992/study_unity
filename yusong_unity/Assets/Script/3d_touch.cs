using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeTouch : MonoBehaviour {
    const float PRESSURE_MAX = 4f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //监测当前是否存在3dtouch
    public bool CheckTouch()
    {
        if (Input.touchPressureSupported)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.pressure >= PRESSURE_MAX)
                {
                    Handheld.Vibrate();
                    return true;
                }
            }
        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(1))
        {

        }
        else if (Input.GetMouseButtonUp(1))
        {
        }
        else if (Input.GetMouseButton(1))
        {
        }
#endif
        return false;
    }

}
