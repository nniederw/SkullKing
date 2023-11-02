using System;
using TMPro;
using UnityEngine;
public class RoundTableElement : MonoBehaviour, TableElement
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
    private TMP_Text _TMP_Text;
    private TMP_Text TMP_Text
    {
        get
        {
            if (_TMP_Text != null) return _TMP_Text;
            _TMP_Text = GetComponentInChildren<TMP_Text>();
            return _TMP_Text;
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
        SetText();
    }
    public float RelativeHeight() => 1f;
    public float RelativeWidth() => 0.35f;
    public void SetRectAnchor(Vector2 min, Vector2 max)
    {
        RectTransform.anchorMin = min;
        RectTransform.anchorMax = max;
    }
    public void OnLocationUpdate(int newPosi, int newPosj)
    {
        Posi = newPosi;
        Posj = newPosj;
        SetText();
    }
    private void SetText()
    {
        TMP_Text.text = Posi.ToString();
    }
}