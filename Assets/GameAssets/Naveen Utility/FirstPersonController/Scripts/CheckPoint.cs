using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool IsReached { get; private set; } = false;

    public Vector3 CheckpointReached()
    {
        IsReached = true;
        return transform.position;
    }


}
