using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject objectToDo;
    public bool on;        

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(on)
        objectToDo.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {               
        if (!on)
        {
            objectToDo.SetActive(false);
        }
    }
}