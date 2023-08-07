using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler
{
    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> SliderHandler = null;
    public Action<PointerEventData> JoyStickBeginHandler = null;
    public Action<PointerEventData> JoyStickDragHandler = null;
    public Action<PointerEventData> JoyStickEndHandler = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (JoyStickBeginHandler != null)
        {
            JoyStickBeginHandler.Invoke(eventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (JoyStickDragHandler != null)
        {
            JoyStickDragHandler.Invoke(eventData);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (JoyStickEndHandler != null)
        {
            JoyStickEndHandler.Invoke(eventData);
        }
    }

    public void OnPointerClick(PointerEventData eventData) // Ŭ������ �� �̺�Ʈ �������̽�
    {
        if (OnClickHandler != null)
        {
            OnClickHandler.Invoke(eventData);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (SliderHandler != null)
        {
            SliderHandler.Invoke(eventData);
        }
    }
}
