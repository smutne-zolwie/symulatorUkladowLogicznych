using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    RectTransform canvasUI;
    GameObject inputOptionsPanel;
    public GameObject linePrefab;
    public List<Gate> gates;
    // Start is called before the first frame update
    float height;
    float width;
    void Start()
    {
        canvasUI = GameObject.Find("UI").GetComponent<RectTransform>();
        inputOptionsPanel = GameObject.Find("InputOptionsPanel");
        inputOptionsPanel.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        height = 2 * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
        canvasUI.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        canvasUI.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }
    public void CloseSelf(GameObject @object)
    {
        @object.SetActive(false);
    }
    public void CreateConnection(GameObject @object)
    {
        GameObject newLine =  Instantiate(linePrefab);
        LineController newLineS = newLine.GetComponent<LineController>();
        newLineS.points[0] = @object.transform;
        newLineS.points[3].position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        while (newLineS.points[3].gameObject.CompareTag("Gate"))
        {
            //if()
        }
    }
}
