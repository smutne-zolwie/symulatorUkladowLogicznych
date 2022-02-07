using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputS : MonoBehaviour
{
    Toggle inputT;
    Image inputI;
    public bool state;
    void Start()
    {
        inputI = gameObject.GetComponent<Image>();
        inputT = gameObject.GetComponent<Toggle>();
        state = inputT.isOn;
        inputT.onValueChanged.AddListener(Switching);
    }
    
    void Switching(bool state)
    {
        this.state = state;        
        if (state)
            inputI.color = Color.red;
        else
            inputI.color = Color.white;
        state = !state;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
