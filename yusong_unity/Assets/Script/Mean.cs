using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mean : MonoBehaviour {
    public Texture2D image1;//储存游戏背景
    public Texture2D image2;//储存游戏触摸时的图片


    

    // Use this for initialization
    void Start () {
        Resolution[] resolutions = Screen.resolutions;
        foreach (var item in resolutions)
        {
            Debug.Log(item.width + "x" + item.height);
        }
        Screen.SetResolution(resolutions[3].width, resolutions[3].height, true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0,0, ScreenValues().x, ScreenValues().y), image1);
        int touchCount = Input.touchCount;
        if (touchCount > 0)
        {
            Debug.Log("检测到的点击位置为 ：" + touchCount);
        }
        for (int i = 0; i < touchCount; i++)
        {
            Vector3 position = Input.GetTouch(i).position;
            float x = position.x;
            float y = position.y;

            GUI.DrawTexture(new Rect(x, ScreenValues().y - y, 120, 120), image2);
            GUI.Label(new Rect(x, ScreenValues().y - y, 120, 120), "坐标为： " + position);
        }
    }

    //获取当前的屏幕坐标
    public Vector2 ScreenValues()
    {
        float left;
        float right;
        float top;
        float bown;
        float width; //屏幕宽
        float heighe; //屏幕高

        Vector3 screen_position = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Mathf.Abs(Camera.main.transform.position.z)));
        left = Camera.main.transform.position.x - (screen_position.x - Camera.main.transform.position.x);
        right = screen_position.x;
        top = screen_position.y;
        bown = Camera.main.transform.position.y - (screen_position.y - Camera.main.transform.position.y);
        width = right - left;
        heighe = top - bown;

        Vector2 Screen_values = new Vector2(width, heighe);
        return Screen_values;
    }

}
