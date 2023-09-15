using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSystem : MonoBehaviour
{
    public List<CheckPoint> PointList;

    public int CurrentCheckPointIndex;
    public PlayerController Player;
    // Start is called before the first frame update
    void Start()
    {
        CurrentCheckPointIndex = PlayerPrefs.GetInt("CheckPoint", 0);
    }
    
    public void LoadPlayerToCheckPoint()
    {
        Player.transform.position = PointList[CurrentCheckPointIndex].transform.position;
    }

}
