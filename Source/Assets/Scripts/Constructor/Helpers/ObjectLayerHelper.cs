using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectLayerHelper : DndHelper, IPointerEnterHandler, IPointerExitHandler
{
    public bool CanPlaceObject;
    public RectTransform Layer;

    // Update is called once per frame
    void Update()
    {
        //CanPlaceObject = EventSystem.current.IsPointerOverGameObject();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CanPlaceObject = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CanPlaceObject = false;
    }
}
