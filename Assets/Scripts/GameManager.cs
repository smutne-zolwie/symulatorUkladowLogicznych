using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    RectTransform canvasUI;
    GameObject inputOptionsPanel;
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

    public void OpenOptionsPanel(Vector2 where)
    {
        inputOptionsPanel.transform.position = where;
        inputOptionsPanel.SetActive(true);
    }
    public void CreateConnection()
    {
        onLine = true;
        GameObject newLine = Instantiate(linePrefab, GameObject.Find("Workbench").GetComponent<Transform>());
        newLineS = newLine.GetComponent<LineController>();
        newLineS.points[0] = activeInput.transform;
        newLineS.points[3] = coursor;
        GameObject[] gates = GameObject.FindGameObjectsWithTag("Gate");
        myTransforms = new Transform[gates.Length];
        int i = 0;
        foreach (GameObject gate in gates)
        {
            myTransforms[i] = gate.transform;
            i++;
        }    
    }
    void CheckDistance()
    {
        if(!newLineS.points[3].gameObject.CompareTag("Gate"))
        {
            //get 3 closest characters (to referencePos)
            Transform nClosest = myTransforms.OrderBy(t => (t.position - coursor.position).sqrMagnitude).FirstOrDefault();
            //or use .FirstOrDefault();  if you need just one            
            if (Vector3.Distance(coursor.position, nClosest.position) < 10)
            {
                newLineS.points[3] = nClosest;
                onLine = false;
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
