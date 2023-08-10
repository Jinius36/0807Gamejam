using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Diagnostics;

public abstract class UI_Base : MonoBehaviour
{
    Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    public abstract void Init();
    protected void Bind<T>(Type type) where T : UnityEngine.Object // Ÿ�Կ� �´� �������� ���� _objects �迭�� ���ε�
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
            {
                objects[i] = Util.FindChild(gameObject, names[i], true);
            }
            else
            {
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);
            }
            if (objects[i] == null)
            {
                Debug.Log($"Failed to bind {names[i]}");
            }
        }
    }

    protected T Get<T>(int index) where T : UnityEngine.Object //���ε� �� _objects �迭���� ���ϴ� �� Get�ؿ�
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
        {
            return null;
        }
        return objects[index] as T;
    }

    protected Text GetText(int index) { return Get<Text>(index); }
    protected Button GetButton(int index) { return Get<Button>(index); }
    protected Image GetImage(int index) { return Get<Image>(index); }

    public static void AddUIEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_EventHandler _event = Util.GetOrAddComponent<UI_EventHandler>(go);
        switch (type)
        {
            case Define.UIEvent.Click:
                _event.OnClickHandler -= action;
                _event.OnClickHandler += action;
                break;
            case Define.UIEvent.BeginDrag:
                _event.BeginDragHandler -= action;
                _event.BeginDragHandler += action;
                break;
            case Define.UIEvent.Drag:
                _event.DragHandler -= action;
                _event.DragHandler += action;
                break;
            case Define.UIEvent.DragEnd:
                _event.DragEndHandler -= action;
                _event.DragEndHandler += action;
                break;
            case Define.UIEvent.PointerUP:
                _event.OnPointerUpHandler -= action;
                _event.OnPointerUpHandler += action;
                break;
            case Define.UIEvent.PointerDown:
                _event.OnPointerDownHandler -= action;
                _event.OnPointerDownHandler += action;
                break;
        }

    }
}
