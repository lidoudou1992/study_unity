using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System;


[System.Serializable]
public class FamilyInfo
{
    public string name;
    public int age;
    public string tellphone;
    public string address;
}

public class FamilyList {
    public List<FamilyInfo> family_list;
}

public class litjson : MonoBehaviour {
    public FamilyList m_FamilyList = null;

    private void Start()
    {
        ReloadFamilyData();
        DisplayFamilyList(m_FamilyList);
    }

    private void ReloadFamilyData()
    {
        UnityEngine.TextAsset s = Resources.Load("family") as TextAsset;
        string tmp = s.text;
        m_FamilyList = JsonMapper.ToObject<FamilyList>(tmp);
    }

    private void DisplayFamilyList(FamilyList familylist)
    {
        if (familylist == null) return;

        //foreach (FamilyInfo item in familylist.family_list)
        //{
        //    Debug.Log("Name:" + item.name + "       Age:" + item.age + "        Tel:" + item.tellphone + "      Addr:" + item.address);
        //}
        for (int i = 0; i < familylist.family_list.Count; i++)
        {
            Debug.Log("[ldd]:::   " + familylist.family_list[i].name);
        }
    }



}
