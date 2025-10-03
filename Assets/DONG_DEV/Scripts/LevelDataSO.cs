using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameData", order = 1)]
[System.Serializable]
public class GameDataSO : ScriptableObject
{
    public AudioClip danceMusic;
    public List<Data> cardItems;
}

[System.Serializable]
public class Data
{
    public Sprite sprite;
    public CheckPoseData chkData;
}

[System.Serializable]
public class CheckPoseData
{
    public List<Path> _path;
}

public enum Path
{
    dau,
    than,
    vai,
    tay_trai,
    tay_phai,
    chan_trai,
    chan_phai
}

