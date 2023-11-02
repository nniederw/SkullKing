using System;
using UnityEngine;
public class EmptyField : MonoBehaviour, TableElement
{
    private RectTransform _RectTransform;
    private RectTransform RectTransform
    {
        get
        {
            if (_RectTransform != null) return _RectTransform;
            _RectTransform = GetComponent<RectTransform>();
            return _RectTransform;
        }
    }
    public void Initialize(int posi, int posj, Func<int, int, TableElement> getElementAt)
    {
    }
    public float RelativeHeight() => 1f;
    public float RelativeWidth() => 0.35f; //TODO find a better solution
    public void SetRectAnchor(Vector2 min, Vector2 max)
    {
        RectTransform.anchorMin = min;
        RectTransform.anchorMax = max;
    }
    public void OnLocationUpdate(int newPosi, int newPosj)
    {
    }
}