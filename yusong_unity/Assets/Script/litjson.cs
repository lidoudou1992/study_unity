using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System;

public class litjson : MonoBehaviour {
    private string tempPath = Application.persistentDataPath + "/temp.json";

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SaveTempData()
    {
        FileInfo fileInfo = new FileInfo(tempPath);
        GameStatus status = new GameStatus();
        //把类转换为Json格式的String
        string str = JsonMapper.ToJson(status);
        //写入本地
        StreamWriter sw = fileInfo.CreateText();
        sw.WriteLine(str);
        sw.Close();
        sw.Dispose();
    }

    public void LoadTempData(String tempString)
    {
        FileInfo fileInfo = new FileInfo(tempPath);
        if (fileInfo.Exists)
        {
            string tempString1 = File.ReadAllText(tempPath);
            GameStatus status = JsonMapper.ToObject<GameStatus>(tempString1);
        }
    }



}
