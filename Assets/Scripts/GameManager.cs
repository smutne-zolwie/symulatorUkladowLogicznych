using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using UnityEngine.EventSystems;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class GameManager : MonoBehaviour
{
    RectTransform canvasUI;
    public Button renameButton;
    public GameObject inputOptionsPanel;
    public GameObject createOptionsPanel;
    public GameObject renamePanel;
    public TMP_InputField renameField, newNameField;
    public Button createConnectionB;
    public GameObject linePrefab;
    public List<Gate> gates;
    public GameObject activeInput;
    public Elements activeElement;
    public Transform coursor;
    public List<GameObject> gatesPrefabs;
    public GameObject inputPrefab, outputPrefab;
    public GameObject newGatePanel;
    public Slider[] colorSliders = new Slider[3];
    public Image newColorI;
    public GameObject gateButtonPrefab;
    public GameObject gateObjPrefab;
    Transform[] myTransforms;
    LineController newLineS;
    bool onLine = false;
    float height;
    float width;

    void Start()
    {
        canvasUI = GameObject.Find("UI").GetComponent<RectTransform>();
        inputOptionsPanel = GameObject.Find("InputOptionsPanel");
        createOptionsPanel = GameObject.Find("CreateOptionsPanel");
        renamePanel = GameObject.Find("RenamePanel");
        newGatePanel = GameObject.Find("NewGatePanel");
        newNameField = newGatePanel.GetComponentInChildren<TMP_InputField>();
        newGatePanel.SetActive(false);
        createOptionsPanel.SetActive(false);
        inputOptionsPanel.SetActive(false);
        renameField = renamePanel.GetComponentInChildren<TMP_InputField>();
        renamePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        SetUpCamera();
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        coursor.position = pos;
        if (onLine)
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
        inputOptionsPanel.transform.position = new Vector2(where.position.x - 5, where.position.y + 5);
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
        SettingPanelsAsLast();
        newLineS = newLine.GetComponent<LineController>();
        newLineS.points[0] = activeInput.transform;
        newLineS.points[3] = coursor;
        List<GameObject> gates = GameObject.FindGameObjectsWithTag("Gate").ToList<GameObject>();
        List<GameObject> gates2 = GameObject.FindGameObjectsWithTag("Output").ToList<GameObject>();
        foreach (GameObject gate2 in gates2)
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
        if (Input.anyKeyDown)
        {
            Destroy(newLineS.gameObject);
            onLine = false;
        }
        if (!newLineS.points[3].gameObject.CompareTag("Gate") || !newLineS.points[3].gameObject.CompareTag("Output"))
        {
            Transform nClosest = myTransforms.OrderBy(t => (t.position - coursor.position).sqrMagnitude)
                .FirstOrDefault();
            if (Vector3.Distance(coursor.position, nClosest.position) < 10)
            {
                Gate nClosestGateS = null;
                bool secondCond = false;
                if (nClosest.gameObject.CompareTag("Gate"))
                {
                    nClosestGateS = nClosest.GetComponent<Gate>();
                    secondCond = true;
                }

                if (secondCond)
                {
                    if (nClosestGateS.input.Count < nClosestGateS.maxInput)
                    {
                        newLineS.points[3] = nClosest;
                        onLine = false;
                        newLineS.connected = true;
                        newLineS.GetElementsScript();
                    }
                }
                if (nClosest.CompareTag("Output"))
                {
                    if (!nClosest.GetComponent<Output>().used)
                    {
                        newLineS.points[3] = nClosest;
                        onLine = false;
                        newLineS.connected = true;
                        newLineS.GetElementsScript();
                        nClosest.GetComponent<Output>().used = true;
                    }
                }
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

    public void CreateGate(int index)
    {
        GameObject gate = Instantiate(gatesPrefabs[index], GameObject.Find("Workbench").transform);
        SettingPanelsAsLast();
    }

    public void OpenRenamePanel()
    {
        renamePanel.SetActive(true);
    }

    public void RenameElement()
    {
        activeElement.elementName.text = renameField.textComponent.text;
        renameField.textComponent.text = "";
        CloseSelf(renamePanel);
    }

    public void CreateInput()
    {
        GameObject input = Instantiate(inputPrefab, GameObject.Find("Workbench").transform);
        SettingPanelsAsLast();
    }

    public void CreateOutput()
    {
        GameObject output = Instantiate(outputPrefab, GameObject.Find("Workbench").transform);
        SettingPanelsAsLast();
    }

    
    public void SettingPanelsAsLast()
    {
        inputOptionsPanel.transform.SetAsLastSibling();
        renamePanel.transform.SetAsLastSibling();
        newGatePanel.transform.SetAsLastSibling();
    }
    #region NewGate

    public void OnSliderValueChanged(TextMeshProUGUI text)
    {
        text.text = text.gameObject.GetComponentInParent<Slider>().value.ToString("F2");
    }

    public void NewColorUpdate()
    {
        Color color = new Color(colorSliders[0].value, colorSliders[1].value, colorSliders[2].value);
        print(color.ToString());
        newColorI.color = color;
    }

    public void SetRandomColor()
    {
        foreach (Slider colSlider in colorSliders)
        {
            float val = Random.Range(0, 256) / 255f;
            colSlider.value = val;
        }

        NewColorUpdate();
    }

    public void OpenGateCreator()
    {
        newGatePanel.SetActive(true);
        SetRandomColor();
    }

    public void SetUpNewGate()
    {
        GameObject newGate = Instantiate(gateObjPrefab, GameObject.Find("Workbench").transform);
        newGate.GetComponent<Image>().color = newColorI.color;
        newGate.GetComponent<Elements>().elementName.text = newNameField.textComponent.text;
        newNameField.textComponent.text = "";
        GameObject[] lineControllersO = GameObject.FindGameObjectsWithTag("Line");
        List<LineController> lineControllers = new List<LineController>();
        foreach (GameObject lineControllerO in lineControllersO)
        {
            lineControllers.Add(lineControllerO.GetComponent<LineController>());
        } //line controllery ktore maja wartosci elements czyli podczepionych elementow
        foreach (LineController controller in lineControllers)
        {
            foreach (Elements controllerElement in controller.elements)
            {

                if (!controllerElement.CompareTag("Input") && !controllerElement.CompareTag("Output"))
                {
                    Vector2 pos = controllerElement.transform.position;
                    controllerElement.transform.localScale = new Vector3(0.2f, 0.2f);
                    controller.lr.endWidth = 1;
                    controller.lr.startWidth = 1;
                    controllerElement.transform.SetParent(newGate.transform);
                    controller.transform.SetParent(newGate.transform);
                    controller.lr.useWorldSpace = false;
                    controllerElement.transform.position = new Vector2(pos.x / 5, pos.y / 5);
                    print("rest");
                    controller.SetLinePosition();
                    //controllerElement.gameObject.GetComponent<Image>().enabled = false;
                    print("setting new parent");
                }
                else
                {
                    Destroy(controllerElement.gameObject);
                    Destroy(controller.gameObject);
                    print("destroyin");
                }

            }
        }
        //set up making connection
    }
    #endregion
}