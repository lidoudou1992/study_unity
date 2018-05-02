using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YuSong{

    namespace yusong{

        public class Mean : MonoBehaviour {
            public Texture2D image1;//储存游戏背景
            public Texture2D image2;//储存游戏触摸时的图片


            

            // Use this for initialization
            void Start () {
                //Debug.Log("当前touch状态为：" + UIcontrol.show_touch);
                Debug.Log("level" + Data.Me.name);
                //无什么大用
//                Resolution[] resolutions = Screen.resolutions;
//                foreach (var item in resolutions)
//                {
//                    Debug.Log(item.width + "x" + item.height);
//                }
//                Screen.SetResolution(resolutions[3].width, resolutions[3].height, true);
            }
            
            // Update is called once per frame
            void Update () {
                
            }

            private void OnGUI()
            {
                int touchCount = Input.touchCount;
                if(touchCount == 3)
                GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), image1); 
                if (touchCount > 0)
                {
                    Debug.Log("检测到的点击位置为 ：" + touchCount);
                }
                for (int i = 0; i < touchCount; i++)
                {
                    Vector3 position = Input.GetTouch(i).position;
                    float x = position.x;
                    float y = position.y;

                    GUI.DrawTexture(new Rect(x, Screen.height - y, 120, 120), image2);
                    GUI.Label(new Rect(x, Screen.height - y, 120, 120), "坐标为： " + position);
                }
            }

            //判断当前是否能够进行

            //获取当前的屏幕分辨率
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
                Debug.Log("当前的屏幕分辨率为：" + "x:" + Screen_values.x + "y:" +Screen_values.y);
                return Screen_values;
            }

        }
    }
}
