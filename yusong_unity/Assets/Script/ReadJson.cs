using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ReadJson: MonoBehaviour {
    public string[] data1;
    

    // Use this for initialization
    void Start () {
        GameStatus status = LoadJSON.LoadJsonFromFile();
        
        var re = status.statusList;
        Debug.Log(re);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
