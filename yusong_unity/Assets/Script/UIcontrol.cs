using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YuSong{

	namespace yusong{

		public class UIcontrol : MonoBehaviour {

            public FamilyList family_list = null;

            //Dropdown
            public Dropdown dropdown;
            public string[] showText;
            public Sprite[] showSprit;
            List<string> itemname;
            List<Sprite> sprite_list;
            public Image other_img;//任意的img，用来赋值替换

            //public static bool show_touch;

            public bool  isPause=false;
            public bool  isFocus=false;

            void OnEnable()
            {
                isPause = false;
                isFocus = false;
            }

            private void Start()
            {
                itemname = new List<string>();
                sprite_list = new List<Sprite>();

                AddName();
                UpdateDrawDownItem(itemname);
                dropdown.onValueChanged.AddListener(delegate
                {
                    DropdownValueChanged(dropdown);
                });
            }

            private void UpdateDrawDownItem(List<string> showNames)
            {
                dropdown.options.Clear();
                Dropdown.OptionData itemData;
                for (int i = 0; i < showNames.Count; i++)
                {
                    //为每一个option赋值
                    itemData = new Dropdown.OptionData();
                    itemData.text = showNames[i];
                    itemData.image = sprite_list[i];
                    dropdown.options.Add(itemData);
                }
                //dropdown初始的选项
                dropdown.captionText.text = showNames[0];
                other_img.sprite = sprite_list[0];
                dropdown.captionImage = other_img;
            }


            void AddName()
            {
                for (int i = 0; i < showText.Length; i++)
                {
                    itemname.Add(showText[i]);
                }
                for (int i = 0; i < showSprit.Length; i++)
                {
                    sprite_list.Add(showSprit[i]);
                }
            }

            void DropdownValueChanged(Dropdown change)
            {
                Debug.Log("new value:" + change.value);
            }

            //读取jaon

            void OnApplicationPause()
            {
#if UNITY_IPHONE || UNITY_ANDROID
                Debug.Log("OnApplicationPause  " + isPause + "  " + isFocus);
                if (!isPause)
                {
                    // 强制暂停时，事
                }
                else
                {
                    isFocus = true;
                }
                isPause = true;
#endif
            }

            void OnApplicationFocus(bool  focus)
            {
#if UNITY_IPHONE || UNITY_ANDROID
                Debug.Log("OnApplicationFocus  " + isPause + "  " + isFocus);
                if (focus)
                {
                    // “启动”手机时，事件
                    isPause = false;
                    isFocus = false;
                }
                if (isPause)
                {
                    isFocus = true;
                }
#endif
            }


        }

	}
}
