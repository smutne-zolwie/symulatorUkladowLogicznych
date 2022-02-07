using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    LineRenderer lr;
    [SerializeField]
    public Transform[] points = new Transform[4];
    public LineRenderer line;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();        
    }

    public void SetUpLine(Transform[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;
    }

    void Start()
    {
        SetUpLine(points);
    }

    void Update()
    {
        points[1].position = new Vector3((points[3].position.x + points[0].position.x) / 2, points[0].position.y, 0);
        points[2].position = new Vector3((points[3].position.x + points[0].position.x) / 2, points[3].position.y, 0);
        for (int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i].position);
        }
    }
}
