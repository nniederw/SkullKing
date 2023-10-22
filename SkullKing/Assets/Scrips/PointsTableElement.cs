using System;
using UnityEngine;
[RequireComponent(typeof(RectTransform))]
public class PointsTableElement : MonoBehaviour, TableElement
{
    private RectTransform _RectTransform;
    private RectTransform RectTransform
    {
        get
        {
            if (_RectTransform != null)
            {
                return _RectTransform;
            }
            _RectTransform = GetComponent<RectTransform>(); return _RectTransform;
        }
    }
    private Func<int, int, TableElement> GetElementAt;
    private int Posi;
    private int Posj;
    public void Initialize(int posi, int posj, Func<int, int, TableElement> getElementAt)
    {
        Posi = posi;
        Posj = posj;
        GetElementAt = getElementAt;
    }

    public void OnLocationUpdate()
    {
        Debug.Log("locationupdate");
    }
    public float RelativeHeight() => 1f;
    public float RelativeWidth() => 1f;
    public void SetRectAnchor(Vector2 min, Vector2 max)
    {
        RectTransform.anchorMin = min;
        RectTransform.anchorMax = max;
    }
}