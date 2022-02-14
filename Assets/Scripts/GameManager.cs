using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    RectTransform canvasUI;
    GameObject inputOptionsPanel;
    public Button createConnectionB;
    public GameObject linePrefab;
    public List<Gate> gates;
    public GameObject activeInput;
    public Transform coursor;
    Transform[] myTransforms;
    LineController newLineS;
    bool onLine = false; 
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
        SetUpCamera();
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        coursor.position = pos;
        if(onLine)
        {
            CheckDistance();
        }
    }
    public void CloseSelf(GameObject @object)
    {
        @object.SetActive(false);
    }

    public void OpenOptionsPanel(Transform where)
    {
        inputOptionsPanel.transform.position = new Vector2(where.position.x,where.position.y);
        inputOptionsPanel.SetActive(true);
        if (where.gameObject.CompareTag("Output"))
        {
            createConnectionB.enabled = false;
        }
        else
        {
            createConnectionB.enabled = true;
        }
        
    }
    public void CreateConnection()
    {
        onLine = true;
        GameObject newLine = Instantiate(linePrefab, GameObject.Find("Workbench").GetComponent<Transform>());
        newLineS = newLine.GetComponent<LineController>();
        newLineS.points[0] = activeInput.transform;
        newLineS.points[3] = coursor;        
        List<GameObject> gates = GameObject.FindGameObjectsWithTag("Gate").ToList<GameObject>();
        List<GameObject> gates2 = GameObject.FindGameObjectsWithTag("Output").ToList<GameObject>();
        foreach(GameObject gate2 in gates2)
        {
            gates.Add(gate2);   
        }
        myTransforms = new Transform[gates.Count];
        int i = 0;
        foreach (GameObject gate in gates)
        {
            myTransforms[i] = gate.transform;
            i++;
        }    
    }
    void CheckDistance()
    {
        if(!newLineS.points[3].gameObject.CompareTag("Gate") || !newLineS.points[3].gameObject.CompareTag("Output"))
        {
            //get 3 closest characters (to referencePos)
            Transform nClosest = myTransforms.OrderBy(t => (t.position - coursor.position).sqrMagnitude).FirstOrDefault();                       
            if (Vector3.Distance(coursor.position, nClosest.position) < 10)
            {
                newLineS.points[3] = nClosest;
                onLine = false;
                newLineS.connected = true;
                newLineS.GetElementsScript();
            }
        }        
    }
    void SetUpCamera()
    {
        height = 2 * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
        canvasUI.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        canvasUI.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }
}
