using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using TMPro;
public class Elements : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    public bool isStatic = false;
    Action onDragA;
    public Action lineA;
    float positionx;
    float positiony;
    public TextMeshProUGUI elementName;
    GameManager gameManager;
    public int maxLine;
    private Gate gate;
    public LineController lineController;
    public bool state {
    get { return mState; }
        set
        {
            if (mState == value) return;
            mState = value;
            if (OnVariableChange != null)
                OnVariableChange(mState);
        }
    }
    public bool mState = false;
    public void OnDrag(PointerEventData eventData)
    {
        onDragA?.Invoke();
        lineA?.Invoke();
    }
    public delegate void OnVariableChangeDelegate(bool newVal);
    public event OnVariableChangeDelegate OnVariableChange;
    void Start()
    {
        if (gameObject.transform.position.x % 10 != 0 || gameObject.transform.position.y % 10 != 0)
        {
            positionx = (gameObject.transform.position.x) / 20;
            positiony = (gameObject.transform.position.y) / 20;
            positionx = (int)Math.Round(positionx, 0);
            positiony = (int)Math.Round(positiony, 0);
            positionx *= 20;
            positiony *= 20;
            gameObject.transform.position = new Vector3(positionx, positiony, 0);
        }
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        onDragA += SetPos;
        if (gameObject.CompareTag("Gate"))
        {
            if (gameObject.TryGetComponent(out Gate gate))
            {
                gate = gameObject.GetComponent<Gate>();
                maxLine = gate.maxInput;
            }
        }else if (gameObject.CompareTag("Input"))
        {
            maxLine = 1;
        }else if (gameObject.CompareTag("Output"))
        {
            maxLine = 1;
        }
    }
    void SetPos()
    {
        if (!isStatic)
        {
            gameObject.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (gameObject.transform.position.x % 10 != 0 || gameObject.transform.position.y % 10 != 0)
            {
                positionx = (gameObject.transform.position.x) / 20;
                positiony = (gameObject.transform.position.y) / 20;
                positionx = (int)Math.Round(positionx, 0);
                positiony = (int)Math.Round(positiony, 0);
                positionx *= 20;
                positiony *= 20;
                gameObject.transform.position = new Vector3(positionx, positiony, 0);
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right) return;//extra warunek
        gameManager.activeElement = gameObject.GetComponent<Elements>();
        gameManager.OpenOptionsPanel(gameManager.coursor.transform);
        if (eventData.pointerClick.CompareTag("Output"))
        {                
            gameManager.createConnectionB.enabled = false;                             
        }
        else
        {
            gameManager.createConnectionB.enabled = true;
            gameManager.activeInput = gameObject;
        }
        gameManager.activeGameObject = gameObject;
        gameManager.renameButton.enabled = !eventData.pointerClick.CompareTag("Gate");
    }    
}
