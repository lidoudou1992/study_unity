using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameStatus{

    public string gameName;
    public string version;
    public bool isStrreo;
    public bool isUseHardWare;
    public refencenes[] statusList;

}


[Serializable]
public class refencenes
{
    public string name;
    public int id;
}
