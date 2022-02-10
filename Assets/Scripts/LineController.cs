using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    LineRenderer lr;    
    public Transform[] points = new Transform[4];
    public LineRenderer line;
    public void SetUpLine(Transform[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;
    }

    void Start()
    {
        lr = gameObject.GetComponent<LineRenderer>();
        SetUpLine(points);
    }

    void Update()
    {               
        
        if(points[0] != null && points[3] != null)
        {
            points[1].position = new Vector3((points[3].position.x + points[0].position.x) / 2, points[0].position.y, 0);
            points[2].position = new Vector3((points[3].position.x + points[0].position.x) / 2, points[3].position.y, 0);
            for (int i = 0; i < points.Length; i++)
            {
                lr.SetPosition(i, points[i].position);
            }
        }        
    }
}
