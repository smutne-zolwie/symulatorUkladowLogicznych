using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    RectTransform canvasUI;
    // Start is called before the first frame update
    float height;
    float width;
    void Start()
    {
        canvasUI = GameObject.Find("UI").GetComponent<RectTransform>();
        
    }
    
    // Update is called once per frame
    void Update()
    {
        height = 2 * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
        canvasUI.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        canvasUI.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }
}
