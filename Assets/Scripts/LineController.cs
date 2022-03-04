using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public LineRenderer lr;
    public Transform[] points = new Transform[4];
    public Elements[] elements = new Elements[2];
    public bool connected = false;
    Color tru = new Color(255 / 255f, 0 / 255f, 0 / 255f, 255 / 255f);
    Color fal = new Color(168 / 255f, 166 / 255f, 158 / 255f, 255 / 255f);

    public void SetUpLine(Transform[] points)
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = points.Length;
        this.points = points;
        lr.material = new Material(Shader.Find("Sprites/Default"));
    }
    public void GetElementsScript()
    {
        Transform elOne = points[0];
        Transform elTwo = points[3];
        elements[0] = elOne.gameObject.GetComponent<Elements>();
        elements[1] = elTwo.gameObject.GetComponent<Elements>();
        foreach (Elements element in elements)
        {
            element.lineA += SetLinePosition;
        }        
        elements[0].OnVariableChange += OnVariableChangeHandler;
        GiveState();
    }
    public void SetColor()
    {   
        if (elements[0].state)
        {
            lr.startColor = tru;
            lr.endColor = tru;
        }
        else
        {
            lr.startColor = fal;
            lr.endColor = fal;
        }
        if(elements[1].gameObject.CompareTag("Output"))
        {
            Output output = elements[1].gameObject.GetComponent<Output>();
            output.Switching(elements[0].state);
        }
    }
    public void GiveState()
    {        
        if (elements[1].gameObject.CompareTag("Gate"))
        {
            Gate gate = elements[1].gameObject.GetComponent<Gate>();
            if (gate.maxInput > gate.elements.Count)
            {
                gate.elements.Add(elements[0]);
                gate.SetUpCalculatuion();
            }
        }
        if(elements[1].gameObject.CompareTag("Output"))
        {
            elements[1].state = elements[0].state;
        }
        SetColor();
    }

    public void UpdateState()
    {
        if (elements[1].gameObject.CompareTag("Gate"))
        {
            Gate gate = elements[1].gameObject.GetComponent<Gate>();
            gate.SetUpCalculatuion();
        }
        if (elements[1].gameObject.CompareTag("Output"))
        {
            elements[1].state = elements[0].state;
        }
        SetColor();
    }
    void Start()
    {
        SetUpLine(points);
    }
    private void Update()
    {
        if (!connected)
            SetLinePosition();
    }
    public void SetLinePosition()
    {
        if (points[0] != null && points[3] != null)
        {
            points[1].position = new Vector3((points[3].position.x + points[0].position.x) / 2, points[0].position.y, 0);
            points[2].position = new Vector3((points[3].position.x + points[0].position.x) / 2, points[3].position.y, 0);
            for (int i = 0; i < points.Length; i++)
            {
                lr.SetPosition(i, points[i].position);
            }
        }
    }
    public void OnVariableChangeHandler(bool newVal)
    {
        SetColor();
        UpdateState();
    }
}
