using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Output : MonoBehaviour
{
    Elements element;
    Image outputI;    

    void Start()
    {
        outputI = GetComponent<Image>();
        element = GetComponent<Elements>();        
    }
    public void Switching(bool newVal)
    {        
        if (newVal)
            outputI.color = Color.red;
        else
            outputI.color = Color.white;        
    }
}
