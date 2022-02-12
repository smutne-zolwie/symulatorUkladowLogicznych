using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InputS : MonoBehaviour
{    
    public Toggle inputT;
    Image inputI;
    bool state;
    Elements element;
    GameManager gameManager;
    void Start()
    {        
        inputI = gameObject.GetComponent<Image>();
        inputT = gameObject.GetComponent<Toggle>();
        state = inputT.isOn;
        inputT.onValueChanged.AddListener(Switching);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        element = GetComponent<Elements>();
        element.state = inputT.isOn;
    }
    
    void Switching(bool state)
    {        
        this.state = state;
        if (state)        
            inputI.color = Color.red;
                             
        else
            inputI.color = Color.white;        
        element.state = state;
    }    
}
