using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InputS : MonoBehaviour, IPointerClickHandler
{
    Toggle inputT;
    Image inputI;    
    public bool state;
    GameManager gameManager;
    void Start()
    {        
        inputI = gameObject.GetComponent<Image>();
        inputT = gameObject.GetComponent<Toggle>();
        state = inputT.isOn;
        inputT.onValueChanged.AddListener(Switching);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            gameManager.activeInput = gameObject;            
            gameManager.OpenOptionsPanel(gameObject.transform.position);            
        }            
    }
}
