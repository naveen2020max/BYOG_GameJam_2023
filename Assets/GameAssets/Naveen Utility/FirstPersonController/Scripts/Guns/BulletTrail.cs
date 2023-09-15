using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    public Vector3 StartPoint, EndPoint;

    private LineRenderer line;

    public BulletTrail(Vector3 start,Vector3 end)
    {
        StartPoint = start;
        EndPoint = end;
    }

    private void Start()
    {
        //line = GetComponent<LineRenderer>();
        //line.SetPosition(0, StartPoint);
        //line.SetPosition(1, EndPoint);

        //Destroy(gameObject, 1);
    }

    public void SetLine(Vector3 start, Vector3 end)
    {
        StartPoint = start;
        EndPoint = end;

        line = GetComponent<LineRenderer>();
        line.SetPosition(0, StartPoint);
        line.SetPosition(1, EndPoint);
        line.startWidth = .1f;
        line.endWidth = .01f;

        Destroy(gameObject, .2f);
    }
}
