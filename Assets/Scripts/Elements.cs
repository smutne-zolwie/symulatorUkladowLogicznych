using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Elements : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    Action onDragA;
    public Action lineA;
    float positionx;
    float positiony;
    GameManager gameManager;
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
    private bool mState = false;
    public void OnDrag(PointerEventData eventData)
    {
        onDragA?.Invoke();
        lineA?.Invoke();
    }
    public delegate void OnVariableChangeDelegate(bool newVal);
    public event OnVariableChangeDelegate OnVariableChange;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        onDragA += SetPos;        
    }
    void SetPos()
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
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            gameManager.activeInput = gameObject;
            gameManager.OpenOptionsPanel(gameObject.transform.position);
        }
    }
}
