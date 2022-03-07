using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class MovingForNewGates : MonoBehaviour, IDragHandler
{
    private float positionx, positiony;
    public GameManager GameManager;
    private Action onDragAction;
    // Start is called before the first frame update
    void Start()
    {
        onDragAction += SetPos;
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        onDragAction?.Invoke(); 
    }
    void SetPos()
    {
        print("XDD");
        gameObject.transform.position = (Vector2)GameManager.coursor.position;
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
