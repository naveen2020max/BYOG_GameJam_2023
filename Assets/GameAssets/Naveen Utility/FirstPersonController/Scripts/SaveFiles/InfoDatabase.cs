using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naveen;

public class InfoDatabase : MonoBehaviour
{
    public static InfoDatabase instance;



    public DataInfo<CheckPointData> CheckpointInfo;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("InfoDatabase instance created");
        }
        else
        {
            Destroy(this);
            Debug.LogError("Two InfoDatabase Exist");

        }
    }

    void Start()
    {
        CheckpointInfo = new DataInfo<CheckPointData>("CheckPoint");
        CheckpointInfo.LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
